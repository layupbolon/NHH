using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 报修评价列表
    /// </summary>
    public class RepairCommentListModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public RepairCommentQueryInfo QueryInfo { get; set; } 

        /// <summary>
        /// 评价列表
        /// </summary>
        public List<RepairCommentInfo> CommentList { get; set; }


        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
