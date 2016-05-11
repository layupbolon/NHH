using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商铺列表查询信息
    /// </summary>
    public class MerchantStoreListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 商铺列表查询信息
        /// </summary>
        public MerchantStoreListQueryInfo()
        {
            OrderBy = "StoreId";
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 楼层ID
        /// </summary>
        public int? FloorId { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int? MerchantId { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName { get; set; }

        /// <summary>
        /// 商铺ID
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// 商铺名称
        /// </summary>
        public string StoreName { get; set; }
    }
}
