using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户列表查询信息
    /// </summary>
    public class MerchantListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 商户列表查询信息
        /// </summary>
        public MerchantListQueryInfo()
        {
            OrderBy = "MerchantName";
        }

        /// <summary>
        /// 商户类型
        /// </summary>
        public int? MerchantType { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName { get; set; }

        /// <summary>
        /// 分页数
        /// </summary>
        public int? Page { get; set; }
    }
}
