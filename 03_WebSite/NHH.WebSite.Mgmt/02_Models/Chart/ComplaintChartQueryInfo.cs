using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Chart
{
    /// <summary>
    /// 投诉图表查询信息
    /// </summary>
    public class ComplaintChartQueryInfo
    {
        private DateTime _startTime = DateTime.Now.AddMonths(-2);
        private DateTime _endTime = DateTime.Now;
        private int _timeType = 1;


        /// <summary>
        /// 时间类型
        /// </summary>
        public int TimeType
        {
            get { return _timeType; }
            set { _timeType = value; }
        }

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
