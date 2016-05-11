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
        private string _orderMode = "desc";

        /// <summary>
        /// 当前分页
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// 是否启用分页
        /// </summary>
        public int? Paging { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string OrderMode
        {
            get { return _orderMode; }
            set { _orderMode = value; }
        }

        /// <summary>
        /// 排序表达式
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string OrderExpression
        {
            get
            {
                if (string.IsNullOrEmpty(OrderBy) || OrderBy.Length == 0)
                    return string.Empty;
                return string.Format("{0} {1}", OrderBy, OrderMode.ToUpper());
            }
        }

        /// <summary>
        /// 当前系统用户Id
        /// </summary>
        public int CurrentUserId { get; set; }

        /// <summary>
        /// 报表Code
        /// </summary>
        public string ReportCode { get; set; }
    }
}
