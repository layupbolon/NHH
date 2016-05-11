using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common
{
    /// <summary>
    /// 商户公共服务
    /// </summary>
    public class MerchantCommonService : NHHService<NHHEntities>, IMerchantCommonService
    {
        /// <summary>
        /// 获取商户列表
        /// </summary>
        /// <param name="merchantName"></param>
        /// <returns></returns>
        public List<MerchantCommonInfo> GetMerchantList(string merchantName)
        {
            var list = new List<MerchantCommonInfo>();
            #region 查询消息
            var query = from m in Context.Merchant
                        where m.Status == 1 && m.MerchantName.Contains(merchantName)
                        select new
                        {
                            m.MerchantID,
                            m.MerchantName,
                        };
            #endregion
            var entityList = query.Take(10).ToList();

            entityList.ForEach(entity =>
            {
                var listModel = new MerchantCommonInfo();
                listModel.MerchantId = entity.MerchantID;
                listModel.MerchantName = entity.MerchantName;
                list.Add(listModel);
            });
            return list;
        }

        /// <summary>
        /// 获取商铺列表
        /// </summary>
        /// <param name="storeName"></param>
        /// <returns></returns>
        public List<MerchantCommonInfo> GetStoreList(string storeName)
        {
            var list = new List<MerchantCommonInfo>();
            #region 查询消息
            var query = from ms in Context.Merchant_Store
                        where ms.Status == 1 && ms.StoreName.Contains(storeName)
                        select new
                        {
                            ms.StoreID,
                            ms.StoreName,
                        };
            #endregion
            var entityList = query.Take(10).ToList();

            entityList.ForEach(entity =>
            {
                var listModel = new MerchantCommonInfo();
                listModel.MerchantId = entity.StoreID;
                listModel.MerchantName = entity.StoreName;
                list.Add(listModel);
            });
            return list;
        }
    }
}
