using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Logging
{
    public class OptLogModel
    {

        public int OptEventLogID { get; set; }

        public string EntityName { get; set; }

        public string EntityAlias { get; set; }

        public string EntityID { get; set; }

        public string Action { get; set; }

        public string ActionAlias { get; set; }

        public string Detail { get; set; }

        public string ClientIP { get; set; }

        public DateTime EventTime { get; set; }

        public string EventUser { get; set; }

    }
}
