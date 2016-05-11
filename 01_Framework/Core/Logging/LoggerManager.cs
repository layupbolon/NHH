using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Framework.Logging
{
    public class LoggerManager
    {
        /// <summary>
        /// 获取系统日志记录器
        /// </summary>
        /// <returns></returns>
        public static IAPPLogger GetAPPLogger()
        {
            return new NHHAPPLogger();
        }

        /// <summary>
        /// 获取系统日志记录器
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static IAPPLogger GetAPPLogger(string category)
        {
            return new NHHAPPLogger(category);
        }

        /// <summary>
        /// 获取API日志记录器
        /// </summary>
        /// <returns></returns>
        public static IAPILogger GetAPILogger()
        {
            return new NHHAPILogger();
        }

        /// <summary>
        /// 获取操作日志记录器
        /// </summary>
        /// <returns></returns>
        public static IOptLogger GetOptLogger()
        {
            return new NHHOptLogger();
        }

        /// <summary>
        /// 获取操作日志记录器
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static IOptLogger GetOptLogger(string category)
        {
            return new NHHOptLogger(category);
        }

    }
}
