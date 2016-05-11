using NHH.Models.Common;
using System;

namespace NHH.Models.Campaign
{
    /// <summary>
    /// 活动查询条件
    /// </summary>
    public class CampaignQueryInfo : QueryInfo
    {
        /// <summary>
        /// 活动查询条件
        /// </summary>
        public CampaignQueryInfo()
        {
            OrderBy = "CampaignId";
        }

        /// <summary>
        /// 活动Id
        /// </summary>
        public int? CampaignId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        public int? CampaignType { get; set; }

        /// <summary>
        /// 活动状态
        /// </summary>
        public int? CampaignStatus { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        private int? _pageType = 1;
        /// <summary>
        /// 1.唐小二  2.官网
        /// </summary>
        public int? PageType
        {
            get { return _pageType; }
            set { _pageType = value; }
        }
    }
}
