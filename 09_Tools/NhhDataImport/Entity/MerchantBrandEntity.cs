using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 商户品牌
    /// </summary>
    public class MerchantBrandEntity : BaseEntity
    {
        public int MerchantBrandID { get; set; }

        public int MerchantID { get; set; }

        public int BrandID { get; set; }

        public string BrandName { get; set; }

        public int BrandType { get; set; }

        public string BrandTypeName { get; set; }

        public int Authorized { get; set; }

        public int? AuthorizedFile { get; set; }

        public DateTime? AuthorizedSince { get; set; }

        public DateTime? AuthorizedTo { get; set; }

        public DateTime? OperationSince { get; set; }

        public DateTime? OperationTo { get; set; }

        public int Revenue { get; set; }
    }
}
