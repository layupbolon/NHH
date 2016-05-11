using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 评价里列表
    /// </summary>
    public class ComplaintCommentListModel : BaseModel
    {
        public List<ComplaintCommentInfo> CommentListInfo { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public ComplaintCommentQueryInfo QueryInfo { get; set; }

    }
}
