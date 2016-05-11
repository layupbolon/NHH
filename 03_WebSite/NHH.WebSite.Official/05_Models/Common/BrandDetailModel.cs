using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    public class BrandDetailModel : BrandInfo
    {
        private CrumbInfo _crumbInfo = new CrumbInfo { };

        /// <summary>
        /// 面包屑信息
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get
            { return _crumbInfo; }
        }

        [Required(ErrorMessage = "请输入品牌过去入驻情况")]
        public string ExistingProject { get; set; }

        public int Revenue { get; set; }

        [Display(Name = "面积需求")]
        [Required(ErrorMessage = "请输入面积需求")]
        //[RegularExpression(@"^[0-9]+(.[0-9]{2})$", ErrorMessage = "{0}必须有2位小数")]
        public decimal AreaUsage { get; set; }

        public int FloorBearing { get; set; }

        public int FloorHeight { get; set; }

        public int ElectricityUsage { get; set; }

        public int WaterUsage { get; set; }

        public int DrainUsage { get; set; }

        public int GasUsage { get; set; }

        public int SmokeUsage { get; set; }

        public int Status { get; set; }

        public DateTime InDate { get; set; }

        public int InUser { get; set; }

        public DateTime EditDate { get; set; }

        public int EditUser { get; set; }

    }
}
