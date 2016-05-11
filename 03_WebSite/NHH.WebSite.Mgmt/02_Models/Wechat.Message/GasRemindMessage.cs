using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Framework.Wechat;

namespace NHH.Wechat.Models.Message
{
    /// <summary>
    /// 煤气提醒消息
    /// </summary>
    public class GasRemindMessage : NHH.Framework.Wechat.requestData
    {
        public requestFieldDetail first { get; set; }

        public requestFieldDetail userName { get; set; }

        public requestFieldDetail address { get; set; }

        public requestFieldDetail month { get; set; }

        public requestFieldDetail remark { get; set; }

    }
}
