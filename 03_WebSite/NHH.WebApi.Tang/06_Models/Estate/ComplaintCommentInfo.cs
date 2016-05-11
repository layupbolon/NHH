using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NHH.Models.Common.Converter;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 投诉评价基本信息
    /// </summary>
    public class ComplaintCommentInfo
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int CommentID { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ComplaintID { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal Speed { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal Quality { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal Attitude { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal Overall { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Comment { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Additional { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Status { get; set; }

        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public System.DateTime InDate { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int InUser { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string InUserName { get; set; }

        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime EditDate { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int EditUser { get; set; }

    }
}
