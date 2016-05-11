using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Framework.Wechat
{
    /// <summary>
    /// 获取发送模板消息的请求结果
    /// </summary>
    public class responseData
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string msgid { get; set; }
    }
}
