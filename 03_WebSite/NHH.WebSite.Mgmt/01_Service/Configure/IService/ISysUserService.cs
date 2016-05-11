using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Configure.IService
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface ISysUserService
    {
        /// <summary>
        /// 获取系统用户列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        SysUserListModel GetUserList(SysUserListQueryInfo queryInfo);

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        SysUserDetailModel GetUserDetail(int userId);

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        SysUserRoleListModel GetUserRoleList(int userId);

        /// <summary>
        /// 编辑更新用户角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateUser(SysUserDetailModel model);

        /// <summary>
        /// 获取个人信息详情
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        SysUserDetailModel GetSysUserDetail(int userId);

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateSysUserInfo(SysUserDetailModel model);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        MessageBag<bool> UpdatePassword(SysUserPasswordModel model);
    }
}
