using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Employee
{
    /// <summary>
    /// 员工列表
    /// </summary>
    public class EmployeeListModel : BaseModel
    {
        /// <summary>
        /// 查询信息
        /// </summary>
        public EmployeeListQueryInfo QueryInfo { get; set; }

        /// <summary>
        /// 员工列表
        /// </summary>
        public List<EmployeeInfo> EmployeeList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }

        /// <summary>
        /// 员工所在部门信息
        /// </summary>
        public DepartmentCommonInfo DeptInfo { get; set; }

        /// <summary>
        /// 所在公司信息
        /// </summary>
        public CompanyCommonInfo CompanyInfo { get; set; }
    }
}
