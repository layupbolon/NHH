using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 年度租金
    /// </summary>
    public class PaymentIncreaseInfo
    {
        /// <summary>
        /// 年
        /// </summary>
        /// <param name="expression"></param>
        public PaymentIncreaseInfo(string expression)
        {
            //if (string.IsNullOrEmpty(expression))
            //    return;

            //IncreaseList = new List<PaymentIncreaseItem>();

            //string[] items = expression.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //int year = 0;
            //decimal amount = 0;
            //foreach (string item in items)
            //{
            //    string[] yearItem = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            //    if (int.TryParse(yearItem[0], out year) &&
            //        decimal.TryParse(yearItem[1], out amount))
            //    {
            //        IncreaseList.Add(new PaymentIncreaseItem
            //        {
            //            YearNum = year,
            //            PaymentMonthlyAmount = amount,
            //        });
            //    }
            //}
        }

        /// <summary>
        /// 递增值类型
        /// 1：百分比
        /// 2：金额
        /// </summary>
        public int IncreaseType { get; set; }

        /// <summary>
        /// 递增列表
        /// </summary>
        public List<PaymentIncreaseItem> IncreaseList { get; set; }
    }

    /// <summary>
    /// 费用递增项
    /// </summary>
    public class PaymentIncreaseItem
    {
        /// <summary>
        /// 年度
        /// </summary>
        public int YearNum { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 月度租金
        /// </summary>
        public decimal PaymentMonthlyAmount { get; set; }
        
        /// <summary>
        /// 递增值
        /// </summary>
        public decimal IncreaseValue { get; set; }
    }
}
