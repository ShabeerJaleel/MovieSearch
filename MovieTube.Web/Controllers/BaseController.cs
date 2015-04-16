using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieTube.Web.Repository;
using MovieTube.Web.Services;
using MovieFinder.Data;

namespace MovieTube.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IMovieRepository repository;
        protected readonly IUserRepository userRepository;
        protected readonly ICookieService cookieService;
        protected readonly IUserProfileService userProfileService;

        public BaseController()
        {
            repository = new MovieRepository();
            userRepository = new UserRepository();
            cookieService = new CookieService();
            userProfileService = new UserProfileService();
        }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cookie = Request.Cookies[Constants.CookieSessionName];
            Profile = userProfileService.UpdateVisitorProfile(
                cookieService.DecryptCookie(cookie != null ? cookie.Value : null),
                Request.UserHostAddress, Request.Url.PathAndQuery,
                Request.QueryString["lang"]);
           if(cookie == null)
               Response.SetCookie(new HttpCookie(Constants.CookieSessionName,
                   cookieService.EncryptCookie(Profile.ID)) { Expires = DateTime.Now.AddYears(100) });
            
            base.OnActionExecuting(filterContext);
        }


        public VisitorProfile Profile { get; private set; }

    }
}
