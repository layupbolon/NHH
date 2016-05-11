using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NHH.Models.Common.Converter;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户通知信息
    /// </summary>
    public class MerchantMessageInfo
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public int MessageID { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MerchantID { get; set; }

        /// <summary>
        /// 商铺ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int StoreID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int UserID { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Subject { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Content { get; set; }

        /// <summary>
        /// 来源类型 1-通知；2-租约；3-账单；4-催款；5-活动；6-报修；7-投诉
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SourceType { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SourceTypeName
        {
            get
            {
                switch (SourceType)
                {
                    case 1:
                        return "通知";
                    case 2:
                        return "租约";
                    case 3:
                        return "账单";
                    case 4:
                        return "催款";
                    case 5:
                        return "活动";
                    case 6:
                        return "报修";
                    case 7:
                        return "投诉";
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// 来源单据ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SourceRefID { get; set; }

        /// <summary>
        /// 状态 -1作废 1未读 2已读
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Status { get; set; }

        public int InUser { get; set; }

        public string InUserName { get; set; }

        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(PropertyName = "CreateTime", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime InDate { get; set; }
    }
}
