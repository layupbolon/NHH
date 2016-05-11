using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhCampaignService
{
    /// <summary>
    /// 企划活动页面
    /// </summary>
    public class CampaignPageInfo
    {
        /// <summary>
        /// 页面Id
        /// </summary>
        public int PageId { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public int CampaignId { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目下所有商户用户信息
        /// </summary>
        public List<MerchantUserInfo> MerchantUserList { get; set; }

        /// <summary>
        /// 页面类型1:h5 2:web
        /// </summary>
        public int PageType { get; set; }

        /// <summary>
        /// 页面标题
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
        /// 发布时间
        /// </summary>
        public DateTime PublishTime { get; set; }

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
