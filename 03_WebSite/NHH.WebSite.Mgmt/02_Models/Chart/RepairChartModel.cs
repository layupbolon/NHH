using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Chart
{
    /// <summary>
    /// 报修图表Model
    /// </summary>
    public class RepairChartModel
    {
        /// <summary>
        /// 标签
        /// </summary>
        [JsonProperty("labels")]
        public List<string> labels { get; set; }

        /// <summary>
        /// 数据集
        /// </summary>
        public List<RepairChartDataSet> datasets { get; set; }
    }

    /// <summary>
    /// 报修图表数据
    /// </summary>
    public class RepairChartDataSet : ChartDataSet
    {
        [JsonProperty("data")]
        public int[] data { get; set; } 
    }
}
