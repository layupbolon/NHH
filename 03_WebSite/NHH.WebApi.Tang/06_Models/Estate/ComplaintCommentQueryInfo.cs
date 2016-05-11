using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 投诉评价查询条件
    /// </summary>
    public class ComplaintCommentQueryInfo : QueryInfo
    {
        public int? ServiceUserId { get; set; }

        public int? ComplaintId { get; set; }

        public int? ComplaintType { get; set; }

        /// <summary>
        /// 投诉提交时间
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// 投诉提交时间
        /// </summary>
        public DateTime? ToDate { get; set; }

    }
}
