using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 基础列表页Model
    /// </summary>
    public class BaseListModel<T> : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public virtual QueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 列表
        /// </summary>
        public List<T> List { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
