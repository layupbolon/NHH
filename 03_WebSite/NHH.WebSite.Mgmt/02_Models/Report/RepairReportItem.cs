using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
    /// <summary>
    /// 报修报表数据
    /// </summary>
    public class RepairReportItem
    {
        public Int64 RowNumber { get; set; }

        public int? MinuteNum { get; set; }

        public int? RepairID { get; set; }

        public string RequestUserName { get; set; }

        public DateTime? RequestTime { get; set; }

        public string RequestDesc { get; set; }

        public string AcceptUserName { get; set; }

        public DateTime? AcceptTime { get; set; }

        public string RepairUserName { get; set; }

        public DateTime? RepairStartTime { get; set; }

        public DateTime? RepairFinishTime { get; set; }

        public decimal? Speed { get; set; }

        public decimal? Quality { get; set; }

        public decimal? Attitude { get; set; }

        public int? MerchantID { get; set; }

        public string BriefName { get; set; }
    }
}
