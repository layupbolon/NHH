using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    /// <summary>
    /// 投诉微信内容
    /// </summary>
    public class ComplaintWechatContentInfo
    {
        /// <summary>
        /// 您好，您的投诉有新的进展
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 某某小区1-101
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 重新指派
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 2014/8/28 10:30
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 正在调查处理中
        /// </summary>
        public string  ComplaintStatus { get; set; }

        /// <summary>
        /// 感谢您对我们服务的支持和监督，祝您生活愉快。
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// #173177
        /// </summary>
        string color = "#173177";
        public string Color { get { return color; } }
    }
}
