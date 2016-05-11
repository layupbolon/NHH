using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 商铺查询信息
    /// </summary>
    public class ProjectUnitQueryInfo : QueryInfo
    {
        private string unitNumber = string.Empty;
        
        /// <summary>
        /// 项目
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public int? FloorId { get; set; }

        /// <summary>
        /// 商铺编号
        /// </summary>
        public string UnitNumber
        {
            get { return unitNumber; }
            set { unitNumber = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? UnitStatus { get; set; }
    }
}
