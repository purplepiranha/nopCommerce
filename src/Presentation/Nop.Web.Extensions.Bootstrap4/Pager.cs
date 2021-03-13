using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Services.Localization;
using Nop.Web.Framework.Extensions;
using Nop.Web.Framework.UI.Paging;

namespace Nop.Web.Extensions.Bootstrap4
{
    /// <summary>
    /// Renders a pager component from an IPageableModel datasource using Bootstrap 4.
    /// </summary>
    public partial class Pager : IHtmlContent
    {
        /// <summary>
        /// Model
        /// </summary>
        protected readonly IPageableModel _model;
        /// <summary>
        /// ViewContext
        /// </summary>
        protected readonly ViewContext _viewContext;
        /// <summary>
        /// Page query string prameter name
        /// </summary>
        protected string _pageQueryName = "page";
        /// <summary>
        /// A value indicating whether to show Total summary
        /// </summary>
        protected bool _showTotalSummary;
        /// <summary>
        /// A value indicating whether to show pager items
        /// </summary>
        protected bool _showPagerItems = true;
        /// <summary>
        /// A value indicating whether to show the first item
        /// </summary>
        protected bool _showFirst = true;
        /// <summary>
        /// A value indicating whether to the previous item
        /// </summary>
        protected bool _showPrevious = true;
        /// <summary>
        /// A value indicating whether to show the next item
        /// </summary>
        protected bool _showNext = true;
        /// <summary>
        /// A value indicating whether to show the last item
        /// </summary>
        protected bool _showLast = true;
        /// <summary>
        /// A value indicating whether to show individual page
        /// </summary>
        protected bool _showIndividualPages = true;
        /// <summary>
        /// A value indicating whether to render empty query string parameters (without values)
        /// </summary>
        protected bool _renderEmptyParameters = true;
        /// <summary>
        /// Number of individual page items to display
        /// </summary>
        protected int _individualPagesDisplayedCount = 5;
        /// <summary>
        /// Boolean parameter names
        /// </summary>
        protected IList<string> _booleanParameterNames;
        /// <summary>
        /// The nav accessibility text
        /// </summary>
        protected string _navAccessibilityText = "page navigation";

        /// <summary>
        /// The current screen reader text
        /// </summary>
        protected string _currentPageScreenReaderText = "current";

        /// <summary>
        /// The use font awesome icons
        /// </summary>
        protected bool _useFontAwesomeIcons = true;
        /// <summary>
        /// The nav extra CSS classes
        /// </summary>
        protected string _navExtraCssClasses = "";
        /// <summary>
        /// The nav summary extra CSS classes
        /// </summary>
        protected string _navSummaryExtraCssClasses = "";
        /// <summary>
        /// The nav pages extra CSS classes
        /// </summary>
        protected string _navPagesExtraCssClasses = "";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="context">ViewContext</param>
        public Pager(IPageableModel model, ViewContext context)
        {
            this._model = model;
            _viewContext = context;
            _booleanParameterNames = new List<string>();
        }

        /// <summary>
        /// ViewContext
        /// </summary>
		protected ViewContext ViewContext => _viewContext;

