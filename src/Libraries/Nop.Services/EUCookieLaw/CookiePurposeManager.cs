using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.EUCookieLaw;
using Nop.Services.Common;

namespace Nop.Services.EUCookieLaw
{
    public class CookiePurposeManager : ICookiePurposeManager
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IGenericAttributeService _genericAttributeService;


        public CookiePurposeManager(
            IWorkContext workContext,
            IStoreContext storeContext,
            IGenericAttributeService genericAttributeService
            )
        {
            _workContext = workContext;
            _storeContext = storeContext;
            _genericAttributeService = genericAttributeService;
        }

        /// <summary>
        /// Sets purposes that have been allowed by the user
        /// </summary>
        /// <param name="allowedPurposes">Comma seperated list of allowed purposes</param>
        public async Task SetAllowed(string allowedPurposes)
        {
            // old way - we could probably remove this but it may break existing plugins
            await _genericAttributeService.SaveAttributeAsync(await _workContext.GetCurrentCustomerAsync(), NopCustomerDefaults.EuCookieLawAcceptedAttribute, true, (await _storeContext.GetCurrentStoreAsync()).Id);

            // new way - store a comma seperated list of purposes
            // note - neccessary purposes aren't stored as they are accepted by default
            await _genericAttributeService.SaveAttributeAsync(await _workContext.GetCurrentCustomerAsync(), NopCustomerDefaults.EuCookieLawAcceptedPurposesAttribute, (allowedPurposes ?? "").ToLower(), (await _storeContext.GetCurrentStoreAsync()).Id);
        }

        public async Task<bool> IsPurposeAllowed(ICookiePurpose purpose)
        {
            if (purpose.IsNecessary)
                return true;

            try
            {
                var customer = await _workContext.GetCurrentCustomerAsync();
                var store = await _storeContext.GetCurrentStoreAsync();
                var allowedPurposes = (await _genericAttributeService.GetAttributeAsync<string>(customer, NopCustomerDefaults.EuCookieLawAcceptedPurposesAttribute, store.Id)).Split(',');

                if (allowedPurposes.Contains(purpose.SystemName.ToLower()))
                    return true;
            }
            catch(Exception)
            {
                // we should only get here if the customer cookie hasn't been set yet
            }

            return false;
        }

        public async Task<bool> IsProviderAllowed(ICookieProvider provider)
        {
            return await IsPurposeAllowed(provider.CookiePurpose);
        }

        public async Task<bool> IsPurposeAllowed<T>() where T : ICookiePurpose, new()
        {
            return await IsPurposeAllowed(new T());
        }

        public async Task<bool> IsProviderAllowed<T>() where T : ICookieProvider, new()
        {
            return await IsProviderAllowed(new T());
        }
    }
}
