using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    public class ComplaintLogInfo
    {
        public int LogID { get; set; }

        public int ComplaintID { get; set; }

        public DateTime LogTime { get; set; }

        public string LogText { get; set; }
    }
}
