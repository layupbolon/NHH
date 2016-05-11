using NHH.Entities;
using NHH.Framework.Service;
using NHH.Service.Permission.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Permission
{
    public class PermissionService : NHHService<NHHEntities>, IPermissionService
    {
        /// <summary>
        /// 按用户分组加载用户功能权限列表
        /// </summary>
        /// <returns></returns>
        public SortedList<int, List<string>> LoadUserFuncPermissions()
        {
            var rlt = new SortedList<int, List<string>>();

            //获取超级管理员默认全权限列表
            var bizAllFunc = this.CreateCacheBizObject<Sys_Function>();
            var listAllFunc = bizAllFunc.TryCacheAllList(x => x.Status == 1);
            rlt.Add(0, listAllFunc.Select(x => x.FunctionKey).ToList());

            //获取普通用户权限列表
            var bizUserFunc = this.CreateCacheBizObject<View_User_Function>();
            var listUserFunc = bizUserFunc.TryCacheAllList();
            var users = listUserFunc.Select(x => x.UserID).Distinct().ToArray();

            foreach (var uid in users)
            {
                rlt.Add(uid, listUserFunc.Where(x => x.UserID == uid).Select(x => x.FunctionKey).ToList());
            }

            return rlt;

        }

        /// <summary>
        /// 按角色分组加载角色功能权限列表
        /// </summary>
        /// <returns></returns>
        public SortedList<int, List<string>> LoadRoleFuncPermissions()
        {
            var rlt = new SortedList<int, List<string>>();
            //获取超级管理员默认全权限列表
            var bizAllFunc = this.CreateCacheBizObject<Sys_Function>();
            var listAllFunc = bizAllFunc.TryCacheAllList(x => x.Status == 1);
            rlt.Add(0, listAllFunc.Select(x => x.FunctionKey).ToList());

            //获取普通角色权限列表
            var bizRoleFunc=this.CreateCacheBizObject<View_Role_Function>();
            var listRoleFunc = bizRoleFunc.TryCacheAllList();
            var roles = listRoleFunc.Select(x => x.RoleID).Distinct().ToArray();

            foreach (var rid in roles)
            {
                rlt.Add(rid, listRoleFunc.Where(x => x.RoleID == rid).Select(x => x.FunctionKey).ToList());
            }

            return rlt;
        }

        /// <summary>
        /// 加载指定用户功能权限列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<string> LoadUserFuncPermissions(int user)
        {
            var rlt = new List<string>();
            if (user == -1)
            {
                var bizAllFunc = this.CreateCacheBizObject<Sys_Function>();
                var listAllFunc = bizAllFunc.TryCacheAllList(x => x.Status == 1);
                rlt.AddRange(listAllFunc.Select(x => x.FunctionKey).ToArray());
            }
            else
            {
                var bizUserFunc = this.CreateCacheBizObject<View_User_Function>();
                var listUserFunc = bizUserFunc.TryCacheAllList(x=>x.UserID==user);
                rlt.AddRange(listUserFunc.Select(x => x.FunctionKey).ToArray());
            }
            return rlt;
        }

        /// <summary>
        /// 加载指定角色功能权限列表
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public List<string> LoadRoleFuncPermissions(int role)
        {
            var rlt = new List<string>();
            if (role == -1)
            {
                var bizAllFunc = this.CreateCacheBizObject<Sys_Function>();
                var listAllFunc = bizAllFunc.TryCacheAllList(x => x.Status == 1);
                rlt.AddRange(listAllFunc.Select(x => x.FunctionKey).ToArray());
            }
            else
            {
                var bizRoleFunc = this.CreateCacheBizObject<View_Role_Function>();
                var listRoleFunc = bizRoleFunc.TryCacheAllList(x=>x.RoleID==role);
                rlt.AddRange(listRoleFunc.Select(x => x.FunctionKey).ToArray());
            }
            return rlt;
        }
    }
}
