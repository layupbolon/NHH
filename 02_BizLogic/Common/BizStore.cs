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
    /// 商户信息业务操作类
    /// </summary>
    public class BizStore : BizObject<NHHEntities, Merchant_Store>
    {
        /// <summary>
        /// 获取商户信息表的主键名称
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "StoreID";
            }
        }
    }
}
