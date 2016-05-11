using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Campaign
{

    /// <summary>
    /// 企划活动页面
    /// </summary>
    public class CampaignPageInfo
    {
        /// <summary>
        /// 活动页面Id
        /// </summary>
        public int PageId { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public int CampaignId { get; set; }

        /// <summary>
        /// 活动页面标题
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string PageCover { get; set; }

        /// <summary>
        /// 页面内容
        /// </summary>
        public string PageContent { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public int PublishStatus { get; set; }

        /// <summary>
        /// 发布状态名称
        /// </summary>
        public string PublishStatusName { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? PublishTime { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        public int PublishUser { get; set; }

        /// <summary>
        /// 备注
        /// </summary>

        public string Remark { get; set; }
    }
}
