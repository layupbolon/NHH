using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    /// <summary>
    /// 列表查询条件
    /// </summary>
    public class SysFunctionListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 功能模块名称
        /// </summary>
        public string FunctionName { get; set; }
    }
}
