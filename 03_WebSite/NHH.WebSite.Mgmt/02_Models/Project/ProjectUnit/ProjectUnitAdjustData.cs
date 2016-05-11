using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.ProjectUnit
{
    /// <summary>
    /// 铺位调整数据
    /// </summary>
    public class ProjectUnitAdjustData
    {
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 新铺位ID
        /// </summary>
        public int UnitID { get; set; }

        /// <summary>
        /// 原始铺位ID
        /// </summary>
        public int[] OriginalUnitID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
