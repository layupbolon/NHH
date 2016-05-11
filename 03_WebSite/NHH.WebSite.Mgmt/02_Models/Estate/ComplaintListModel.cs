using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 投诉列表信息
    /// </summary>
    public class ComplaintListModel:BaseModel
    {
        public List<ComplaintInfo> ComplaintListInfo { get; set; }

        /// <summary>
        /// 翻页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

        public ComplaintListQueryInfo QueryInfo { get; set; }



    }
}
