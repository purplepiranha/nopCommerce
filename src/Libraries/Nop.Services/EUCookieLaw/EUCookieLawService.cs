using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.EUCookieLaw;
using Nop.Core.Infrastructure;
using Nop.Services.Common;

namespace Nop.Services.EUCookieLaw
{
    public class EUCookieLawService : IEUCookieLawService
    {
        #region Fields
        private readonly ITypeFinder _typeFinder;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        #endregion

        #region Ctr
        public EUCookieLawService(
            ITypeFinder typeFinder,
            IGenericAttributeService genericAttributeService,
            IWorkContext workContext,
            IStoreContext storeContext
            )
        {
            _typeFinder = typeFinder;
            _genericAttributeService = genericAttributeService;
            _workContext = workContext;
            _storeContext = storeContext;
        }
        #endregion

        #region Methods
        public async Task<IList<ICookie>> GetAllCookiesAsync()
        {
            //TODO: This may bring back inactive plugins as well.
            // Need to check and work out a solution if necessary.
            return await FindAndActivateAllCookies()
                .OrderBy(x => x.CookiePurpose.Order)
                .ThenBy(x => x.CookieProvider.Order)
                .ThenBy(x => x.CookieProvider.Name)
                .ThenBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<bool> IsCookieAllowedAsync<T>() where T : ICookie
        {
            var cookie = Activator.CreateInstance<T>();
            return await IsCookieAllowedAsync(cookie);
        }

        public async Task<bool> IsCookieAllowedAsync<T>(T cookie) where T : ICookie
        {
            return await IsCookiePurposeAllowedAsync(cookie.CookiePurpose);
        }

        public async Task<bool> IsCookieProviderAllowedAsync<T>() where T : ICookieProvider
        {
            var provider = Activator.CreateInstance<T>();
            return await IsCookieProviderAllowedAsync(provider);
        }

        public async Task<bool> IsCookieProviderAllowedAsync<T>(T provider) where T : ICookieProvider
        {
            return await IsCookiePurposeAllowedAsync(provider.CookiePurpose);
        }

        public async Task<bool> IsCookiePurposeAllowedAsync<T>() where T : ICookiePurpose
        {
            var purpose = Activator.CreateInstance<T>();
            return await IsCookiePurposeAllowedAsync(purpose);
        }

        public async Task<bool> IsCookiePurposeAllowedAsync<T>(T purpose) where T : ICookiePurpose
        {
            return await CheckPurposeAllowed(purpose);
        }


        #endregion

        #region Helpers

        private IEnumerable<ICookie> FindAndActivateAllCookies()
        {
            var types = _typeFinder.FindClassesOfType<ICookie>();

            foreach (var t in types)
            {
                yield return (ICookie)Activator.CreateInstance(t);
            }
        }

        private async Task<bool> CheckPurposeAllowed(ICookiePurpose purpose)
        {
            if (purpose.IsNecessary)
                return true;

            var allowedPurposes = (await _genericAttributeService.GetAttributeAsync<string>(await _workContext.GetCurrentCustomerAsync(), NopCustomerDefaults.EuCookieLawAcceptedPurposesAttribute, (await _storeContext.GetCurrentStoreAsync()).Id)).Split(',');

            if (allowedPurposes.Contains(purpose.SystemName))
                return true;

            return false;

        }
        #endregion
    }
}