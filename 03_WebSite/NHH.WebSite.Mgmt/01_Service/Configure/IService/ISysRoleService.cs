using NHH.Models.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Configure.IService
{
    /// <summary>
    /// 角色服务接口
    /// </summary>
    public interface ISysRoleService
    {
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        SysRoleListModel GetRoleList();

        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddRole(SysRoleDetailModel model);

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        SysRoleDetailModel GetRoleDetail(int roleId);


        /// <summary>
        /// 编辑更新角色功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateRole(SysRoleDetailModel model);

    }
}
