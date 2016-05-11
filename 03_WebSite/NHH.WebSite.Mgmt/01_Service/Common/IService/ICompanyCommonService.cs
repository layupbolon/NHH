using NHH.Models.Common;
using NHH.Models.Common.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common.IService
{
    /// <summary>
    /// 公司公共服务接口
    /// </summary>
    public interface ICompanyCommonService
    {
        /// <summary>
        /// 获了公司类型列表
        /// </summary>
        /// <returns></returns>
        List<CompanyTypeInfo> GetCompanyTypeList();

        /// <summary>
        /// 获取公司基本信息列表
        /// </summary>
        /// <param name="companyType"></param>
        /// <returns></returns>
        List<CompanyCommonInfo> GetCompanyList(int companyType);

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        List<DepartmentCommonInfo> GetDepartmentList(int companyId);

        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        List<EmployeeInfo> GetEmployeeList(int departmentId);

        /// <summary>
        /// 获取公司基本信息
        /// </summary>
        /// <param name="companyType"></param>
        /// <returns></returns>
        List<CompanyCommonInfo> GetCompanyList();

        /// <summary>
        /// 获取客服人员/维修人员 信息
        /// </summary>
        /// <returns></returns>
        List<EmployeeCommonInfo> GetServiceUserList();
    }
}
