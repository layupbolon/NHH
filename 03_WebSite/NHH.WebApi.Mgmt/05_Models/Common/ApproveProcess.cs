using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    public class ApproveProcess
    {
        public int ProcessID { get; set; }
        public int ConfigID { get; set; }
        public int GroupNum { get; set; }
        public int RefID { get; set; }
        public int? Approver { get; set; }
        public DateTime? ApproveTime { get; set; }
        public string Reason { get; set; }
        public int? Result { get; set; }

        public int ConfigType { get; set; }

        public int Step { get; set; }

        public string ShowDepartmentName { get; set; }
    }
}