        /// <summary>
        /// Set 
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Pager</returns>
        public Pager QueryParam(string value)
        {
            _pageQueryName = value;
            return this;
        }
        /// <summary>
        /// Set a value indicating whether to show Total summary
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Pager</returns>
        public Pager ShowTotalSummary(bool value)
        {
            _showTotalSummary = value;
            return this;
        }
        /// <summary>
        /// Set a value indicating whether to show pager items
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Pager</returns>
        public Pager ShowPagerItems(bool value)
        {
            _showPagerItems = value;
            return this;
        }
        /// <summary>
        /// Set a value indicating whether to show the first item
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Pager</returns>
        public Pager ShowFirst(bool value)
        {
            _showFirst = value;
            return this;
        }
        /// <summary>
        /// Set a value indicating whether to the previous item
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Pager</returns>
        public Pager ShowPrevious(bool value)
        {
            _showPrevious = value;
            return this;
        }
        /// <summary>
        /// Set a  value indicating whether to show the next item
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Pager</returns>
        public Pager ShowNext(bool value)
        {
            _showNext = value;
            return this;
        }
        /// <summary>
        /// Set a value indicating whether to show the last item
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Pager</returns>
        public Pager ShowLast(bool value)
        {
            _showLast = value;
            return this;
        }
        /// <summary>
        /// Set number of individual page items to display
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Pager</returns>
        public Pager ShowIndividualPages(bool value)
        {
            _showIndividualPages = value;
            return this;
        }
        /// <summary>
        /// Set a value indicating whether to render empty query string parameters (without values)
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Pager</returns>
        public Pager RenderEmptyParameters(bool value)
        {
            _renderEmptyParameters = value;
            return this;
        }
        /// <summary>
        /// Set number of individual page items to display
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Pager</returns>
        public Pager IndividualPagesDisplayedCount(int value)
        {
            _individualPagesDisplayedCount = value;
            return this;
        }
        /// <summary>
        /// little hack here due to ugly MVC implementation
        /// find more info here: http://www.mindstorminteractive.com/topics/jquery-fix-asp-net-mvc-checkbox-truefalse-value/
        /// </summary>
        /// <param name="paramName">Parameter name</param>
        /// <returns>Pager</returns>
        public Pager BooleanParameterName(string paramName)
        {
            _booleanParameterNames.Add(paramName);
            return this;
        }
        /// <summary>
        /// Set individual page pager css class name
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Pager</returns>
        public Pager NavAccessibilityText(string value)
        {
            _navAccessibilityText = value;
            return this;
        }

        /// <summary>
        /// Currents the page screen reader text.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Pager CurrentPageScreenReaderText(string value)
        {
            _currentPageScreenReaderText = value;
            return this;
        }
        /// <summary>
        /// Uses the font awesome icons.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public Pager UseFontAwesomeIcons(bool value)
        {
            _useFontAwesomeIcons = value;
            return this;
        }
        /// <summary>
        /// Navs the extra CSS classes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Pager NavExtraCssClasses(string value)
        {
            _navExtraCssClasses = value;
            return this;
        }
        /// <summary>
        /// Navs the summary extra CSS classes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Pager NavSummaryExtraCssClasses(string value)
        {
            _navSummaryExtraCssClasses = value;
            return this;
        }
        /// <summary>
        /// Navs the pages extra CSS classes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public Pager NavPagesExtraCssClasses(string value)
        {
            _navPagesExtraCssClasses = value;
            return this;
        }

        /// <summary>
        /// Write control
        /// </summary>
        /// <param name="writer">Writer</param>
        /// <param name="encoder">Encoder</param>
	    public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            var htmlString = AsyncUtil.RunSync<string>(() => GenerateHtmlStringAsync());
            writer.Write(htmlString);
        }
        /// <summary>
        /// Generate HTML control
        /// </summary>
        /// <returns>HTML control</returns>
	    public override string ToString()
        {
            return AsyncUtil.RunSync<string>(() => GenerateHtmlStringAsync());
        }

