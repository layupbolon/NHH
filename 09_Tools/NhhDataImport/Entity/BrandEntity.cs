using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 品牌
    /// </summary>
    public class BrandEntity : BaseEntity
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string BrandLevel { get; set; }

        public int? BrandLevelID { get; set; }

        public string BrandIcon { get; set; }

        public string BizType { get; set; }

        public int? BizTypeID { get; set; }

        public string BizCategory { get; set; }

        public int? BizCategoryID { get; set; }

        public string ExistingProject { get; set; }

        public int? Revenue { get; set; }

        public decimal? AreaUsage { get; set; }

        public int? FloorBearing { get; set; }

        public int? FloorHeight { get; set; }

        public int? ElectricityUsage { get; set; }

        public int? WaterUsage { get; set; }

        public int? DrainUsage { get; set; }

        public int? GasUsage { get; set; }

        public int? SmokeUsage { get; set; }
    }
}
