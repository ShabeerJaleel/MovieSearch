using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTube.Web.Services
{
    public interface ICookieService
    {
        string EncryptCookie(Guid id);
        Guid? DecryptCookie(string token);
    }
}
