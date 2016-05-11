using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Message.Models.Sms
{
    /// <summary>
    /// 特殊单据审批通知
    /// </summary>
    public class MerchantDocumentsAuditNotify
    {
        public MerchantDocumentsAuditNotify(string storeName, string applicationType)
        {
            this.StoreName = storeName;
            this.ApplicationType = applicationType;
        }

        public const string TemplateCode = "SMS_3125137";
        /// <summary>
        /// 店铺名
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 单据类型
        /// </summary>
        public string ApplicationType { get; set; }
    }

    /// <summary>
    /// 特殊单据撤回通知
    /// </summary>
    public class MerchantDocumentsCancelNotify
    {
        public MerchantDocumentsCancelNotify(string merchantName, string applicationType)
        {
            this.MechantName = merchantName;
            this.ApplicationType = applicationType;
        }
        public const string TemplateCode = "SMS_3055625";
        /// <summary>
        /// 店铺名
        /// </summary>
        public string MechantName { get; set; }
        /// <summary>
        /// 单据类型
        /// </summary>
        public string ApplicationType { get; set; }

    }
    /// <summary>
    /// 商户证照审核通知
    /// </summary>
    public class MerchantAttachmentAuditNotify
    {
        public MerchantAttachmentAuditNotify(string storeName, string photoID)
        {
            this.StoreName = storeName;
            this.PhotoID = photoID;
        }
        public const string TemplateCode = "SMS_3110231";
        /// <summary>
        /// 商户名
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 证照类型
        /// </summary>
        public string PhotoID { get; set; }
    }
    /// <summary>
    /// 商户证照有效期提醒
    /// </summary>
    public class MerchantAttachmentExpiredNotify
    {
        public MerchantAttachmentExpiredNotify(string storeName)
        {
            this.StoreName = storeName;
        }
        public const string TemplateCode = "SMS_3125466";
        /// <summary>
        /// 商户名
        /// </summary>
        public string StoreName { get; set; }
    }

    /// <summary>
    /// 缴费提醒
    /// </summary>
    public class PaymentNotify
    {
        public PaymentNotify(string userName)
        {
            this.Username = userName;
        }
        public const string TemplateCode = "SMS_3585207";

        /// <summary>
        /// 商户用户名
        /// </summary>
        public string Username { get; set; }
    }

    /// <summary>
    /// 商户合同到期提醒
    /// </summary>
    public class MerchantContractNotify
    {
        public MerchantContractNotify(string storeName, string contractCode, string date)
        {
            this.StoreName = storeName;
            this.ContractID = contractCode;
            this.Date = date;
        }
        public const string TemplateCode = "SMS_5070204";

        /// <summary>
        /// 店名
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractID { get; set; }

        /// <summary>
        /// 到期日
        /// </summary>
        public string Date { get; set; }
    }
}
