using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using NHH.Framework.Service;
using NHH.Models.Official;
using NHH.Service.Official.IService;

namespace NHH.WebSite.Official
{
    public partial class ADPositionDetail : Page
    {
        public ADPositionInfo AdPositionInfo;
        public int ADPositionID;
        public int projectID;
        public string building;
        public int regionID;
        public int ADType;
        public int ElectricityType;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["adid"] == null || !int.TryParse(Request.QueryString["adid"], out ADPositionID))
                Response.Redirect("Business.aspx");

            if (Request.QueryString["pid"] == null || !int.TryParse(Request.QueryString["pid"], out projectID))
                projectID = 0;
            building = Request.QueryString["bid"];
            if (Request.QueryString["rid"] == null || !int.TryParse(Request.QueryString["rid"], out regionID))
                regionID = 0;
            if (Request.QueryString["adType"] == null || !int.TryParse(Request.QueryString["adType"], out ADType))
                ADType = 0;
            if (Request.QueryString["eType"] == null || !int.TryParse(Request.QueryString["eType"], out ElectricityType))
                ElectricityType = 0;

            var service = NHHServiceFactory.Instance.CreateService<IBusinessService>();
            AdPositionInfo = service.GetAdPositionInfo(ADPositionID);
        }

        public string GetFirstPic()
        {
            if (AdPositionInfo.PicList != null && AdPositionInfo.PicList.Any() && !string.IsNullOrEmpty(AdPositionInfo.PicList.FirstOrDefault()))
            {
                return Framework.Utility.UrlHelper.GetImageUrl(AdPositionInfo.PicList.FirstOrDefault());
            }

            return "#";
        }

        public string GetPicList()
        {
            if (AdPositionInfo.PicList == null || !AdPositionInfo.PicList.Any())
                return string.Empty;

            var sb = new StringBuilder();
            var i = 1;
            AdPositionInfo.PicList.ForEach(item =>
            {
                sb.AppendFormat(
                    i == 1
                        ? "<li class=\"current\"><a href=\"{0}\" bigimg=\"{0}\"><img src=\"{0}\" alt=\"\" /></a></li>"
                        : "<li><a href=\"{0}\" bigimg=\"{0}\"><img src=\"{0}\" alt=\"\" /></a></li>",
                    Framework.Utility.UrlHelper.GetImageUrl(item));

                i++;
            });

            return sb.ToString();
        }
    }
}