        /// <summary>
        /// Generate HTML control
        /// </summary>
        /// <returns>HTML control</returns>
        public virtual async Task<string> GenerateHtmlStringAsync()
        {
            if (_model.TotalItems == 0)
                return null;
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();

            var ulPaginationBuilder = new TagBuilder("ul");
            ulPaginationBuilder.AddCssClass("pagination");
            if (!string.IsNullOrWhiteSpace(_navPagesExtraCssClasses))
                ulPaginationBuilder.AddCssClass(_navPagesExtraCssClasses);

            if (_showPagerItems && (_model.TotalPages > 1))
            {
                var isFirst = _model.PageIndex == 0;
                var isLast = (_model.PageIndex + 1) >= _model.TotalPages;

                string firstIcon = "", previousIcon = "", nextIcon = "", lastIcon = "";

                if (_useFontAwesomeIcons)
                {
                    firstIcon = "fas fa-angle-double-left";
                    previousIcon = "fas fa-angle-left";
                    nextIcon = "fas fa-angle-right";
                    lastIcon = "fas fa-angle-double-right";
                }

                if (_showFirst)
                    ulPaginationBuilder.InnerHtml.AppendHtml(CreatePageItem(1, await localizationService.GetResourceAsync("Pager.First"), false, isFirst, firstIcon));
                
                if (_showPrevious)
                    ulPaginationBuilder.InnerHtml.AppendHtml(CreatePageItem(_model.PageIndex, await localizationService.GetResourceAsync("Pager.Previous"), false, isFirst, previousIcon));
                
                if (_showIndividualPages)
                {
                    //individual pages
                    var firstIndividualPageIndex = GetFirstIndividualPageIndex();
                    var lastIndividualPageIndex = GetLastIndividualPageIndex();
                    for (var i = firstIndividualPageIndex; i <= lastIndividualPageIndex; i++)
                    {
                        ulPaginationBuilder.InnerHtml.AppendHtml(CreatePageItem(i + 1, (i + 1).ToString(), _model.PageIndex == i, false));
                    }
                }

                if (_showNext)
                    ulPaginationBuilder.InnerHtml.AppendHtml(CreatePageItem(_model.PageIndex + 2, await localizationService.GetResourceAsync("Pager.Next"), false, isLast, nextIcon));
                

                if (_showLast)
                    ulPaginationBuilder.InnerHtml.AppendHtml(CreatePageItem(_model.TotalPages, await localizationService.GetResourceAsync("Pager.Last"), false, isLast, lastIcon));
            }

            var navBuilder = new TagBuilder("nav");
            if(!string.IsNullOrWhiteSpace(_navAccessibilityText))
                navBuilder.Attributes.Add(new KeyValuePair<string, string>("aria-label", _navAccessibilityText));
            if (!string.IsNullOrWhiteSpace(_navExtraCssClasses))
                navBuilder.AddCssClass(_navExtraCssClasses);

            if (_showTotalSummary && (_model.TotalPages > 0))
            {
                var divSummaryBuilder = new TagBuilder("div");
                divSummaryBuilder.AddCssClass("pagination");
                if (!string.IsNullOrWhiteSpace(_navSummaryExtraCssClasses))
                    divSummaryBuilder.AddCssClass(_navSummaryExtraCssClasses);

                var spanSummaryBuilder = new TagBuilder("span");
                spanSummaryBuilder.AddCssClass("page-item disabled");
                var span2SummaryBuilder = new TagBuilder("span");
                span2SummaryBuilder.AddCssClass("page-link");
                span2SummaryBuilder.InnerHtml.Append(string.Format(await localizationService.GetResourceAsync("Pager.CurrentPage"), _model.PageIndex + 1, _model.TotalPages, _model.TotalItems));
                spanSummaryBuilder.InnerHtml.AppendHtml(span2SummaryBuilder);
                divSummaryBuilder.InnerHtml.AppendHtml(spanSummaryBuilder);
                navBuilder.InnerHtml.AppendHtml(divSummaryBuilder);
            }

            navBuilder.InnerHtml.AppendHtml(ulPaginationBuilder);

            //result = string.Format("<ul class=\"pagination\">{0}</ul>", result);
            //result = string.Format("<nav{0}>", string.IsNullOrEmpty(_navAccessibilityText) ? "" : " aria-label=\"" + _navAccessibilityText + "\"") + result + "</ul>";

            return await navBuilder.RenderHtmlContentAsync();
        }
        /// <summary>
        /// Is pager empty (only one page)?
        /// </summary>
        /// <returns>Result</returns>
	    public virtual async Task<bool> IsEmpty()
        {
            var html = await GenerateHtmlStringAsync();
            return string.IsNullOrEmpty(html);
        }

