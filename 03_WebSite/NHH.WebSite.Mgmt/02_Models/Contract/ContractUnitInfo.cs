using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 合同铺位信息
    /// </summary>
    public class ContractUnitInfo
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public int ContractID { get; set; }

        /// <summary>
        /// 计租面积
        /// </summary>
        public decimal ContractArea { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public int ContractStatus { get; set; }

        /// <summary>
        /// 商铺列表
        /// </summary>
        public List<ProjectUnitListItem> UnitList { get; set; }
    }
}
