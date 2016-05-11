using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 维修列表查询条件
    /// </summary>
    public class RepairListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 维修列表查询条件
        /// </summary>
        public RepairListQueryInfo()
        {
            OrderBy = "RepairId";
        }

        /// <summary>
        /// ID
        /// </summary>
        public int? RepairId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// 维修人员
        /// </summary>
        public int? RepairUserId { get; set; }

        /// <summary>
        /// 筛选开始时间
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// 筛选结束时间
        /// </summary>
        public DateTime? ToDate { get; set; }

    }
}
