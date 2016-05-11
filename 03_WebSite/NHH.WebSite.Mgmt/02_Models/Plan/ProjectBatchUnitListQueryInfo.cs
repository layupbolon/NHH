using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 项目招商批次铺位列表查询信息
    /// </summary>
    public class ProjectBatchUnitListQueryInfo : QueryInfo
    {
        public ProjectBatchUnitListQueryInfo()
        {
            OrderBy = "UnitID";
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 招商批次
        /// </summary>
        public int? BatchId { get; set; }
    }
}
