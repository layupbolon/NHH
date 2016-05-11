using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// NHHHTML扩展
    /// </summary>
    public static class NHHHtmlExtension
    {
        /// <summary>
        /// 可排序的链接
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText">显示文字</param>
        /// <param name="orderBy">后台排序字段</param>
        /// <returns></returns>
        public static MvcHtmlString SortLink(this HtmlHelper htmlHelper, string linkText, string orderBy)
        {
            TagBuilder tagBuilder = new TagBuilder("a");
            var innerHtml = string.Format("{0}<span class='sort'></span>", HttpUtility.HtmlEncode(linkText));
            tagBuilder.InnerHtml = innerHtml;
            tagBuilder.AddCssClass("sortLink");
            tagBuilder.MergeAttribute("href", "###");
            tagBuilder.MergeAttribute("sort-field", orderBy);
            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }
    }
}
