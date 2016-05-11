using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    /// <summary>
    /// 角色权限model
    /// </summary>
    public class SysRoleFunctionListModel
    {
        /// <summary>
        /// 所有功能模块
        /// </summary>
        public List<SysFunctionInfo> AllFunctionList { get; set; }

        /// <summary>
        /// 角色已有功能模块
        /// </summary>
        public List<SysFunctionInfo> RoleFunctionList { get; set; }
    }
}
