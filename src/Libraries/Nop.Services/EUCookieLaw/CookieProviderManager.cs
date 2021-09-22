using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.EUCookieLaw;
using Nop.Core.Infrastructure;

namespace Nop.Services.EUCookieLaw
{
    public class CookieProviderManager : ICookieProviderManager
    {
        #region Fields
        private readonly ITypeFinder _typeFinder;

        private IEnumerable<ICookieProvider> _cookieProviders;
        #endregion

        #region Ctr
        public CookieProviderManager(
            ITypeFinder typeFinder            
            )
        {
            _typeFinder = typeFinder;

            _cookieProviders = LoadAllCookieProviders().OrderBy(x => x.Order).ThenBy(x => x.Name).ToList();
        }
        #endregion

        #region Methods
        public IEnumerable<ICookieProvider> GetAllCookieProviders()
        {
            return _cookieProviders;
        }
        #endregion

        #region Helpers
        private IEnumerable<ICookieProvider> LoadAllCookieProviders()
        {
            var types = _typeFinder.FindClassesOfType<ICookieProvider>();

            foreach (var t in types)
            {
                yield return (ICookieProvider)Activator.CreateInstance(t);
            }
        }
        #endregion

    }
}
