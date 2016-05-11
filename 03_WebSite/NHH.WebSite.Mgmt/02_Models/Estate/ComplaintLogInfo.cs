using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 投诉日志
    /// </summary>
    public class ComplaintLogInfo
    {
        /// <summary>
        /// 日志Id
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// 投诉Id
        /// </summary>
        public int ComplaintId { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int LogUserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string LogUserName { get; set; }

        /// <summary>
        /// 日志详细内容
        /// </summary>
        public string LogText { get; set; }
    }
}
