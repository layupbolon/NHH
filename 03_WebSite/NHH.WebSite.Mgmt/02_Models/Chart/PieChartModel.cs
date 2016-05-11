using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Chart
{
    /// <summary>
    /// 饼图Model
    /// </summary>
    public class PieChartModel
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<PieChartDataItem> data { get; set; }
    }
}
