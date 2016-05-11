using NHH.Models.Configure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Configure.IService
{
    /// <summary>
    /// 角色功能服务接口
    /// </summary>
    public interface ISysRoleFunctionService
    {
        /// <summary>
        /// 获取角色功能列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<SysFunctionInfo> GetRoleFunctionList(int roleId);

        /// <summary>
        /// 获取角色功能列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysFunctionInfo> GetSystemFunctionList(int? roleId, string keyword);


        /// <summary>
        /// 查询功能模块
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        SysRoleFunctionListModel GetRoleFunctionList(string keyword, int roleId);


        /// <summary>
        /// 添加功能
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        bool AddFunction(int roleId, int functionId);

        /// <summary>
        /// 移除功能
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        bool RemoveFunction(int roleId, int functionId);
    }
}
