using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NHH.Models.Common.Converter;

namespace NHH.Models.Campaign
{
    public class CampaignCommentInfo
    {
        public int CommentID { get; set; }
        public int CampaignID { get; set; }
        public int Overall { get; set; }
        public string CommentContent { get; set; }
        public string CommentAdditional { get; set; }
        public int CommentUserID { get; set; }
        public string CommentUserName { get; set; }
        public int Status { get; set; }

        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(PropertyName = "CreateTime", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime InDate { get; set; }
        public int InUser { get; set; }

        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(PropertyName = "CreateTime", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? EditDate { get; set; }
        public int EditUser { get; set; }
    }
}
