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
    public class CookieRegistrar : ICookieRegistrar
    {
        #region Fields
        private readonly ITypeFinder _typeFinder;

        private IEnumerable<ICookieProvider> _cookieProviders;
        private IEnumerable<ICookiePurpose> _cookiePurposes;
        #endregion

        #region Ctr
        public CookieRegistrar(
            ITypeFinder typeFinder
            )
        {
            _typeFinder = typeFinder;

            _cookieProviders = LoadAllCookieProviders().OrderBy(x => x.Order).ThenBy(x => x.Name).ToList();

            // we only get the purposes that are in use, rather than a list of everything possible
            _cookiePurposes = _cookieProviders.Select(x => x.CookiePurpose).Distinct(new CookiePurposeEqualityComparer()).OrderBy(x => x.Order).ThenBy(x => x.SystemName).ToList();
        }
        #endregion

        #region Methods
        public IEnumerable<ICookieProvider> GetAllCookieProviders()
        {
            return _cookieProviders;
        }

        public IEnumerable<ICookiePurpose> GetAllCookiePurposes()
        {
            return _cookiePurposes;
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
