using System;
using System.Web.UI;
using NHH.Framework.Service;
using NHH.Service.Official.IService;

namespace NHH.WebSite.Official
{
    public partial class CampaignDetail : Page
    {
        public int pageID;
        public ICampaignService service;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["pageID"] == null || !int.TryParse(Request.QueryString["pageID"], out pageID))
                Response.Redirect("CampaignList.aspx");

            service = NHHServiceFactory.Instance.CreateService<ICampaignService>();
        }
    }
}