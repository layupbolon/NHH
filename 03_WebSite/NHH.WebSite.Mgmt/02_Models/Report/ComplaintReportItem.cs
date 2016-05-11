using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
    /// <summary>
    /// 投诉报表数据
    /// </summary>
    public class ComplaintReportItem
    {
        public Int64 RowNumber { get; set; }

        public int? MinuteNum { get; set; }

        public int? ComplaintID { get; set; }

        public string RequestUserName { get; set; }

        public DateTime? RequestTime { get; set; }

        public string RequestDesc { get; set; }

        public string AcceptUserName { get; set; }

        public DateTime? AcceptTime { get; set; }

        public string ServiceUserName { get; set; }

        public DateTime? ServiceStartTime { get; set; }

        public DateTime? ServiceFinishTime { get; set; }

        public decimal? Speed { get; set; }

        public decimal? Quality { get; set; }

        public decimal? Attitude { get; set; }

        public int? MerchantID { get; set; }

        public string BriefName { get; set; }
    }
}
