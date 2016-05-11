using NHH.Entities;
using NHH.Framework.Security.Cryptography;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Configure;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NHH.Service.Configure
{
    public class SysUserService : NHHService<NHHEntities>, ISysUserService
    {
        /// <summary>
        /// 获取系统用户列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public SysUserListModel GetUserList(SysUserListQueryInfo queryInfo)
        {
            var model = new SysUserListModel();
            model.QueryInfo = queryInfo;
            model.UserList = new List<SysUserInfo>();
            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };

            var query = from u in Context.Sys_User
                        where u.Status == 1
                        select new
                        {
                            CompanyID = (int?)u.Employee.CompanyID,
                            DepartmentID = (int?)u.Employee.DepartmentID,
                            u.UserID,
                            u.EmployeeID,
                            u.Employee.EmployeeName,
                            u.UserType,
                            u.UserName,
                            u.LastLoginTime,
                            u.LastLoginIP,
                            u.InDate,
                        };
            #region 查询信息
            if (queryInfo.CompanyId.HasValue)
            {
                query = query.Where(item => item.CompanyID.Value == queryInfo.CompanyId.Value);
            }
            if (queryInfo.DepartmentId.HasValue)
            {
                query = query.Where(item => item.DepartmentID.Value == queryInfo.DepartmentId.Value);
            }
            if (!string.IsNullOrEmpty(queryInfo.UserName))
            {
                query = query.Where(item => item.UserName.Contains(queryInfo.UserName));
            }
            #endregion

            model.PagingInfo.TotalCount = query.Count();
            var entityList = query.OrderByDescending(item => item.UserID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();

            if (entityList != null)
            {
                foreach (var entity in entityList)
                {
                    var user = new SysUserInfo();
                    user.UserId = entity.UserID;
                    user.EmployeeId = entity.EmployeeID.HasValue ? entity.EmployeeID.Value : 0;
                    user.EmployeeName = entity.EmployeeName;
                    user.UserType = entity.UserType;
                    user.UserName = entity.UserName;
                    user.LastLoginTime = entity.LastLoginTime;
                    user.LastLoginIP = entity.LastLoginIP;
                    user.InDate = entity.InDate;
                    model.UserList.Add(user);
                }
            }
            return model;
        }

        /// <summary>
        /// 编辑更新用户角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUser(SysUserDetailModel model)
        {
            var urInstance = CreateBizObject<Sys_User_Role>();

            string[] roleList = null;
            var roleId = 0;

            #region 新增角色
            if (model.AddRoleList != null)
            {
                roleList = model.AddRoleList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var role in roleList)
                {
                    if (!int.TryParse(role, out roleId))
                    {
                        break;
                    }
                    var urEntity = new Sys_User_Role()
                    {
                        UserID = model.UserId,
                        RoleID = roleId,
                        InDate = DateTime.Now,
                        InUser = model.InUser,
                        EditDate = DateTime.Now,
                        EditUser = model.InUser,
                    };
                    urInstance.Insert(urEntity);
                    this.SaveChanges();
                }
            }
            #endregion

            #region 移除角色
            if (model.RemoveRoleList != null)
            {
                roleList = model.RemoveRoleList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var role in roleList)
                {
                    if (!int.TryParse(role, out roleId))
                    {
                        break;
                    }

                    var entity = urInstance.TopOne(m => m.UserID == model.UserId && m.RoleID == roleId);
                    if (entity != null)
                    {
                        urInstance.Delete(entity);
                        this.SaveChanges();
                    }
                }
            }
            #endregion


            return true;
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SysUserDetailModel GetUserDetail(int userId)
        {
            var model = new SysUserDetailModel();
            var query = from u in Context.Sys_User
                        where u.UserID == userId && u.Status == 1
                        select new
                        {
                            u.UserID,
                            u.EmployeeID,
                            u.UserName
                        };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.UserId = entity.UserID;
                model.EmployeeId = entity.EmployeeID.HasValue ? entity.EmployeeID.Value : 0;
                model.UserName = entity.UserName;
            }

            #region 用户角色列表
            var query1 = from ur in Context.Sys_User_Role
                         join r in Context.Sys_Role on ur.RoleID equals r.RoleID
                         where r.Status == 1 && ur.UserID == userId
                         select new
                         {
                             r.RoleID,
                             r.RoleName,
                         };
            model.RoleList = new List<SysRoleInfo>();
            var roleList = query1.ToList();
            if (roleList != null)
            {
                roleList.ForEach(role =>
                {
                    model.RoleList.Add(new SysRoleInfo
                    {
                        RoleId = role.RoleID,
                        RoleName = role.RoleName,
                    });
                });
            }
            #endregion
            return model;
        }

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SysUserRoleListModel GetUserRoleList(int userId)
        {
            var model = new SysUserRoleListModel();
            model.AllRoleList = new List<SysRoleInfo>();
            model.UserRoleList = new List<SysRoleInfo>();

            var query0 = from r in Context.Sys_Role
                         where r.Status == 1
                         select new
                         {
                             r.RoleID,
                             r.RoleName,
                         };
            var roleList = query0.ToList();
            if (roleList != null)
            {
                roleList.ForEach(role =>
                {
                    model.AllRoleList.Add(new SysRoleInfo
                    {
                        RoleId = role.RoleID,
                        RoleName = role.RoleName,
                    });
                });
            }

            var query1 = from ur in Context.Sys_User_Role
                         join r in Context.Sys_Role on ur.RoleID equals r.RoleID
                         where r.Status == 1 && ur.UserID == userId
                         select new
                         {
                             r.RoleID,
                             r.RoleName,
                         };

            roleList = query1.ToList();
            if (roleList != null)
            {
                roleList.ForEach(role =>
                {
                    model.UserRoleList.Add(new SysRoleInfo
                    {
                        RoleId = role.RoleID,
                        RoleName = role.RoleName,
                    });
                });
            }
            return model;
        }

        /// <summary>
        /// 获取个人信息详情
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public SysUserDetailModel GetSysUserDetail(int userId)
        {
            var model = new SysUserDetailModel();
            var query = from su in Context.Sys_User
                        join em in Context.Employee on su.EmployeeID equals em.EmployeeID
                        join dept in Context.Department on em.DepartmentID equals dept.DepartmentID
                        join cm in Context.Company on em.CompanyID equals cm.CompanyID
                        where su.UserID == userId && su.Status == 1 && em.Status == 1
                        select new
                        {
                            su.UserID,
                            su.UserName,
                            em.EmployeeID,
                            em.EmployeeCode,
                            em.EmployeeName,
                            em.IDNumber,
                            em.Email,
                            em.Moblie,
                            em.Phone,
                            em.Ext,
                            em.Gender,
                            em.Birthday,
                            dept.DepartmentName,
                            cm.CompanyName
                        };

            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.UserId = entity.UserID;
                model.UserName = entity.UserName;
                model.EmployeeId = entity.EmployeeID;
                model.EmployeeName = entity.EmployeeName;
                model.EmployeeCode = entity.EmployeeCode;
                model.IDNumber = entity.IDNumber;
                model.Email = entity.Email;
                model.Mobile = entity.Moblie;
                model.Phone = entity.Phone;
                model.Ext = entity.Ext;
                model.Gender = entity.Gender;
                model.Birthday = entity.Birthday;
                model.DeptName = entity.DepartmentName;
                model.CompanyName = entity.CompanyName;
            }
            return model;
        }

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateSysUserInfo(SysUserDetailModel model)
        {
            var instance = CreateBizObject<Employee>();
            var entity = instance.GetBySysNo(model.EmployeeId);
            if (entity != null)
            {
                entity.Moblie = model.Mobile;
                entity.Phone = model.Phone;
                entity.Ext = model.Ext;
                entity.EditDate = DateTime.Now;
                entity.EditUser = model.UserId;

                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改个人账号密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public MessageBag<bool> UpdatePassword(SysUserPasswordModel model)
        {
            var instance = CreateBizObject<Sys_User>();
            var entity = instance.GetBySysNo(model.UserId);

            var message = new MessageBag<bool>();
            if (entity == null)
            {
                message.Code = -1;
                message.Desc = "账号异常";
                return message;
            }

            if (entity.Password != Cryptographer.SHA1.Encrypt(model.OldPassword))
            {
                message.Code = -1;
                message.Desc = "原密码输入不正确";
                return message;
            }

            entity.Password = Cryptographer.SHA1.Encrypt(model.NewPassword);
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserId;

            instance.Update(entity);
            this.SaveChanges();

            message.Desc = "密码修改成功";

            return message;
        }
    }
}
