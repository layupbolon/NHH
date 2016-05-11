using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    /// <summary>
    /// 用户列表
    /// </summary>
    public class SysUserListModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public SysUserListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 用户列表
        /// </summary>
        public List<SysUserInfo> UserList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
