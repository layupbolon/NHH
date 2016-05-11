using System;
using NHH.Models.Common;

namespace NHH.Models.Operations
{
    public class IntentionQuery : QueryInfo
    {
        /// <summary>
        /// 入驻意向类别
        /// </summary>
        public int? IntentionType { get; set; }
        /// <summary>
        /// 入驻所属项目
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 提交时间起
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 提交时间止
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 状态 -1作废 1未处理 2已通知 3已接单
        /// </summary>
        public int? Status { get; set; }

    }
}
