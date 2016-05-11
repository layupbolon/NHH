using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Official;
using NHH.Service.Official.IService;

namespace NHH.Service.Official
{
    public class CampaignService : NHHService<NHHEntities>, ICampaignService
    {
        /// <summary>
        /// 获取全部活动
        /// </summary>
        /// <returns></returns>
        private List<CampaignInfo> GetAllCampaign()
        {
            var query = from c in Context.Campaign
                        join cp in Context.Campaign_Page on c.CampaignID equals cp.CampaignID
                        where c.Status == 1 && cp.PageType == 2 && cp.PublishStatus == 3
                        select new CampaignInfo()
                        {
                            CampaignID = c.CampaignID,
                            PageID = cp.PageID,
                            CampaignName = c.CampaignName,
                            StartDate = c.StartDate,
                            EndDate = c.EndDate,
                            PublishDate = cp.PublishTime.Value,
                            CampaignBrief = c.CampaignBrief,
                            CampaignCover = cp.PageCover
                        };

            if (!query.Any()) return new List<CampaignInfo>();

            return query.OrderByDescending(a => a.StartDate).ToList();
        }

        /// <summary>
        /// 获取顶部三个活动
        /// </summary>
        /// <returns></returns>
        public string GetTopCampaign()
        {
            try
            {
                var list = GetAllCampaign().Take(3).ToList();
                if (!list.Any()) return string.Empty;

                var sb = new StringBuilder();
                var i = 1;
                string startDate;
                string endDate;
                list.ForEach(info =>
                {
                    startDate = info.StartDate.ToString("MM月dd日");
                    endDate = info.EndDate.ToString("MM月dd日");

                    sb.AppendLine(i == 1
                        ? "<div class=\"two-column u-half fl\">"
                        : "<div class=\"two-column u-half u-half-h fl\">");
                    sb.AppendFormat("<a href=\"{0}\" target=\"_blank\">", string.Format("CampaignDetail.aspx?pageID={0}", info.PageID));
                    sb.AppendFormat("<div class=\"adbg\" style=\"background-image: url({0});\"></div>",
                        Framework.Utility.UrlHelper.GetImageUrl(info.CampaignCover));
                    if (i == 1)
                    {
                        sb.AppendLine("<div class=\"bg\"></div>");
                        sb.AppendLine("<div class=\"panel-content activA\">");
                    }
                    else
                    {
                        sb.AppendLine("<div class=\"panel-content activA pb20\">");
                    }
                    sb.AppendFormat("<h3>{0}</h3>", info.CampaignName);
                    sb.AppendFormat("<blockquote class=\"mt10\">活动时间：{0}<br><p>{1}</p></blockquote>",
                        startDate.Equals(endDate, StringComparison.CurrentCulture) ? startDate : string.Format("{0} 至 {1}", startDate, endDate),
                        info.CampaignBrief);
                    sb.AppendLine("</div></a></div>");

                    i++;
                });

                return sb.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取剩下的活动
        /// </summary>
        /// <returns></returns>
        public string GetOtherCampaign()
        {
            try
            {
                var list = GetAllCampaign().Skip(3).ToList();
                if (!list.Any()) return string.Empty;

                var sb = new StringBuilder();
                string startDate;
                string endDate;
                list.ForEach(info =>
                {
                    startDate = info.StartDate.ToString("MM月dd日");
                    endDate = info.EndDate.ToString("MM月dd日");

                    sb.AppendFormat("<dd><a class=\"imgCover\" href=\"{0}\" target=\"_blank\"><img src=\"{1}\"></a>", string.Format("CampaignDetail.aspx?pageID={0}", info.PageID),
                        Framework.Utility.UrlHelper.GetImageUrl(info.CampaignCover));
                    sb.AppendFormat("<a href=\"{0}\" target=\"_blank\"><h4>{1}</h4>",
                        string.Format("CampaignDetail.aspx?pageID={0}", info.PageID), info.CampaignName);
                    sb.AppendFormat("<p>活动时间：{0}</p>",
                        startDate.Equals(endDate, StringComparison.CurrentCulture)
                            ? startDate
                            : string.Format("{0} 至 {1}", startDate, endDate));
                    sb.AppendFormat("<p>{0}</p></a></dd>", info.CampaignBrief);
                });

                return sb.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取活动内容
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public string GetCampaignContent(int pageID)
        {
            return Context.Campaign_Page.Where(a => a.PageID == pageID).Select(a => a.PageContent).FirstOrDefault();
        }
    }
}
