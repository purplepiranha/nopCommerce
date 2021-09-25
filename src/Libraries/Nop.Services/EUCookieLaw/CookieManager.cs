using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.EUCookieLaw;
using Nop.Core.Http;
using Nop.Core.Security;
using Nop.Services.Common;

namespace Nop.Services.EUCookieLaw
{
    public class CookieManager : ICookieManager
    {
        #region Fields
        private readonly ICookieRegistrar _cookieRegistrar;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHelper _webHelper;
        private readonly CookieSettings _cookieSettings;
        #endregion

        #region Ctr
        public CookieManager(
            ICookieRegistrar cookieRegistrar,
            IWorkContext workContext,
            IStoreContext storeContext,
            IGenericAttributeService genericAttributeService,
            IHttpContextAccessor httpContextAccessor,
            IWebHelper webHelper,
            CookieSettings cookieSettings
            )
        {
            _cookieRegistrar = cookieRegistrar;
            _workContext = workContext;
            _storeContext = storeContext;
            _genericAttributeService = genericAttributeService;
            _httpContextAccessor = httpContextAccessor;
            _webHelper = webHelper;
            _cookieSettings = cookieSettings;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Sets purposes that have been allowed by the user
        /// </summary>
        /// <param name="allowedPurposes">Comma seperated list of allowed purposes</param>
        public async Task UpdateCookieAcceptance(string allowedPurposes)
        {
            // this is what determines whether to display the notice or whether it's already been accepted
            await _genericAttributeService.SaveAttributeAsync(await _workContext.GetCurrentCustomerAsync(), NopCustomerDefaults.EuCookieLawAcceptedAttribute, true, (await _storeContext.GetCurrentStoreAsync()).Id);

            // standardise as lowercase
            allowedPurposes = (allowedPurposes ?? "").ToLower();

            // store a comma seperated list of purposes
            // note - neccessary purposes aren't stored as they are accepted by default (this allows us to tell whether the purposes have changed at next login)
            await _genericAttributeService.SaveAttributeAsync(await _workContext.GetCurrentCustomerAsync(), NopCustomerDefaults.EuCookieLawAcceptedPurposesAttribute, allowedPurposes, (await _storeContext.GetCurrentStoreAsync()).Id);

            //delete current cookie value
            var cookieName = $"{NopCookieDefaults.Prefix}{NopCookieDefaults.EuCookieConsentPurposesCookie}";
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

            //get date of cookie expiration
            var cookieExpires = _cookieSettings.CustomerCookieExpires;
            var cookieExpiresDate = DateTime.Now.AddHours(cookieExpires);

            //set new cookie value
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = cookieExpiresDate,
                Secure = _webHelper.IsCurrentConnectionSecured()
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, allowedPurposes, options);

            RemoveDisallowedCookies(allowedPurposes.Split(','));
        }

        /// <summary>
        /// Checks whether the user has given consent to the type of cookie that a provider uses.
        /// Note: do not use in situations where the cookies have been modified, such as internally in the
        /// CookieManager class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsProviderAllowed<T>() where T : ICookieProvider, new()
        {
            var cookieName = $"{NopCookieDefaults.Prefix}{NopCookieDefaults.EuCookieConsentPurposesCookie}";
            var allowedProviders = (_httpContextAccessor.HttpContext?.Request?.Cookies[cookieName]) ?? string.Empty;
            return IsProviderAllowed(new T(), allowedProviders.Split(','));
        }

        #endregion
        #region Helpers
        private IEnumerable<ICookieProvider> GetAcceptedCookieProviders(string[] allowedPurposes)
        {
            var providers = _cookieRegistrar.GetAllCookieProviders();
            foreach (var provider in providers)
            {
                var allowed = IsProviderAllowed(provider, allowedPurposes);

                if (allowed)
                {
                    yield return provider;
                }
            }
        }

        private bool IsProviderAllowed(ICookieProvider provider, string[] allowedPurposes)
        {
            return IsPurposeAllowed(provider.CookiePurpose, allowedPurposes);
        }

        private bool IsPurposeAllowed<T>(string[] allowedPurposes) where T : ICookiePurpose, new()
        {
            return IsPurposeAllowed(new T(), allowedPurposes);
        }

        private bool IsProviderAllowed<T>(string[] allowedPurposes) where T : ICookieProvider, new()
        {
            return IsProviderAllowed(new T(), allowedPurposes);
        }

        private bool IsPurposeAllowed(ICookiePurpose purpose, string[] allowedPurposes)
        {
            if (purpose.IsNecessary)
                return true;

            if (allowedPurposes.Contains(purpose.SystemName.ToLower()))
                return true;

            return false;
        }

        private IEnumerable<string> GetAllowedCookies(string[] allowedPurposes)
        {
            var providers = GetAcceptedCookieProviders(allowedPurposes).ToArray();

            var cookies = new List<string>();

            for (int i = 0; i < providers.Count(); i++)
            {
                var provider = providers[i];
                for (int j = 0; j < provider.CookieNames.Length; j++)
                {
                    cookies.Add(provider.CookieNames[j]);
                }
            }

            return cookies;
        }

        private void RemoveDisallowedCookies(string[] allowedPurposes)
        {
            // remove cookies for which consent has been withdrawn
            var allCookies = _httpContextAccessor.HttpContext.Request.Cookies.Keys;

            var allowed = GetAllowedCookies(allowedPurposes);

            foreach (var cookie in allCookies)
            {
                if (!allowed.Contains(cookie))
                {
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie);
                }
            }
        }
        #endregion
    }
}
