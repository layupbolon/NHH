using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common.IService
{
    /// <summary>
    /// 商户公共服务接口
    /// </summary>
    public interface IMerchantCommonService
    {
        /// <summary>
        /// 获取商户列表
        /// </summary>
        /// <param name="merchantName"></param>
        /// <returns></returns>
        List<MerchantCommonInfo> GetMerchantList(string merchantName);

        /// <summary>
        /// 获取商铺列表
        /// </summary>
        /// <param name="storeName"></param>
        /// <returns></returns>
        List<MerchantCommonInfo> GetStoreList(string storeName);
    }
}
