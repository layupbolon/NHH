using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Chart
{
    /// <summary>
    /// 图表柱状图数据集
    /// </summary>
    public class BarChartDataSet<T>
    {
        public string label { get; set; }

        public string fillColor { get; set; }

        public string strokeColor { get; set; }

        public string highlightFill { get; set; }

        public string highlightStroke { get; set; }

        public List<T> data { get; set; }
    }
}
