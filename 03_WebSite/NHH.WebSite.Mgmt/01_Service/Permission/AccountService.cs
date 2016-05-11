using NHH.Entities;
using NHH.Framework.Caching;
using NHH.Framework.Security.Cryptography;
using NHH.Framework.Service;
using NHH.Models.Permission;
using NHH.Service.Permission.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Permission
{
    public class AccountService : NHHService<NHHEntities>, IAccountService
    {
        public MessageBag<UserLoginResult> Login(UserLoginRequest req)
        {
            //TODO:用户校验
            var rlt = new MessageBag<UserLoginResult>();
            req.Password=Cryptographer.SHA1.Encrypt(req.Password);
            var biz = this.CreateBizObject<Sys_User>();

            var user = biz.TopOne(x => x.UserName == req.UserName && x.Password == req.Password && x.Status == 1);
            if (user!=null)
            {
                rlt.Data = new UserLoginResult();
                rlt.Data.UserID = user.UserID;
                rlt.Data.UserName = user.UserName;
                rlt.Data.RoleIDs = user.Sys_User_Role.Where(x => x.Sys_Role.Status == 1).Select(x => x.RoleID).ToArray();
                rlt.Data.RoleNames = user.Sys_User_Role.Where(x => x.Sys_Role.Status == 1).Select(x => x.Sys_Role.RoleName).ToArray();
                if (user.Employee != null)
                {
                    rlt.Data.EmployeeID = user.Employee.EmployeeID;
                    rlt.Data.EmployeeName = user.Employee.EmployeeName;
                    rlt.Data.DepartmentID = user.Employee.Department1.DepartmentID;
                    rlt.Data.DepartmentName = user.Employee.Department1.DepartmentName;
                    rlt.Data.CompanyID = user.Employee.Company.CompanyID;
                    rlt.Data.CompanyName = user.Employee.Company.BriefName;
                }

                user.LastLoginTime = DateTime.Now;
                user.LastLoginIP = req.ClientIP;
                this.SaveChanges();

            }
            else
            {
                rlt.Code = 9999;
                rlt.Desc = "用户名或密码错误";
            }
            
            return rlt;
        }

        /// <summary>
        /// 判断登录名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool SearchExsitName(string userName)
        {
            var biz = this.CreateBizObject<Sys_User>();
            bool isexist = biz.Exists(p => p.UserName == userName);
            return isexist;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool UpdatePassword(string userName, string password)
        {
            var biz = this.CreateBizObject<Sys_User>();
            var entity = biz.TopOne(p => p.UserName == userName);
            if (entity != null)
            {
                entity.EditDate = DateTime.Now;
                entity.EditUser = 1;
                entity.Password = Cryptographer.SHA1.Encrypt(password);
                biz.Update(entity);
                this.SaveChanges();
            }
            return true;
        }
    }
}
