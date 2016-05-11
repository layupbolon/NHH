using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
    /// <summary>
    /// 投诉报表查询信息
    /// </summary>
    public class ComplaintReportQueryInfo : QueryInfo
    {
        private DateTime _startTime = DateTime.Now.AddMonths(-1);
        private DateTime _endTime = DateTime.Now;

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

    }
}
