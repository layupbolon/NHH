using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Report
{
    /// <summary>
    /// KPI报表统计信息
    /// </summary>
    public class KpiReportCountInfo
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目总面积
        /// </summary>
        public decimal TotalArea { get; set; }

        /// <summary>
        /// 项目总铺数
        /// </summary>
        public int TotalUnit { get; set; }


        /// <summary>
        /// 已签约总面积
        /// 项目已完成签约商铺的面积数（签约面积）
        /// </summary>
        public decimal SignedTotalArea { get; set; }

        /// <summary>
        /// 已签约总铺数
        /// 项目已完成签约的商铺数
        /// </summary>
        public int SignedTotalUnit { get; set; }

        /// <summary>
        /// 统计范围内的已签约总面积
        /// 统计范围内已完成签约商铺的面积数（签约面积）
        /// </summary>
        public decimal CountSignedTotalArea { get; set; }

        /// <summary>
        /// 统计范围内的已签约总铺数
        /// 统计范围内已完成签约的商铺数
        /// </summary>
        public int CountSignedTotalUnit { get; set; }

        /// <summary>
        /// 统计范围内的已签约月总租金
        /// </summary>
        public decimal CountSignedTotalRent { get; set; }

        /// <summary>
        /// 统计总租金
        /// 统计范围内已完成签约商铺租金总和+未签约商铺计划租金总和
        /// </summary>
        public decimal CountTotalRent { get; set; }

        /// <summary>
        /// 统计总面积
        /// 统计范围内已完成签约商铺面积总和+未签约商铺计划面积总和
        /// </summary>
        public decimal CountTotalArea { get; set; }

        /// <summary>
        /// 统计总铺数
        /// 统计范围内商铺总数
        /// </summary>
        public int CountTotalUnit { get; set; }

        /// <summary>
        /// 已签约商铺占比
        /// </summary>
        public decimal SignedUnitProportion
        {
            get
            {
                if (CountTotalUnit == 0) return 0;
                return (decimal)CountSignedTotalUnit / CountTotalUnit;
            }
        }

        /// <summary>
        /// 已签约面积占比
        /// </summary>
        public decimal SignedAreaProportion
        {
            get
            {
                if (CountTotalArea == 0) return 0;
                return CountSignedTotalArea / CountTotalArea;
            }
        }

        /// <summary>
        /// 已签约租金的占比
        /// </summary>
        public decimal SignedRentProportion
        {
            get
            {
                if (CountTotalRent == 0) return 0;
                return CountSignedTotalRent / CountTotalRent;
            }
        }
    }
}
