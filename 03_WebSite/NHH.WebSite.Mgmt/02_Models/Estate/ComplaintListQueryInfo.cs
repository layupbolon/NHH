using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{

    /// <summary>
    /// 投诉查询信息
    /// </summary>
    public class ComplaintListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 投诉查询信息
        /// </summary>
        public ComplaintListQueryInfo()
        {
            OrderBy = "ComplaintId";
        }

        public int? ComplaintStatus { get; set; }

        public int? ComplaintId { get; set; }

        public int? ComplaintType { get; set; }

        public int? ServiceUserId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
