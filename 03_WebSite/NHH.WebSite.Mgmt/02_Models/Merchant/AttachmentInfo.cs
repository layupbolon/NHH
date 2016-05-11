using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户上传附件基本信息
    /// </summary>
    public class AttachmentInfo
    {
        /// <summary>
        /// 商户Id
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 附件Id
        /// </summary>
        public int AttId { get; set; }

        /// <summary>
        /// 附件名 例：Merchant-150914142332-6.jpg
        /// </summary>
        public string AttName { get; set; }

        /// <summary>
        /// 附件编号
        /// </summary>
        public string AttCode { get; set; }

        /// <summary>
        /// 附件类型
        /// </summary>
        public int AttType { get; set; }

        /// <summary>
        /// 附件类型
        /// </summary>
        public string AttTypeName { get; set; }

        /// <summary>
        /// 附件路径 
        /// </summary>
        public string AttPath { get; set; }

        /// <summary>
        /// 上传用户
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 有效期到期日
        /// </summary>
        public DateTime? ExpiredDate { get; set; }

        /// <summary>
        /// 到期天数
        /// </summary>
        public int? ExpiredDays { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int AuditStatus { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string AuditStatusName { get; set; }

    }
}
