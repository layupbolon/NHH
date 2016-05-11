using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Framework.Logging
{
    /// <summary>
    /// 操作日志记录器接口
    /// </summary>
    public interface IOptLogger
    {
        /// <summary>
        /// 记录业务操作日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="entity"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <param name="detail"></param>
        /// <param name="user"></param>
        /// <param name="time"></param>
        void Log(object sender, string entity, string key, string action, string detail, string user, DateTime? time = null);

    }
}
