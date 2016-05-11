using NHH.Framework.Service;
using NHH.Models.Merchant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Merchant.IService
{
    /// <summary>
    /// 商户品牌服务接口
    /// </summary>
    public interface IMerchantBrandService
    {
        /// <summary>
        /// 获取商户品牌列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        MerchantBrandListModel GetBrandList(int merchantId, int? page);

        /// <summary>
        /// 获取商户品牌详情
        /// </summary>
        /// <param name="mbId"></param>
        /// <returns></returns>
        MerchantBrandDetailModel GetBrandDetail(int mbId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        void Add(MerchantBrandDetailModel model);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        void Edit(MerchantBrandDetailModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="merchantBrandId"></param>
        MessageBag<bool> DelelteMerchantBrand(int merchantBrandId);
        /// <summary>
        ///  根据商户id返回所有其经营品牌信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        List<MerchantBrandInfo> GetBrandListAll(int merchantId);
    }
}
