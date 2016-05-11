using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Configure;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Service.Configure
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class SysRoleService : NHHService<NHHEntities>, ISysRoleService
    {
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public SysRoleListModel GetRoleList()
        {
            var model = new SysRoleListModel();
            model.RoleList = new List<SysRoleInfo>();
            var query = from r in Context.Sys_Role
                        where r.Status == 1
                        select new
                        {
                            r.RoleID,
                            r.RoleName
                        };
            var queryList = query.ToList();
            if (queryList != null)
            {
                queryList.ForEach(item =>
                {
                    var role = new SysRoleInfo()
                    {
                        RoleId = item.RoleID,
                        RoleName = item.RoleName
                    };
                    model.RoleList.Add(role);
                });
            }
            return model;
        }

        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddRole(SysRoleDetailModel model)
        {
            var roleInstance = CreateBizObject<Sys_Role>();
            var entity = new Sys_Role()
            {
                RoleName = model.RoleName,
                Status = 1,
                InDate = DateTime.Now,
                InUser = 0,
                EditDate = DateTime.Now,
                EditUser = 0
            };
            roleInstance.Insert(entity);
            this.SaveChanges();
            model.RoleId = entity.RoleID;
            return true;
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public SysRoleDetailModel GetRoleDetail(int roleId)
        {
            var model = new SysRoleDetailModel();
            model.RoleFunctionList = new List<SysFunctionInfo>();

            #region 获取角色基本信息
            var query0 = from r in Context.Sys_Role
                         where r.RoleID == roleId && r.Status == 1
                         select new
                         {
                             r.RoleID,
                             r.RoleName
                         };
            var entity = query0.FirstOrDefault();
            if (entity == null)
            {
                return model;
            }
            model.RoleId = entity.RoleID;
            model.RoleName = entity.RoleName;

            #endregion

            #region 获取角色下的功能模块
            var query1 = from ur in Context.Sys_Role_Function
                         join f in Context.Sys_Function on ur.FunctionID equals f.FunctionID
                         where ur.RoleID == roleId
                         select new
                         {
                             f.FunctionID,
                             f.FunctionName
                         };
            var entityList1 = query1.ToList();
            if (entityList1 != null)
            {
                foreach (var entity1 in entityList1)
                {
                    var info = new SysFunctionInfo();
                    info.FunctionId = entity1.FunctionID;
                    info.FunctionName = entity1.FunctionName;
                    model.RoleFunctionList.Add(info);
                }
            }
            #endregion

            return model;
        }


        /// <summary>
        /// 编辑更新角色功能模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateRole(SysRoleDetailModel model)
        {
            var instance = CreateBizObject<Sys_Role>();
            var entity = instance.GetBySysNo(model.RoleId);
            if (entity != null)
            {
                entity.RoleName = model.RoleName;
                entity.EditDate = DateTime.Now;
                entity.EditUser = model.InUser;
                instance.Update(entity);
                this.SaveChanges();
            }
            return true;
        }
    }
}
