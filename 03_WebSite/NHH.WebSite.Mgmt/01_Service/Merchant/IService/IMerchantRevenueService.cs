using NHH.Models.Merchant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Merchant.IService
{
    /// <summary>
    /// 商户商铺销售接口
    /// </summary>
    public interface IMerchantRevenueService
    {
        /// <summary>
        /// 商户商铺销售统计
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        MerchantRevenueReportModel GetRevenueReport(MerchantRevenueListQueryInfo queryInfo);

        /// <summary>
        /// 获取商户销售列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        MerchantRevenueListModel GetRevenueList(MerchantRevenueListQueryInfo queryInfo);
        
        /// <summary>
        /// 获取商户商铺日销售额模板
        /// </summary>
        /// <param name="queryInfo"></param>
        List<MerchantRevenueStoreInfo> GetRevenueTemplate(MerchantRevenueListQueryInfo queryInfo);

        /// <summary>
        /// 更新商户销售信息
        /// </summary>
        /// <param name="model"></param>
        bool UpdateMerchantRevenue(MerchantRevenueDetailModel model);

        /// <summary>
        /// 添加商户销售数据
        /// </summary>
        /// <param name="model"></param>
        void AddMerchantRevenue(MerchantRevenueListModel model);
    }
}
