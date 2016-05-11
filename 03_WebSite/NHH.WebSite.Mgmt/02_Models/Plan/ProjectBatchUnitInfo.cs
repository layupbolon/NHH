using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 项目招商批次铺位信息
    /// </summary>
    public class ProjectBatchUnitInfo
    {
        public int BatchID { get; set; }

        /// <summary>
        /// 批次编号
        /// </summary>
        public string BatchCode { get; set; }

        public int UnitId { get; set; }

        /// <summary>
        /// 铺位编号
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// 铺位面积
        /// </summary>
        public decimal UnitArea { get; set; }

        /// <summary>
        /// 铺位类型
        /// </summary>
        public string UnitType { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        public string BizType { get; set; }
    }
}
