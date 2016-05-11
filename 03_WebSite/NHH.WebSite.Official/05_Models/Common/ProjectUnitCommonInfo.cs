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
        private FloorCommonInfo floorInfo = new FloorCommonInfo();
        private ProjectUnitTypeInfo unitType = new ProjectUnitTypeInfo();
        private BizTypeInfo bizType = new BizTypeInfo();
        private ProjectUnitStatusInfo unitStatus = new ProjectUnitStatusInfo();

        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 楼层信息
        /// </summary>
        public FloorCommonInfo FloorInfo
        {
            get { return floorInfo; }
        }

        /// <summary>
        /// 商铺系统编号
        /// </summary>
        public int UnitId { get; set; }

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
        public decimal UnitArea { get; set; }

        /// <summary>
        /// 套内面积
        /// </summary>
        public decimal InternalUnitArea { get; set; }

        /// <summary>
        /// 平面图位置
        /// </summary>
        [Required(ErrorMessage = "请选择平面图位置图")]
        public string FloorMapFileName { get; set; }

        /// <summary>
        /// 平面坐标
        /// </summary>
        public string FloorMapLocation { get; set; }

        /// <summary>
        /// 商铺类型
        /// </summary>
        public ProjectUnitTypeInfo UnitType
        {
            get { return unitType; }
        }

        /// <summary>
        /// 状态信息
        /// </summary>
        public ProjectUnitStatusInfo UnitStatus
        {
            get { return unitStatus; }
        }

        /// <summary>
        /// 业态信息
        /// </summary>
        public BizTypeInfo BizType
        {
            get { return bizType; }
        }
    }
}
