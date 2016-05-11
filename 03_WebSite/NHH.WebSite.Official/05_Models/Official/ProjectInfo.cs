using System;

namespace NHH.Models.Official
{
    public class ProjectInfo
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目简称
        /// </summary>
        public string ProjectBriefName { get; set; }

        /// <summary>
        /// 项目特色
        /// </summary>
        public string Feature { get; set; }

        /// <summary>
        /// 商业运营面积
        /// </summary>
        public decimal OperatingArea { get; set; }

        /// <summary>
        /// 商业建筑面积
        /// </summary>
        public decimal GrossArea { get; set; }

        /// <summary>
        /// 开业时间
        /// </summary>
        public DateTime OpeningDate { get; set; }

        /// <summary>
        /// 商圈人口
        /// </summary>
        public decimal Population { get; set; }

        /// <summary>
        /// 商业类型
        /// </summary>
        public string BusinessType { get; set; }

        /// <summary>
        /// 月均人流
        /// </summary>
        public decimal MonthlyHumanTraffic { get; set; }

        /// <summary>
        /// 地理位置
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 交通状况
        /// </summary>
        public string PublicTransport { get; set; }

        /// <summary>
        /// 咨询热线
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 项目概况
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public decimal Latitude { get; set; }

        public string SEO_Title { get; set; }

        public string Keywords { get; set; }

        public string Description { get; set; }
    }
}
