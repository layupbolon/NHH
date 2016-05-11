using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    public class MerchantAttachmentModel
    {
        /// <summary>
        /// 营业执照
        /// </summary>
        public AttachmentInfo BusinessCertificate { get; set; }

        /// <summary>
        /// 组织结构代码证
        /// </summary>
        public AttachmentInfo OrganizationCertificate { get; set; }

        /// <summary>
        /// 税务登记证
        /// </summary>
        public AttachmentInfo TaxCertificate { get; set; }

        /// <summary>
        /// 法人代表身份证
        /// </summary>
        public AttachmentInfo IdCertificate { get; set; }

    }
}
