using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.ProjectUnit
{
    /// <summary>
    /// 铺位初始化信息
    /// </summary>
    public class ProjectUnitInitInfo
    {
        private int unitCodeLength = 2;
        private int startNumber = 1;

        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 楼宇ID
        /// </summary>
        public int? BuildingId { get; set; }

        /// <summary>
        /// 楼层ID
        /// </summary>
        public int? FloorId { get; set; }

        /// <summary>
        /// 铺位数量
        /// </summary>
        [Required(ErrorMessage = "请输入铺位数量", AllowEmptyStrings = false)]
        public int UnitNumber { get; set; }

        /// <summary>
        /// 铺位编号起始数
        /// </summary>
        public int StartNumber
        {
            get { return startNumber; }
            set { startNumber = value; }
        }

        /// <summary>
        /// 铺位编号长度
        /// 不包括楼层号
        /// </summary>
        [Range(2, 5)]
        public int UnitCodeLength
        {
            get { return unitCodeLength; }
            set { unitCodeLength = value; }
        }

        /// <summary>
        /// 铺位前缀
        /// </summary>
        public string UnitPrefix { get; set; }

        /// <summary>
        /// 当前操作者
        /// </summary>
        public int UserId { get; set; }
    }
}
