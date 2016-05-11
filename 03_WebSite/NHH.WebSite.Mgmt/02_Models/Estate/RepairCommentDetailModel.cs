using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 报修评价详情
    /// </summary>
    public class RepairCommentDetailModel 
    {
        private CrumbInfo _crumbInfo = new CrumbInfo();

        /// <summary>
        /// 面包屑
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get { return _crumbInfo; }
        }

        /// <summary>
        /// 维修编号
        /// </summary>
        public int RepairId { get; set; }

        /// <summary>
        /// 点评编号
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// 平均分（评分）
        /// </summary>
        public decimal? Overall { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        public decimal? Speed { get; set; }

        /// <summary>
        /// 维修结果
        /// </summary>
        public decimal? Quality { get; set; }

        /// <summary>
        /// 服务态度
        /// </summary>
        public decimal? Attitude { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 追加评论
        /// </summary>
        public string Additional { get; set; }

        /// <summary>
        /// 评价内容+追加评论
        /// </summary>
        public string AllComment { get; set; }
    }
}
