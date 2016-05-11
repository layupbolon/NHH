using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 项目详情实体
    /// </summary>
    public class ProjectInfoModel : ProjectInfo
    {
        private CrumbInfo _crumbInfo = new CrumbInfo();

        /// <summary>
        /// 面包屑
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get { return _crumbInfo; }
        }

        public Nullable<decimal> SumTotalConstructionArea { get; set; }
        public string CompanyName { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public DateTime InDate { get; set; }

        public int InUser { get; set; }

        public DateTime EditDate { get; set; }

        public int EditUser { get; set; }
    }
}
