using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieTube.Data;

namespace MovieTube.Web.Attributes
{
    public class UserProfileFilterAttribute : ActionFilterAttribute
    {
        private readonly IRepository repository;

        public UserProfileFilterAttribute()
        {
            this.repository = new Repository();
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