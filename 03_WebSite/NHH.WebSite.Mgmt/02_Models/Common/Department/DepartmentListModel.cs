using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Department
{
    /// <summary>
    /// 部门列表
    /// </summary>
    public class DepartmentListModel : BaseModel
    {
        /// <summary>
        /// 部门列表
        /// </summary>
        public List<DepartmentInfo> DepartmentList { get; set; }

        public CompanyCommonInfo CompanyInfo { get; set; }
    }
}
