using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.Adpoint
{
    public class AdPointListModel : BaseModel
    {
        /// <summary>
        /// 分页
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

        /// <summary>
        /// 查询项
        /// </summary>
        public AdPointQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 广告位列表
        /// </summary>
        public IList<AdPointListInfo> AdPointList { get; set; }
    }
}
