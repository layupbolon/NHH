using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NHH.Models.Common.Converter;
using NHH.Models.Common.Enum.CommonEnums;

namespace NHH.Models.Merchant
{
    public class MerchantAttachmentInfo
    {
        public int ProjectID { get; set; }
        /// <summary>
        /// 附件ID
        /// </summary>
        public int AttachmentID { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MerchantID { get; set; }
        /// <summary>
        /// 附件类型
        /// </summary>
        public int AttachmentType { get; set; }

        /// <summary>
        /// 附件类型名
        /// </summary>
        public string AttachmentTypeName { get; set; }
            //    switch (AttachmentType)
            //    {
            //        case (int)MerchantAttachTypeEnum.BrandLicense:
            //            return "品牌授权书";
            //        case (int)MerchantAttachTypeEnum.BusinessLicense:
            //            return "营业执照";
            //        case (int)MerchantAttachTypeEnum.IdCardBack:
            //            return "身份证(背面)";
            //        case (int)MerchantAttachTypeEnum.IdCardFront:
            //            return "身份证(正面)";
            //        case (int)MerchantAttachTypeEnum.OrganizationCodeCertificate:
            //            return "组织机构代码证";
            //        case (int)MerchantAttachTypeEnum.Other:
            //            return "其它";
            //        case (int)MerchantAttachTypeEnum.PowerofAttorney:
            //            return "委托书";
            //        case (int)MerchantAttachTypeEnum.TaxRegistrationCertificate:
            //            return "税务登记证";
            //        default:
            //            return null;
            //    }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string AttachmentName { get; set; }
        /// <summary>
        /// 附件存储路径
        /// </summary>
        public string AttachmentPath { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Status { get; set; }

        public string StatusStr
        {
            get
            {
                switch (Status)
                {
                    case -1:
                        return "做废";
                    case 1:
                        return "待审核";
                    case 2:
                        return "审核未通过";
                    case 3:
                        return "已审核";
                    case 99:
                        return "即将过期";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpiredDate { get; set; }

        /// <summary>
        /// 证照号
        /// </summary>
        public string AttachmentCode { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int AuditStatus { get; set; }

        public int InUser { get; set; }

        public string InUserName { get; set; }

        [JsonConverter(typeof(ChinaLongDateConverter))]
        public DateTime InDate { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int EditUser { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime EditDate { get; set; }
    }
}
