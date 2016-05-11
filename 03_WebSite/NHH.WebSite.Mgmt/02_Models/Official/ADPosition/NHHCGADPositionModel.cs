using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;
using NHH.Models.Official.Common;

namespace NHH.Models.Official.ADPosition
{
    public class NHHCGADPositionModel : BaseModel
    {
        public int ADPositionID { get; set; }

        [Required(ErrorMessage = "请选择所属的项目")]
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "请填写楼别")]
        public string Building { get; set; }

        [Required(ErrorMessage = "请选择区域")]
        public int Region { get; set; }

        public string RegionName { get; set; }

        //[Required(ErrorMessage = "请填写楼层")]
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "楼层只能输入数字")]
        //public int FloorID { get; set; }
        
        public string FloorRemark { get; set; }

        [Required(ErrorMessage = "请填写广告位编号")]
        public string ADPositionNumber { get; set; }

        [Required(ErrorMessage = "请选择广告位类型")]
        public int ADType { get; set; }
        public string ADTypeName { get; set; }

        [Required(ErrorMessage = "请填写规格")]
        public string Size { get; set; }

        //[Required(ErrorMessage = "请输入日租金")]
        //[RegularExpression(@"^\d*(\.\d{1,2})?$", ErrorMessage = "日租金只能输入数字,最多保留2位小数")]
        //public decimal DailyPrice { get; set; }

        public string RentRemark { get; set; }

        [Required(ErrorMessage = "请输入位置描述")]
        public string Position { get; set; }

        [Required(ErrorMessage = "请输入联系人")]
        public string Contacts { get; set; }
        public int Status { get; set; }
        public string StatusStr
        {
            get
            {
                switch (Status)
                {
                    case 1:
                        return "待出租";
                    case 2:
                        return "意向洽谈中";
                    case 3:
                        return "已出租";
                    default:
                        return "";
                }
            }
        }
        public DateTime InDate { get; set; }
        public int InUser { get; set; }

        public List<NHHCGPicModel> PicList { get; set; }

        [Required(ErrorMessage = "请输入展位特色")]
        public string Feature { get; set; }

        //[Required(ErrorMessage = "请选择电费承担类型")]
        public int ElectricityType { get; set; }

        public string ElectricityTypeName { get; set; }
    }
}
