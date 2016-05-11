using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Entities;
using NHH.Framework.BizLogic;

namespace NHH.BizLogic.Common
{
    /// <summary>
    /// 项目楼宇信息业务操作类
    /// </summary>
    public class BizProjectBuilding : BizObject<NHHEntities, Project_Building>
    {
        /// <summary>
        /// 获取项目楼宇信息表的主键名称
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "BuildingID";
            }
        }
    }
}
