using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Utility
{
    /// <summary>
    /// 类型转换
    /// </summary>
    public class ConvertHelper
    {
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return string.Empty;

            return obj.ToString();
        }

        /// <summary>
        /// 获取整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return 0;
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 获取可空整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? ToNullableInt(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            return Convert.ToInt32(obj);            
        }
    }
}
