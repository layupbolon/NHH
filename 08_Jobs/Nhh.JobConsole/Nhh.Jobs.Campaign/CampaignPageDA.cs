using Nhh.Jobs.Utility;
using NHH.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Data;

namespace Nhh.Jobs.Campaign
{
    /// <summary>
    /// 企划内容
    /// </summary>
    public class CampaignPageDA
    {
        /// <summary>
        /// 获取需要发送的消息
        /// </summary>
        /// <returns></returns>
        public static List<CampaignPageInfo> GetCampaignList()
        {
            var strCmd = @"SELECT cp.[PageID]
                                                 ,cp.[CampaignID]
                                                 ,c.[CampaignBrief]
                                                 ,c.[Location]
                                                 ,c.[ProjectId]
                                                 ,cp.[PageType]
                                                 ,cp.[PageTitle]
                                                 ,cp.[PageCover]
                                                 ,cp.[PageContent]
                                                 ,cp.[PublishStatus]
                                                 ,cp.[PublishTime]
                                         FROM [dbo].[Campaign_Page](Nolock) cp
                                         INNER JOIN [dbo].[Campaign](Nolock) c on cp.[CampaignID]=c.[CampaignID]
                                         WHERE  cp.PublishStatus=1 And GETDATE() between DATEADD(MINUTE,-30,cp.PublishTime) and DATEADD(MINUTE,30,cp.PublishTime)";
            var table = SqlHelper.ExecuteDataTable(strCmd);
            if (table == null || table.Rows == null || table.Rows.Count == 0)
                return null;

            var campaignPageList = new List<CampaignPageInfo>();
            foreach (DataRow row in table.Rows)
            {
                var info = new CampaignPageInfo();
                info.PageId = (int)row["PageID"];
                info.CampaignId = (int)row["CampaignID"];
                info.CampaignBrief = row["CampaignBrief"].ToString();
                info.Location = row["Location"].ToString();
                info.PageTitle = row["PageTitle"].ToString();
                info.PageCover = row["PageCover"].ToString();
                info.PageContent = row["PageContent"].ToString();
                info.ProjectId = (int)row["ProjectId"];
                info.MerchantUserList = MerchantUserDA.GetMerchantUserList(info.ProjectId);
                info.PageType = (int) row["PageType"];

                var publishTime = DateTime.Now;
                DateTime.TryParse(row["PublishTime"].ToString(), out publishTime);

                info.PublishTime = publishTime;

                //改为发送中，避免重复发送消息
                UpdateStatus(info.PageId, 2);

                campaignPageList.Add(info);
            }

            return campaignPageList;
        }

        /// <summary>
        /// 更新企划页面发布状态
        /// 1:待发送
        /// 2:发送中
        /// 3:已发送
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        internal static int UpdateStatus(int pageId, int publishStatus)
        {
            string strCmd = string.Format(@"Update [dbo].[Campaign_Page] set PublishStatus = {0},EditDate = '{1}' where PageID = {2}", publishStatus, DateTime.Now, pageId);
            return SqlHelper.ExecuteNonQuery(strCmd);
        }
    }
}
