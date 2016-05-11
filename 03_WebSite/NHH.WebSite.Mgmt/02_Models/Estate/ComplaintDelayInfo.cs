using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 投诉延迟信息
    /// </summary>
    public class ComplaintDelayInfo
    {
        public int ComplaintId { get; set; }

        /// <summary>
        /// 延迟原因
        /// </summary>
        public string DelayReason { get; set; }

        /// <summary>
        /// 预计完成时间
        /// </summary>
        public DateTime? EstimatedFinishTime { get; set; }

        /// <summary>
        /// 延迟时间
        /// </summary>
        public double? EstimatedHour { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public UserInfo UserInfo { get; set; }
    }
}
