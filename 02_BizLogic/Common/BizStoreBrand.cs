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
    /// 商户品牌信息业务操作类
    /// </summary>
    public class BizStore_Brand : BizObject<NHHEntities, Merchant_Brand>
    {
        /// <summary>
        /// 获取商户品牌信息表的主键名称
        /// </summary>
        protected override string PrimaryKey
        {
            get
            {
                return "StoreBrandID";
            }
        }
    }
}
