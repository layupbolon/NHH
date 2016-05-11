using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
    /// <summary>
    /// 租金预估查询信息
    /// </summary>
    public class RentPreviewQueryInfo
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }

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
