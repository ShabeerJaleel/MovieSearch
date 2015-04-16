using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieTube.Web.Repository;

namespace MovieTube.Web.Attributes
{
    public class UserProfileFilterAttribute : ActionFilterAttribute
    {
        private readonly IUserRepository repository;

        public UserProfileFilterAttribute()
        {
            this.repository = new UserRepository();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
         
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //filterContext.HttpContext.Response.Cookies.Add(new HttpCookie(name, value));

            base.OnActionExecuted(filterContext);
        }
    }
}