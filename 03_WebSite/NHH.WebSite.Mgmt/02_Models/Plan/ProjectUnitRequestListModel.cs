using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 铺位租决列表
    /// </summary>
    public class ProjectUnitRequestListModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public ProjectUnitRequestListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 租决列表
        /// </summary>
        public List<ProjectUnitRequestInfo> RequestList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
