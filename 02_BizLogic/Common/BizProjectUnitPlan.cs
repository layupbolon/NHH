using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Entities;
using NHH.Framework.BizLogic;

namespace NHH.Entities
{
    /// <summary>
    /// 商铺规划信息业务操作类
    /// </summary>
    public class BizProjectUnitPlan : BizObject<NHHEntities, Project_UnitPlan>
    {
        /// <summary>
        /// 获取商铺规划信息表的主键名称
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "UnitPlanID";
            }
        }
    }
}
