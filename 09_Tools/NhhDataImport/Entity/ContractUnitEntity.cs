using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 合同铺位
    /// </summary>
    public class ContractUnitEntity : BaseEntity
    {
        public int ContractUnitID { get; set; }

        public int ContractID { get; set; }

        public int UnitID { get; set; }

        public decimal? UnitAvgAera { get; set; }
    }
}
