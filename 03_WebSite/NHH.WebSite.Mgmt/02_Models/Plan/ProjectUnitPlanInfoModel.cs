using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 商铺筹划信息
    /// </summary>
    public class ProjectUnitPlanInfoModel : BaseModel
    {
        /// <summary>
        /// 商铺ID
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// 商铺信息
        /// </summary>
        public ProjectUnitCommonInfo UnitInfo { get; set; }
    }
}
