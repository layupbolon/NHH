using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.ProjectUnit
{
    /// <summary>
    /// 铺位调整列表查询信息
    /// </summary>
    public class ProjectUnitAdjustListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 铺位调整列表查询信息
        /// </summary>
        public ProjectUnitAdjustListQueryInfo()
        {
            OrderBy = "UnitID";
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 调整类型
        /// </summary>
        public int? AdjustType { get; set; }

        /// <summary>
        /// 楼宇
        /// </summary>
        public int? BuildingId { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public int? FloorId { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        public int? BizType { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int? UnitType { get; set; }

        /// <summary>
        /// 铺位编号
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
