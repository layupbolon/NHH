using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
    /// <summary>
    /// 投诉报表Model
    /// </summary>
    public class ComplaintReportModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public ComplaintReportQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<ComplaintReportItem> Data { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
