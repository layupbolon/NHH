using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NHH.Models.Common.Converter;

namespace NHH.Models.Campaign
{
    public class CampaignAttachmentInfo
    {
        /// <summary>
        /// 附件ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int AttachmentID { get; set; }

        /// <summary>
        /// 活动ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int CampaignID { get; set; }

        /// <summary>
        /// 附件类型
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int AttachmentType { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AttachmentName { get; set; }

        /// <summary>
        /// 附件路径
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AttachmentPath { get; set; }

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
        public DateTime? EditDate { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int EditUser { get; set; }
    }
}
