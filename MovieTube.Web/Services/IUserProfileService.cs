using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieFinder.Data;

namespace MovieTube.Web.Services
{
    public interface IUserProfileService
    {
        VisitorProfile UpdateVisitorProfile(
            Guid? sessionID, string ip, 
            string url, string language = "");

    }
}