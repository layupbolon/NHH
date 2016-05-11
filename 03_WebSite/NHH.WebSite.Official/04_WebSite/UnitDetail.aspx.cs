using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using NHH.Framework.Service;
using NHH.Models.Official;
using NHH.Service.Official.IService;

namespace NHH.WebSite.Official
{
    public partial class UnitDetail : Page
    {
        public UnitInfo unitInfo;
        public int unitID;
        public int projectID;
        public string building;
        public int bizType;
        public int unitArea;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["unitID"] == null || !int.TryParse(Request.QueryString["unitID"], out unitID))
                Response.Redirect("Business.aspx");

            if (Request.QueryString["pid"] == null || !int.TryParse(Request.QueryString["pid"], out projectID))
                projectID = 0;
            building = Request.QueryString["bid"];
            if (Request.QueryString["biz"] == null || !int.TryParse(Request.QueryString["biz"], out bizType))
                bizType = 0;
            if (Request.QueryString["area"] == null || !int.TryParse(Request.QueryString["area"], out unitArea))
                unitArea = 0;

            var service = NHHServiceFactory.Instance.CreateService<IBusinessService>();
            unitInfo = service.GetUnitInfo(unitID);
        }

        public string GetFirstPic()
        {
            if (unitInfo.PicList != null && unitInfo.PicList.Any() && !string.IsNullOrEmpty(unitInfo.PicList.FirstOrDefault()))
            {
                return Framework.Utility.UrlHelper.GetImageUrl(unitInfo.PicList.FirstOrDefault());
            }

            return "#";
        }

        public string GetPicList()
        {
            if (unitInfo.PicList == null || !unitInfo.PicList.Any())
                return string.Empty;

            var sb = new StringBuilder();
            var i = 1;
            unitInfo.PicList.ForEach(item =>
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