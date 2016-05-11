using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Chart
{
    /// <summary>
    /// 饼图数据
    /// </summary>
    public class PieChartDataItem
    {

        /// <summary>
        /// 标签
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public decimal value { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// 高亮颜色
        /// </summary>
        public string highlight { get; set; }
    }
}
