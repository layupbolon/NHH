﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 投诉评价基本信息
    /// </summary>
    public class ComplaintCommentInfo
    {
        /// <summary>
        /// 评价编号
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// 投诉编号
        /// </summary>
        public int ComplaintId { get; set; }

        /// <summary>
        /// 响应速度
        /// </summary>
        public decimal? Speed { get; set; }

        /// <summary>
        /// 服务态度
        /// </summary>
        public decimal? Attitude { get; set; }

        /// <summary>
        /// 维修结果
        /// </summary>
        public decimal? Quality { get; set; }

        /// <summary>
        /// 平均分
        /// </summary>
        public decimal? Overall { get; set; }

        /// <summary>
        /// 评价
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 客服人员姓名
        /// </summary>
        public string ServiceUserName { get; set; }

    }
}
