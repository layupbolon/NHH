using System;

namespace Nhh.Jobs.Model
{
    /// <summary>
    /// 商户证照审核人信息（证照）
    /// </summary>
    public class AttachApproveInfo
    {
        /// <summary>
        /// 商户证照ID
        /// </summary>
        public int AttachmentID { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantID { get; set; }

        /// <summary>
        /// 商户证照审核人ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 审核人手机
        /// </summary>
        public string Mobile { get; set;}

        /// <summary>
        /// 商户名
        /// </summary>
        public string MerchantName { get; set; }
    }
}
