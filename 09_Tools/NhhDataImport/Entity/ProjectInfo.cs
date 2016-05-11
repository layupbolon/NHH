using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 项目信息
    /// </summary>
    public class ProjectInfo
    {
        public int ProjectID { get; set; }

        public int OwnerCompanyID { get; set; }

        public int? InvestCompanyID { get; set; }

        public int ManageCompanyID { get; set; }
    }
}
