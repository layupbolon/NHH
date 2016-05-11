using NHH.Entities;
using NHH.Framework.BizLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.BizLogic.Common
{
    public class BizProject : BizObject<NHHEntities, Project>
    {
        /// <summary>
        /// 项目id
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "ProjectID";
            }
        }
       
    }
}
