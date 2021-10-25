using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Codecool.CodecoolShop.Services
{
    public class CookieService
    {
        public CookieService()
        {
        }

        public CookieOptions GenerateCookieOptions()
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = SetCookieExpiration();
            return cookieOptions;
        }

        public DateTime SetCookieExpiration()
        {
            return DateTime.Now.AddDays(1);
        }
    }
}
