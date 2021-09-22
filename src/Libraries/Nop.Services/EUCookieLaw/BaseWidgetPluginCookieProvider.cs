using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.EUCookieLaw;
using Nop.Core.Infrastructure;
using Nop.Services.Cms;

namespace Nop.Services.EUCookieLaw
{
    public abstract class BaseWidgetPluginCookieProvider<TPurpose> : BaseCookieProvider<TPurpose>  where TPurpose : class, ICookiePurpose
    {
        protected readonly IWidgetPluginManager _widgetPluginManager;

        public BaseWidgetPluginCookieProvider() : base()
        {
            _widgetPluginManager = EngineContext.Current.Resolve<IWidgetPluginManager>();
        }

        public abstract override Task<bool> IsActiveAsync();
    }
}
