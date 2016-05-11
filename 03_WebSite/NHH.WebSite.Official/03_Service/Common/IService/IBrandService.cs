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
        /// <param name="pagingInfo"></param>
        /// <returns></returns>
        BrandListModel GetBrandList(string brandName, PagingInfo pagingInfo);

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
