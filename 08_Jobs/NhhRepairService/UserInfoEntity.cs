using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhRepairService
{
    /// <summary>
    /// 系统用户信息
    /// </summary>
    public class UserInfoEntity
    {
        /// <summary>
        /// 工程主管系统用户Id
        /// </summary>
        public int GovernorUserId { get; set; }

        /// <summary>
        /// 工程经理或者项目总经理系统用户Id
        /// </summary>
        public int ManagerUserId { get; set; }

    }
}
