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
    /// 员工公共服务
    /// </summary>
    public class EmployeeCommonService : NHHService<NHHEntities>, IEmployeeCommonService
    {
        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<CompanyCommonInfo> GetCompanyList(int userId)
        {
            var list = new List<CompanyCommonInfo>();

            var query = from u in Context.Sys_User
                        join e in Context.Employee on u.EmployeeID equals e.EmployeeID
                        join c in Context.Company on e.CompanyID equals c.CompanyID
                        where u.Status == 1 && c.Status == 1 && u.UserID == userId
                        select new
                        {
                            e.CompanyID,
                            c.BriefName,
                            c.CompanyName,
                            c.OwnerCompanyID,
                        };
            var entity = query.FirstOrDefault();
            if (entity == null)
            {
                return list;
            }

            list.Add(new CompanyCommonInfo
            {
                Id = entity.CompanyID,
                Name = entity.CompanyName,
                ShortName = entity.BriefName,
                OwnerCompanyID = entity.OwnerCompanyID.HasValue ? entity.OwnerCompanyID.Value : 0,
            });


            var query1 = from c in Context.Company
                         where c.Status == 1 && c.OwnerCompanyID == entity.CompanyID
                         select new
                         {
                             c.CompanyID,
                             c.CompanyName,
                             c.BriefName,
                             c.OwnerCompanyID,
                         };

            var entityList = query1.ToList();
            if (entityList != null)
            {
                entityList.ForEach(item =>
                {
                    list.Add(new CompanyCommonInfo
                    {
                        Id = item.CompanyID,
                        Name = item.CompanyName,
                        ShortName = item.BriefName,
                        OwnerCompanyID = item.OwnerCompanyID.HasValue ? item.OwnerCompanyID.Value : 0,
                    });
                });
            }
            return list;
        }


        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<EmployeeCommonInfo> GetEmployeeList(int companyId, int? departmentId)
        {
            var list = new List<EmployeeCommonInfo>();
            var query = from u in Context.Sys_User
                        join e in Context.Employee on u.EmployeeID equals e.EmployeeID
                        join d in Context.Department on e.DepartmentID equals d.DepartmentID
                        where u.Status == 1 && e.CompanyID == companyId
                        select new
                        {
                            u.UserID,
                            e.EmployeeID,
                            e.EmployeeName,
                            e.DepartmentID,
                            d.DepartmentName
                        };

            if (departmentId.HasValue && departmentId.Value > 0)
            {
                query = query.Where(item => item.DepartmentID == departmentId.Value);
            }

            var entityList = query.ToList();

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var info = new EmployeeCommonInfo();
                    info.Id = entity.EmployeeID;
                    info.Name = entity.EmployeeName;
                    info.UserId = entity.UserID;
                    info.Department = entity.DepartmentName;
                    list.Add(info);
                });
            }
            return list;
        }
    }
}
