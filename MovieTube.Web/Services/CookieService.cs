using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MovieTube.Web.Services
{
    public class CookieService : ICookieService
    {
        public string EncryptCookie(Guid id)
        {
            return id.ToString();

           return  FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1,
                Constants.CookieSessionName,
                DateTime.Now,DateTime.Now.AddYears(100),
                true, 
                id.ToString()));
            
        }

        public Guid? DecryptCookie(string cookie)
        {
            if (String.IsNullOrWhiteSpace(cookie))
                return null;
            return new Guid(cookie);
            var ticket = FormsAuthentication.Decrypt(cookie);
            return new Guid(ticket.UserData);
        }
    }
}