using NHH.Framework.Service;
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
    /// 员工服务接口
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// 获取员工详情信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        EmployeeDetailModel GetEmployeeDetail(int employeeId);

        /// <summary>
        /// 员工按条件查找列表页面
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        EmployeeListModel GetEmployeeList(EmployeeListQueryInfo queryInfo, DepartmentCommonInfo deptInfo);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        MessageBag<bool> AddEmployee(EmployeeDetailModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="employeeId"></param>
        void DeleteEmployee(int employeeId);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void UpdateEmployee(EmployeeDetailModel model);

    }
}
