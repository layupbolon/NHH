using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Official
{
    public class NewsListModel : BaseModel
    {
        /// <summary>
        /// 活动列表
        /// </summary>
        public List<NewsModel> NewsModelList { get; set; }

        /// <summary>
        /// 翻页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

        /// <summary>
        /// 活动查询条件
        /// </summary>
        public NewsQueryInfo QueryInfo { get; set; }
    }
}
