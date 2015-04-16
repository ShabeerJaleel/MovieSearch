using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieTube.Web.Models;
using MovieFinder.Data;

namespace MovieTube.Web.Repository
{
    public class UserRepository : IUserRepository
    {
        public VisitorProfile GetVisitor(Guid id)
        {
            using (var db = new MovieFinderEntities())
            {
                var vp = db.VisitorProfiles
                          .First(x => x.ID == id);
                if (db == null)
                    new ArgumentException(String.Format("Visitor with id {0} does not exist", id));
                return vp;
            }
        }

        public void UpdateVisitor(VisitorProfile model)
        {
            using (var db = new MovieFinderEntities())
            {
            }
        }
    }
}