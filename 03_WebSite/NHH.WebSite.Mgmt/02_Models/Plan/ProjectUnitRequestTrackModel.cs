using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 铺位租决跟踪
    /// </summary>
    public class ProjectUnitRequestTrackModel : BaseModel
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
        /// 总租决面积
        /// </summary>
        public decimal TotalRequestArea { get; set; }

        /// <summary>
        /// 总租决租金
        /// </summary>
        public decimal TotalRequestRent { get; set; }

        /// <summary>
        /// 总租决铺位数
        /// </summary>
        public int TotalRequestUnit { get; set; }

        /// <summary>
        /// 铺位类型
        /// </summary>
        public List<ProjectUnitRequestTrackItem> UnitTypeList { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        public List<ProjectUnitRequestTrackItem> BizTypeList { get; set; }
    }

    /// <summary>
    /// 铺位租决跟踪项
    /// </summary>
    public class ProjectUnitRequestTrackItem
    {
        /// <summary>
        /// 跟踪类型
        /// </summary>
        public int TrackType { get; set; }

        /// <summary>
        /// 跟踪类型名称
        /// </summary>
        public string TrackTypeName { get; set; }

        /// <summary>
        /// 铺位数
        /// </summary>
        public int UnitNum { get; set; }

        /// <summary>
        /// 租决铺位数
        /// </summary>
        public int RequestUnitNum { get; set; }

        /// <summary>
        /// 铺位数占比
        /// </summary>
        public decimal UnitNumProp { get; set; }

        /// <summary>
        /// 面积
        /// </summary>
        public decimal Area { get; set; }

        /// <summary>
        /// 租决面积
        /// </summary>
        public decimal RequestArea { get; set; }

        /// <summary>
        /// 面积占比
        /// </summary>
        public decimal AreaProp { get; set; }

        /// <summary>
        /// 租金
        /// </summary>
        public decimal Rent { get; set; }

        /// <summary>
        /// 租决租金
        /// </summary>
        public decimal RequestRent { get; set; }

        /// <summary>
        /// 租金点比
        /// </summary>
        public decimal RentProp { get; set; }
    }
}
