using NHH.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace NHH.Models.Campaign
{
    public class CampaignPlanDetailModel : BaseModel
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
        [Required(ErrorMessage = "请输入活动名称")]
        public string CampaignName { get; set; }

        /// <summary>
        /// 活动类型Id
        /// </summary>
        [Required(ErrorMessage = "请选择活动类型")]
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
        /// 活动简介
        /// </summary>
        [Required(ErrorMessage = "请输入活动简介")]
        public string CampaignBrief { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        [Required(ErrorMessage = "请选择项目")]
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 活动地点
        /// </summary>
        [Required(ErrorMessage = "请输入活动地点")]
        public string Location { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "请输入活动开始时间")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required(ErrorMessage = "请输入活动结束时间")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 活动页面标题
        /// </summary>
        [Required(ErrorMessage = "请输入页面主题")]
        public string PageTitle { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [Required(ErrorMessage = "请输入发布时间")]
        public string PublishDate { get; set; }

        /// <summary>
        /// 发布时间节点
        /// </summary>
        public int PublishTime { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public string PublishStatusName { get; set; }

        /// <summary>
        /// 发布状态Id
        /// </summary>
        public int PublishStatus { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string PageCover { get; set; }

        /// <summary>
        /// 页面内容
        /// </summary>
        [Required(ErrorMessage = "请输入页面内容")]
        public string PageContent { get; set; }

        /// <summary>
        /// 获取系统用户
        /// </summary>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// 页面类型 1-H5; 2-WEB
        /// </summary>
        public int PageType { get; set; }

        /// <summary>
        /// SEO标题
        /// </summary>
        public string SEO_Title { get; set; }

        /// <summary>
        /// SEO关键字
        /// </summary>
        public string KeyWords { get; set; }

        /// <summary>
        /// SEO描述
        /// </summary>
        public string Description { get; set; }
    }
}
