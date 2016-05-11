using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Operations
{
    public class NewsInfo
    {
        /// <summary>
        /// 新闻编号
        /// </summary>
        public int NewsID { get; set; }
        /// <summary>
        /// 新闻类型
        /// </summary>
        public int NewsType { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string NewsTitle { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string NewsContent { get; set; }
        /// <summary>
        /// 新闻简介
        /// </summary>
        public string NewsBrief { get; set; }
        /// <summary>
        /// 横幅图片
        /// </summary>
        public string NewsCover { get; set; }
        /// <summary>
        /// 发表时间
        /// </summary>
        public DateTime PublishTime { get; set; }
        /// <summary>
        /// 状态 -1无效 1有效
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 置顶 0否 1是
        /// </summary>
        public int Stick { get; set; }
        public int InUser { get; set; }

        public string EmployeeName { get; set; }
        public DateTime InDate { get; set; }
        public int? EditUser { get; set; }
        public DateTime? EditDate { get; set; }
        public int LastNewsID { get; set; }
        public string LastNewsTitle { get; set; }
        public int NextNewsID { get; set; }
        public string NextNewsTitle { get; set; }
    }
}
