using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 查询基本信息
    /// </summary>
    public class QueryInfo
    {
        /// <summary>
        /// 当前分页
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string OrderBy { get; set; }
    }
}
