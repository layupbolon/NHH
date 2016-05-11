using NHH.Models.Merchant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Merchant.IService
{
    /// <summary>
    /// 商户商铺服务接口
    /// </summary>
    public interface IMerchantStoreService
    {
        string GetStoreName(int storeId);

        /// <summary>
        /// 获取商铺列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        List<MerchantStoreInfo> GetStoreList(int merchantId);

        /// <summary>
        /// 获取商户商铺列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        MerchantStoreListModel GetMerchantStoreList(MerchantStoreListQueryInfo queryInfo);

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        MerchantStoreDetailModel GetDetail(int storeId);

        /// <summary>
        /// 获取商户商铺列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        List<MerchantStoreInfo> GetMerchantStoreList(int projectId);
    }
}
