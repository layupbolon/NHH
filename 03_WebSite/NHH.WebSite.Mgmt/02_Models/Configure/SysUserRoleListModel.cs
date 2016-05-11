using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    /// <summary>
    /// 用户角色列表
    /// </summary>
    public class SysUserRoleListModel
    {
        /// <summary>
        /// 所有的角色
        /// </summary>
        public List<SysRoleInfo> AllRoleList { get; set; }

        /// <summary>
        /// 用户已有角色列表
        /// </summary>
        public List<SysRoleInfo> UserRoleList { get; set; }
    }
}
