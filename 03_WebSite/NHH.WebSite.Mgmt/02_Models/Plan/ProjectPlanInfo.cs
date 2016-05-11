using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 招商计划信息
    /// </summary>
    public class ProjectPlanInfo
    {
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
        /// 计租面积
        /// </summary>
        public decimal UnitArea { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string UnitType { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        public string BizType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string UnitStatus { get; set; }

        /// <summary>
        /// 招商部门
        /// </summary>
        public string LeasingDepartment { get; set; }

        /// <summary>
        /// 招商负责人
        /// </summary>
        public string LeasingPerson { get; set; }

        /// <summary>
        /// 平面图位置
        /// </summary>
        public string UnitMapFileName { get; set; }

        /// <summary>
        /// 当前用户
        /// </summary>
        public int CurrentUserId { get; set; }

        /// <summary>
        /// 标准租金
        /// </summary>
        public decimal StandardRent { get; set; }

        /// <summary>
        /// 标准物业费
        /// </summary>
        public decimal StandardMgmtFee { get; set; }

        /// <summary>
        /// 年总收益
        /// </summary>
        public decimal YearTotalAmount
        {
            get
            {
                return StandardRent * UnitArea * 360 + StandardMgmtFee * UnitArea * 360;
            }
        }
    }
}
