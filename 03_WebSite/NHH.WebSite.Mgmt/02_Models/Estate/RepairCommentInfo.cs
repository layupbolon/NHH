using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 报修评价
    /// </summary>
    public  class RepairCommentInfo
    {
        /// <summary>
        /// 评价ID
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// 维修人
        /// </summary>
        public int? RepairUserId { get; set; }

        /// <summary>
        /// 维修人
        /// </summary>
        public string RepairUserName { get; set; }

        /// <summary>
        /// 响应速度
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
        /// 平均分
        /// </summary>
        public decimal? Overall { get; set; }

        /// <summary>
        /// 评价
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        ///报修编号
        /// </summary>
        public int? RepairId { get; set; }
    }
}
