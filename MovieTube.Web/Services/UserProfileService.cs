using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieFinder.Data;
using MovieTube.Web.Repository;

namespace MovieTube.Web.Services
{
    public class UserProfileService : IUserProfileService
    {

        public VisitorProfile UpdateVisitorProfile(Guid? sessionID, string ip, string url, string language = "")
        {
            VisitorProfile vp = null;
            using (var db = new MovieFinderEntities())
            {
                if (sessionID != null)
                    vp = db.VisitorProfiles.FirstOrDefault(x => x.ID == sessionID);
                if (vp == null)
                {
                    vp = new VisitorProfile { LastAccessedLanguage = "" };
                    db.VisitorProfiles.Add(vp);
                }
                vp.LastAccessedIP = ip;
                vp.LastAccessedLanguage = language ?? vp.LastAccessedLanguage;
                vp.LastAccessedTime = DateTime.Now;
                vp.LastAccessedUrl = url;
                vp.HitCount++;
                db.SaveChanges();

            }


            return vp;
        }
    }
}