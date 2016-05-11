using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 项目列表Model
    /// </summary>
    public class ProjectListModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public ProjectListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 项目列表
        /// </summary>
        public List<ProjectInfo> ProjectList { get; set; }

        /// <summary>
        /// 分页
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
