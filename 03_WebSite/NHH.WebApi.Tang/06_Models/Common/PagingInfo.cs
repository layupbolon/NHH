using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class PagingInfo
    {
        private int pageSize =2;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public int PageIndex { get; set; }

        public int PageCount
        {
            get
            {
                if (TotalCount == 0)
                    return 1;
                var number = TotalCount / pageSize;
                if (TotalCount % pageSize > 0)
                    number += 1;
                return number;
            }
        }

        public int TotalCount { get; set; }

        /// <summary>
        /// 跳过数
        /// </summary>
        public int SkipNum
        {
            get 
            {
                return (PageIndex - 1) * PageSize;
            }
        }

        /// <summary>
        /// 取多少个
        /// </summary>
        public int TakeNum
        {
            get {
                if (pageSize > TotalCount)
                    return TotalCount;
                return pageSize;
            }
        }

        /// <summary>
        /// 起始页
        /// </summary>
        public int StartPage
        {
            get
            {
                int startPage = PageIndex < 6 ? 1 : PageIndex - 6;
                if (startPage + 11 > PageCount)
                    startPage = PageCount - 11;
                return startPage < 1 ? 1 : startPage;
            }
        }

        /// <summary>
        /// 结束页
        /// </summary>
        public int EndPage
        {
            get 
            {
                int endPage = StartPage + 12;
                if (endPage > PageCount)
                    endPage = PageCount;
                return endPage;
            }
        }
    }
}
