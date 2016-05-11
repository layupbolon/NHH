using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Company
{
    /// <summary>
    /// 公司列表
    /// </summary>
    public class CompanyListModel : BaseModel
    {
        /// <summary>
        /// 公司列表
        /// </summary>
        public List<CompanyInfo> CompanyList { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }
}
