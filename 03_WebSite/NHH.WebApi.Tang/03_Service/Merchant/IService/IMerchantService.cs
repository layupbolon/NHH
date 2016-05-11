using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Merchant;
using NHH.Models.Common;
namespace NHH.Service.Merchant.IService
{
    /// <summary>
    /// 商户服务接口
    /// </summary>
    public interface IMerchantService
    {
        /// <summary>
        /// 根据店铺编号获取店铺所在的铺位列表
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        List<SelectListItemInfo> GetUnitListByStoreId(int storeId);
        /// <summary>
        /// 根据商户ID获取项目ID
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        int GetProjectByMerchantId(int merchantId);

        /// <summary>
        /// 获得精简的商家内容
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        MerchantInfo GetSimpleMerchantDetail(int merchantId);

        /// <summary>
        /// 获取商户详细信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        MerchantDetailModel GetMerchantDetail(int merchantId);

        ///// <summary>
        ///// 获取商户列表
        ///// </summary>
        ///// <param name="merchantType"></param>
        ///// <returns></returns>
        //MerchantListModel GetMerchantList(int? merchantType, PagingInfo pagingInfo);

        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="detailModel"></param>
        ///// <param name="financeModel"></param>
        ///// <returns></returns>
        //bool AddMerchant(MerchantDetailModel model);

        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="merchantId"></param>
        //void DeleteMerchant(int merchantId);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="detailModel"></param>
        /// <param name="financeModel"></param>
        bool UpdateMerchant(MerchantDetailModel detailModel, MerchantFinanceModel financeModel);

        

        

        
    }
}
