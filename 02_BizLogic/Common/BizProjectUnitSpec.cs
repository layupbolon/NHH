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
    /// 商铺技术条件标准信息业务操作类
    /// </summary>
    public class BizProjectUnitSpec : BizObject<NHHEntities, Project_UnitSpec>
    {
        /// <summary>
        /// 获取商铺技术条件标准信息表的主键名称
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "UnitSpecID";
            }
        }
    }
}
