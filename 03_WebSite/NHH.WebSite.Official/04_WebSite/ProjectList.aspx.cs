using System;
using System.Web.UI;
using NHH.Framework.Service;
using NHH.Service.Official.IService;

namespace NHH.WebSite.Official
{
    public partial class ProjectList : Page
    {
        public IProjectListService service;

        protected void Page_Load(object sender, EventArgs e)
        {
            service = NHHServiceFactory.Instance.CreateService<IProjectListService>();
        }
    }
}