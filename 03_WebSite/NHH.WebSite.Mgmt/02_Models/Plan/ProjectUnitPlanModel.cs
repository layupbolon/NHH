using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 商铺租决信息
    /// </summary>
    public class ProjectUnitPlanModel
    {
        /// <summary>
        /// 商铺ID
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// 租决租金
        /// </summary>
        public decimal StandardRent { get; set; }

        public int BizTypeId { get; set; }

        /// <summary>
        /// 业态规划
        /// </summary>
        public string BizTypeName { get; set; }
        
        public int UnitTypeId { get; set; }

        /// <summary>
        /// 商铺类型
        /// </summary>
        public string UnitTypeName { get; set; }

        /// <summary>
        /// 是否标杆品牌
        /// </summary>
        public bool IsBenchmarking { get; set; }

        /// <summary>
        /// 招商管理公司
        /// </summary>
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        /// <summary>
        /// 招商管理部门
        /// </summary>
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        /// <summary>
        /// 当前用户
        /// </summary>
        public int UserId { get; set; }
    }
}
