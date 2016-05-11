using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 铺位租决列表查询信息
    /// </summary>
    public class ProjectUnitRequestListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 铺位租决列表查询信息
        /// </summary>
        public ProjectUnitRequestListQueryInfo()
        {
            OrderBy = "UnitID";
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 楼宇ID
        /// </summary>
        public int? BuildingId { get; set; }

        /// <summary>
        /// 楼宇名称
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// 楼层ID
        /// </summary>
        public int? FloorId { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 铺位类型
        /// </summary>
        public int? UnitType { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        public int? BizType { get; set; }

        /// <summary>
        /// 租决状态
        /// </summary>
        public int? RequestStatus { get; set; }
    }
}
