using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
    /// <summary>
    /// KPI报表数据项
    /// </summary>
    public class KpiReportItemInfo
    {
        public int BuildingID { get; set; }
        public string BuildingName { get; set; }

        public int FloorID { get; set; }

        public int FloorNumber { get; set; }
        public string FloorName { get; set; }

        public string UnitNumber { get; set; }

        public int BizTypeID { get; set; }

        public int BizTypeName { get; set; }

        public int BrandId { get; set; }

        public string BrandName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int UnitType { get; set; }

        public string UnitTypeName { get; set; }

        public int BizCategoryID { get; set; }

        public string BizCategoryName { get; set; }
        public DateTime ContractEndDate { get; set; }

        //public decimal RentFree { get; set; }

        //public decimal MgmtFree { get; set; }

        //public string RentMode { get; set; }
        /// <summary>
        /// 月租金
        /// </summary>
        public decimal RPMAmount { get; set; }
        /// <summary>
        /// 月物业费
        /// </summary>
        public decimal PMAmount { get; set; }
        /// <summary>
        /// 租金递增要求
        /// </summary>
        public string rentSytle { get; set; }
        /// <summary>
        /// 租期
        /// </summary>
        public string rentRange { get; set; }
        /// <summary>
        /// 租金，扣点，租金+扣点
        /// </summary>
        public string PaymentTermsTypeName { get; set; }
        /// <summary>
        /// 签约面积
        /// </summary>
        public decimal UnitArea { get; set; }
        /// <summary>
        /// 质量保证金
        /// </summary>
        public decimal QualityBond { get; set; }
        /// <summary>
        /// 停车场
        /// </summary>
        public int ParkingLotNum {get; set; }
        /// <summary>
        /// 多金点
        /// </summary>
        public int AdPointNum { get; set; }
        /// <summary>
        /// 租金，扣点，租金+扣点
        /// </summary>
        public int PaymentTermsType { get; set; }
        /// <summary>
        /// 日结，月结，季结，半年结，年结
        /// </summary>
        public int PaymentPeriod { get; set; }
        /// <summary>
        /// 押金月数
        /// </summary>
        public int DepositMonthly { get; set; }
    }
}
