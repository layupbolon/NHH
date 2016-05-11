using System.Collections.Generic;

namespace NHH.Framework.Baidu.Push
{
    public class BaiduPushQueryParams
    {
        public uint total_num { get; set; }
        public uint amount { get; set; }
        public List<BaiduPushQueryBinds> binds { get; set; }
    }
}
