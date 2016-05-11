﻿using Newtonsoft.Json;
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
    public class BarChartModel<T>
    {
        /// <summary>
        /// 标签
        /// </summary>
        [JsonProperty("labels")]
        public List<string> labels { get; set; }

        /// <summary>
        /// 数据集
        /// </summary>
        [JsonProperty("datasets")]
        public List<BarChartDataSet<T>> datasets { get; set; }
    }
}