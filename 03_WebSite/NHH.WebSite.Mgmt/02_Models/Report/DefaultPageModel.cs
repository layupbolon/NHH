using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
    /// <summary>
    /// 首页Model
    /// </summary>
    public class DefaultPageModel : BaseModel
    {
        /// <summary>
        /// 统计信息列表
        /// </summary>
        public List<KpiReportCountInfo> CountInfoList { get; set; }
    }
}
