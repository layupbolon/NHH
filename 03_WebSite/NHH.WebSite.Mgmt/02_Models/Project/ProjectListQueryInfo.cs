using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 项目列表查询信息
    /// </summary>
    public class ProjectListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 项目列表查询信息
        /// </summary>
        public ProjectListQueryInfo()
        {
            OrderBy = "ProjectName";
        }

        private string projectName = "";

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 地区ID
        /// </summary>
        public int? RegionId { get; set; }

        /// <summary>
        /// 省ID
        /// </summary>
        public int? ProvinceId { get;set; }

        /// <summary>
        /// 市ID
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// 所属阶段
        /// </summary>
        public int? Stage { get; set; }

    }
}
