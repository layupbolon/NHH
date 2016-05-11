using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Merchant;
using NHH.Service.Merchant.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Merchant
{
    /// <summary>
    /// 商户商铺销售实现类
    /// </summary>
    public class MerchantRevenueService : NHHService<NHHEntities>, IMerchantRevenueService
    {
        /// <summary>
        /// 商户商铺销售统计
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public MerchantRevenueReportModel GetRevenueReport(MerchantRevenueListQueryInfo queryInfo)
        {
            #region 30天限制
            if (!queryInfo.FromDate.HasValue)
            {
                queryInfo.FromDate = DateTime.Now.AddDays(-15);
            }
            if (!queryInfo.ToDate.HasValue)
            {
                queryInfo.ToDate = DateTime.Now;
            }
            var ts = queryInfo.ToDate.Value - queryInfo.FromDate.Value;
            if (ts.TotalDays > 30)
            {
                queryInfo.ToDate = queryInfo.FromDate.Value.AddMonths(1);
            }
            #endregion
            var model = new MerchantRevenueReportModel();
            model.QueryInfo = queryInfo;
            model.FloorList = new List<MerchantRevenueReportFloorItem>();
            #region 业态列表
            var bizTypeList = (from bt in Context.BizType
                               where bt.Status == 1
                               select new
                               {
                                   bt.BizTypeID,
                                   bt.BizTypeName,
                               }).ToList();
            #endregion

            #region 日期列表
            var dateList = new List<DateTime>();
            var dayCount = (queryInfo.ToDate.Value - queryInfo.FromDate.Value).TotalDays;
            var day = queryInfo.FromDate.Value;
            for (var dayNum = 0; dayNum <= dayCount; dayNum++)
            {
                dateList.Add(day);
                day = day.AddDays(1);
            }
            #endregion

            #region 楼层列表
            var entityList = (from f in Context.Project_Floor
                              join b in Context.Project_Building on f.BuildingID equals b.BuildingID
                              where f.Status == 1 && f.BuildingID == queryInfo.BuildingId
                              select new
                              {
                                  b.BuildingName,
                                  f.FloorID,
                                  f.FloorName,
                              }).ToList();
            #endregion

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var floor = new MerchantRevenueReportFloorItem
                    {
                        FloorId = entity.FloorID,
                        FloorName = string.Format("{0}{1}", entity.BuildingName, entity.FloorName),
                    };
                    floor.BizTypeList = new List<MerchantRevenueReportBizTypeItem>();
                    bizTypeList.ForEach(bizType =>
                    {
                        var bizTypeItem = new MerchantRevenueReportBizTypeItem
                        {
                            BizTypeId = bizType.BizTypeID,
                            BizTypeName = bizType.BizTypeName,
                            StoreList = GetRevenueCountStoreList(floor.FloorId, bizType.BizTypeID, dateList, queryInfo),
                        };
                        //商铺列表
                        if (bizTypeItem.StoreList.Count > 0)
                        {
                            //业态小计
                            bizTypeItem.BizTypeCount = new MerchantRevenueReportBizTypeCount();
                            bizTypeItem.BizTypeCount.DateList = new List<MerchantRevenueReportDateCount>();
                            foreach (var date in dateList)
                            {
                                var bizTypeDate = new MerchantRevenueReportDateCount
                                {
                                    Date = date,
                                    TotalRevenue = 0,
                                };
                                //商铺业态每天小计
                                bizTypeItem.StoreList.ForEach(storeItem =>
                                {
                                    bizTypeDate.TotalRevenue += (from r in storeItem.RevenueList
                                                                 where (r.Date - date).TotalDays == 0
                                                                 select r.Revenue).Sum();
                                });

                                bizTypeItem.BizTypeCount.DateList.Add(bizTypeDate);
                            }
                            //业态总小计
                            bizTypeItem.BizTypeCount.TotalRevenue = (from d in bizTypeItem.BizTypeCount.DateList
                                                                     select d.TotalRevenue).Sum();
                            floor.BizTypeList.Add(bizTypeItem);
                        }
                    });
                    //楼层小计
                    floor.FloorCount = new MerchantRevenueReportFloorCount();
                    floor.FloorCount.DateList = new List<MerchantRevenueReportDateCount>();
                    foreach (var date in dateList)
                    {
                        var floorDate = new MerchantRevenueReportDateCount
                        {
                            Date = date,
                            TotalRevenue = 0,
                        };
                        floor.BizTypeList.ForEach(bizTypeItem =>
                        {
                            floorDate.TotalRevenue += (from d in bizTypeItem.BizTypeCount.DateList
                                                       where (d.Date - date).TotalDays == 0
                                                       select d.TotalRevenue).Sum();
                        });
                        floor.FloorCount.DateList.Add(floorDate);
                    }
                    floor.FloorCount.TotalRevenue = (from f in floor.FloorCount.DateList
                                                     select f.TotalRevenue).Sum();
                    model.FloorList.Add(floor);
                });

                //合计信息
                model.CountInfo = new MerchantRevenueReportCountInfo();
                model.CountInfo.DateList = new List<MerchantRevenueReportDateCount>();
                foreach (var date in dateList)
                {
                    var dateCount = new MerchantRevenueReportDateCount
                    {
                        Date = date,
                        TotalRevenue = 0,
                    };
                    //每天合计
                    model.FloorList.ForEach(floorItem =>
                    {
                        dateCount.TotalRevenue += (from d in floorItem.FloorCount.DateList
                                                   where (d.Date - date).TotalDays == 0
                                                   select d.TotalRevenue).Sum();
                    });

                    model.CountInfo.DateList.Add(dateCount);
                }
                //总合计
                model.CountInfo.TotalRevenue = (from d in model.CountInfo.DateList
                                                select d.TotalRevenue).Sum();
            }
            return model;
        }

        /// <summary>
        /// 获取商铺列表
        /// </summary>
        /// <param name="floorId"></param>
        /// <param name="bizType"></param>
        /// <param name="dateList"></param>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        private List<MerchantRevenueStoreInfo> GetRevenueCountStoreList(int floorId, int bizType, List<DateTime> dateList, MerchantRevenueListQueryInfo queryInfo)
        {
            var storeList = new List<MerchantRevenueStoreInfo>();

            var revenueCount = 0m;

            #region 商铺查询
            var query = from ms in Context.Merchant_Store
                        where ms.Status == 1 && ms.BizTypeID == bizType
                        select new
                        {
                            ms.StoreID,
                            ms.StoreName,
                            ms.RentContractID,
                        };
            //某个楼层的商铺
            query = query.Where(item => (from pu in Context.Project_Unit
                                         join cu in Context.Contract_Unit on pu.UnitID equals cu.UnitID
                                         where pu.Status == 1 && pu.FloorID == floorId && cu.ContractID == item.RentContractID
                                         select cu.UnitID).Any());
            #endregion
            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    revenueCount = 0m;
                    var store = new MerchantRevenueStoreInfo
                    {
                        StoreId = entity.StoreID,
                        StoreName = entity.StoreName,
                    };
                    #region 铺位编号
                    var projectUnitList = (from pu in Context.Project_Unit
                                           join cu in Context.Contract_Unit on pu.UnitID equals cu.UnitID
                                           where pu.Status == 1 && cu.ContractID == entity.RentContractID
                                           select pu.UnitNumber).ToArray();
                    if (projectUnitList != null)
                    {
                        store.ProjectUnitList = string.Join("、", projectUnitList);
                    }
                    #endregion

                    #region 销售数据
                    store.RevenueList = new List<MerchantRevenueItem>();
                    var revenueList = (from mr in Context.Merchant_Revenue
                                       where mr.Status == 1 && mr.StoreID == entity.StoreID && mr.StartDate >= queryInfo.FromDate && mr.StartDate <= queryInfo.ToDate.Value
                                       select new
                                       {
                                           mr.Revenue,
                                           mr.StartDate
                                       }).ToList();
                    foreach (var date in dateList)
                    {
                        var revenueItem = new MerchantRevenueItem
                        {
                            Date = date,
                            Revenue = 0,
                        };
                        var theRevenue = revenueList.Find(item => (item.StartDate - date).TotalDays == 0);
                        if (theRevenue != null)
                        {
                            revenueItem.Revenue = theRevenue.Revenue;
                        }
                        revenueCount += revenueItem.Revenue;
                        store.RevenueList.Add(revenueItem);
                    }
                    #endregion
                    //商铺合计
                    store.Revenue = revenueCount;
                    storeList.Add(store);
                });
            }
            return storeList;
        }
        
        /// <summary>
        /// 获取商户销售列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public MerchantRevenueListModel GetRevenueList(MerchantRevenueListQueryInfo queryInfo)
        {
            var model = new MerchantRevenueListModel();
            model.StoreList = new List<MerchantRevenueStoreInfo>();
            model.QueryInfo = queryInfo;
            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };

            #region 日期处理
            if (!queryInfo.FromDate.HasValue)
            {
                queryInfo.FromDate = DateTime.Now.AddMonths(-1);
            }
            if (!queryInfo.ToDate.HasValue)
            {
                queryInfo.ToDate = DateTime.Now;
            }

            var ts = queryInfo.ToDate.Value - queryInfo.FromDate.Value;
            if (ts.TotalDays > 30)
            {
                queryInfo.ToDate = queryInfo.FromDate.Value.AddMonths(1);
            }
            #endregion

            #region 查询条件

            var query = from ms in Context.Merchant_Store
                         join m in Context.Merchant on ms.MerchantID equals m.MerchantID
                         where ms.Status == 1 && m.Status == 1
                         select new
                         {
                             ms.MerchantID,
                             m.MerchantName,
                             m.BriefName,
                             ms.StoreID,
                             ms.StoreName,
                         };
            //项目
            if (queryInfo.ProjectId.HasValue)
            {
                query = query.Where(item => (from c in Context.Contract
                                               where c.MerchantID == item.MerchantID && c.ContractStatus == 3 && c.ProjectID == queryInfo.ProjectId.Value
                                               select c.ContractCode).Any());
            }
            if (queryInfo.StoreId.HasValue)
            {
                query = query.Where(m => m.StoreID == queryInfo.StoreId.Value);
            }
            if (!string.IsNullOrEmpty(queryInfo.StoreName))
            {
                query = query.Where(m => m.StoreName.Contains(queryInfo.StoreName));
            }           
            #endregion

            model.PagingInfo.TotalCount = query.Count();

            query = query.OrderByDescending(m => m.StoreID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);

            var entityList = query.ToList();

            entityList.ForEach(entity =>
            {
                var info = new MerchantRevenueStoreInfo()
                {
                    MerchantId = entity.MerchantID,
                    BriefName = entity.BriefName,
                    StoreId = entity.StoreID,
                    StoreName = entity.StoreName,
                };
                info.RevenueList = GetRevenueItemList(entity.StoreID, queryInfo);
                model.StoreList.Add(info);
            });
            return model;
        }

        /// <summary>
        /// 获取商户商铺日销售额模板
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public List<MerchantRevenueStoreInfo> GetRevenueTemplate(MerchantRevenueListQueryInfo queryInfo)
        {
            #region 30天限制
            if (!queryInfo.FromDate.HasValue)
            {
                queryInfo.FromDate = DateTime.Now.AddDays(-7);
            }
            if (!queryInfo.ToDate.HasValue)
            {
                queryInfo.ToDate = DateTime.Now;
            }
            var ts = queryInfo.ToDate.Value - queryInfo.FromDate.Value;
            if (ts.TotalDays > 30)
            {
                queryInfo.ToDate = queryInfo.FromDate.Value.AddMonths(1);
            }
            #endregion

            var list = new List<MerchantRevenueStoreInfo>();
            var query = from ms in Context.Merchant_Store
                        join bt in Context.BizType on ms.BizTypeID equals bt.BizTypeID
                        join cu in Context.Contract_Unit on ms.RentContractID equals cu.ContractID
                        join pu in Context.Project_Unit on cu.UnitID equals pu.UnitID
                        where ms.Status == 1
                        select new
                        {
                            ms.RentContractID,
                            ms.StoreID,
                            ms.StoreName,
                            ms.BizTypeID,
                            bt.BizTypeName,
                            pu.ProjectID,
                            pu.BuildingID,
                            pu.FloorID,
                            pu.UnitType,
                        };

            if (queryInfo.ProjectId.HasValue && queryInfo.ProjectId.Value > 0)
            {
                query = query.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }
            if (queryInfo.BuildingId.HasValue && queryInfo.BuildingId.Value > 0)
            {
                query = query.Where(item => item.BuildingID == queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue && queryInfo.FloorId.Value > 0)
            {
                query = query.Where(item => item.FloorID == queryInfo.FloorId.Value);
            }
            if (queryInfo.UnitType.HasValue && queryInfo.UnitType.Value > 0)
            {
                query = query.Where(item => item.UnitType == queryInfo.UnitType.Value);
            }
            if (queryInfo.BizType.HasValue && queryInfo.BizType.Value > 0)
            {
                query = query.Where(item => item.BizTypeID == queryInfo.BizType.Value);
            }

            if (!query.Any())
                return list;

            var entityList = query.ToList();

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var store = new MerchantRevenueStoreInfo
                    {
                        StoreId = entity.StoreID,
                        StoreName = entity.StoreName,
                        BizTypeName = entity.BizTypeName,
                    };
                    //铺位编号
                    var projectUnitList = (from cu in Context.Contract_Unit
                                           join pu in Context.Project_Unit on cu.UnitID equals pu.UnitID
                                           where cu.ContractID == entity.RentContractID
                                           select pu.UnitNumber).ToArray();
                    if (projectUnitList != null)
                    {
                        store.ProjectUnitList = string.Join("、", projectUnitList);
                    }
                    //每天的销售额
                    store.RevenueList = GetRevenueItemList(entity.StoreID, queryInfo);
                    list.Add(store);
                });
            }

            list.OrderBy(item => item.ProjectUnitList);
            return list;
        }

        /// <summary>
        /// 更新商户销售信息
        /// </summary>
        /// <param name="model"></param>
        public bool UpdateMerchantRevenue(MerchantRevenueDetailModel model)
        {
            var instance = CreateBizObject<Merchant_Revenue>();
            var entity = instance.GetBySysNo(model.RevenueId);
            if (entity == null)
            {
                return false;
            }
            entity.Revenue = model.Revenue;
            entity.AfterTax = entity.Revenue * (1 - entity.TaxRate);
            entity.EditDate = DateTime.Now;
            entity.EditUser = 0;
            instance.Update(entity);
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 添加商户销售数据
        /// </summary>
        /// <param name="list"></param>
        public void AddMerchantRevenue(MerchantRevenueListModel model)
        {
            if (model.StoreList == null || model.StoreList.Count == 0)
                return;

            var bll = CreateBizObject<Merchant_Revenue>();
            model.StoreList.ForEach(store =>
            {
                store.MerchantId = (from ms in Context.Merchant_Store
                                    where ms.StoreID == store.StoreId
                                    select ms.MerchantID).FirstOrDefault();

                store.RevenueList.ForEach(revenue =>
                {
                    if (revenue.Revenue <= 0)
                        return;

                    //重复则更新
                    var entity = (from mr in Context.Merchant_Revenue
                                  where mr.StoreID == store.StoreId && mr.StartDate == revenue.Date
                                  select mr).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.Revenue = revenue.Revenue;
                        entity.TaxRate = revenue.TaxRate;
                        entity.AfterTax = revenue.Revenue * (1 - revenue.TaxRate);
                        entity.EditDate = DateTime.Now;
                        entity.EditUser = model.QueryInfo.CurrentUserId;
                        bll.Update(entity);
                        this.SaveChanges();
                        return;
                    }
                    //新增
                    entity = new NHH.Entities.Merchant_Revenue
                    {
                        MerchantID = store.MerchantId,
                        StoreID = store.StoreId,
                        StartDate = revenue.Date,
                        EndDate = revenue.Date,
                        Revenue = revenue.Revenue,
                        TaxRate = revenue.TaxRate,
                        AfterTax = revenue.Revenue * (1 - revenue.TaxRate),
                        Status = 1,
                        InDate = DateTime.Now,
                        InUser = model.QueryInfo.CurrentUserId,
                        EditDate = DateTime.Now,
                        EditUser = model.QueryInfo.CurrentUserId,
                    };
                    bll.Insert(entity);
                    this.SaveChanges();
                });
            });
        }


        /// <summary>
        /// 获取单个商户销售列表
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        private List<MerchantRevenueItem> GetRevenueItemList(int storeId, MerchantRevenueListQueryInfo queryInfo)
        {
            var list = new List<MerchantRevenueItem>();

            var fromDate = queryInfo.FromDate.Value;
            while (fromDate <= queryInfo.ToDate.Value)
            {
                list.Add(new MerchantRevenueItem
                {
                    RevenueId = 0,
                    Revenue = 0,
                    TaxRate = 1,
                    AfterTax = 0,
                    Date = fromDate,
                });
                fromDate = fromDate.AddDays(1);
            }

            var query = from mr in Context.Merchant_Revenue
                        where mr.Status == 1 && mr.StoreID == storeId && mr.StartDate >= queryInfo.FromDate.Value && mr.StartDate <= queryInfo.ToDate.Value
                        select new
                        {
                            mr.RevenueID,
                            mr.Revenue,
                            mr.TaxRate,
                            mr.AfterTax,
                            mr.StartDate,
                            mr.EndDate
                        };

            var entityList = query.ToList();

            list.ForEach(item =>
            {
                var theEntity = entityList.Find(entity => (entity.StartDate - item.Date).TotalDays == 0);
                if (theEntity != null)
                {
                    item.RevenueId = theEntity.RevenueID;
                    item.Revenue = theEntity.Revenue;
                    item.TaxRate = theEntity.TaxRate;
                    item.AfterTax = theEntity.AfterTax;
                }
            });

            return list;
        }

    }
}
