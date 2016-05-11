using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Operations
{
    public class IntentionInfo
    {
        /// <summary>
        /// 入驻意向编号
        /// </summary>
        public int IntentionID { get; set; }
        /// <summary>
        /// 入驻意向类别
        /// </summary>
        public int IntentionType { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// 意向所属项目
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime InDate { get; set; }
        /// <summary>
        /// 来源 1官网 2唐小二
        /// </summary>
        public int Source { get; set; }
        /// <summary>
        /// 状态  -1作废 1未处理 2已发消息 3已接单
        /// </summary>
        public int Status { get; set; }
    }
}
