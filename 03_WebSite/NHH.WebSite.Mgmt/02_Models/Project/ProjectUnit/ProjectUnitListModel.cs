using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.ProjectUnit
{
    /// <summary>
    /// 铺位列表Model
    /// </summary>
    public class ProjectUnitListModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public ProjectUnitListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 楼宇列表
        /// </summary>
        public List<BuildingCommonInfo> BuildingList { get; set; }

        /// <summary>
        /// 楼层列表
        /// </summary>
        public List<FloorCommonInfo> FloorList { get; set; }

        /// <summary>
        /// 商铺列表
        /// </summary>
        public List<ProjectUnitInfo> ProjectUnitList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
