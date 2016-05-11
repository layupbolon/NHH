using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.ProjectUnit
{
    /// <summary>
    /// 铺位拆分数据
    /// </summary>
    public class ProjectUnitSplitData
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 铺位编号
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// 新铺位列表
        /// </summary>
        public UnitItem[] NewUnitList { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public int InUser { get; set; }
    }

    public class UnitItem
    {
        public string UnitCode { get; set; }

        public decimal UnitArea { get; set; }
    }
}
