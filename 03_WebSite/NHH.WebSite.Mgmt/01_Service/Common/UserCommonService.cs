using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common
{
    /// <summary>
    /// 用户公共服务接口
    /// </summary>
    public class UserCommonService : NHHService<NHHEntities>, IUserCommonService
    {
        /// <summary>
        /// 获取用户的默认项目ID列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<int> GetProjectIdList(int userId)
        {
            var list = new List<int>();
            var query = from u in Context.Sys_User
                        join e in Context.Employee on u.EmployeeID equals e.EmployeeID
                        join p in Context.Project on e.CompanyID equals p.ManageCompanyID
                        where u.UserID == userId && p.Status == 1
                        select new
                        {
                            p.ProjectID
                        };

            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    list.Add(entity.ProjectID);
                });
            }

            return list;
        }

        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public EmployeeCommonInfo GetEmployeeInfo(int userId)
        {
            var info = new EmployeeCommonInfo();
            var query = from u in Context.Sys_User
                        join e in Context.Employee on u.EmployeeID equals e.EmployeeID
                        join d in Context.Department on e.DepartmentID equals d.DepartmentID
                        where u.Status == 1 && e.Status == 1 && u.UserID == userId
                        select new
                        {
                            u.UserID,
                            e.EmployeeID,
                            e.EmployeeName,
                            d.DepartmentName,
                        };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                info.UserId = entity.UserID;
                info.Id = entity.EmployeeID;
                info.Name = entity.EmployeeName;
                info.Department = entity.DepartmentName;
            }
            return info;
        }
    }
}
