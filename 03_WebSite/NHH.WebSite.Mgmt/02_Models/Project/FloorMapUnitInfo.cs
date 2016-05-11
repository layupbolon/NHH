using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 楼层平面图单元信息
    /// </summary>
    public class FloorMapUnitInfo
    {
        public int? FloorMapUnitID { get; set; }

        public int? FloorID { get; set; }

        public int? UnitID { get; set; }

        public string TextPosition { get; set; }

        public string PathLine { get; set; }

        public string PathQuad1 { get; set; }

        public string PathQuad2 { get; set; }

        public string PathQuad3 { get; set; }

        public string PathQuad4 { get; set; }

        public int? Type { get; set; }

        public string Comments { get; set; }

        public string UnitNumber { get; set; }

        public decimal UnitAera { get; set; }

        public int BizTypeID { get; set; }

        public int ContractStatus { get; set; }

        public string ContractStatusName { get; set; }

        public int UnitType { get; set; }

        public string UnitTypeName { get; set; }

        public int ProjectUnitStatus { get; set; }

        public string UnitStatusName { get; set; }

        public string BrandName { get; set; }

        public string ContractStartDate { get; set; }

        public string ContractEndDate { get; set; }

        public decimal? ContractArea { get; set; }

        public decimal? ContractUnitRent { get; set; }

        public decimal? ContractMgmtFee { get; set; }

        public int? DecorationLength { get; set; }

        public DateTime? DecorationEndDate { get; set; }

        public int? ContractLength { get; set; }

        public decimal? QualityBond { get; set; }

        public int? AdPointNum { get; set; }

        public int? ParkingLotNum { get; set; }
    }
}
