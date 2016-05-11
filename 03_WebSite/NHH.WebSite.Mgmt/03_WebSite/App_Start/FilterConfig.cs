using NHH.Framework.Web;
using NHH.WebSite.Management.Common;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new NHHHandleErrorAttribute());
            filters.Add(new NHHWebAuthorizeAttribute());
        }
    }
}
