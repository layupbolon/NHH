using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Project
{
    public class ProjectAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Project";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Project",
                "Project/{controller}/{action}/{id}",
                new { controller = "ProjectInfo", action = "Index", id = UrlParameter.Optional },
                new string[] { "NHH.WebSite.Management.Areas.Project.Controllers" }
            );
        }
    }
}