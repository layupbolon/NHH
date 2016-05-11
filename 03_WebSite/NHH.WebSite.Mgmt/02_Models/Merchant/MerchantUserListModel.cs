using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户用户列表信息
    /// </summary>
    public class MerchantUserListModel : BaseModel
    {
        /// <summary>
        /// 查询消息
        /// </summary>
        public MerchantUserListQueryInfo QueryInfo { get; set; }
        
        
        /// <summary>
        /// 商户用户信息
        /// </summary>
        public List<MerchantUserInfo> MerchantUserList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

    }
}