        /// <summary>
        /// Get first individual page index
        /// </summary>
        /// <returns>Page index</returns>
        protected virtual int GetFirstIndividualPageIndex()
        {
            if ((_model.TotalPages < _individualPagesDisplayedCount) ||
                ((_model.PageIndex - (_individualPagesDisplayedCount / 2)) < 0))
            {
                return 0;
            }
            if ((_model.PageIndex + (_individualPagesDisplayedCount / 2)) >= _model.TotalPages)
            {
                return (_model.TotalPages - _individualPagesDisplayedCount);
            }
            return (_model.PageIndex - (_individualPagesDisplayedCount / 2));
        }
        /// <summary>
        /// Get last individual page index
        /// </summary>
        /// <returns>Page index</returns>
        protected virtual int GetLastIndividualPageIndex()
        {
            var num = _individualPagesDisplayedCount / 2;
            if ((_individualPagesDisplayedCount % 2) == 0)
            {
                num--;
            }
            if ((_model.TotalPages < _individualPagesDisplayedCount) ||
                ((_model.PageIndex + num) >= _model.TotalPages))
            {
                return (_model.TotalPages - 1);
            }
            if ((_model.PageIndex - (_individualPagesDisplayedCount / 2)) < 0)
            {
                return (_individualPagesDisplayedCount - 1);
            }
            return (_model.PageIndex + num);
        }
        /// <summary>
        /// Create page link
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="text">Text</param>
        /// <param name="cssClass">CSS class</param>
        /// <returns>Link</returns>
		protected virtual TagBuilder CreatePageItem(int pageNumber, string text, bool isCurrentPage = false, bool isDisabled = false, string iconClass = "")
        {
            var liBuilder = new TagBuilder("li");
            liBuilder.AddCssClass("page-item");

            var aBuilder = new TagBuilder("a");
            aBuilder.AddCssClass("page-link");

            if (!string.IsNullOrWhiteSpace(iconClass))
            {
                aBuilder.Attributes.Add(new KeyValuePair<string, string>("aria-label", text));
                var iconBuilder = new TagBuilder("i");
                iconBuilder.Attributes.Add(new KeyValuePair<string, string>("aria-hidden", "true"));
                iconBuilder.AddCssClass(iconClass);
                aBuilder.InnerHtml.AppendHtml(iconBuilder);
            }
            else
            {
                aBuilder.InnerHtml.AppendHtml(text);
            }

            
            aBuilder.MergeAttribute("href", CreateDefaultUrl(pageNumber));
            
            if (isCurrentPage)
            {
                liBuilder.AddCssClass("active");
                aBuilder.Attributes.Add(new KeyValuePair<string, string>("aria-current", "page"));

                if (!string.IsNullOrWhiteSpace(_currentPageScreenReaderText))
                {
                    var srBuilder = new TagBuilder("span");
                    srBuilder.AddCssClass("sr-only");
                    srBuilder.InnerHtml.Append(_currentPageScreenReaderText);
                    aBuilder.InnerHtml.Append(" ");
                    aBuilder.InnerHtml.AppendHtml(srBuilder);
                }
            }

            if (isDisabled)
            {
                liBuilder.AddCssClass("disabled");
                aBuilder.Attributes.Add(new KeyValuePair<string, string>("aria-disabled", "true"));
                aBuilder.Attributes.Add(new KeyValuePair<string, string>("tabindex", "-1"));
            }

            liBuilder.InnerHtml.AppendHtml(aBuilder);
            //return liBuilder.RenderHtmlContent();
            return liBuilder;
        }
        /// <summary>
        /// Create default URL
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <returns>URL</returns>
        protected virtual string CreateDefaultUrl(int pageNumber)
        {
            var routeValues = new RouteValueDictionary();

            var parametersWithEmptyValues = new List<string>();
            foreach (var key in _viewContext.HttpContext.Request.Query.Keys.Where(key => key != null))
            {
                var value = _viewContext.HttpContext.Request.Query[key].ToString();
                if (_renderEmptyParameters && string.IsNullOrEmpty(value))
                {
                    //we store query string parameters with empty values separately
                    //we need to do it because they are not properly processed in the UrlHelper.GenerateUrl method (dropped for some reasons)
                    parametersWithEmptyValues.Add(key);
                }
                else
                {
                    if (_booleanParameterNames.Contains(key, StringComparer.InvariantCultureIgnoreCase))
                    {
                        //little hack here due to ugly MVC implementation
                        //find more info here: http://www.mindstorminteractive.com/topics/jquery-fix-asp-net-mvc-checkbox-truefalse-value/
                        if (!string.IsNullOrEmpty(value) && value.Equals("true,false", StringComparison.InvariantCultureIgnoreCase))
                        {
                            value = "true";
                        }
                    }
                    routeValues[key] = value;
                }
            }

            if (pageNumber > 1)
            {
                routeValues[_pageQueryName] = pageNumber;
            }
            else
            {
                //SEO. we do not render pageindex query string parameter for the first page
                if (routeValues.ContainsKey(_pageQueryName))
                {
                    routeValues.Remove(_pageQueryName);
                }
            }

            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            var url = webHelper.GetThisPageUrl(false);
            foreach (var routeValue in routeValues)
            {
                url = webHelper.ModifyQueryString(url, routeValue.Key, routeValue.Value?.ToString());
            }
            if (_renderEmptyParameters && parametersWithEmptyValues.Any())
            {
                foreach (var key in parametersWithEmptyValues)
                {
                    url = webHelper.ModifyQueryString(url, key);
                }
            }
            return url;
        }

    }
}
