using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 招商列表页Model
    /// </summary>
    public class ProjectPlanListModel : BaseModel
    {
        /// <summary>
        /// 查询消息
        /// </summary>
        public ProjectPlanListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 项目招商计划列表
        /// </summary>
        public List<ProjectPlanInfo> ProjectPlanList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
