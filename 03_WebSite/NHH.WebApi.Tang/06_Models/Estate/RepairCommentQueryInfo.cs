using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 维修评价查询信息
    /// </summary>
    public class RepairCommentQueryInfo : QueryInfo
    {
        /// <summary>
        /// 报修编号
        /// </summary>
        public int? RepairId { get; set; }

        /// <summary>
        /// 维修人ID
        /// </summary>
        public int? RepairUserId { get; set; }
    }
}
