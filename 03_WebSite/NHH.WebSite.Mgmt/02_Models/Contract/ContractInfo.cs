using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 租约信息
    /// </summary>
    public class ContractInfo
    {
        /// <summary>
        /// 合同ID
        /// </summary>
        public int ContractID { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractCode { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public int ContractStatus { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public string ContractStatusName { get; set; }        

        /// <summary>
        /// 租金单价
        /// </summary>
        public decimal ContractUnitRent { get; set; }

        /// <summary>
        /// 物业费单价
        /// </summary>
        public decimal ContractMgmtFee { get; set; }

        /// <summary>
        /// 月租金
        /// </summary>
        public decimal MonthlyRent { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantID { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime InDate { get; set; }

        /// <summary>
        /// 合同生效日期
        /// </summary>
        public DateTime ContractStartDate { get; set; }

        /// <summary>
        /// 合同结束时间
        /// </summary>
        public DateTime ContractEndDate { get; set; }

        /// <summary>
        /// 到期天数
        /// </summary>
        public string ExpireDays
        {
            get
            {
                var days = (int)(ContractEndDate - DateTime.Now).TotalDays;
                if (days > 365)
                {
                    return string.Format("{0}年零{1}天", days / 365, days % 365);
                }
                if (days > 30)
                {
                    return string.Format("{0}月零{1}天", days / 30, days % 30);
                }
                return string.Format("{0}天", days);
            }
        }

        /// <summary>
        /// 合同总天数
        /// </summary>
        public int ContractLength { get; set; }

        /// <summary>
        /// 进场日期
        /// </summary>
        public DateTime AccessDate { get; set; }

        /// <summary>
        /// 装修期限（天）
        /// </summary>
        public int DecorationLength { get; set; }

        /// <summary>
        /// 装修开始时间
        /// </summary>
        public DateTime DecorationStartDate { get; set; }

        /// <summary>
        /// 装修结束时间
        /// </summary>
        public DateTime DecorationEndDate { get; set; }

        /// <summary>
        /// 停车位数量
        /// </summary>
        public int ParkingLotNum { get; set; }

        /// <summary>
        /// 广告位数量
        /// </summary>
        public int AdPointNum{get; set;}
        /// <summary>
        /// 免租金期限（天）
        /// </summary>
        public int RentFreeLength { get; set; }

        /// <summary>
        /// 免租金开始时间
        /// </summary>
        public DateTime RentFreeStartDate { get; set; }

        /// <summary>
        /// 免租金结束时间
        /// </summary>
        public DateTime RentFreeEndDate { get; set; }

        /// <summary>
        /// 装修管理费
        /// </summary>
        public decimal DecorationMgmtFee { get; set; }

        /// <summary>
        /// 装修保证金
        /// </summary>
        public decimal DecorationBond { get; set; }

        /// <summary>
        /// 履约保证金
        /// </summary>
        public decimal BidBond { get; set; }

        /// <summary>
        /// 质量保证金
        /// </summary>
        public decimal QualityBond { get; set; }

        /// <summary>
        /// 管理保证金
        /// </summary>
        public decimal ManageBond { get; set; }

        /// <summary>
        /// 签约人姓名
        /// </summary>
        public string SignerName { get; set; }

        /// <summary>
        /// 签约人身份证号
        /// </summary>
        public string SignerIDNumber { get; set; }

        /// <summary>
        /// 签约人电话
        /// </summary>
        public string SignerPhone { get; set; }

        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 经办人电话
        /// </summary>
        public string OperatorPhone { get; set; }

        /// <summary>
        /// 广告位备注
        /// </summary>
        public string AdPointMemo { get; set; }

        /// <summary>
        /// 停车位备注
        /// </summary>
        public string ParkingLotMemo { get; set; }

        /// <summary>
        /// 交付标准
        /// </summary>
        public string ConditionText { get; set; }

        /// <summary>
        /// 铺位信息
        /// </summary>
        public ContractUnitInfo UnitInfo { get; set; }
    }
}
