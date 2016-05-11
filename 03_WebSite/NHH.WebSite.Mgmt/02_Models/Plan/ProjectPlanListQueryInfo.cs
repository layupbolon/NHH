using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 招商计划查询信息
    /// </summary>
    public class ProjectPlanListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 招商计划查询信息
        /// </summary>
        public ProjectPlanListQueryInfo()
        {
            OrderBy = "FloorId";
        }

        /// <summary>
        /// 项目Id
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
        /// 楼层Id
        /// </summary>
        public int? FloorId { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public int? BatchId { get; set; }

        /// <summary>
        /// 商铺类型
        /// </summary>
        public int? UnitType { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        public int? BizType { get; set; }

        /// <summary>
        /// 商铺状态
        /// </summary>
        public int? UnitStatus { get; set; }
    }
}
