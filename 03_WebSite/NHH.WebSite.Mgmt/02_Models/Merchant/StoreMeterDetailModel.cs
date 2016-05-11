using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商铺计量表详情
    /// </summary>
    public class StoreMeterDetailModel : StoreMeterInfo
    {
        private CrumbInfo _crumbInfo = new CrumbInfo();

        /// <summary>
        /// 面包屑信息
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get { return _crumbInfo; }
        }
    }
}
