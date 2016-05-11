using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户附件列表
    /// </summary>
    public class MerchantAttachmentListModel : BaseModel
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantId { get; set; }
        
        /// <summary>
        /// 获取单个商户证照列表
        /// </summary>
        public List<AttachmentInfo> AttachmentList { get; set; }
    }
}
