using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
    /// <summary>
    /// KPI报表查询信息
    /// </summary>
    public class KpiReportQueryInfo : QueryInfo
    {
        private DateTime _startTime = new DateTime(1900, 1, 1);
        private DateTime _endTime = DateTime.Now;

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId{get;set;}

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        /// <summary>
        /// 楼宇ID
        /// </summary>
        public int? BuildingId { get; set; }

        /// <summary>
        /// 楼层ID
        /// </summary>
        public int? FloorId { get; set; }

        /// <summary>
        /// 业态ID
        /// </summary>
        public int? BizTypeId { get; set; }

        /// <summary>
        /// 经营品类ID
        /// </summary>
        public int? BizCategoryId { get; set; }

        /// <summary>
        /// 品牌级别
        /// </summary>
        public int? BrandLevel { get; set; }

        /// <summary>
        /// 商铺类型
        /// </summary>
        public int? UnitType { get; set; }

        /// <summary>
        /// 面积范围
        /// </summary>
        public decimal? UnitArea { get; set; }
    }
}
