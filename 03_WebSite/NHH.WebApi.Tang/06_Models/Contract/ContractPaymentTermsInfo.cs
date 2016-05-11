using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NHH.Models.Contract
{
    public class ContractPaymentTermsInfo
    {
        /// <summary>
        /// 租约支付条款ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int PaymentTermsID { get; set; }

        /// <summary>
        /// 租赁方式 1租金 2扣点 3租金与扣点两者取高
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int PaymentTermsType { get; set; }

        /// <summary>
        /// 租赁方式描述
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string PaymentTermsTypeDescription
        {
            get
            {
                switch (PaymentTermsType)
                {
                    case 1:
                        return "租金";
                    case 2:
                        return "扣点";
                    case 3:
                        return "租金与扣点两者取高";
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// 费用科目ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int PaymentItemID { get; set; }

        /// <summary>
        /// 费用科目名称
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string PaymentItemName { get; set; }

        /// <summary>
        /// 租金账期 1日结 2月结 3季结 4半年结 5年结
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int PaymentPeriod { get; set; }

        /// <summary>
        /// 租金账期描述
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string PaymentPeriodDescription
        {
            get
            {
                switch (PaymentPeriod)
                {
                    case 1:
                        return "日结";
                    case 2:
                        return "月结";
                    case 3:
                        return "季结";
                    case 4:
                        return "半年结";
                    case 5:
                        return "年结";
                    default:
                        return null;
                }
            }
        }
    }
}
