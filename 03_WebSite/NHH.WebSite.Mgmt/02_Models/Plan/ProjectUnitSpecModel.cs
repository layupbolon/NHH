using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 交付标准
    /// </summary>
    public class ProjectUnitSpecModel : ProjectUnitSpecTemplateModel
    {
        public int UnitId { get; set; }

        public int UnitSpecID { get; set; }

        /// <summary>
        /// 交房标准
        /// </summary>
        public int Condition { get; set; }

        /// <summary>
        /// 交房标准
        /// </summary>
        public string ConditionString { get; set; }

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
