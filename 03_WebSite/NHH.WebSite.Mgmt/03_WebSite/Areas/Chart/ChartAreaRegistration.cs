using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Chart
{
    public class ChartAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Chart";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Chart_default",
                "Chart/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "NHH.WebSite.Management.Areas.Chart.Controllers" }
            );
        }
    }
}