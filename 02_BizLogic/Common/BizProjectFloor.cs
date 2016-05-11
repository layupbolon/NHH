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
    /// 项目楼层信息业务操作类
    /// </summary>
    public class BizProjectFloor : BizObject<NHHEntities, Project_Floor>
    {
        /// <summary>
        /// 获取项目楼层信息表的主键名称
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "FloorID";
            }
        }
    }
}
