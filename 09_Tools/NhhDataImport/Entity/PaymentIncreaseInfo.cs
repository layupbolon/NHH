using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 费用递增信息
    /// </summary>
    public class PaymentIncreaseInfo
    {
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
        /// 月租金
        /// </summary>
        public decimal PaymentMonthlyAmount { get; set; }
        
        /// <summary>
        /// 递增值
        /// </summary>
        public decimal IncreaseValue { get; set; }
    }
}
