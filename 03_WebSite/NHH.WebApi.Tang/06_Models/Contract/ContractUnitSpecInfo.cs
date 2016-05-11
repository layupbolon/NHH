using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 交付标准和商户责任
    /// </summary>
    public class ContractUnitSpecInfo
    {
        /// <summary>
        /// PK
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int UnitSpecID { get; set; }

        /// <summary>
        /// 租约ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ContractID { get; set; }

        /// <summary>
        /// 铺位ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int UnitID { get; set; }

        /// <summary>
        /// 类型 1交付标准 2商户责任
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SpecType { get; set; }

        /// <summary>
        /// 类型描述
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SpecTypeDescription
        {
            get
            {
                switch (SpecType)
                {
                    case 1:
                        return "交付标准";
                    case 2:
                        return "商户责任";
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// 地面
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Floor { get; set; }
        
        /// <summary>
        /// 顶面
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Ceiling { get; set; }

        /// <summary>
        /// 墙
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Wall { get; set; }

        /// <summary>
        /// 柱
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Pillar { get; set; }

        /// <summary>
        /// 楼板承重
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FloorBearing { get; set; }

        /// <summary>
        /// 给水
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string WaterSupply { get; set; }

        /// <summary>
        /// 排水
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string WaterDrain { get; set; }

        /// <summary>
        /// 店铺门面
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Door { get; set; }

        /// <summary>
        /// 标牌
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Logo { get; set; }

        /// <summary>
        /// 电量
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ElectricityUsage { get; set; }

        /// <summary>
        /// 消防系统
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string FireProtection { get; set; }

        /// <summary>
        /// 广播系统
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Broadcasting { get; set; }

        /// <summary>
        /// 空调系统
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AirCondition { get; set; }

        /// <summary>
        /// 排油烟系统
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Smoke { get; set; }

        /// <summary>
        /// 安防系统
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Security { get; set; }

        /// <summary>
        /// 综合布线
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Wiring { get; set; }

        /// <summary>
        /// 上下水
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Water { get; set; }

        /// <summary>
        /// 燃气
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Gas { get; set; }
    }
}
