using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 项目招商批次列表查询信息
    /// </summary>
    public class ProjectBatchListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 项目招商批次列表查询信息
        /// </summary>
        public ProjectBatchListQueryInfo()
        {
            OrderBy = "BatchID";
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectId { get; set; }
    }
}
