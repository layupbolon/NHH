using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    /// <summary>
    /// 角色列表
    /// </summary>
    public class SysRoleListModel : BaseModel
    {
        /// <summary>
        /// 角色列表
        /// </summary>
        public List<SysRoleInfo> RoleList { get; set; }
    }
}
