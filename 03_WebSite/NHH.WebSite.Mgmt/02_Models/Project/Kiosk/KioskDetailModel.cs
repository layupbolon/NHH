using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.Kiosk
{
    public class KioskDetailModel : BaseModel
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

        /// <summary>
        /// 多经点编号
        /// </summary>
        public int KioskID { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        [Required(ErrorMessage = "请选择项目")]
        public int ProjectID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 楼宇ID
        /// </summary>
        public int BuildingID { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        [Required(ErrorMessage = "请选择楼层")]
        public int FloorId { get; set; }

        /// <summary>
        /// 楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 多经点位号
        /// </summary>
        [Required(ErrorMessage = "请输入多经点位号")]
        public string KioskNumber { get; set; }

        /// <summary>
        /// 位置描述
        /// </summary>
        public string Location { get; set; }


        /// <summary>
        /// 坐标
        /// </summary>
        public string FloorMapLocation { get; set; }

        /// <summary>
        /// 平面图X
        /// </summary>
        public int? FloorMapX { get; set; }

        /// <summary>
        /// 平面图Y
        /// </summary>
        public int? FloorMapY { get; set; }

        /// <summary>
        /// 计租面积
        /// </summary>
        [Display(Name = "计租面积")]
        [RegularExpression(@"^[0-9]+(.[0-9]{2}$)", ErrorMessage = "{0}必须有2位小数")]
        public decimal? Area { get; set; }

        /// <summary>
        /// 经营业态
        /// </summary>
        public int? BizTypeID { get; set; }

        /// <summary>
        /// 使用率
        /// </summary>
        public decimal? OccupyRate { get; set; }

        /// <summary>
        /// 租金
        /// </summary>
        public decimal Rent { get; set; }

        /// <summary>
        /// 签约状态
        /// </summary>
        public int ContractStatus { get; set; }

        /// <summary>
        /// 合同到期时间
        /// </summary>
        public DateTime? ContractEnd { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime InDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int InUser { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
        public DateTime? EditDate { get; set; }

        /// <summary>
        /// 编辑人
        /// </summary>
        public int EditUser { get; set; }

        /// <summary>
        /// 楼层图片
        /// </summary>
        public string FloorMapFileName { get; set; }

        /// <summary>
        /// 多经点位类型
        /// </summary>
        [Required(ErrorMessage = "请选择多经点位类型")]
        public int KioskType { get; set; }

    }
}
