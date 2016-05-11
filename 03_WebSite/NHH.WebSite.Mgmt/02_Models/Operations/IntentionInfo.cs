using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Operations
{
    public class IntentionInfo : BaseModel
    {
        /// <summary>
        /// 入驻意向编号
        /// </summary>
        public int IntentionID { get; set; }
        /// <summary>
        /// 入驻意向类别
        /// </summary>
        public int IntentionType { get; set; }

        public string IntentionTypeName { get; set; }

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

        public string SourceString
        {
            get
            {
                switch (Source)
                {
                    case 1:
                        return "官网";
                    case 2:
                        return "唐小二";
                    default:
                        return "";
                }
            }
        }
        /// <summary>
        /// 状态 
        /// </summary>
        public int Status { get; set; }

        public string StatusString { get; set; }
        //{
        //    get
        //    {
        //        switch (Status)
        //        {
        //            case -1:
        //                return "已作废";
        //            case 1:
        //                return "待处理";
        //            case 2:
        //                return "已通知";
        //            case 3:
        //                return "已接单";
        //            default:
        //                return "";

        //        }
        //    }
        //}
        /// <summary>
        /// 处理人编号
        /// </summary>
        public int? ProcessUserID { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public string ProcessUserName { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ProcessTime { get; set; }
    }
}
