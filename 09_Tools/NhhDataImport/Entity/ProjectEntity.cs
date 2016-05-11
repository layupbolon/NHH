using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 项目
    /// </summary>
    public class ProjectEntity : BaseEntity
    {
        public string ProjectName { get; set; }
        public int ProjectID { get; set; }
        public string ProjectSummary { get; set; }
        public int RegionID { get; set; }
        public int ProvinceID { get; set; }
        public int CityID { get; set; }
        public string AddressLine { get; set; }
        public string AddressInfo { get; set; }
        public string Zipcode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? GroundArea { get; set; }
        public decimal? UndergroundArea { get; set; }
        public decimal? TotalArea { get; set; }
        public string RenderingFileName { get; set; }
        public string PlanSummary { get; set; }
        public string PlanHighlight { get; set; }
        public int? ParkingLotNum { get; set; }
        public int? AdPointNum { get; set; }
        public int? MultiBizPositionNum { get; set; }
        public DateTime? GrandOpeningDate { get; set; }
        public int Stage { get; set; }
        public int OwnerCompanyID { get; set; }
        public int? InvestCompanyID { get; set; }
        public int ManageCompanyID { get; set; }
        public int? BuildingNum { get; set; }

        public string Province { get; set; }
        public string City { get; set; }
        public string Region { get; set; }

        public string StageStr { get; set; }
        public string OwnerCompany {get; set; }
        public string InvestCompany { get; set; }
        public string ManageCompany { get; set; }
        public string ProjectCode { get; set; }
        public string BriefName { get; set; }

    }
}
