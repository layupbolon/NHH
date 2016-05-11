using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 楼层信息
    /// </summary>
    public class FloorInfo
    {
        /// <summary>
        /// 楼层id
        /// </summary>
        public int FloorID { get; set; }

        /// <summary>
        /// 楼宇id
        /// </summary>
        public int BuildingID { get; set; }

        public int ProjectID { get; set; }

        /// <summary>
        /// 楼层对应的图片链接
        /// </summary>
        public string FloorMapFileName { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public int FloorNumber { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 楼层说明
        /// </summary>
        [StringLength(500, ErrorMessage = "楼层描述长度过长")]
        public string FloorDescription { get; set; }

        /// <summary>
        /// 总计租面积
        /// </summary>
        public decimal TotalRentArea { get; set; }

        /// <summary>
        /// 总计租单元
        /// </summary>
        public int TotalRentUnit { get; set; }

        /// <summary>
        /// 总计租单元数
        /// </summary>
        public decimal ContractRentArea { get; set; }

        /// <summary>
        /// 签约面积
        /// </summary>
        public int ContractRentUnit { get; set; }

        /// <summary>
        /// 铺位状态
        /// </summary>
        public int Status { get; set; }

        public System.DateTime InDate { get; set; }

        public int InUser { get; set; }

        public System.DateTime EditDate { get; set; }

        public int EditUser { get; set; }
    }
}
