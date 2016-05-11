using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.Adpoint
{
    public class AdPointDetailModel : BaseModel
    {
        /// <summary>
        /// 广告位编号
        /// </summary>
        public int AdPointID { get; set; }

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
        ///  楼层名称
        /// </summary>
        public string FloorName { get; set; }

        /// <summary>
        /// 广告位类型
        /// </summary>
        [Required(ErrorMessage = "请选择广告位类型")]
        public int AdPointType { get; set; }

        /// <summary>
        /// 广告媒介
        /// </summary>
        [Required(ErrorMessage = "请选择广告媒介")]
        public int AdPointMedia { get; set; }

        /// <summary>
        /// 广告位号
        /// </summary>
        [Required(ErrorMessage = "请输入广告位号")]
        public string AdPointNumber { get; set; }

        /// <summary>
        /// 位置描述
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 坐标
        /// </summary>
        public string FloorMapLocation { get; set; }

        /// <summary>
        /// 楼层的图片
        /// </summary>
        public string FloorMapFileName { get; set; }

        public int? FloorMapX { get; set; }

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

    }
}
