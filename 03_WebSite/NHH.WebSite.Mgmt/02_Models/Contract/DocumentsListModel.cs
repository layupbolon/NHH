using System.Collections.Generic;
using NHH.Models.Common;

namespace NHH.Models.Contract
{
    public class DocumentsListModel : BaseModel
    {
        /// <summary>
        /// 单据查询条件
        /// </summary>
        public DocumentsQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 单据列表
        /// </summary>
        public List<DocumentsInfo> DocumentsInfos { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
