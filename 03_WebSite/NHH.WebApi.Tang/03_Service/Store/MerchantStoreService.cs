using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Framework.Utility;
using NHH.Models.Common;
using NHH.Models.Contract;
using NHH.Models.Store;
using NHH.Service.Store.IService;

namespace NHH.Service.Store
{
    public class MerchantStoreService : NHHService<NHHEntities>, IMerchantStoreService
    {
        /// <summary>
        /// 获取商户的所有店铺列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public List<MerchantStoreView> GetMerchantStoreList(int merchantId)
        {
            var model = new List<MerchantStoreView>();
            var query = from ms in Context.Merchant_Store 
                        join bt in Context.BizType
                        on ms.BizTypeID equals bt.BizTypeID
                join ct in Context.Contract
                    on ms.RentContractID equals ct.ContractID
                where ms.MerchantID == merchantId && ms.Status == 1
                select new
                {
                    ms.StoreID,
                    ms.StoreName,
                    bt.BizTypeName,
                    ct.ContractEndDate,
                    StoreLogo = ((from msi in Context.Merchant_StoreImage where msi.StoreID == ms.StoreID && msi.Status == 1 orderby msi.SeqNo ascending select msi.ImagePath).Take(1).FirstOrDefault())
                };
            var entityList = query.OrderBy(item => item.StoreID).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var item = new MerchantStoreView
                    {
                        StoreID = entity.StoreID,
                        StoreName = entity.StoreName,
                        BizTypeName = entity.BizTypeName,
                        StoreLogo = UrlHelper.GetImageUrl(entity.StoreLogo, 100),
                        ContractEndDate = entity.ContractEndDate.Value
                    };
                    model.Add(item);
                });
            }
            return model;
        }

        /// <summary>
        /// 简单的店铺信息
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public MerchantStoreView GetSimpleMerchantStoreDetail(int storeId)
        {
            var model = new MerchantStoreView();
            var instance = CreateBizObject<Merchant_Store>();
            var entity = instance.GetBySysNo(storeId);
            if (entity != null)
            {
                model.StoreID = entity.StoreID;
                model.StoreName = entity.StoreName;
                model.MerchantID = entity.MerchantID;
            }
            return model;
        }

        /// <summary>
        /// 获取指定的店铺信息
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public MerchantStoreView GetMerchantStoreDetail(int storeId)
        {
            var model = new MerchantStoreView();
            var query = from ms in Context.Merchant_Store
                        join bt in Context.BizType
                        on ms.BizTypeID equals bt.BizTypeID
                join ct in Context.Contract
                    on ms.RentContractID equals ct.ContractID
                join cu in Context.Contract_Unit
                    on ct.ContractID equals cu.ContractID
                join pu in Context.Project_Unit
                    on cu.UnitID equals pu.UnitID
                join pf in Context.Project_Floor
                    on pu.FloorID equals pf.FloorID
                join pb in Context.Project_Building
                    on pu.BuildingID equals pb.BuildingID
                join pj in Context.Project
                    on pu.ProjectID equals pj.ProjectID
                join pv in Context.Province
                    on pj.ProvinceID equals pv.ProvinceID
                join c in Context.City
                    on pj.CityID equals c.CityID
                join cpt in Context.Contract_PaymentTerms
                    on ct.ContractID equals cpt.ContractID
                where ms.StoreID == storeId && cpt.PaymentItemID == 1
                select new
                {
                    ms.StoreID,
                    ms.StoreName,
                    ct.ContractEndDate,
                    pv.ProvinceName,
                    c.CityName,
                    pj.AddressLine,
                    pb.BuildingName,
                    pf.FloorNumber,
                    pu.UnitNumber,
                    //pu.UnitAera,
                    ms.RentArea,
                    cpt.PaymentMonthlyAmount,
                    ms.MerchantID,
                    bt.BizTypeName,
                    StoreLogo = ((from msi in Context.Merchant_StoreImage where msi.StoreID == ms.StoreID && msi.Status == 1 orderby msi.SeqNo ascending select msi.ImagePath).Take(1).FirstOrDefault()),
                    BrandList = (from cb in Context.Contract_Brand
                                     join mb in Context.Merchant_Brand
                                          on cb.MerchantBrandID equals mb.MerchantBrandID
                                     join b in Context.Brand
                                         on mb.BrandID equals b.BrandID
                                     where cb.ContractID == ct.ContractID && cb.Status == 1 && mb.Status == 1
                                     select new
                                     {
                                         cb.ContractBrandID,
                                         b.BrandName,
                                         b.BrandIcon
                                     })
                };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.StoreID = entity.StoreID;
                model.StoreName = entity.StoreName;
                model.BizTypeName = entity.BizTypeName;
                model.StoreLogo = UrlHelper.GetImageUrl(entity.StoreLogo, 100);
                model.ContractEndDate = entity.ContractEndDate;
                model.StoreAddress = entity.ProvinceName + entity.CityName + entity.AddressLine + entity.BuildingName +
                                     entity.FloorNumber.ToString("##.###") + "F";
                model.StoreNumber = entity.UnitNumber;
                model.StoreArea = entity.RentArea;
                model.StoreRent = entity.PaymentMonthlyAmount == null ? 0 : entity.PaymentMonthlyAmount.Value;
                model.MerchantID = entity.MerchantID;

                //获取店铺图片列表
                model.MerchantStoreImageList = new List<MerchantStoreImageInfo>();
                model.MerchantStoreImageList = GetMerchantStoreImageList(model.StoreID, model.MerchantID);

                if (entity.BrandList != null)
                {
                    model.BrandList = new List<ContractBrandInfo>();
                    entity.BrandList.ToList()
                        .ForEach(item => model.BrandList.Add(new ContractBrandInfo
                        {
                            ContractBrandID = item.ContractBrandID,
                            BrandName = item.BrandName,
                            BrandIcon = UrlHelper.GetImageUrl(item.BrandIcon,100)
                        }));
                }
            }
            return model;
        }

        /// <summary>
        /// 获取店铺图片列表
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public List<MerchantStoreImageInfo> GetMerchantStoreImageList(int storeId, int merchantId)
        {
            var model = new List<MerchantStoreImageInfo>();
            var query = from msi in Context.Merchant_StoreImage
                where msi.StoreID == storeId && msi.MerchantID == merchantId && msi.Status == 1
                select new
                {
                    msi.ImageID,
                    msi.SeqNo,
                    msi.ImageName,
                    msi.ImagePath
                };
            var entityList = query.OrderBy(m => m.SeqNo).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var item = new MerchantStoreImageInfo
                    {
                        ImageID = entity.ImageID,
                        SeqNo = entity.SeqNo == null ? 1 : entity.SeqNo.Value,
                        ImageName = entity.ImageName,
                        ImagePath = UrlHelper.GetImageUrl(entity.ImagePath, 100),
                        ImageBigPath = UrlHelper.GetImageUrl(entity.ImagePath)
                    };
                    model.Add(item);
                });
            }
            return model;
        }

        /// <summary>
        /// 添加店铺图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public MerchantStoreImageInfo AddMerchantStoreImage(MerchantStoreImageInfo model)
        {
            var instance = CreateBizObject<Merchant_StoreImage>();
            var entity = new Merchant_StoreImage()
            {
                StoreID = model.StoreID,
                MerchantID = model.MerchantID,
                SeqNo = model.SeqNo,
                ImageName = model.ImageName,
                ImagePath = model.ImagePath,
                Status = 1,
                InDate = DateTime.Now,
                InUser = model.InUser,
                EditDate = DateTime.Now,
                EditUser = model.InUser,
            };
            instance.Insert(entity);
            this.SaveChanges();
            model.ImageID = entity.ImageID;
            if (model.ImageID > 0)
                return model;
            else
                return null;
        }

        /// <summary>
        /// 删除店铺图片（状态改为作废）
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public bool DeleteMerchantStoreImage(int imageId)
        {
            var instance = CreateBizObject<Merchant_StoreImage>();
            var entity = instance.GetBySysNo(imageId);
            if (entity != null)
            {
                entity.Status = -1;
                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更新指定店铺的名称
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="storeName"></param>
        /// <returns></returns>
        public bool UpdateMerchantStoreName(int storeId, string storeName)
        {
            var instance = CreateBizObject<Merchant_Store>();
            var entity = instance.GetBySysNo(storeId);
            if (entity != null)
            {
                entity.StoreName = storeName;

                instance.Update(entity);
                this.SaveChanges();
                return  true;
            }
            return false;
        }

        /// <summary>
        /// 根据条件获取商户营收列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public MerchantRevenueListModel GetMerchantRevenuelList(MerchantRevenueListQuery queryInfo)
        {
            var model = new MerchantRevenueListModel();
            model.MerchantRevenueInfo = new List<MerchantRevenueInfo>();
            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page;
            model.PagingInfo.PageSize = queryInfo.Size;

            var query = from mr in Context.Merchant_Revenue
                        join ms in Context.Merchant_Store on mr.StoreID equals ms.StoreID
                        where mr.Status == 1 && mr.MerchantID == queryInfo.MerchantID
                        select new
                        {
                            mr.RevenueID,
                            mr.StartDate,
                            mr.StoreID,
                            ms.StoreName,
                            mr.Revenue
                        };
            if (queryInfo.StoreID.HasValue)
                query = query.Where(item => item.StoreID == queryInfo.StoreID.Value);
            if (queryInfo.BeginTime.HasValue)
                query = query.Where(item => item.StartDate >= queryInfo.BeginTime.Value);
            if (queryInfo.EndTime.HasValue)
                query = query.Where(item => item.StartDate <= queryInfo.EndTime.Value);

            model.PagingInfo.TotalCount = query.Count();

            query = query.OrderByDescending(item => item.StartDate).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);

            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var contract = new MerchantRevenueInfo();
                    contract.RevenueID = entity.RevenueID;
                    contract.StoreID = entity.StoreID;
                    contract.StoreName = entity.StoreName;
                    contract.StartDate = entity.StartDate;
                    contract.Revenue = entity.Revenue;

                    model.MerchantRevenueInfo.Add(contract);
                });
            }
            return model;
        }

        /// <summary>
        /// 当天的营收是否填过
        /// </summary>
        /// <param name="date"></param>
        /// <returns>True|False</returns>
        public bool RevenueIsExistsDate(string date)
        {
            DateTime startDate = Convert.ToDateTime(date);
            int count = Context.Merchant_Revenue.Count(item => item.StartDate == startDate && item.Status == 1);
            return count > 0;
        }

        /// <summary>
        /// 新增商家店铺营收
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddMerchantRevenue(MerchantRevenueInfo model)
        {
            //如果当天营收已添加过则不能添加了
            if (RevenueIsExistsDate(model.StartDate.ToString("yyyy-MM-dd")))
                return false;

            var instance = CreateBizObject<Merchant_Revenue>();
            var entity = new Merchant_Revenue()
            {
                StoreID = model.StoreID,
                MerchantID = model.MerchantID,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Revenue = model.Revenue,
                Status = 1,
                InDate = DateTime.Now,
                InUser = model.InUser,
                EditDate = DateTime.Now,
                EditUser = model.InUser,
            };
            instance.Insert(entity);
            this.SaveChanges();
            model.RevenueID = entity.RevenueID;
            return model.RevenueID > 0;
        }

        /// <summary>
        /// 更新商家店铺营收
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMerchantRevenue(MerchantRevenueInfo model)
        {
            bool result = false;
            var instance = CreateBizObject<Merchant_Revenue>();
            var entity = instance.GetBySysNo(model.RevenueID);

            if (entity != null)
            {
                entity.Revenue = model.Revenue;
                entity.EditDate = DateTime.Now;
                entity.EditUser = model.EditUser;
                instance.Update(entity);
                this.SaveChanges();
                result =  true;
            }
            return result;
        }

        /// <summary>
        /// 根据店铺编号取当前店铺所在项目的管理公司编号
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public int GetManageCompanyIDByStoreID(int storeID)
        {
            int manageCompanyID = (from ms in Context.Merchant_Store
                join cu in Context.Contract_Unit on ms.RentContractID equals cu.ContractID
                join pu in Context.Project_Unit on cu.UnitID equals pu.UnitID
                join pj in Context.Project on pu.ProjectID equals pj.ProjectID
                where ms.StoreID == storeID
                select pj.ManageCompanyID).FirstOrDefault();
            return manageCompanyID;
        }
    }
}
