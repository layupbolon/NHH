using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 商铺信息
    /// </summary>
    public class ProjectUnitInfo
    {
        public int UnitId { get; set; }

        /// <summary>
        /// 商铺编号
        /// </summary>
        public string UnitNumber { get;set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set;}

        /// <summary>
        /// 楼层
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 计租面积
        /// </summary>
        public decimal UnitArea { get; set; }

        public string BuildingName { get; set; }

        public string UnitType { get; set; }

        public string UnitStatus { get; set; }

        /// <summary>
        /// 租金
        /// </summary>
        public decimal? Rent { get; set; }

        public string StoreName { get; set; }

        public string BrandName { get; set; }
    }
}
