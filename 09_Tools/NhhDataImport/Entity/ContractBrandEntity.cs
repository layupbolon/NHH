using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 合同品牌
    /// </summary>
    public class ContractBrandEntity : BaseEntity
    {
        public int ContractBrandID { get; set; }

        public int ContractID { get; set; }

        public int MerchantBrandID { get; set; }
    }
}
