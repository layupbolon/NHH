using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Desktop
{
    public class DesktopAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Desktop";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Desktop_default",
                "Desktop/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "NHH.WebSite.Management.Areas.Desktop.Controllers" }
            );
        }
    }
}