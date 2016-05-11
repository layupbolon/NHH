using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Logging
{
    public class OptLogQueryInfo : QueryInfo
    {
        public string EntityName { get; set; }

        public string EntityID { get; set; }

        public string ActionType { get; set; }

        public string UserName { get; set; }

        public string Company { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
