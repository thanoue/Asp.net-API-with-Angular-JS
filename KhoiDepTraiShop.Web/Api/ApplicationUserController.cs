using KhoiDepTraiShop.Model.Models;
using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.Infrastructure.Core;
using KhoiDepTraiShop.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KhoiDepTraiShop.Web.Infrastructure.Extensions;
using System.Linq;
using System;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using KhoiDepTraiShop.Web.App_Start;
using KhoiDepTraiShop.Web.Models.Applications;
using KhoiDepTraiShop.Common.Exeptions;
using System.Threading.Tasks;

namespace KhoiDepTraiShop.Web.Api
{

    [Authorize]
    [RoutePrefix("api/applicationUser")]
    public class ApplicationUserController : ApiControllerBase
    {

        private IApplicationGroupService _appGroupService;
        private IApplicationRoleService _appRoleService;
        public ApplicationUserController(
            IApplicationGroupService appGroupService,
            IApplicationRoleService appRoleService,
            ApplicationUserManager userManager,
            IErrorService errorService)
            : base(userManager,errorService)
        {
            _appRoleService = appRoleService;
            _appGroupService = appGroupService;
         
        }
        [Route("getlistpaging")]
        [HttpGet]
        [Authorize(Roles = "ViewUser")]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = UserManager.Users;
                var modelVm = model.ToList().ToViewModelList();

                PaginationSet<ApplicationUserViewModel> pagedSet = new PaginationSet<ApplicationUserViewModel>()
                {
                    Page = page,
                    TotalRow = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = modelVm
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [Route("detail/{id}")]
        [HttpGet]
        [Authorize(Roles = "ViewUser")]
        public HttpResponseMessage Details(HttpRequestMessage request, string id)
        {
            if (string.IsNullOrEmpty(id))
            {

                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " không có giá trị.");
            }
            var user = UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Không có dữ liệu");
            }
            else
            {
                var applicationUserViewModel = user.Result.ToModel();
                var listGroup = _appGroupService.GetListGroupByUserId(applicationUserViewModel.Id);
                applicationUserViewModel.Groups = listGroup.ToList().ToViewModelList();
                return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
            }

        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "AddUser")]
        public async Task<HttpResponseMessage> Create(HttpRequestMessage request, ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppUser = new ApplicationUser();
                applicationUserViewModel.ToEntity( newAppUser);
                var key = new Random(); var cridential = key.Next(5000, 9000);
                newAppUser.EmailConfirmed = false;
                newAppUser.CridentialCode = cridential.ToString();
      
                try
                {                   
                    var result = await UserManager.CreateAsync(newAppUser, applicationUserViewModel.Password);
                    if (result.Succeeded)
                    {
                        var listAppUserGroup = new List<ApplicationUserGroup>();
                        foreach (var group in applicationUserViewModel.Groups)
                        {
                            listAppUserGroup.Add(new ApplicationUserGroup()
                            {
                                GroupId = group.ID,
                                UserId = newAppUser.Id
                            });
                            //add role to user
                            var listRole = _appRoleService.GetListRoleByGroupId(group.ID);
                            foreach (var role in listRole)
                            {
                                await UserManager.RemoveFromRoleAsync(newAppUser.Id, role.Name);
                                await UserManager.AddToRoleAsync(newAppUser.Id, role.Name);
                            }
                        }
                        _appGroupService.AddUserToGroups(listAppUserGroup, newAppUser.Id);
                        _appGroupService.Save();


                        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);

                    }
                    else
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
                catch (Exception ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "UpdateUser")]
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request, ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var appUser = await UserManager.FindByIdAsync(applicationUserViewModel.Id);
                try
                {
                    applicationUserViewModel.ToEntity( appUser);
                    var result = await UserManager.UpdateAsync(appUser);
                    if (result.Succeeded)
                    {
                        var listAppUserGroup = new List<ApplicationUserGroup>();
                        foreach (var group in applicationUserViewModel.Groups)
                        {
                            listAppUserGroup.Add(new ApplicationUserGroup()
                            {
                                GroupId = group.ID,
                                UserId = applicationUserViewModel.Id
                            });
                            //add role to user
                            var listRole = _appRoleService.GetListRoleByGroupId(group.ID);
                            foreach (var role in listRole)
                            {
                                await UserManager.RemoveFromRoleAsync(appUser.Id, role.Name);
                                await UserManager.AddToRoleAsync(appUser.Id, role.Name);
                            }
                        }
                        _appGroupService.AddUserToGroups(listAppUserGroup, applicationUserViewModel.Id);
                        _appGroupService.Save();
                        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);

                    }
                    else
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "DeleteUser")]
        public async Task<HttpResponseMessage> Delete(HttpRequestMessage request, string id)
        {
            var appUser = await UserManager.FindByIdAsync(id);
            var result = await UserManager.DeleteAsync(appUser);
            if (result.Succeeded)
                return request.CreateResponse(HttpStatusCode.OK, id);
            else
                return request.CreateErrorResponse(HttpStatusCode.OK, string.Join(",", result.Errors));
        }

    }
}