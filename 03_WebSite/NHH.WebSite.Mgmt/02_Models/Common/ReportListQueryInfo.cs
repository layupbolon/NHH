using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 报表列表查询信息
    /// </summary>
    public class ReportListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string ReportName { get; set; }
    }
}
