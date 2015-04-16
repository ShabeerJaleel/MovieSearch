using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTube.Web.Services
{
    interface ICookieService
    {
        void AddSessionCookie(Guid id);
        Guid GetSessionID();
    }
}
