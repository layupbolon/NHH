using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Chart
{
    /// <summary>
    /// 投诉图表数据
    /// </summary>
    public class ComplaintChartDataItem
    {
        public int CompanyId { get; set; }

        public string BriefName { get; set; }

        public int WeekNum { get; set; }

        public int MinuteNum { get; set; }
    }
}
