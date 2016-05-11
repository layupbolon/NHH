using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Enum.CommonEnums
{
    public enum MessageTypeEnum
    {
        [Description("邮件")]
        Email = 1,
        [Description("短信")]
        SMS = 2,
        [Description("微信")]
        Wechat = 3
    }
}
