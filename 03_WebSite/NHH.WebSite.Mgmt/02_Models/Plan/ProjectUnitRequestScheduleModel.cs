using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 铺位租决进度
    /// </summary>
    public class ProjectUnitRequestScheduleModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public ProjectUnitRequestListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 总面积
        /// </summary>
        public decimal TotalArea { get; set; }

        /// <summary>
        /// 总租金
        /// </summary>
        public decimal TotalRent { get; set; }

        /// <summary>
        /// 总铺位数
        /// </summary>
        public int TotalUnit { get; set; }

        /// <summary>
        /// 进度数据
        /// </summary>
        public List<ProjectUnitRequestScheduleItem> ScheduleData { get; set; }
    }

    public class ProjectUnitRequestScheduleItem
    {/// <summary>
        /// 
        /// </summary>
        public int RequestStatus { get; set; }

        /// <summary>
        /// 租决状态
        /// </summary>
        public string RequestStatusName { get; set; }

        /// <summary>
        /// 铺位数
        /// </summary>
        public int UnitNum { get; set; }

        /// <summary>
        /// 铺位数占比
        /// </summary>
        public decimal UnitNumProp { get; set; }

        /// <summary>
        /// 面积
        /// </summary>
        public decimal Area { get; set; }

        /// <summary>
        /// 面积占比
        /// </summary>
        public decimal AreaProp { get; set; }

        /// <summary>
        /// 租金
        /// </summary>
        public decimal Rent { get; set; }

        /// <summary>
        /// 租金点比
        /// </summary>
        public decimal RnetProp { get; set; }
    }
}
