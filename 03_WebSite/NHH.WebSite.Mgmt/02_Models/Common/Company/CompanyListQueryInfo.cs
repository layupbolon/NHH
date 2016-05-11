using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Company
{
    /// <summary>
    /// 公司列表查询信息
    /// </summary>
    public class CompanyListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 公司列表查询信息
        /// </summary>
        public CompanyListQueryInfo()
        {
            OrderBy = "CompanyType";
        }

        /// <summary>
        /// 公司类型
        /// </summary>
        public int? CompanyType { get; set; }
    }
}
