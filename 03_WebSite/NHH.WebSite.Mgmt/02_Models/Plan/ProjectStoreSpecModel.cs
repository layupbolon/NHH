using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 商户责任
    /// </summary>
    public class ProjectStoreSpecModel : ProjectUnitSpecTemplateModel
    {
        public int UnitId { get; set; }

        public int UnitSpecID { get; set; }

        /// <summary>
        /// 是否保存模板
        /// </summary>
        public bool Template { get; set; }

        /// <summary>
        /// 当前用户
        /// </summary>
        public int UserId { get; set; }
    }
}
