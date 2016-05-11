using System;
using System.Web.UI;
using NHH.Framework.Service;
using NHH.Models.Official;
using NHH.Service.Official.IService;

namespace NHH.WebSite.Official
{
    public partial class Project : Page
    {
        public int projectID;
        public IProjectService service;
        public ProjectInfo projectInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["projectID"] == null ||
                !int.TryParse(Request.QueryString["projectID"], out projectID))
                Response.Redirect("ProjectList.aspx");

            service = NHHServiceFactory.Instance.CreateService<IProjectService>();

            projectInfo = service.GetProjectInfo(projectID);
        }
    }
}