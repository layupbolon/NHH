using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    public class RepairLogInfo
    {
        public int LogID { get; set; }

        public int RepairID { get; set; }

        public DateTime LogTime { get; set; }

        public string LogText { get; set; }
    }
}
