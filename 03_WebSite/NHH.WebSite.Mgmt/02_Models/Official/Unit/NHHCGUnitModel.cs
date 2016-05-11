using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;
using NHH.Models.Official.Common;

namespace NHH.Models.Official.Unit
{
    public class NHHCGUnitModel : BaseModel
    {
        public int UnitID { get; set; }

        [Required(ErrorMessage = "请选择所属的项目")]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "请填写楼别")]
        public string Building { get; set; }
        public string ProjectName { get; set; }

        //[Required(ErrorMessage = "请填写楼层")]
        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "楼层只能输入数字")]
        //public int Floor { get; set; }

        public string FloorRemark { get; set; }

        [Required(ErrorMessage = "请填写铺位号")]
        public string UnitNumber { get; set; }

        [Required(ErrorMessage = "请输入面积")]
        [RegularExpression(@"^\d*(\.\d{1,2})?$", ErrorMessage = "面积只能输入数字,最多保留2位小数")]
        public decimal Area { get; set; }

        //[Required(ErrorMessage = "请输入日租金")]
        //[RegularExpression(@"^\d*(\.\d{1,2})?$", ErrorMessage = "日租金只能输入数字,最多保留2位小数")]
        //public decimal DailyPrice { get; set; }

        public string RentRemark { get; set; }

        [Required(ErrorMessage = "请输入物业费")]
        [RegularExpression(@"^\d*(\.\d{1,2})?$", ErrorMessage = "物业费只能输入数字,最多保留2位小数")]
        public decimal PropertyCosts { get; set; }

        //[Required(ErrorMessage = "请选择业态类型")]
        //public int BizType { get; set; }
        //public string BizTypeName { get; set; }

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

        public List<int> BizTypeList { get; set; }

        public string BizTypes { get; set; }

        public string BizTypeNames { get; set; }
    }
}
