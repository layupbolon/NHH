using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhComplaintService
{
    /// <summary>
    /// 获取投诉信息
    /// </summary>
    public class ComplaintInfo
    {
        /// <summary>
        /// 投诉Id
        /// </summary>
        public int ComplaintId { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 投诉申请时间
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 投诉状态
        /// </summary>
        public int ComplaintStatus { get; set; }
    }
}
