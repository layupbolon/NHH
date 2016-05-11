using System;
using NHH.Models.Common;

namespace NHH.Models.Official
{
    /// <summary>
    /// 官网新闻查询条件
    /// </summary>
    public class NewsQueryInfo : QueryInfo
    {
        /// <summary>
        /// 新闻类型
        /// </summary>
        public int? NewsType { get; set; }

        private int? _newsTarget = 1;

        /// <summary>
        /// 目标 
        /// 1唐小二 2官网 
        /// </summary>
        public int? NewsTarget
        {
            get { return _newsTarget; }
            set { _newsTarget = value; }
        }

        /// <summary>
        /// 新闻标题
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 所属项目
        /// </summary>
        public int? ProjectID { get; set; }

        /// <summary>
        /// 新闻状态
        /// -1已作废  1未发布  2已发布
        /// </summary>
        public int? NewsStatus { get; set; }

        /// <summary>
        /// 发布开始时间
        /// </summary>
        public DateTime? PubStartTime { get; set; }

        /// <summary>
        /// 发布结束时间
        /// </summary>
        public DateTime? PubEndTime { get; set; }

        /// <summary>
        /// 提交开始时间
        /// </summary>
        public DateTime? InStartTime { get; set; }

        /// <summary>
        /// 提交结束时间
        /// </summary>
        public DateTime? InEndTime { get; set; }
    }
}
