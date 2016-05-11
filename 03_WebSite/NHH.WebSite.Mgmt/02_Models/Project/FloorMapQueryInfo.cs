using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 楼层平面图查询信息
    /// </summary>
    public class FloorMapQueryInfo
    {
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
        /// 租约状态
        /// </summary>
        public int? ContractStatus { get; set; }

        /// <summary>
        /// 商铺状态
        /// </summary>
        public int? ProjectUnitStatus { get; set; }
    }
}
