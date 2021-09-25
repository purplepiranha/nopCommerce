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
    public class EuCookieLawDialogViewComponent : NopViewComponent
    {
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;
        private readonly StoreInformationSettings _storeInformationSettings;
        private readonly ICookieRegistrar _cookieProviderManager;

        public EuCookieLawDialogViewComponent(IGenericAttributeService genericAttributeService,
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
        public async Task<IViewComponentResult> InvokeAsync(bool isChangeRequest = false)
        {
            if (!isChangeRequest && await _genericAttributeService.GetAttributeAsync<bool>(await _workContext.GetCurrentCustomerAsync(), NopCustomerDefaults.EuCookieLawAcceptedAttribute, (await _storeContext.GetCurrentStoreAsync()).Id))
                //already accepted
                return Content("");

            //ignore notification?
            //right now it's used during logout so popup window is not displayed twice
            if (!isChangeRequest && TempData[$"{NopCookieDefaults.Prefix}{NopCookieDefaults.IgnoreEuCookieLawWarning}"] != null && Convert.ToBoolean(TempData[$"{NopCookieDefaults.Prefix}{NopCookieDefaults.IgnoreEuCookieLawWarning}"]))
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