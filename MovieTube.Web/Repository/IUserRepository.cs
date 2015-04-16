using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieTube.Web.Models;
using MovieFinder.Data;

namespace MovieTube.Web.Repository
{
    public interface IUserRepository
    {
        VisitorProfile GetVisitor(Guid id);
        void UpdateVisitor(VisitorProfile model);
    }
}
