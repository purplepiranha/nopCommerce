using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nop.Core.EUCookieLaw;
using Nop.Core.Http;
using Nop.Core.Security;
using Nop.Web.Framework.EUCookieLaw.Providers;
using Nop.Web.Framework.EUCookieLaw.Purposes;
using Nop.Core.Infrastructure;

namespace Nop.Web.Framework.EUCookieLaw
{
    public class CustomerCookie : BaseCookie<WebsiteNecessaryCookieProvider>, ICookie
    {
        private readonly CookieSettings _cookieSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomerCookie() : base()
        {
            _cookieSettings = EngineContext.Current.Resolve<CookieSettings>();
            _httpContextAccessor = EngineContext.Current.Resolve<IHttpContextAccessor>();
        }

        public int? ExpiryHours => _cookieSettings.CustomerCookieExpires;
        public string Name => $"{ NopCookieDefaults.Prefix }{ NopCookieDefaults.CustomerCookie }";
        public string Domain => _httpContextAccessor.HttpContext.Request.Host.Host;
        public string Type => CookieTypeDefaults.Cookie;
    }
}
