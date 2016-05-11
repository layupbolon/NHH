using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
    /// <summary>
    /// KPI报表数据Item
    /// </summary>
    public class KpiReportItem
    {
        public int BuildingID { get; set; }

        /// <summary>
        /// 楼宇
        /// </summary>
        public string BuildingName { get; set; }

        public int FloorID { get; set; }

        public int FloorNumber { get; set; }

        public string UnitNumber { get; set; }

        public int? BizTypeID { get; set; }

        /// <summary>
        /// 业态
        /// </summary>
        public string BizTypeName { get; set; }        

        public int? UnitTypeID { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string UnitTypeName { get; set; }

        /// <summary>
        /// 签约面积
        /// </summary>
        public decimal? UnitArea { get; set; }

        public int? BizCategoryID { get; set; }

        /// <summary>
        /// 经营品类
        /// </summary>
        public string BizCategoryName { get; set; }

        /// <summary>
        /// 合同ID
        /// </summary>
        public int ContractID { get; set; }

        /// <summary>
        /// 租期
        /// </summary>
        public int? ContractLength { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime? ContractEndDate { get; set; }

        /// <summary>
        /// 租金单价
        /// </summary>
        public decimal? ContractUnitRent { get; set; }

        /// <summary>
        /// 物业费单价
        /// </summary>
        public decimal? ContractMgmtFee { get; set; }

        public string RentMode { get; set; }

        /// <summary>
        /// 质保金
        /// </summary>
        public decimal? QualityBond { get; set; }

        /// <summary>
        /// 停车位
        /// </summary>
        public int? ParkingLotNum { get; set; }

        /// <summary>
        /// 广告位
        /// </summary>
        public int? AdPointNum { get; set; }

        /// <summary>
        /// 租押方式
        /// </summary>
        public string DepositMode { get; set; }

        /// <summary>
        /// 租金递增方式
        /// </summary>
        public string IncreaseExpression { get; set; }

        /// <summary>
        /// 品牌名称列表
        /// </summary>
        public string BrandNameList { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public string FloorName
        {
            get
            {
                string strFormat = FloorNumber < 1 ? "B{0}" : "{0}F";
                return string.Format(strFormat, (int)(Math.Abs(FloorNumber)));
            }
        }

        /// <summary>
        /// 月租金
        /// </summary>
        public decimal MonthlyRent
        {
            get
            {
                if (!ContractUnitRent.HasValue || !UnitArea.HasValue)
                    return 0M;
                return ContractUnitRent.Value * UnitArea.Value * 30M;
            }
        }

        /// <summary>
        /// 月管理费
        /// </summary>
        public decimal MonthlyMgmt
        {
            get
            {
                if (!ContractMgmtFee.HasValue || !UnitArea.HasValue)
                    return 0M;
                return ContractMgmtFee.Value * UnitArea.Value * 30M;
            }
        }
    }

    public class KpiReportItemData
    {
        /// <summary>
        /// 支付费用科目
        /// </summary>
        public int? PaymentItemID { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public int? ContractID { get; set; }

        /// <summary>
        /// 租赁方式
        /// </summary>
        public int? PaymentTermsType { get; set; }

        /// <summary>
        /// 支付帐期
        /// </summary>
        public int? PaymentPeriod { get; set; }

        /// <summary>
        /// 押月数
        /// </summary>
        public int? DepositMonthly { get; set; }

        /// <summary>
        /// 月付款
        /// </summary>
        public decimal? PaymentMonthlyAmount { get; set; }

        /// <summary>
        /// 日付款
        /// </summary>
        public decimal? PaymentDailyAmount { get; set; }

        /// <summary>
        /// 收租方式
        /// </summary>
        public string PaymentTermsTypeString
        {
            get
            {
                if (!PaymentTermsType.HasValue)
                    return string.Empty;

                switch (PaymentTermsType.Value)
                {
                    case 1:
                        return "租金";
                    case 2:
                        return "扣点";
                    case 3:
                        return "租金与扣点两者取高";
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 租押方式
        /// </summary>
        public string DepositMode
        {
            get
            {
                if (!DepositMonthly.HasValue || !PaymentPeriod.HasValue)
                    return string.Empty;
                return string.Format("押{0}付{1}", DepositMonthly.Value, PaymentPeriod.Value);
            }
        }

        /// <summary>
        /// 递增方式
        /// </summary>
        public string IncreaseExpression { get; set; }

        /// <summary>
        /// 扣点方式
        /// </summary>
        public string CommissionExpression
        {
            get;
            set;
        }
    }
}
