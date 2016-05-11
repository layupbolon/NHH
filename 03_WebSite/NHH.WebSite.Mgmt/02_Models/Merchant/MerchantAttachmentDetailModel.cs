using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 
    /// </summary>
    public class MerchantAttachmentDetailModel : BaseModel
    {
        /// <summary>
        /// 商户Id
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 证照Id
        /// </summary>
        public int AttachmentId { get; set; }

        /// <summary>
        /// 证照类型
        /// </summary>
        public int AttachmentType { get; set; }

        /// <summary>
        /// 附件名
        /// </summary>
        public string AttachmentName { get; set; }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachmentPath { get; set; }

        /// <summary>
        /// 证件编号
        /// </summary>
        public string AttachmentCode { get; set; }

        /// <summary>
        /// 有效到期日
        /// </summary>
        public DateTime? ExpiredDate { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int AuditStatus { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string AuditStatusName { get; set; }

        /// <summary>
        /// 包含附件path和name
        /// </summary>
        public string FileResult { get; set; }

        /// <summary>
        /// 系统用户Id
        /// </summary>
        public int UserId { get; set; }
    }
}
