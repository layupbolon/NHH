using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
    /// <summary>
    /// KPI报表
    /// </summary>
    public class KpiReportModel : BaseModel
    {
        /// <summary>
        /// 查询条件
        /// </summary>
        public KpiReportQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 统计信息
        /// </summary>
        public KpiReportCountInfo CountInfo { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<KpiReportItem> Data { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }


}
