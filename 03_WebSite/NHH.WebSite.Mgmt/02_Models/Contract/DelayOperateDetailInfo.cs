using System;

namespace NHH.Models.Contract
{
    public class DelayOperateDetailInfo : DocumentsInfo
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
        /// 原因
        /// </summary>
        public string Reason { get; set; }
    }
}
