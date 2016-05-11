using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Utility
{
    public class DirectoryUtil
    {
        /// <summary>
        /// 构造子文件夹
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static string BuildDir(string parent, string sub)
        {
            var path = string.Format("{0}/{1}/", parent, sub);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}
