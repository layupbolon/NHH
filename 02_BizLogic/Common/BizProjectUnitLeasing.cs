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
    /// 招商筹划信息业务操作类
    /// </summary>
    public class BizProjectUnitLeasing : BizObject<NHHEntities, Project_UnitLeasing>
    {
        /// <summary>
        /// 获取招商筹划信息表的主键名称
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "UnitLeasingID";
            }
        }
    }
}
