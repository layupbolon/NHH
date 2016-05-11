using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Configure;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Configure
{
    /// <summary>
    /// 角色功能服务
    /// </summary>
    public class SysRoleFunctionService : NHHService<NHHEntities>, ISysRoleFunctionService
    {

        /// <summary>
        /// 获取角色功能列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<SysFunctionInfo> GetRoleFunctionList(int roleId)
        {
            var list = new List<SysFunctionInfo>();
            var query = from ur in Context.Sys_Role_Function
                        join f in Context.Sys_Function on ur.FunctionID equals f.FunctionID
                        where ur.RoleID == roleId
                        select new
                        {
                            f.FunctionID,
                            f.FunctionName
                        };
            var entityList = query.OrderBy(item => item.FunctionName).ToList();
            if (entityList != null)
            {
                foreach (var entity in entityList)
                {
                    var info = new SysFunctionInfo();
                    info.FunctionId = entity.FunctionID;
                    info.FunctionName = entity.FunctionName;
                    list.Add(info);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取角色功能列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysFunctionInfo> GetSystemFunctionList(int? roleId, string keyword)
        {
            var list = new List<SysFunctionInfo>();
            var query = from f in Context.Sys_Function
                        where f.Status == 1
                        select new
                        {
                            f.FunctionID,
                            f.FunctionName,
                            f.FunctionKey,
                        };
            if (roleId.HasValue)
            {
                query = query.Where(item => !(from rf in Context.Sys_Role_Function
                                              where rf.RoleID == roleId
                                              select rf.FunctionID).Any(fun => fun == item.FunctionID));
            }

            if (!string.IsNullOrEmpty(keyword) && keyword.Length > 0)
            {
                query = query.Where(item => item.FunctionName.Contains(keyword) || item.FunctionKey.Contains(keyword));
            }

            var entityList = query.OrderBy(item => item.FunctionName).ToList();
            if (entityList != null)
            {
                foreach (var entity in entityList)
                {
                    var info = new SysFunctionInfo();
                    info.FunctionId = entity.FunctionID;
                    info.FunctionName = entity.FunctionName;
                    list.Add(info);
                }
            }

            return list;
        }

        /// <summary>
        /// 功能模块列表
        /// </summary>
        /// <returns></returns>
        public SysRoleFunctionListModel GetRoleFunctionList(string keyword, int roleId)
        {

            var model = new SysRoleFunctionListModel();
            model.AllFunctionList = new List<SysFunctionInfo>();
            model.RoleFunctionList = new List<SysFunctionInfo>();

            #region 根据条件获取所有功能模块


            var query0 = from r in Context.Sys_Function
                         where r.FunctionKey.Contains(keyword) || r.FunctionName.Contains(keyword) && r.Status == 1
                         select new
                         {
                             r.FunctionID,
                             r.FunctionKey,
                             r.FunctionName
                         };
            var entity0 = query0.ToList();
            if (entity0 != null)
            {
                entity0.ForEach(item =>
                {
                    var info = new SysFunctionInfo()
                    {
                        FunctionId = item.FunctionID,
                        FunctionKey = item.FunctionKey,
                        FunctionName = item.FunctionName
                    };
                    model.AllFunctionList.Add(info);
                });
            }
            #endregion

            #region 获取角色已有功能模块
            var query1 = from r in Context.Sys_Role_Function
                         where r.RoleID == roleId
                         select new
                         {
                             r.FunctionID
                         };
            var entity1 = query1.ToList();
            if (entity1 != null)
            {
                foreach (var item in entity1)
                {
                    var info = new SysFunctionInfo() { FunctionId = item.FunctionID };
                    model.RoleFunctionList.Add(info);
                }
            }

            #endregion

            return model;
        }



        /// <summary>
        /// 添加功能
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public bool AddFunction(int roleId, int functionId)
        {
            var bll = CreateBizObject<Sys_Role_Function>();
            if (bll.Exists(item => item.RoleID == roleId && item.FunctionID == functionId))
                return true;
            var entity = new Sys_Role_Function
            {
                RoleID = roleId,
                FunctionID = functionId,
                EditDate = DateTime.Now,
                InUser = 0,
                EditUser = 0,
                InDate = DateTime.Now,
            };
            bll.Insert(entity);
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 移除功能
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public bool RemoveFunction(int roleId, int functionId)
        {
            var bll = CreateBizObject<Sys_Role_Function>();
            var entity = bll.TopOne(item => item.RoleID == roleId && item.FunctionID == functionId);
            if (entity == null)
                return false;
            bll.Delete(entity);
            this.SaveChanges();
            return true;
        }
    }
}
