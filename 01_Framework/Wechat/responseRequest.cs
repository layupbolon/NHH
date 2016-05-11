using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Framework.Wechat
{
    /// <summary>
    /// 获取access_token的结果
    /// </summary>
    public class responseRequest
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
    }
}
