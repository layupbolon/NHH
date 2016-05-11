using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Common
{
    public class CommonAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Common";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Common_default",
                "Common/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "NHH.WebSite.Management.Areas.Common.Controllers" }
            );
        }
    }
}