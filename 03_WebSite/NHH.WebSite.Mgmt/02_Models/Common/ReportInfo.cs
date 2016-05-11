using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 报表
    /// </summary>
    public class ReportInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ReportID { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string ReportCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string ReportName { get; set; }

        /// <summary>
        /// 字段列表
        /// </summary>
        public List<ReportFieldInfo> FieldList { get; set; }

        public int InUser { get; set; }
    }
}
