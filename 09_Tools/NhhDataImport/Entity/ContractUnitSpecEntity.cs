using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 租约交付标准与商户责任
    /// </summary>
    public class ContractUnitSpecEntity : BaseEntity
    {
        public int UnitSpecID { get; set; }

        public int UnitID { get; set; }

        public int ContractID { get; set; }

        public int SpecType { get; set; }

        public string Floor { get; set; }

        public string Ceiling { get; set; }

        public string Wall { get; set; }

        public string Pillar { get; set; }

        public string FloorBearing { get; set; }

        public string WaterSupply { get; set; }

        public string WaterDrain { get; set; }

        public string Door { get; set; }

        public string Logo { get; set; }

        public string ElectricityUsage { get; set; }

        public string FireProtection { get; set; }

        public string Broadcasting { get; set; }

        public string AirCondition { get; set; }

        public string Smoke { get; set; }

        public string Security { get; set; }

        public string Wiring { get; set; }

        public string Water { get; set; }

        public string Gas { get; set; }
    }
}
