using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;
namespace NHH.Service.Common.IService
{
    /// <summary>
    /// 品牌服务接口
    /// </summary>
    public interface IBrandService
    {
        /// <summary>
        /// 获取品牌详细信息
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        BrandDetailModel GetBrandDetail(int brandId);

        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <param name="brandName"></param>
        /// <returns></returns>
        List<BrandInfo> GetBrandList(string brandName);

        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        BrandListModel GetBrandList(BrandListQueryInfo queryInfo);

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddBrand(BrandDetailModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        void UpdateBrand(BrandDetailModel model);
    }
}
