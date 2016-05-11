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
    /// 项目商铺信息业务操作类
    /// </summary>
    public class BizProjectUnit : BizObject<NHHEntities, Project_Unit>
    {
        /// <summary>
        /// 设置项目商铺信息表的主键名称
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "UnitID";
            }
        }
    }
}
