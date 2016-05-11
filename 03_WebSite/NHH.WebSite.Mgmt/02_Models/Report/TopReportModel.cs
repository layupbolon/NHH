using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
    /// <summary>
    /// Top10报表Model
    /// </summary>
    public class TopReportModel : BaseModel
    {
        /// <summary>
        /// 查询条件
        /// </summary>
        public TopReportQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<TopReportItem> Data { get; set; }
    }
}
