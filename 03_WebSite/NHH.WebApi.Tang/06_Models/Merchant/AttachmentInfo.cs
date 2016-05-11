using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户上传附件基本信息
    /// </summary>
    public class AttachmentInfo
    {
        /// <summary>
        /// 附件Id
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Id { get; set; }

        /// <summary>
        /// 商户Id
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MerchantId { get; set; }

        /// <summary>
        /// 附件类型 1营业执照 2组织结构代码证 3税务登记证 4自然人身份证 5品牌授权书
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Type { get; set; }

        /// <summary>
        /// 附件名 例：Merchant-150914142332-6.jpg
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// 附件路径 例：../../UploadFiles/MerchantPictures/Merchant-150914142332-6.jpg
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Path { get; set; }

        /// <summary>
        /// 附件原始图片路径
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string OriginalPicPath { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int InUser { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? InDate { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int EditUser { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? EditDate { get; set; }

    }
}
