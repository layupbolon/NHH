using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    /// <summary>
    /// 功能模块列表
    /// </summary>
    public class SysFunctionListModel : BaseModel
    {
        /// <summary>
        /// 列表信息
        /// </summary>
        public List<SysFunctionInfo> FunctionList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

        /// <summary>
        /// 列表查询条件
        /// </summary>
        public SysFunctionListQueryInfo QueryInfo { get; set; }
    }
}
