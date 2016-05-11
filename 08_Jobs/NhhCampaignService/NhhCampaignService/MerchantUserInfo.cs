using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhCampaignService
{
    /// <summary>
    /// 商户用户信息
    /// </summary>
    public class MerchantUserInfo
    {
        /// <summary>
        /// 商户用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 商户用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 商户Id
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        public string WechatOpenId { get; set; }
    }
}
