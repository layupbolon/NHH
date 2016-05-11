using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 铺位信息
    /// </summary>
    public class ProjectUnitInfo : ProjectUnitCommonInfo
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 楼宇ID
        /// </summary>
        public int BuildingId { get; set; }

        /// <summary>
        /// 楼层ID
        /// </summary>
        public int FloorId { get; set; }

        /// <summary>
        /// 签约状态
        /// </summary>
        public int ContractStatus { get; set; }

        /// <summary>
        /// 商铺类型ID
        /// </summary>
        public int UnitTypeId { get; set; }

        /// <summary>
        /// 铺位类型
        /// </summary>
        public string UnitTypeName { get; set; }

        /// <summary>
        /// 铺位状态
        /// </summary>
        public string UnitStatusName { get; set; }

        /// <summary>
        /// 商铺平面图
        /// </summary>
        public string UnitMapFileName { get; set; }

        /// <summary>
        /// 平面坐标
        /// </summary>
        public string FloorMapLocation { get; set; }

        public int? FloorMapX { get; set; }

        public int? FloorMapY { get; set; }

        public string FloorMapFileName { get; set; }

        public int UserId { get; set; }
    }
}
