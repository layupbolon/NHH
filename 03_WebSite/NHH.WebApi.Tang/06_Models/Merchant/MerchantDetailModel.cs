using Newtonsoft.Json;
using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户详细信息Model
    /// </summary>
    public class MerchantDetailModel : MerchantInfo
    {
        /// <summary>
        /// 省份ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ProvinceId { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int CityId { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string CityName { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AddressLine { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Zipcode { get; set; }

        /// <summary>
        /// 营业执照注册号
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string LicenseId { get; set; }

        /// <summary>
        /// 注册省份ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int RegisterProvinceId { get; set; }

        /// <summary>
        /// 注册省份
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RegisterProvinceName { get; set; }

        /// <summary>
        /// 注册城市ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int RegisterCityId { get; set; }

        /// <summary>
        /// 注册城市
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RegisterCityName { get; set; }

        /// <summary>
        /// 注册详细地址
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RegisterAddressLine { get; set; }

        /// <summary>
        /// 联系人身份证号码
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ContactIDNumber { get; set; }

        /// <summary>
        /// 联系人邮箱
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ContactEmail { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ContactPhone { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int InUser { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? InDate { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int EditUser { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? EditDate { get; set; }

        /// <summary>
        ///商户财务信息
        /// </summary>
        public MerchantFinanceModel FinanceInfo { get; set; }

        ///// <summary>
        ///// 商户证照信息
        ///// </summary>
        //public MerchantAttachmentModel MerchantAttachmentModel { get; set; }

        /// <summary>
        /// 商户附件列表
        /// </summary>
        public List<AttachmentInfo> AttachmentList { get; set; }


    }
}
