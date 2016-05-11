using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Employee
{
    /// <summary>
    /// 员工列表查询信息
    /// </summary>
    public class EmployeeListQueryInfo : QueryInfo
    {

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 所属公司名
        /// </summary>
        public string Company { get; set; }

    }
}
