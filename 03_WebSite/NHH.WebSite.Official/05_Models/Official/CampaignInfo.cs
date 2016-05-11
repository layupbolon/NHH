using System;

namespace NHH.Models.Official
{
    public class CampaignInfo
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        public int CampaignID { get; set; }

        /// <summary>
        /// 活动页面ID
        /// </summary>
        public int PageID { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// 活动开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 活动结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// 活动简介
        /// </summary>
        public string CampaignBrief { get; set; }

        /// <summary>
        /// 活动图片
        /// </summary>
        public string CampaignCover { get; set; }
    }
}
