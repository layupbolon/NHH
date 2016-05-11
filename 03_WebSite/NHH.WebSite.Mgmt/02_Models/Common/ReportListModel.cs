using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 报表列表
    /// </summary>
    public class ReportListModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public ReportListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 报表列表
        /// </summary>
        public List<ReportInfo> ReportList { get; set; }

        /// <summary>
        /// 分页
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
