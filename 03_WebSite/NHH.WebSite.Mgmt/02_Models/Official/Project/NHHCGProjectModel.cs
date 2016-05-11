using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;
using NHH.Models.Official.Common;

namespace NHH.Models.Official.Project
{
    public class NHHCGProjectModel : BaseModel
    {
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "请填写项目名称")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "请填写项目简称")]
        public string BriefName { get; set; }

        [Required(ErrorMessage = "请填写特色介绍")]
        public string Feature { get; set; }

        [Required(ErrorMessage = "请填写开业时间")]
        public DateTime OpeningDate { get; set; }

        [Required(ErrorMessage = "请填写商业运营面积")]
        [RegularExpression(@"^\d*(\.\d{1,2})?$", ErrorMessage = "商业运营面积只能输入数字,最多保留2位小数")]
        public decimal OperatingArea { get; set; }

        [Required(ErrorMessage = "请填写商业建筑面积")]
        [RegularExpression(@"^\d*(\.\d{1,2})?$", ErrorMessage = "商业建筑面积只能输入数字,最多保留2位小数")]
        public decimal GrossArea { get; set; }

        [Required(ErrorMessage = "请填写商圈人口")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "商圈人口只能输入数字")]
        public int Population { get; set; }

        [Required(ErrorMessage = "请选择商业类型")]
        public int BusinessType { get; set; }

        public string BusinessTypeName
        {
            get
            {
                switch (BusinessType)
                {
                    case 1:
                        return "购物中心";
                    default:
                        return "其它";
                }
            }
        }

        [Required(ErrorMessage = "请填写商圈位置")]
        public string Position { get; set; }

        [Required(ErrorMessage = "请填写月人流量")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "月人流量只能输入数字")]
        public int MonthlyHumanTraffic { get; set; }

        [Required(ErrorMessage = "请填写公共交通")]
        public string PublicTransport { get; set; }

        [Required(ErrorMessage = "请填写咨询电话")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "请填写项目介绍")]
        public string Introduce { get; set; }

        [RegularExpression(@"^\d*(\.\d{1,6})?$", ErrorMessage = "经度只能输入数字,最多保留6位小数")]
        public decimal? Longitude { get; set; }

        [RegularExpression(@"^\d*(\.\d{1,6})?$", ErrorMessage = "纬度只能输入数字,最多保留6位小数")]
        public decimal? Latitude { get; set; }
        public int Status { get; set; }
        public DateTime InDate { get; set; }
        public int InUser { get; set; }

        public List<NHHCGPicModel> MerchantLogoList { get; set; }

        public List<NHHCGPicModel> ProjectPicList { get; set; } 
    }
}
