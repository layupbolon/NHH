using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 楼宇信息
    /// </summary>
    public class BuildingInfo
    {
        public int ProjectID { get; set; }

        [StringLength(50, ErrorMessage = "项目名称长度过长")]
        public string ProjectName { get; set; }

        public int BuildingID { get; set; }

        /// <summary>
        /// 楼宇编码
        /// </summary>
        public string BuildingCode { get; set; }

        /// <summary>
        /// 楼宇名称
        /// </summary>
        [StringLength(50, ErrorMessage = "楼宇名称长度过长")]
        [Required(ErrorMessage = "请输入楼宇名称")]
        public string BuildingName { get; set; }

        [Required(ErrorMessage = "请输入地上楼层数量")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "只能输入数字")]
        public int GroundFloorNum { get; set; }


        [Required(ErrorMessage = "请输入地下楼层数量")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "只能输入数字")]
        public int UndergroundFloorNum { get; set; }

        [Required(ErrorMessage = "请输入地上建筑面积")]
        [RegularExpression(@"^\d*(\.\d{1,2})?$", ErrorMessage = "只能输入数字,最多保留2位")]
        public decimal BuildingGroundArea { get; set; }

        [Required(ErrorMessage = "请输入地下建筑面积")]
        [RegularExpression(@"^\d*(\.\d{1,2})?$", ErrorMessage = "只能输入数字,最多保留2位")]
        public decimal BuildingUndergroundArea { get; set; }

        public decimal TotalConstructionArea { get; set; }

        public decimal TotalRentArea { get; set; }

        public int TotalRentUnit { get; set; }

        public string PlanSummary { get; set; }

        public string PlanHighlight { get; set; }

        public string RenderingFileName { get; set; }

        /// <summary>
        /// 入驻商家
        /// </summary>
        [StringLength(500, ErrorMessage = "入驻商家名称长度过长")]
        public string ContractStore { get; set; }

        public int Status { get; set; }

        public DateTime InDate { get; set; }

        public int InUser { get; set; }

        public DateTime EditDate { get; set; }

        public int EditUser { get; set; }
    }
}
