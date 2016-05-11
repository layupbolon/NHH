using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 项目招商批次铺位列表
    /// </summary>
    public class ProjectBatchUnitListModel : BaseModel
    {
        /// <summary>
        /// 查询消息
        /// </summary>
        public ProjectBatchUnitListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 铺位列表
        /// </summary>
        public List<ProjectBatchUnitInfo> UnitList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
