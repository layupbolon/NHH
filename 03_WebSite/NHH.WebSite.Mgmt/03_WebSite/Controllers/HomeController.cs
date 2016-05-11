using NHH.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //首页默认跳转
            var home = ParamManager.GetStringValue("website:default");
            return Redirect(home);
        }
    }
}