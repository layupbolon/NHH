using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户列表model
    /// </summary>
    public class MerchantListModel : BaseModel
    {
        /// <summary>
        /// 查询消息
        /// </summary>
        public MerchantListQueryInfo QueryInfo { get; set; }
    }
}
