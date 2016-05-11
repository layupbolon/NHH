using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace NHH.Models.Common.Enum
{
    public enum ApiResultEnum
    {
        [Description("很抱歉！系统内部错误，请联系管理员！")]
        Error=-1,
        [Description("Success")]
        Success = 0,
        [Description("没有更新任何行")]
        NoUpdateAnyRows = 100
    }
}