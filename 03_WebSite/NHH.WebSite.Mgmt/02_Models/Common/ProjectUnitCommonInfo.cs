using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 商铺公共信息
    /// </summary>
    public class ProjectUnitCommonInfo
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 楼宇名称
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 商铺系统编号
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// 铺位状态
        /// </summary>
        public int UnitStatus { get; set; }

        /// <summary>
        /// 商铺编号
        /// </summary>
        [Required(ErrorMessage = "请输入商铺编号")]
        public string UnitNumber { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        [Display(Name = "计租面积")]
        [Required(ErrorMessage = "请输入计租面积")]
        [RegularExpression(@"^[0-9]+(.[0-9]{1,2})?$", ErrorMessage = "{0}数值应为小数点后两位")]
        [Range(typeof(decimal), "0.00", "999999999.00", ErrorMessage = "{0}长度太长")]
        public decimal UnitArea { get; set; }

    }
}
