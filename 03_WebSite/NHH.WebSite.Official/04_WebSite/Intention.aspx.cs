using System;
using System.Web.UI;
using NHH.Framework.Service;
using NHH.Service.Official.IService;

namespace NHH.WebSite.Official
{
    public partial class Intention : Page
    {
        public int type;
        public int projectID;
        public IProjectService projectService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] == null || !int.TryParse(Request.QueryString["type"], out type))
                Response.Redirect("Business.aspx");

            if (Request.QueryString["pid"] == null || !int.TryParse(Request.QueryString["pid"], out projectID))
                projectID = 0;

            projectService = NHHServiceFactory.Instance.CreateService<IProjectService>();
        }
    }
}