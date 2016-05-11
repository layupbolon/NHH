using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NHH.Models.Common.Converter;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 商户租约补充协议
    /// </summary>
    public class ContractSupplementaryInfo
    {
        /// <summary>
        /// 补充协议ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SupplementaryID { get; set; }

        /// <summary>
        /// 租约ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ContractID { get; set; }

        /// <summary>
        /// 协议类型
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SupplementaryType { get; set; }

        /// <summary>
        /// 协议类型描述
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SupplementaryTypeDescription
        {
            get
            {
                switch (SupplementaryType)
                {
                        //TODO:待确认
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// 协议人姓名
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SignerName { get; set; }

        /// <summary>
        /// 协议人身份证号
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SignerIDNumber { get; set; }

        /// <summary>
        /// 协议人联系电话
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SignerPhone { get; set; }

        /// <summary>
        /// 协议内容
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SupplementaryContent { get; set; }

        /// <summary>
        /// 协议起始日期
        /// </summary>
        [JsonConverter(typeof(ChinaShortDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? SupplementaryStartDate { get; set; }

        /// <summary>
        /// 协议结束日期
        /// </summary>
        [JsonConverter(typeof(ChinaShortDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? SupplementaryEndDate { get; set; }

    }
}
