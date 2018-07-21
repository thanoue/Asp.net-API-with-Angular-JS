using KhoiDepTraiShop.Model.Models;
using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.App_Start;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
namespace KhoiDepTraiShop.Web.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {

        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {

                _userManager = value;
            }
        }
        private ApplicationUserManager _userManager;
        private IErrorService _errorService;

        public ApiControllerBase(IErrorService errorService)
        {
            this._errorService = errorService;
        }

        public ApiControllerBase(ApplicationUserManager userManager,IErrorService errorService):this(errorService)
        {
            _userManager = userManager;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {

                    Trace.WriteLine("Entity of type "+eve.Entry.Entity.GetType().Name.ToString()+" in state \"{eve.Entry.State}\" ");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine("  - Propety: "+ve.PropertyName.ToString()+" ,Error: "+ve.ErrorMessage.ToString());
                    }
                }
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        protected void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.CreateDate = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                _errorService.Create(error);
                _errorService.Save();
            }
            catch
            {
            }
        }
    }
}