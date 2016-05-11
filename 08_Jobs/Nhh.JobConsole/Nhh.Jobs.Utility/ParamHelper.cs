using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Utility
{
    /// <summary>
    /// 参数
    /// </summary>
    public static class ParamHelper
    {
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetString(string name)
        {
            return ConfigurationManager.AppSettings[name].ToString();
        }


        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetValue(string name)
        {
            int result = 0;
            int.TryParse(ConfigurationManager.AppSettings[name], out result);
            return result;
        }
    }
}
