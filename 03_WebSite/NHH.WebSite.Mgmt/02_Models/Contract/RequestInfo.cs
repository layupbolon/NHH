using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 意向信息
    /// </summary>
    public class RequestInfo
    {
        public int RequestID { get; set; }

        public int MerchantID { get; set; }

        public int BrandID { get; set; }

        public int ProjectID { get; set; }

        public string BuildingIDs { get; set; }

        public string FloorIDs { get; set; }

        public string UnitIDs { get; set; }

        public string SpecialRequest { get; set; }

        public decimal AreaRequest { get; set; }

        public int FloorBearing { get; set; }

        public int FloorHeight { get; set; }

        public int RentLength { get; set; }

        public string RentMethod { get; set; }

        public string PaymentMethod { get; set; }

        public string Condition { get; set; }

        public int DecorationLength { get; set; }

        public int ElectricityUsage { get; set; }

        public int WaterUsage { get; set; }

        public int DrainUsage { get; set; }

        public int GasUsage { get; set; }

        public int SmokeUsage { get; set; }
    }
}
