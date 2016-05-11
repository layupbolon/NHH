 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using Newtonsoft.Json;
 using NHH.Models.Common.Converter;

namespace NHH.Models.Campaign
{
    public class CampaignInfo
    {
        /// <summary>
        /// 活动ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int CampaignID { get; set; }

        /// <summary>
        /// 活动Code
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string CampaignCode { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string CampaignName { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int CampaignType { get; set; }

        /// <summary>
        /// 活动简介
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string CampaignBrief { get; set; }

        /// <summary>
        /// 活动状态 1策划中 2进行中 3待小结 4已完成
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int CampaignStatus { get; set; }

        /// <summary>
        /// 所属项目
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ProjectID { get; set; }

        /// <summary>
        /// 活动地点
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Location { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 预算
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal Budget { get; set; }

        /// <summary>
        /// 策划负责人ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int PlanUserID { get; set; }

        /// <summary>
        /// 策划负责人姓名
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string PlanUserName { get; set; }

        /// <summary>
        /// 策划时间
        /// </summary>
        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime PlanTime { get; set; }

        /// <summary>
        /// 执行负责人ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int RunUserID { get; set; }

        /// <summary>
        /// 执行负责人姓名
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RunUserName { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime RunTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Status { get; set; }

        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime InDate { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int InUser { get; set; }

        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime EditDate { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int EditUser { get; set; }

        /// <summary>
        /// 管理公司名称
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ManageCompanyName { get; set; }

        /// <summary>
        /// 页面标题
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// 页面内容
        /// </summary>
        public string PageContent { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string PageCover { get; set; }
        
    }
}
