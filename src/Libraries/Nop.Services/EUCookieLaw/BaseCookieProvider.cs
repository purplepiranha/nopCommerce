using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.EUCookieLaw;

namespace Nop.Services.EUCookieLaw
{
    public abstract class BaseCookieProvider<TPurpose> where TPurpose : class, ICookiePurpose
    {
        protected readonly ICookiePurpose _purpose;

        public BaseCookieProvider()
        {
            _purpose = (ICookiePurpose)Activator.CreateInstance(typeof(TPurpose));
        }

        public ICookiePurpose CookiePurpose {
            get {
                return _purpose;
            }
        }

        public virtual int Order => 0;

        public virtual async Task<bool> IsActiveAsync()
        {
            return true;
        }
    }
}
