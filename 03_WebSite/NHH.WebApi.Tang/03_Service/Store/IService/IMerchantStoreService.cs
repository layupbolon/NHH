using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Store;

namespace NHH.Service.Store.IService
{
    public interface IMerchantStoreService
    {
        /// <summary>
        /// 获取商户的所有店铺列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        List<MerchantStoreView> GetMerchantStoreList(int merchantId);

        /// <summary>
        /// 简单的店铺信息
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        MerchantStoreView GetSimpleMerchantStoreDetail(int storeId);

        /// <summary>
        /// 获取指定的店铺信息
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        MerchantStoreView GetMerchantStoreDetail(int storeId);

        /// <summary>
        /// 获取店铺图片列表
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        List<MerchantStoreImageInfo> GetMerchantStoreImageList(int storeId, int merchantId);

        /// <summary>
        /// 添加店铺图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        MerchantStoreImageInfo AddMerchantStoreImage(MerchantStoreImageInfo model);

        /// <summary>
        /// 删除店铺图片（状态改为作废）
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        bool DeleteMerchantStoreImage(int imageId);

        /// <summary>
        /// 更新指定店铺的名称
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="storeName"></param>
        /// <returns></returns>
        bool UpdateMerchantStoreName(int storeId, string storeName);

        /// <summary>
        /// 当天的营收是否填过
        /// </summary>
        /// <param name="date"></param>
        /// <returns>True|False</returns>
        bool RevenueIsExistsDate(string date);

        /// <summary>
        /// 新增商家店铺营收
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddMerchantRevenue(MerchantRevenueInfo model);

        /// <summary>
        /// 更新商家店铺营收
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateMerchantRevenue(MerchantRevenueInfo model);

        /// <summary>
        /// 根据条件获取商户营收列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        MerchantRevenueListModel GetMerchantRevenuelList(MerchantRevenueListQuery queryInfo);

        /// <summary>
        /// 根据店铺编号取当前店铺所在项目的管理公司编号
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        int GetManageCompanyIDByStoreID(int storeID);
    }
}
