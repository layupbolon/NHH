using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    public class ProjectFloorDto
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
        public string FloorPlanFileName { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public decimal FloorNumber { get; set; }
        /// <summary>
        /// 楼层说明
        /// </summary>
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
        /// <summary>
        /// 所属图片集合
        /// </summary>
        public List<string> Imglist { get; set; }
    }
}
