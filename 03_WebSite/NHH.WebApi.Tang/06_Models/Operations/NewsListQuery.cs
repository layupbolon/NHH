using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Operations
{
    public class NewsListQuery
    {
        public int ProjectID { get; set; }

        /// <summary>
        /// 发表时间起
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 发表时间止
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int Size { get; set; }
    }
}
