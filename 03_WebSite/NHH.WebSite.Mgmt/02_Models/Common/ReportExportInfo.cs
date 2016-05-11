using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 报表导出信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReportExportInfo<T> : ReportConfigInfo
    {
        /// <summary>
        /// 主体内容
        /// </summary>
        public List<T> Body { get; set; }
    }
}
