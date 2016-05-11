using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Framework.Wechat
{
    public class fileResponseData
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// media_id
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public int created_at { get; set; }
    }
}
