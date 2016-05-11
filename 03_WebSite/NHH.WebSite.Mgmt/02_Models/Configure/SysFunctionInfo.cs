using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    public class SysFunctionInfo
    {
        /// <summary>
        /// 功能模块Id
        /// </summary>
        public int FunctionId { get; set; }

        /// <summary>
        /// 功能模块key
        /// </summary>
        public string FunctionKey { get; set; }

        /// <summary>
        /// 功能模块名称
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 功能模块描述
        /// </summary>
        public string FunctionDescription { get; set; }

    }
}
