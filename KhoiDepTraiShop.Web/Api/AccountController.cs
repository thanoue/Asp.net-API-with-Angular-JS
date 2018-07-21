using KhoiDepTraiShop.Service;
using KhoiDepTraiShop.Web.App_Start;
using KhoiDepTraiShop.Web.Infrastructure.Core;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace KhoiDepTraiShop.Web.Api
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiControllerBase
    {
        private ApplicationSignInManager _signInManager;       
      
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IErrorService errorService)
            :base(userManager,errorService)
        {           
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
     
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<HttpResponseMessage> Login(HttpRequestMessage request,string email,string passWord,bool rememberMe)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest,ModelState);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(email,passWord,rememberMe,shouldLockout:false);
            return request.CreateResponse(HttpStatusCode.OK, "Success");
        }

    }
}
