using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户品牌信息
    /// </summary>
    public class MerchantBrandInfo
    {
        public int MerchantBrandID { get; set; }

        public int MerchantID { get; set; }

        [Required(ErrorMessage = "请选择品牌名称")]
        public int BrandID { get; set; }

        public string BrandName { get; set; }

        public string BrandType { get; set; }

        [Required(ErrorMessage = "请选择经营方式")]
        public int BrandTypeId { get; set; }

        public string BrandLevel { get; set; }

        [Required(ErrorMessage = "请选择品牌级次")]
        public int BrandLevelId { get; set; }

        public string BizType { get; set; }

        public string BizCategory { get; set; }

        public int Authorized { get; set; }

        public int? AuthorizedFile { get; set; }

        public string AuthorizedFilePath { get; set; }

        public string AuthorizedFileName { get; set; }

        [Required(ErrorMessage = "请输入授权期限")]
        public DateTime? AuthorizedSince { get; set; }

        public DateTime? AuthorizedTo { get; set; }

        public DateTime? OperationSince { get; set; }

        public DateTime? OperationTo { get; set; }

        public int? Revenue { get; set; }

        public string MerchantName { get; set; }

        public int InUser { get; set; }
    }
}
