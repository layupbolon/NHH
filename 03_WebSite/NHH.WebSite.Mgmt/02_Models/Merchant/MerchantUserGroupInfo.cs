using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户用户组信息
    /// </summary>
    public class MerchantUserGroupInfo
    {
        /// <summary>
        /// 商铺ID
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 商铺名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 铺位编号列表
        /// </summary>
        public string UnitNumberList { get; set; }

        /// <summary>
        /// 用户列表
        /// </summary>
        public List<MerchantUserInfo> UserList { get; set; }
    }
}
