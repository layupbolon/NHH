using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    /// <summary>
    /// 项目业务配置信息
    /// </summary>
    public class ProjectBizConfigListModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public ProjectBizConfigListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 列表
        /// </summary>
        public List<ProjectBizConfigInfo> ConfigList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
