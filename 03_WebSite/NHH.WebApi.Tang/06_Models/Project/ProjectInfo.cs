using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 项目信息
    /// </summary>
    public class ProjectInfo
    {
        public int ProjectID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(50, ErrorMessage = "项目名称长度过长")]
        [Required(ErrorMessage = "请填写项目名称")]
        public string ProjectName { get; set; }

        /// <summary>
        /// 区域名
        /// </summary>
        [Required(ErrorMessage = "请选择区域")]
        public string RegionName { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [Required(ErrorMessage = "请选择区域")]
        public int RegionID { get; set; }

        /// <summary>
        ///项目阶段
        /// </summary>
         [Required(ErrorMessage = "请选择阶段")]
        public int Stage { get; set; }
         [Required(ErrorMessage = "请选择阶段")]
        public string ProjectStage { get; set; }

        /// <summary>
        /// 开业时间
        /// </summary>
          [Required(ErrorMessage = "请选择开业时间")]
        public string GrandOpeningDate { get; set; }

        /// <summary>
        /// 管理公司
        /// </summary>
         [Required(ErrorMessage = "请选择管理公司")]
        public string ManageCompanyName { get; set; }

        /// <summary>
        /// 管理公司id
        /// </summary>
         [Required(ErrorMessage = "请选择管理公司")]
        public int ManageCompanyId { get; set; }

        /// <summary>
        /// 业主公司
        /// </summary>
        [Required(ErrorMessage = "请选择业主公司")]
        public int? OwnerCompanyId { get; set; }

        /// <summary>
        /// 业主公司
        /// </summary>
        [Required(ErrorMessage = "请选择业主公司")]
        public string OwnerCompanyName { get; set; }

        /// <summary>
        /// 总建筑面积
        /// </summary>
       [Required(ErrorMessage = "请填写建筑面积")]
       [RegularExpression(@"^\d*(\.\d{1,4})?$", ErrorMessage = "只能输入数字,最多保留4位")]
        public decimal TotalArea { get; set; }

        /// <summary>
        /// 主力店个数
        /// </summary>
        public int MainProjectCount { get; set; }

        /// <summary>
        /// 步行街个数
        /// </summary>
        public int PedestrianStreet { get; set; }

        /// <summary>
        /// 停车位个数
        /// </summary>
       [Required(ErrorMessage = "请填写停车位数量")]
       [RegularExpression(@"^[0-9]*$", ErrorMessage = "只能输入数字")]
        public int ParkingLotNum { get; set; }

        /// <summary>
        /// 计租面积
        /// </summary> 
        public decimal TotalRentArea { get; set; }

        /// <summary>
        /// 广告位个数
        /// </summary>
        [Required(ErrorMessage = "请填写多经点数量")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "只能输入数字")]
        public int AdPointNum { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [Required(ErrorMessage = "请选择城市")]
        public string City { get; set; }


        public string ProvinceAndcity { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
       [Required(ErrorMessage = "请填写地址")]
        public string AddressLine { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [Required(ErrorMessage = "请填写位置")]
        public string AddressInfo { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
       [Required(ErrorMessage = "请填写经度")]
       [RegularExpression(@"^[-]?(\d|([1-9]\d)|(1[0-7]\d)|(180))(\.\d*)?$", ErrorMessage = "只能输入数字,精度保留4位")]
        public decimal Longitude { get; set; }
        
        /// <summary>
        /// 纬度
        /// </summary>
        [Required(ErrorMessage = "请填写纬度")]
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "只能输入数字")]
        [RegularExpression(@"^[-]?(\d|([1-8]\d)|(90))(\.\d*)?$", ErrorMessage = "只能输入数字,精度保留4位")]
        public decimal Latitude { get; set; }

        /// <summary>
        /// 管理公司
        /// </summary>
       //[Required(ErrorMessage = "请选择管理公司")]
        public string GmangerCompany { get; set; }

         //[Required(ErrorMessage = "请选择管理公司")]
        public int GmangerCompanyId { get; set; }
        
        /// <summary>
        /// 邮编
        /// </summary>
        [Required(ErrorMessage = "请选填写邮编")]
        [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "请输入正确的邮编格式")]
        public string ZipCode { get; set; }

        /// <summary>
        /// 地上建筑面积
        /// </summary>
        [Required(ErrorMessage = "请填写地上建筑面积")]
        [RegularExpression(@"^\d*(\.\d{1,4})?$", ErrorMessage = "只能输入数字,最多保留4位")]
        public decimal GroundArea { get; set; }

        /// <summary>
        /// 地下建筑面积
        /// </summary>
         [Required(ErrorMessage = "请填写地下建筑面积")]
         [RegularExpression(@"^\d*(\.\d{1,4})?$", ErrorMessage = "只能输入数字,最多保留4位")]
        public decimal UndergroundArea { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string RenderingFileName { get; set; }

          [Required(ErrorMessage = "请填写多经点数量")]
          [RegularExpression(@"^[0-9]*$", ErrorMessage = "只能输入数字")]
        public int MultiBizPositionNum { get; set; }

        int cityID;
        /// <summary>
        /// 城市id
        /// </summary>
       [Required(ErrorMessage = "请选择城市")]
        public int CityID
        {
            get { return cityID; }
            set { cityID = value; }
        }

        /// <summary>
        /// 省id
        /// </summary>
        [Required(ErrorMessage = "请选择省份")]
        public int ProvinceID { get; set; }

        /// <summary>
        /// 规划定位
        /// </summary>
        public string PlanSummary { get; set; }

        /// <summary>
        /// 规划亮点
        /// </summary>
        public string PlanHighlight { get; set; }

        ///// <summary>
        ///// 分页
        ///// </summary>
        public long Rownum { get; set; }

        public string ProjectSummary { get; set; }

        /// <summary>
        /// 投资公司id
        /// </summary>
        public int? InvestCompanyId { get; set; }

        /// <summary>
        /// 投资公司名称
        /// </summary>
        public string InvestCompanyName { get; set; }
    }
}
