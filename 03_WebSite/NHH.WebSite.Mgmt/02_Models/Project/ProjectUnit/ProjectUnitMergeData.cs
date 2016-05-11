using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.ProjectUnit
{
    /// <summary>
    /// 铺位合并数据
    /// </summary>
    public class ProjectUnitMergeData
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 新铺位ID
        /// </summary>
        public int UnitID { get; set; }

        /// <summary>
        /// 新铺位编号
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// 新铺位面积
        /// </summary>
        public decimal UnitArea { get; set; }

        /// <summary>
        /// 原始铺位ID
        /// </summary>
        public int[] OriginalUnitID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public int InUser { get; set; }
    }
}
