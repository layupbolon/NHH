using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Operations
{
    public class NewsListModel
    {
        public NewsListModel()
        {
        }

        public NewsListModel(int pageIndex, int pageSize)
        {
            this.PagingInfo=new PagingInfo();
            this.PagingInfo.PageIndex = pageIndex;
            this.PagingInfo.PageSize = pageSize;
        }
        public List<NewsInfo> NewsList { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
