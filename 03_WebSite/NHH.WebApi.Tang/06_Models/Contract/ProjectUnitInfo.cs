using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 商铺信息
    /// </summary>
    public class ProjectUnitInfo
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int UnitID { get; set; }

        /// <summary>
        /// 商铺编号
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string UnitNumber { get;set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ProjectName { get; set;}

        /// <summary>
        /// 楼层
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FloorName { get; set; }

        /// <summary>
        /// 计租面积
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal UnitArea { get; set; }

        /// <summary>
        /// 商铺类型 1主力店 2次主力店 3步行街/
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string UnitType { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string UnitStatus { get; set; }

        /// <summary>
        /// 租金
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal? Rent { get; set; }

    }
}
