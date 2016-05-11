using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 商户商铺
    /// </summary>
    public class MerchantStoreEntity : BaseEntity
    {
        public int StoreID { get; set; }

        public string StoreName { get; set; }

        public int MerchantID { get; set; }

        public string BrandName { get; set; }

        public int BizTypeID { get; set; }

        public string BizType { get; set; }

        public int BizCategoryID { get; set; }

        public string BizCategory { get; set; }

        public decimal RentArea { get; set; }

        public DateTime RentStartDate { get; set; }

        public DateTime RentEndDate { get; set; }

        public int RentLength { get; set; }

        public int RentContractID { get; set; }

    }
}
