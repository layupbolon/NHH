using NHH.Framework.Wechat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Wechat.Models.Message
{
    /// <summary>
    /// 维修提醒消息
    /// </summary>
    public class RepairRemindMessage : NHH.Framework.Wechat.requestData
    {
        /// <summary>
        /// 测试标题
        /// </summary>
        public requestFieldDetail first { get; set; }

        /// <summary>
        /// 报修房号
        /// </summary>
        public requestFieldDetail keyword1 { get; set; }

        /// <summary>
        /// 报修主题
        /// </summary>
        public requestFieldDetail keyword2 { get; set; }

        /// <summary>
        /// 报修时间
        /// </summary>
        public requestFieldDetail keyword3 { get; set; }

        /// <summary>
        /// 当前进展
        /// </summary>
        public requestFieldDetail keyword4 { get; set; }

        /// <summary>
        /// 预计完成时间
        /// </summary>
        public requestFieldDetail keyword5 { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public requestFieldDetail remark { get; set; }

    }
}
