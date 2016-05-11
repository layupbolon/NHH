using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户销售列表Model
    /// </summary>
    public class MerchantRevenueListModel : BaseModel
    {
        /// <summary>
        /// 商户销售查询条件
        /// </summary>
        public MerchantRevenueListQueryInfo QueryInfo { get; set; }


        /// <summary>
        /// 商户销售商铺列表
        /// </summary>
        public List<MerchantRevenueStoreInfo> StoreList { get; set; }


        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
