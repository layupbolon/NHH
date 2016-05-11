using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Plan
{
    public class PlanAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Plan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Plan_default",
                "Plan/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "NHH.WebSite.Management.Areas.Plan.Controllers" }
            );
        }
    }
}