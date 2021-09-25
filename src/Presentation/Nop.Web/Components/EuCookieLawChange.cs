using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain;
using Nop.Core.Domain.Customers;
using Nop.Services.EUCookieLaw;
using Nop.Core.Http;
using Nop.Services.Common;
using Nop.Web.Framework.Components;

namespace Nop.Web.Components
{
    public class EuCookieLawChangeViewComponent : NopViewComponent
    {
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;
        private readonly StoreInformationSettings _storeInformationSettings;
        private readonly ICookieRegistrar _cookieProviderManager;

        public EuCookieLawChangeViewComponent(IGenericAttributeService genericAttributeService,
            IStoreContext storeContext,
            IWorkContext workContext,
            StoreInformationSettings storeInformationSettings,
            ICookieRegistrar cookieProviderManager)
        {
            _genericAttributeService = genericAttributeService;
            _storeContext = storeContext;
            _workContext = workContext;
            _storeInformationSettings = storeInformationSettings;
            _cookieProviderManager = cookieProviderManager;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!_storeInformationSettings.DisplayEuCookieLawWarning)
                //disabled
                return Content("");

            //ignore search engines because some pages could be indexed with the EU cookie as description
            if ((await _workContext.GetCurrentCustomerAsync()).IsSearchEngineAccount())
                return Content("");

            if (!await _genericAttributeService.GetAttributeAsync<bool>(await _workContext.GetCurrentCustomerAsync(), NopCustomerDefaults.EuCookieLawAcceptedAttribute, (await _storeContext.GetCurrentStoreAsync()).Id))
                //not yet accepted
                return Content("");

            var purposes = await _cookieProviderManager.GetAllCookieProviders()
                .OrderBy(x => x.CookiePurpose.Order)
                .ThenBy(x => x.Order)
                .ThenBy(x => x.Name)
                .Select(x => x.CookiePurpose).Distinct(new CookiePurposeEqualityComparer()).ToListAsync();

            return View(purposes);
        }
    }
}