using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NHH.Models.Common.Converter;

namespace NHH.Models.Store
{
    public class MerchantStoreImageInfo
    {
        /// <summary>
        /// 图片ID
        /// </summary>
        public int ImageID { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int StoreID { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MerchantID { get; set; }

        /// <summary>
        /// 图片序号
        /// </summary>
        public int SeqNo { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 图片原图路径
        /// </summary>
        public string ImageBigPath { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int InUser { get; set; }

        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime InDate { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int EditUser { get; set; }

        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime EditDate { get; set; }

       
    }
}
