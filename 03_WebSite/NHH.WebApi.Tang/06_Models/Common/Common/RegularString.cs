using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Common
{
    public class RegularString
    {
        /// <summary>
        /// 验证Email正则
        /// </summary>
        public static string Email {
            get { return @"^\s*([A-Za-z0-9_-]+(\.\w+)*@(\w+\.)+\w{2,5})\s*$"; }
        }
        /// <summary>
        /// 验证手机号正则  以1开头11位数字
        /// </summary>
        public static string Mobile
        {
            get { return @"^1\d{10}$"; }
        }
        /// <summary>
        /// 验证身份证正则
        /// </summary>
        public static string IDNumber
        {
            get { return @"\d{17}[\d|X]|\d{15}"; }
        }
        /// <summary>
        /// 验证邮编正则
        /// </summary>
        public static string ZipCode
        {
            get { return @"^[0-9]{6}$"; }
        }
        /// <summary>
        /// 密码正则 全数字、全字母、全特殊字符（~!@#$%^&*.）的任意组合，长度6-22
        /// </summary>
        public static string Password
        {
            get { return @"^[\@A-Za-z0-9\!\#\$\%\^\&\*\.\~]{6,22}$"; }
        }
    }
}
