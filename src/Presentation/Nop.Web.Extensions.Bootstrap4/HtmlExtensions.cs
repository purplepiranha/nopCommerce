using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.UI.Paging;

namespace Nop.Web.Extensions.Bootstrap4
{
    public static class HtmlExtensions
    {
        public static Pager BootstrapPager(this IHtmlHelper helper, IPageableModel pagination)
        {
            return new Pager(pagination, helper.ViewContext);
        }
    }
}
