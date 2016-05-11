using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 报表配置信息
    /// </summary>
    public class ReportConfigInfo
    {
        public int UserId { get; set; }

        public string ReportCode { get; set; }

        public string ReportName { get; set; }

        public List<ReportFieldInfo> FieldList { get; set; }
    }
}
