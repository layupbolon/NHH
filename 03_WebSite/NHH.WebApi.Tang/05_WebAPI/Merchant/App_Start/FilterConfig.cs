using NHH.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebAPI.Merchant
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new NHHHandleErrorAttribute());
            filters.Add(new NHHAuthorizeAttribute());
        }
    }
}