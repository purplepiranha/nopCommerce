using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.EUCookieLaw
{
    public abstract class BaseCookie<TProvider> where TProvider : class, ICookieProvider
    {
        private readonly ICookieProvider _cookieProvider;

        public BaseCookie()
        {
            _cookieProvider = (ICookieProvider)Activator.CreateInstance(typeof(TProvider));
        }

        public ICookieProvider CookieProvider
        {
            get
            {
                return _cookieProvider;
            }
        }

        public ICookiePurpose CookiePurpose
        {
            get
            {
                return _cookieProvider.CookiePurpose;
            }
        }
    }
}
