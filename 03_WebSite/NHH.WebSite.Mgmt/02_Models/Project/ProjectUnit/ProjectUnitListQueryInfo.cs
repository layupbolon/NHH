using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.ProjectUnit
{
    /// <summary>
    /// 商铺列表查询信息
    /// </summary>
    public class ProjectUnitListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 商铺列表查询信息
        /// </summary>
        public ProjectUnitListQueryInfo()
        {
            OrderBy = "UnitID";
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 楼宇Id
        /// </summary>
        public int? BuildingId { get; set; }

        /// <summary>
        /// 楼层Id
        /// </summary>
        public int? FloorId { get; set; }

        /// <summary>
        /// 商铺类型
        /// </summary>
        public int? UnitType { get; set; }
        /// <summary>
        /// 页面跳转来源
        /// </summary>
        public bool? SoureFlag { get; set; }

    }
}
