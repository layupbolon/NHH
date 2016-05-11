using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Permission.IService
{
    public interface IPermissionService
    {
        /// <summary>
        /// 加载指定用户功能权限列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<string> LoadUserFuncPermissions(int user);

        /// <summary>
        /// 加载指定角色功能权限列表
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        List<string> LoadRoleFuncPermissions(int role);

        /// <summary>
        /// 按用户分组加载用户功能权限列表
        /// </summary>
        /// <returns></returns>
        SortedList<int, List<string>> LoadUserFuncPermissions();

        /// <summary>
        /// 按角色分组加载角色功能权限列表
        /// </summary>
        /// <returns></returns>

        SortedList<int, List<string>> LoadRoleFuncPermissions();
    }
}
