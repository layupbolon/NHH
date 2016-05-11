using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NHH.Models.Common.Converter;
using NHH.Models.Contract;

namespace NHH.Models.Store
{
    public class MerchantStoreView
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// 店铺名
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantID { get; set; }

        public string BizTypeName { get; set; }

        public string StoreLogo { get; set; }
        /// <summary>
        /// 到期日期
        /// </summary>
        [JsonConverter(typeof(ChinaShortDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? ContractEndDate { get; set; }

        /// <summary>
        /// 店铺地址
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string StoreAddress { get; set; }

        /// <summary>
        /// 店铺铺位号
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string StoreNumber { get; set; }

        /// <summary>
        /// 店铺面积
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal StoreArea { get; set; }

        /// <summary>
        /// 店铺租金（月）
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal StoreRent { get; set; }

        /// <summary>
        /// 图片列表
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<MerchantStoreImageInfo> MerchantStoreImageList { get; set; }

        /// <summary>
        /// 品牌列表
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<ContractBrandInfo> BrandList { get; set; }

        //[JsonConverter(typeof(ChinaLongDateConverter))]
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        //public DateTime InDate { get; set; }

        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        //public int InUser { get; set; }

        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        //public string InUserName { get; set; }

        //[JsonConverter(typeof(ChinaLongDateConverter))]
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        //public DateTime EditDate { get; set; }

        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        //public int EditUser { get; set; }

        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        //public string EditUserName { get; set; }
    }
}
