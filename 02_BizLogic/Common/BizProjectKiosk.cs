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
    /// 项目多金点信息业务操作类
    /// </summary>
    public class BizProjectKiosk : BizObject<NHHEntities, Project_Kiosk>
    {
        /// <summary>
        /// 设置项目多金点信息表的主键名称
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "KioskID";
            }
        }
    }
}
