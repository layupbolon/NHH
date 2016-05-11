using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Campaign
{
    public class CampaignAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Campaign";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Campaign_default",
                "Campaign/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}