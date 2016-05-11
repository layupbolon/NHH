using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 租约补充协议
    /// </summary>
    public class ContractSupplementaryEntity : BaseEntity
    {
        public int SupplementaryID { get; set; }

        public int ContractID { get; set; }

        public int SupplementaryType { get; set; }

        public string SignerName { get; set; }

        public string SignerIDNumber { get; set; }

        public string SignerPhone { get; set; }

        public int OwnerCompanyID { get; set; }

        public int? InvestCompanyID { get; set; }

        public int ManageCompanyID { get; set; }

        public string OperatorName { get; set; }

        public string OperatorPhone { get; set; }

        public string SupplementaryContent { get; set; }

        public DateTime SupplementaryStartDate { get; set; }

        public DateTime SupplementaryEndDate { get; set; }
    }
}
