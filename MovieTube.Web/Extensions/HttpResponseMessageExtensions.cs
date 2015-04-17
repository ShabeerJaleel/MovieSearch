using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;

namespace System.Web.Http
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Retrieves an individual cookie from the cookies collection
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static void SetCookie(this HttpResponseMessage response, string cookieName, string value, DateTime expiry)
        {
            response.Headers.AddCookies(new CookieHeaderValue[]{
                new CookieHeaderValue(cookieName, value){ 
                     Expires  = expiry
                }
            });
        }

    }
}