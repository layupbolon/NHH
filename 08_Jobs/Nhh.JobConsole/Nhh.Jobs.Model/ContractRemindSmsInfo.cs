using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Model
{
    public class ContractRemindSmsInfo
    {
        public int ContractId { get; set; }

        public int UserId { get; set; }

        public string Mobile { get; set; }

        public string StoreName { get; set; }

        public string ContractEndDate { get; set; }

        public string ContractCode { get; set; }

    }
}
