using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 扣点租金
    /// </summary>
    public class PaymentCommissionInfo
    {
        /// <summary>
        /// 扣点租金
        /// </summary>
        /// <param name="expression"></param>
        public PaymentCommissionInfo(string expression)
        {
            //if (string.IsNullOrEmpty(expression))
            //    return;

            //string[] info = expression.Split(new char[] { ',' }, StringSplitOptions.None);
            //int withTax = 0;
            //int.TryParse(info[0], out withTax);
            //WithTax = withTax;

            //decimal amount = 0;
            //decimal rate = 0;
            //int year = 0;

            ////按金额扣点
            //var amountListString = info[1];
            //if (!string.IsNullOrEmpty(amountListString))
            //{
            //    AmountList = new List<PaymentCommissionAmountItem>();
            //    var amountListArr = amountListString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (string amountItemString in amountListArr)
            //    {
            //        var amountItemArr = amountItemString.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            //        decimal.TryParse(amountItemArr[0], out amount);
            //        decimal.TryParse(amountItemArr[1], out rate);
            //        AmountList.Add(new PaymentCommissionAmountItem
            //        {
            //            Amount2 = amount,
            //            Rate = rate,
            //        });
            //    }
            //}
            ////按年限扣点
            //var timeListString = info[2];
            //if (!string.IsNullOrEmpty(timeListString))
            //{
            //    TimeList = new List<PaymentCommissionTimeItem>();
            //    var timeListArr = timeListString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (string timeItemString in timeListArr)
            //    {
            //        var timeItemArr = timeItemString.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            //        if (!int.TryParse(timeItemArr[0], out year))
            //        {
            //            continue;
            //        }

            //        var rateString = timeItemArr[1];

            //        var timeItem = new PaymentCommissionTimeItem
            //        {
            //            YearNum = year,
            //        };

            //        //简单扣点
            //        if (decimal.TryParse(rateString, out rate))
            //        {
            //            timeItem.Rate = rate;
            //            TimeList.Add(timeItem);
            //        }
            //        else if (rateString.IndexOf("]") > 0)
            //        {
            //            //按折扣比例扣点
            //            rateString = rateString.Replace("[", "").Replace("]", "");
            //            timeItem.DiscountRateList = new List<DiscountRateInfo>();
            //            decimal discount = 0;
            //            var discountStringArr = rateString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //            foreach (string discountString in discountStringArr)
            //            {
            //                var discountItem = discountString.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
            //                if (decimal.TryParse(discountItem[0], out discount) &&
            //                    decimal.TryParse(discountItem[1], out rate))
            //                {
            //                    timeItem.DiscountRateList.Add(new DiscountRateInfo
            //                    {
            //                        Discount2 = discount,
            //                        Rate = rate,
            //                    });
            //                }
            //            }
            //            TimeList.Add(timeItem);
            //        }
            //        else if (rateString.IndexOf("}") > 0)
            //        {
            //            //按营业金额扣点
            //            rateString = rateString.Replace("{", "").Replace("}", "");
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 是否含税
        /// 0：税后
        /// 1：税前
        /// </summary>
        public int WithTax { get; set; }

        /// <summary>
        /// 营业年限扣点列表
        /// </summary>
        public List<PaymentCommissionTimeItem> TimeList { get; set; }

        /// <summary>
        /// 营业金额扣点列表
        /// </summary>
        public List<PaymentCommissionAmountItem> AmountList { get; set; }

        /// <summary>
        /// 扣点备注
        /// </summary>
        public string CommissionRemark { get; set; }
    }

    /// <summary>
    /// 租金扣点营业时间项
    /// </summary>
    public class PaymentCommissionTimeItem
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 年度
        /// </summary>
        public int YearNum { get; set; }

        /// <summary>
        /// 扣点比例
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// 折扣扣点列表
        /// </summary>
        public List<DiscountRateInfo> DiscountRateList { get; set; }
    }

    /// <summary>
    /// 租金扣点营业金额项
    /// </summary>
    public class PaymentCommissionAmountItem
    {
        /// <summary>
        /// 金额1
        /// </summary>
        public decimal Amount1 { get; set; }

        /// <summary>
        /// 金额2
        /// </summary>
        public decimal Amount2 { get; set; }

        /// <summary>
        /// 扣点比例
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// 金额备注
        /// </summary>
        public string AmountRemark { get; set; }
    }

    /// <summary>
    /// 折扣扣点信息
    /// </summary>
    public class DiscountRateInfo
    {
        /// <summary>
        /// 折扣1
        /// </summary>
        public decimal Discount1 { get; set; }

        /// <summary>
        /// 折扣2
        /// </summary>
        public decimal Discount2 { get; set; }

        /// <summary>
        /// 扣点比例
        /// </summary>
        public decimal Rate { get; set; }
    }
}
