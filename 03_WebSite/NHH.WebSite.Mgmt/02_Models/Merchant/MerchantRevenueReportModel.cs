using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户销售额统计
    /// </summary>
    public class MerchantRevenueReportModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public MerchantRevenueListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 楼层列表
        /// </summary>
        public List<MerchantRevenueReportFloorItem> FloorList { get; set; }

        /// <summary>
        /// 合计信息
        /// </summary>
        public MerchantRevenueReportCountInfo CountInfo { get; set; }
    }
}
