using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Web.Http;
using MovieTube.Web.Repository;
using MovieTube.Web.Services;

namespace MovieTube.Web.Attributes
{
    public class UserProfileFilterAttribute : ActionFilterAttribute
    {
        private readonly IUserProfileService userProfileService;
        private readonly ICookieService cookieService;

        public UserProfileFilterAttribute()
        {
            this.userProfileService = new UserProfileService();
            this.cookieService = new CookieService();
        }

        public override void  OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var cookie = actionContext.Request.Headers.GetCookies(Constants.CookieSessionName).FirstOrDefault();

            var profile = userProfileService.UpdateVisitorProfile(
                    cookieService.DecryptCookie(cookie != null ? cookie[Constants.CookieSessionName].Value : null),
                    actionContext.Request.GetClientIpAddress(), actionContext.Request.RequestUri.PathAndQuery,
                    actionContext.Request.GetQueryString("lang"));
               if(cookie == null)
                   actionContext.Response.SetCookie(Constants.CookieSessionName,
                       cookieService.EncryptCookie(profile.ID), DateTime.Now.AddYears(100));

 	         base.OnActionExecuting(actionContext);
        }

        

    }
}