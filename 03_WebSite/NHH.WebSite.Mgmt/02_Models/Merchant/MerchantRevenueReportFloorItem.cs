using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户销售额楼层统计
    /// </summary>
    public class MerchantRevenueReportFloorItem
    {
        /// <summary>
        /// 楼层ID
        /// </summary>
        public int FloorId { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 业态列表
        /// </summary>
        public List<MerchantRevenueReportBizTypeItem> BizTypeList { get; set; }

        /// <summary>
        /// 楼层合计信息
        /// </summary>
        public MerchantRevenueReportFloorCount FloorCount { get; set; }
    }
}
