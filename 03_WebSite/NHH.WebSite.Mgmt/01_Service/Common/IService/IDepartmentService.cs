using NHH.Models.Common.Company;
using NHH.Models.Common.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common.IService
{
    /// <summary>
    /// 部门服务接口
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// 获取公司ID
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        int GetCompanyId(int departmentId);

        /// <summary>
        /// 部门详情
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        DepartmentDetailModel GetDeptDetail(int deptId);

        /// <summary>
        /// 部门查询页面
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        DepartmentListModel GetDeptList(int companyId);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        bool AddDept(DepartmentDetailModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="deptId"></param>
        bool DeleteDept(int deptId);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateDept(DepartmentDetailModel model);

    }
}
