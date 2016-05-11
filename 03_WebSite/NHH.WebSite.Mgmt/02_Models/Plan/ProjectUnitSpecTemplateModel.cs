using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 交付标准模板
    /// </summary>
    public class ProjectUnitSpecTemplateModel
    {
        /// <summary>
        /// 所选择的模板
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// 地面
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// 顶面
        /// </summary>
        public string Ceiling { get; set; }

        /// <summary>
        /// 墙
        /// </summary>
        public string Wall { get; set; }

        /// <summary>
        /// 柱
        /// </summary>
        public string Pillar { get; set; }

        /// <summary>
        /// 楼板承重
        /// </summary>
        public string FloorBearing { get; set; }

        /// <summary>
        /// 给水
        /// </summary>
        public string WaterSupply { get; set; }

        /// <summary>
        /// 排水
        /// </summary>
        public string WaterDrain { get; set; }

        /// <summary>
        /// 店铺门面
        /// </summary>
        public string Door { get; set; }

        /// <summary>
        /// 标牌
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 电量
        /// </summary>
        public string ElectricityUsage { get; set; }

        /// <summary>
        /// 消防系统
        /// </summary>
        public string FireProtection { get; set; }

        /// <summary>
        /// 广播系统
        /// </summary>
        public string Broadcasting { get; set; }

        /// <summary>
        /// 空调系统
        /// </summary>
        public string AirCondition { get; set; }

        /// <summary>
        /// 排油烟系统
        /// </summary>
        public string Smoke { get; set; }

        /// <summary>
        /// 安防系统
        /// </summary>
        public string Security { get; set; }

        /// <summary>
        /// 综合布线
        /// </summary>
        public string Wiring { get; set; }

        /// <summary>
        /// 上下水
        /// </summary>
        public string Water { get; set; }

        /// <summary>
        /// 燃气
        /// </summary>
        public string Gas { get; set; }
    }
}
