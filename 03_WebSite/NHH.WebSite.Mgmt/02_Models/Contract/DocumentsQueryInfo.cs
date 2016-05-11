using System;
using NHH.Models.Common;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 特殊单据查询信息
    /// </summary>
    public class DocumentsQueryInfo : QueryInfo
    {
        /// <summary>
        /// 单据种类
        /// </summary>
        public int? DocumentType { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 店铺名
        /// </summary>
        public string MerchantStoreName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
