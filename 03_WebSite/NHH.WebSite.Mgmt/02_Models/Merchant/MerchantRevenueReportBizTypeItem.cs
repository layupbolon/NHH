using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户销售额统计业态
    /// </summary>
    public class MerchantRevenueReportBizTypeItem
    {
        /// <summary>
        /// 业态ID
        /// </summary>
        public int BizTypeId { get; set; }

        /// <summary>
        /// 业态名称
        /// </summary>
        public string BizTypeName { get; set; }

        /// <summary>
        /// 商铺列表
        /// </summary>
        public List<MerchantRevenueStoreInfo> StoreList { get; set; }

        /// <summary>
        /// 业态合计信息
        /// </summary>
        public MerchantRevenueReportBizTypeCount BizTypeCount { get; set; }
    }
}
