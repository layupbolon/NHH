using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Common.Employee;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common
{
    /// <summary>
    /// 公司公共服务
    /// </summary>
    public class CompanyCommonService : NHHService<NHHEntities>, ICompanyCommonService
    {
        /// <summary>
        /// 获取公司类型列表
        /// </summary>
        /// <returns></returns>
        public List<CompanyTypeInfo> GetCompanyTypeList()
        {
            var list = new List<CompanyTypeInfo>();
            var entityList = CreateBizObject<View_CompanyType>().GetAllList();
            entityList.ForEach(entity =>
            {
                list.Add(new CompanyTypeInfo
                {
                    Id = entity.CompanyTypeID,
                    Name = entity.CompanyTypeName,
                });
            });
            return list;
        }

        /// <summary>
        /// 获取公司基本信息列表
        /// </summary>
        /// <param name="companyType"></param>
        /// <returns></returns>
        public List<CompanyCommonInfo> GetCompanyList(int companyType)
        {
            var list = new List<CompanyCommonInfo>();

            Expression<Func<Company, bool>> where = company => company.CompanyType == companyType && company.Status == 1;
            var entityList = CreateBizObject<Company>().GetAllList(where);
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var company = new CompanyCommonInfo
                    {
                        Id = entity.CompanyID,
                        Name = entity.CompanyName,
                        ShortName = entity.BriefName,
                    };
                    list.Add(company);
                });
            }

            return list;
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<DepartmentCommonInfo> GetDepartmentList(int companyId)
        {
            var list = new List<DepartmentCommonInfo>();

            Expression<Func<Department, bool>> where = item => item.CompanyID == companyId && item.Status == 1;
            var entityList = CreateBizObject<Department>().GetAllList(where);
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var company = new DepartmentCommonInfo
                    {
                        DepartmentID = entity.DepartmentID,
                        DepartmentName = entity.DepartmentName,
                    };
                    list.Add(company);
                });
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<EmployeeInfo> GetEmployeeList(int departmentId)
        {
            var list = new List<EmployeeInfo>();

            Expression<Func<Employee, bool>> where = item => item.DepartmentID == departmentId && item.Status == 1;
            var entityList = CreateBizObject<Employee>().GetAllList(where);
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var company = new EmployeeInfo
                    {
                        EmployeeId = entity.EmployeeID,
                        EmployeeName = entity.EmployeeName,
                    };
                    list.Add(company);
                });
            }

            return list;
        }

        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <returns></returns>
        public List<CompanyCommonInfo> GetCompanyList()
        {
            var list = new List<CompanyCommonInfo>();
            Expression<Func<Company, bool>> where = company => company.Status == 1;
            var entityList = CreateBizObject<Company>().GetAllList(where);
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var company = new CompanyCommonInfo
                    {
                        Id = entity.CompanyID,
                        Name = entity.CompanyName,
                        ShortName = entity.BriefName,
                    };
                    list.Add(company);
                });
            }
            return list;
        }

        /// <summary>
        /// 获取客服人员/维修人员 信息
        /// </summary>
        /// <returns></returns>
        public List<EmployeeCommonInfo> GetServiceUserList()
        {
            var empList = new List<EmployeeCommonInfo>();

            var query = from ep in Context.Employee
                        select new
                        {
                            ep.EmployeeID,
                            ep.EmployeeName
                        };

            var entity = query.ToList();
            if (entity != null)
            {
                entity.ForEach(item =>
                {
                    var empInfo = new EmployeeCommonInfo();
                    empInfo.Id = item.EmployeeID;
                    empInfo.Name = item.EmployeeName;
                    empList.Add(empInfo);
                });
            }
            return empList;
        }
    }
}
