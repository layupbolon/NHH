using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Campaign
{
    /// <summary>
    /// 企划活动列表信息
    /// </summary>
    public class CampaignInfo : BaseModel
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public int CampaignId { get; set; }

        /// <summary>
        /// 活动编号
        /// </summary>
        public string CampaignCode { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 活动类型Id
        /// </summary>
        public int CampaignType { get; set; }

        /// <summary>
        /// 活动类型名称
        /// </summary>
        public string CampaignTypeName { get; set; }

        /// <summary>
        /// 活动状态Id
        /// </summary>
        public int CampaignStatus { get; set; }

        /// <summary>
        /// 活动状态名称
        /// </summary>
        public string CampaignStatusName { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public int PublishStatus { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 活动目标
        /// </summary>
        public string PageTypeName { get; set; }

        /// <summary>
        /// 页面ID
        /// </summary>
        public int PageID { get; set; }

    }
}
