namespace Nhh.Jobs.Model
{
    /// <summary>
    /// 商户管理员信息（证照）
    /// </summary>
    public class MerchantRoleUserInfo
    {
        /// <summary>
        /// 商户证照ID
        /// </summary>
        public int AttachmentID { get; set; }

        /// <summary>
        /// 证照类型
        /// </summary>
        public string AttachmentType { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantID { get; set; }

        /// <summary>
        /// 商户微信OpenID
        /// </summary>
        public string WechatOpenID { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// 商户用户ID
        /// </summary>
        public int UserID { get; set; }
    }
}
