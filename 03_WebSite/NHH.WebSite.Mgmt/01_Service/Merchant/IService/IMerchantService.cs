using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Merchant;
using NHH.Models.Common;
using NHH.Framework.Service;
namespace NHH.Service.Merchant.IService
{
    /// <summary>
    /// 商户服务接口
    /// </summary>
    public interface IMerchantService
    {
        /// <summary>
        /// 获取商户详细信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        MerchantDetailModel GetMerchantDetail(int merchantId);

        /// <summary>
        /// 获取商户列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        BaseListModel<MerchantInfo> GetMerchantList(MerchantListQueryInfo queryInfo);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="detailModel"></param>
        /// <param name="financeModel"></param>
        /// <returns></returns>
        bool AddMerchant(MerchantDetailModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="merchantId"></param>
        void DeleteMerchant(int merchantId);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="detailModel"></param>
        /// <param name="financeModel"></param>
        void UpdateMerchant(MerchantDetailModel detailModel, MerchantFinanceModel financeModel);

        /// <summary>
        /// 获取商户类型
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        int GetMerchantType(int merchantId);
    }
}
