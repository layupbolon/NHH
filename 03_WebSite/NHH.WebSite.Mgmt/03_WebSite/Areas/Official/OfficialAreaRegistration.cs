using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Official
{
    public class OfficialAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Official";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Official_default",
                "Official/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "NHH.WebSite.Management.Areas.Official.Controllers" }
            );
        }
    }
}