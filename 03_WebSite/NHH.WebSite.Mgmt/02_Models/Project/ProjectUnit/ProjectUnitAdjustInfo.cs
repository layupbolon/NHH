using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.ProjectUnit
{
    /// <summary>
    /// 铺位调整信息
    /// </summary>
    public class ProjectUnitAdjustInfo
    {
        /// <summary>
        /// 调整ID
        /// </summary>
        public int AdjustId { get; set; }

        /// <summary>
        /// 调整类型
        /// </summary>
        public DateTime AdjustDate { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 楼宇名称
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 铺位ID
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// 铺位编号
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// 铺位面积
        /// </summary>
        public decimal UnitArea { get; set; }

        /// <summary>
        /// 调整类型
        /// </summary>
        public string AdjustType { get; set; }

        /// <summary>
        /// 原铺位ID
        /// </summary>
        public int OriginalUnitID { get; set; }

        /// <summary>
        /// 原铺位编号
        /// </summary>
        public string OriginalUnitNumber { get; set; }

        /// <summary>
        /// 原铺位面积
        /// </summary>
        public decimal OriginalUnitArea { get; set; }
    }
}
