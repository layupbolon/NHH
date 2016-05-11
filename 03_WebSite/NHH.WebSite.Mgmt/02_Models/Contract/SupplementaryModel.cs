using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 补充协议
    /// </summary>
    public class SupplementaryModel : SupplementaryInfo
    {
        /// <summary>
        /// 商铺面积
        /// </summary>
        public decimal UnitArea { get; set; }

        /// <summary>
        /// 租金单价
        /// </summary>
        public decimal UnitRent { get; set; }

        /// <summary>
        /// 物业费
        /// </summary>
        public decimal MgmtFee { get; set; }
    }
}
