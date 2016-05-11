using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户商铺信息
    /// </summary>
    public class MerchantStoreInfo
    {
        public int StoreID { get; set; }

        public string StoreName { get; set; }

        public int MerchantID { get; set; }

        public string MerchantName { get; set; }

        public string BrandName { get; set; }

        public int BrandID { get; set; }

        public int BizTypeID { get; set; }

        public string BizTypeName { get; set; }

        public int BizCategoryID { get; set; }

        public string BizCategoryName { get; set; }

        public decimal RentArea { get; set; }

        public DateTime RentStartDate { get; set; }

        public DateTime RentEndDate { get; set; }

        public int RentLength { get; set; }

        public int RentContractID { get; set; }

        public string RentContractCode { get; set; }

        /// <summary>
        /// 铺位列表
        /// </summary>
        public string ProjectUnitList { get; set; }

        public int Status { get; set; }

        public System.DateTime InDate { get; set; }

        public int InUser { get; set; }

        public System.DateTime EditDate { get; set; }

        public int EditUser { get; set; }
    }
}
