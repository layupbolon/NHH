using NHH.Framework.Wechat;

namespace NHH.Message.Models.Wechat
{
    /// <summary>
    /// 商户证照审核提醒
    /// </summary>
    public class MerchantLicenseMessage : requestData
    {
        /// <summary>
        /// 标题
        /// </summary>
        public requestFieldDetail first { get; set; }

        /// <summary>
        /// 证照类型
        /// </summary>
        public requestFieldDetail keyword1 { get; set; }

        /// <summary>
        /// 审核结果
        /// </summary>
        public requestFieldDetail keyword2 { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public requestFieldDetail keyword3 { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public requestFieldDetail remark { get; set; }
    }
}
