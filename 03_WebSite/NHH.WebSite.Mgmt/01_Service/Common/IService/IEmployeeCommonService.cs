using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common.IService
{
    /// <summary>
    /// 员工公共服务接口
    /// </summary>
    public interface IEmployeeCommonService
    {
        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<CompanyCommonInfo> GetCompanyList(int userId);

        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        List<EmployeeCommonInfo> GetEmployeeList(int companyId, int? departmentId);
    }
}
