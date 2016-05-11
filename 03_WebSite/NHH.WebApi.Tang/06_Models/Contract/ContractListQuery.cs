using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 合同列表查询信息
    /// </summary>
    public class ContractListQuery 
    {
        /// <summary>
        /// 商户编号
        /// </summary>
        public int MerchantID { get; set; }
        /// <summary>
        /// 合约状态 1待签约 2签约中 3执行中 4已结束
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 合约有效区间开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 合约有效区间结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 排序方式 1到期时间 2状态
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int Size { get; set; }
    }
}
