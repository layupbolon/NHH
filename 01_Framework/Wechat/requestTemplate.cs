using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Framework.Wechat
{
    /// <summary>
    /// 请求模板
    /// </summary>
    public class requestTemplate
    {
        public string touser { get; set; }
        public string template_id { get; set; }
        public string url { get; set; }
        public string topcolor { get; set; }
        public object data { get; set; }
    }
}
