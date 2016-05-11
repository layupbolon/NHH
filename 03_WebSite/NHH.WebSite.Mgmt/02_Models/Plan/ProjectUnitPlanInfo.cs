using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 铺位筹划信息
    /// </summary>
    public class ProjectUnitPlanInfo
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 楼宇名称
        /// </summary>
        public string BuindingName { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 批次编号
        /// </summary>
        public string BatchCode { get; set; }

        /// <summary>
        /// 铺位ID
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// 铺位编号
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// 计租面积
        /// </summary>
        public decimal UnitArea { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string UnitTypeName { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        public string BizTypeName { get; set; }

        /// <summary>
        /// 品类
        /// </summary>
        public string BizCategoryName { get; set; }

        /// <summary>
        /// 租金
        /// </summary>
        public decimal StandardRent { get; set; }

        /// <summary>
        /// 物业费
        /// </summary>
        public decimal StandardMgmtFee { get; set; }
    }
}
