using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 商铺信息
    /// </summary>
    public class ProjectUnitInfoModel
    {
        public int UnitPlanId { get; set; }

        /// <summary>
        /// 铺位ID
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// 经营业态
        /// </summary>
        public BizTypeInfo BizType { get; set; }

        /// <summary>
        /// 经营品类
        /// </summary>
        public BizCategoryInfo BizCategory { get; set; }

        /// <summary>
        /// 商铺类型
        /// </summary>
        public ProjectUnitTypeInfo UnitType { get; set; }

        /// <summary>
        /// 租决租金标准
        /// </summary>
        public decimal StandardRent { get; set; }

        /// <summary>
        /// 建议租金标准
        /// </summary>
        public decimal RecommendedRent { get; set; }

        /// <summary>
        /// 租赁报价
        /// </summary>
        public decimal QuotationRent { get; set; }

        /// <summary>
        /// 租决物业费标准
        /// </summary>
        public decimal StandardMgmtFee { get; set; }

        /// <summary>
        /// 租赁年限上限（月）
        /// </summary>
        public int RentLengthUpper { get; set; }

        /// <summary>
        /// 租赁年限下限（月）
        /// </summary>
        public int RentLengthBottom { get; set; }

        /// <summary>
        /// 租押方式要求（押几个月）
        /// </summary>
        public int DepositMonthly { get; set; }

        /// <summary>
        /// 支付帐期月数
        /// </summary>
        public int PaymentPeriod { get; set; }

        /// <summary>
        /// 租押方式
        /// </summary>
        public string RentChargeMode { get; set; }

        /// <summary>
        /// 租押方式
        /// </summary>
        public string RentChargeModeString { get; set; }

        /// <summary>
        /// 租金递增方式要求
        /// </summary>
        public string IncreaseTypeString { get; set; }

        /// <summary>
        /// 租金递增类型，递增或递减
        /// </summary>
        public int IncreaseType { get; set; }

        /// <summary>
        /// 租金递增值类型，￥ OR %
        /// </summary>
        public int IncreaseAmountType { get; set; }

        /// <summary>
        /// 租金递增值
        /// </summary>
        public decimal IncreaseAmount { get; set; }

        /// <summary>
        /// 租金递增起始年数
        /// </summary>
        public int IncreaseStartYears { get; set; }

        /// <summary>
        /// 租金递增间隔年数
        /// </summary>
        public int IncreaseStepYears { get; set; }

        /// <summary>
        /// 商铺质保金要求
        /// </summary>
        public decimal BidBond { get; set; }

        /// <summary>
        /// 物业保证金
        /// </summary>
        public decimal ManageBond { get; set; }

        /// <summary>
        /// 免租期上限
        /// </summary>
        public int RentFreeLength { get; set; }

        /// <summary>
        /// 装修期上限
        /// </summary>
        public int DecorationLength { get; set; }

        /// <summary>
        /// 装修管理费要求
        /// </summary>
        public decimal DecorationMgmtFee { get; set; }

        /// <summary>
        /// 装修保证金要求
        /// </summary>
        public decimal DecorationBond { get; set; }

        /// <summary>
        /// 质量保证金
        /// </summary>
        public decimal QualityBond { get; set; }

        /// <summary>
        /// 免费停车位上限
        /// </summary>
        public int ParkingLotNum { get; set; }

        /// <summary>
        /// 免费广告位上限
        /// </summary>
        public int AdPointNum { get; set; }

        /// <summary>
        /// 交付标准
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// 当前用户
        /// </summary>
        public int UserId { get; set; }
    }
}
