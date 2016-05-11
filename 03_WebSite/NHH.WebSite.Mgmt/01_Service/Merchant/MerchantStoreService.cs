using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Merchant;
using NHH.Service.Merchant.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Merchant
{
    /// <summary>
    /// 商户商铺服务
    /// </summary>
    public class MerchantStoreService : NHHService<NHHEntities>, IMerchantStoreService
    {
        /// <summary>
        /// 获取商铺名称
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public string GetStoreName(int storeId)
        {
            return (from ms in Context.Merchant_Store
                    where ms.StoreID == storeId
                    select ms.StoreName).FirstOrDefault();
        }

        /// <summary>
        /// 获取商铺列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public List<MerchantStoreInfo> GetStoreList(int merchantId)
        {
            var list = new List<MerchantStoreInfo>();
            var query = from ms in Context.Merchant_Store
                        where ms.Status == 1 && ms.MerchantID == merchantId
                        select new
                        {
                            ms.StoreID,
                            ms.StoreName,
                            ms.RentContractID,
                        };

            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    list.Add(new MerchantStoreInfo
                    {
                        StoreID = entity.StoreID,
                        StoreName = entity.StoreName,
                        RentContractID = entity.RentContractID,
                    });
                });
            }
            return list;
        }

        /// <summary>
        /// 获取商户商铺列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public MerchantStoreListModel GetMerchantStoreList(MerchantStoreListQueryInfo queryInfo)
        {
            var model = new MerchantStoreListModel();
            model.QueryInfo = queryInfo;
            model.StoreList = new List<MerchantStoreInfo>();
            model.PagingInfo = new Models.Common.PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1,
            };

            #region 查询信息
            var query = from ms in Context.Merchant_Store
                        join m in Context.Merchant on ms.MerchantID equals m.MerchantID
                        join c in Context.Contract on ms.RentContractID equals c.ContractID
                        where ms.Status == 1
                        select new
                        {
                            m.MerchantID,
                            m.MerchantName,
                            ms.StoreID,
                            ms.StoreName,
                            ms.RentArea,
                            ms.RentStartDate,
                            ms.RentEndDate,
                            ms.BrandName,
                            ms.RentContractID,
                            c.ContractCode,
                            c.ProjectID,
                            ms.BizType.BizTypeName,
                            ms.BizCategory.BizCategoryName,
                        };

            if (queryInfo.ProjectId.HasValue)
            {
                query = query.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }

            if (queryInfo.MerchantId.HasValue)
            {
                query = query.Where(item => item.MerchantID == queryInfo.MerchantId.Value);
            }

            if (!string.IsNullOrEmpty(queryInfo.MerchantName) && queryInfo.MerchantName.Length > 0)
            {
                query = query.Where(item => item.MerchantName.Contains(queryInfo.MerchantName));
            }

            if (queryInfo.StoreId.HasValue)
            {
                query = query.Where(item => item.StoreID == queryInfo.StoreId.Value);
            }
            if (!string.IsNullOrEmpty(queryInfo.StoreName) && queryInfo.StoreName.Length > 0)
            {
                query = query.Where(item => item.StoreName.Contains(queryInfo.StoreName));
            }
            #endregion

            model.PagingInfo.TotalCount = query.Count();
            var entityList = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (entityList == null)
                return model;

            entityList.ForEach(entity =>
            {
                model.StoreList.Add(new MerchantStoreInfo
                {
                    MerchantID = entity.MerchantID,
                    MerchantName = entity.MerchantName,
                    StoreID = entity.StoreID,
                    StoreName = entity.StoreName,
                    BrandName = entity.BrandName,
                    RentArea = entity.RentArea,
                    RentStartDate = entity.RentStartDate,
                    RentEndDate = entity.RentEndDate,
                    RentContractID = entity.RentContractID,
                    RentContractCode = entity.ContractCode,
                    BizCategoryName = entity.BizCategoryName,
                    BizTypeName = entity.BizTypeName,
                });
            });

            return model;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public MerchantStoreDetailModel GetDetail(int storeId)
        {
            var model = new MerchantStoreDetailModel();
            model.MerchantInfo = new MerchantInfo();
            model.MerchantInfo.MerchantTypeInfo = new Models.Common.MerchantTypeInfo();

            #region 查询信息
            var query = from ms in Context.Merchant_Store
                        join m in Context.Merchant on ms.MerchantID equals m.MerchantID
                        join c in Context.Contract on ms.RentContractID equals c.ContractID
                        where ms.Status == 1 && ms.StoreID == storeId
                        select new
                        {
                            m.MerchantID,
                            m.MerchantName,
                            m.BriefName,
                            m.MerchantType,
                            m.ContactName,
                            m.ContactPhone,
                            m.AddressLine,
                            ms.StoreID,
                            ms.StoreName,
                            ms.RentArea,
                            ms.RentStartDate,
                            ms.RentEndDate,
                            ms.BrandName,
                            ms.RentContractID,
                            c.ContractCode,
                            c.ProjectID,
                            ms.BizType.BizTypeName,
                            ms.BizCategory.BizCategoryName,
                        };
            #endregion

            var entity = query.FirstOrDefault();
            if (entity == null)
                return model;

            model.MerchantID = entity.MerchantID;
            model.MerchantName = entity.MerchantName;
            model.StoreID = entity.StoreID;
            model.StoreName = entity.StoreName;
            model.BrandName = entity.BrandName;
            model.RentArea = entity.RentArea;
            model.RentStartDate = entity.RentStartDate;
            model.RentEndDate = entity.RentEndDate;
            model.RentContractID = entity.RentContractID;
            model.RentContractCode = entity.ContractCode;
            model.BizCategoryName = entity.BizCategoryName;
            model.BizTypeName = entity.BizTypeName;

            model.MerchantInfo.BriefName = entity.BriefName;
            model.MerchantInfo.MerchantName = entity.MerchantName;
            model.MerchantInfo.MerchantType = entity.MerchantType;
            model.MerchantInfo.MerchantTypeInfo.Name = entity.MerchantType == 1 ? "公司" : "自然人";
            model.MerchantInfo.ContactName = entity.ContactName;
            model.MerchantInfo.ContactPhone = entity.ContactPhone;
            model.MerchantInfo.MerchantAddress = entity.AddressLine;
            return model;
        }

        /// <summary>
        /// 获取商户商铺列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<MerchantStoreInfo> GetMerchantStoreList(int projectId)
        {
            var list = new List<MerchantStoreInfo>();
            var query = from ms in Context.Merchant_Store
                        join m in Context.Merchant on ms.MerchantID equals m.MerchantID
                        join c in Context.Contract on ms.RentContractID equals c.ContractID
                        where ms.Status == 1 && c.ProjectID == projectId
                        select new
                        {
                            m.MerchantID,
                            m.MerchantName,
                            ms.StoreID,
                            ms.StoreName,
                            ms.RentArea,
                            ms.RentStartDate,
                            ms.RentEndDate,
                            ms.BrandName,
                        };

            var entityList = query.ToList();
            if (entityList == null)
                return list;

            entityList.ForEach(entity =>
            {
                list.Add(new MerchantStoreInfo
                {
                    MerchantID = entity.MerchantID,
                    MerchantName = entity.MerchantName,
                    StoreID = entity.StoreID,
                    StoreName = entity.StoreName,
                    BrandName = entity.BrandName,
                    RentArea = entity.RentArea,
                    RentStartDate = entity.RentStartDate,
                    RentEndDate = entity.RentEndDate,
                });
            });
            return list;
        }
    }
}
