using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using NHH.Framework.Service;
using NHH.Models.Official;
using NHH.Service.Official.IService;

namespace NHH.WebSite.Official
{
    public partial class KioskDetail : Page
    {
        public int kioskID;
        public KioskInfo kioskInfo;
        public int projectID;
        public int floorID;
        public int regionID;
        public int businessScopeID;
        public int areaID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["kid"] == null || !int.TryParse(Request.QueryString["kid"], out kioskID))
                Response.Redirect("Business.aspx");

            if (Request.QueryString["pid"] == null || !int.TryParse(Request.QueryString["pid"], out projectID))
                projectID = 0;
            if (Request.QueryString["fid"] == null || !int.TryParse(Request.QueryString["fid"], out floorID))
                floorID = 0;
            if (Request.QueryString["rid"] == null || !int.TryParse(Request.QueryString["rid"], out regionID))
                regionID = 0;
            if (Request.QueryString["busScope"] == null || !int.TryParse(Request.QueryString["busScope"], out businessScopeID))
                businessScopeID = 0;
            if (Request.QueryString["area"] == null || !int.TryParse(Request.QueryString["area"], out areaID))
                areaID = 0;

            var service = NHHServiceFactory.Instance.CreateService<IBusinessService>();
            kioskInfo = service.GetKioskInfo(kioskID);
        }

        public string GetFirstPic()
        {
            if (kioskInfo.PicList != null && kioskInfo.PicList.Any() && !string.IsNullOrEmpty(kioskInfo.PicList.FirstOrDefault()))
            {
                return Framework.Utility.UrlHelper.GetImageUrl(kioskInfo.PicList.FirstOrDefault());
            }

            return "#";
        }

        public string GetPicList()
        {
            if (kioskInfo.PicList == null || !kioskInfo.PicList.Any())
                return string.Empty;

            var sb = new StringBuilder();
            var i = 1;
            kioskInfo.PicList.ForEach(item =>
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