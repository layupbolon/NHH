using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.ProjectUnit
{
    /// <summary>
    /// 铺位调整列表
    /// </summary>
    public class ProjectUnitAdjustListModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public ProjectUnitAdjustListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 调整列表
        /// </summary>
        public List<ProjectUnitAdjustInfo> AdjustList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
