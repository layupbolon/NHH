using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 项目筹划铺位信息
    /// </summary>
    public class ProjectPlanUnitInfo : ProjectUnitCommonInfo
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        public BizTypeInfo BizType { get; set; }

        /// <summary>
        /// 品类
        /// </summary>
        public BizCategoryInfo BizCategory { get; set; }

        /// <summary>
        /// 铺位类型
        /// </summary>
        public ProjectUnitTypeInfo UnitType { get; set; }

        /// <summary>
        /// 当前用户
        /// </summary>
        public int CurrentUserId { get; set; }
    }
}
