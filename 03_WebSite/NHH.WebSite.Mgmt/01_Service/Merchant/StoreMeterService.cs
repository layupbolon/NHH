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
    /// 商钱计量表服务
    /// </summary>
    public class StoreMeterService : NHHService<NHHEntities>, IStoreMeterService
    {
        /// <summary>
        /// 获取商铺计量表ID
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="meterCode"></param>
        /// <returns></returns>
        public int GetMeterId(int storeId, string meterCode)
        {
            int meterId = 0;

            var query = from sm in Context.Merchant_StoreMeter
                        where sm.StoreID == storeId && sm.MeterCode == meterCode
                        select sm.MeterID;

            if (query.Any())
            {
                meterId = query.FirstOrDefault();
            }

            return meterId;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public StoreMeterListModel GetList(StoreMeterListQueryInfo queryInfo)
        {
            var model = new StoreMeterListModel();
            model.StoreList = new List<MerchantStoreInfo>();
            model.MeterList = new List<StoreMeterInfo>();
            model.QueryInfo = queryInfo;
            model.PagingInfo = new Models.Common.PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1,
            };


            #region 商铺列表
            var query1 = from ms in Context.Merchant_Store
                         join c in Context.Contract on ms.RentContractID equals c.ContractID
                         where ms.Status == 1
                         select new
                         {
                             c.ProjectID,
                             ms.StoreID,
                             ms.StoreName,
                             ms.MerchantID,
                             ms.RentContractID,
                         };

            if (queryInfo.ProjectId.HasValue)
            {
                query1 = query1.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }
            if (queryInfo.BuildingId.HasValue)
            {
                query1 = query1.Where(item => (from cu in Context.Contract_Unit
                                               join pu in Context.Project_Unit on cu.UnitID equals pu.UnitID
                                               where cu.ContractID == item.RentContractID && pu.BuildingID == queryInfo.BuildingId.Value
                                               select cu.UnitID).Any());
            }
            if (queryInfo.FloorId.HasValue)
            {
                query1 = query1.Where(item => (from cu in Context.Contract_Unit
                                               join pu in Context.Project_Unit on cu.UnitID equals pu.UnitID
                                               where cu.ContractID == item.RentContractID && pu.FloorID == queryInfo.FloorId.Value
                                               select cu.UnitID).Any());
            }
            if (!string.IsNullOrEmpty(queryInfo.StoreName) && queryInfo.StoreName.Length > 0)
            {
                query1 = query1.Where(item => item.StoreName.Contains(queryInfo.StoreName));
            }

            model.PagingInfo.TotalCount = query1.Count();
            var entityList1 = query1.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (entityList1 != null)
            {
                entityList1.ForEach(entity1 =>
                {
                    var store = new MerchantStoreInfo
                    {
                        StoreID = entity1.StoreID,
                        StoreName = entity1.StoreName,
                        MerchantID = entity1.MerchantID,
                    };
                    model.StoreList.Add(store);
                });
            }
            #endregion

            int storeId = queryInfo.StoreId.HasValue ? queryInfo.StoreId.Value : 0;

            #region 计量表列表
            var query2 = from sm in Context.Merchant_StoreMeter
                         join d in Context.Dictionary on sm.MeterType equals d.FieldValue
                         where sm.Status == 1 && d.FieldType == "MeterType" && sm.StoreID == storeId
                         select new
                         {
                             sm.StoreID,
                             sm.MeterID,
                             sm.MeterCode,
                             sm.MeterType,
                             sm.MeterAttr,
                             sm.LastReading,
                             sm.LastNumber,
                             MeterTypeName = d.FieldName
                         };
            var entityList2 = query2.ToList();
            if (entityList2 != null)
            {
                entityList2.ForEach(entity2 =>
                {
                    var meter = new StoreMeterInfo
                    {
                        StoreID = entity2.StoreID,
                        MeterID = entity2.MeterID,
                        MeterCode = entity2.MeterCode,
                        MeterAttr = entity2.MeterAttr,
                        MeterType = entity2.MeterType,
                        MeterTypeName = entity2.MeterTypeName,
                        LastReading = entity2.LastReading,
                        LastNumber = entity2.LastNumber,
                    };
                    model.MeterList.Add(meter);
                });
            }
            #endregion
            return model;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="info"></param>
        public void Add(StoreMeterInfo info)
        {
            var store = (from ms in Context.Merchant_Store
                         where ms.StoreID == info.StoreID
                         select new { ms.MerchantID, ms.StoreName }).FirstOrDefault();
            if (store == null)
                return;

            var query = from sm in Context.Merchant_StoreMeter
                        where sm.StoreID == info.StoreID && sm.MeterType == info.MeterType && sm.MeterCode == info.MeterCode
                        select sm.MeterID;
            if (query.Any())
                return;

            info.StoreName = store.StoreName;
            info.MerchantID = store.MerchantID;

            var bll = CreateBizObject<NHH.Entities.Merchant_StoreMeter>();

            var entity = new Merchant_StoreMeter
            {
                MerchantID = info.MerchantID,
                StoreID = info.StoreID,
                MeterType = info.MeterType,
                MeterCode = info.MeterCode,
                MeterAttr = info.MeterAttr,
                LastReading = DateTime.Now,
                LastNumber = 0,
                Status = 1,
                InDate = DateTime.Now,
                InUser = info.InUser,
            };
            bll.Insert(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="info"></param>
        public void Edit(StoreMeterInfo info)
        {
            var bll = CreateBizObject<NHH.Entities.Merchant_StoreMeter>();
            var entity = bll.GetBySysNo(info.MeterID);
            if (entity == null)
                return;

            entity.MeterCode = info.MeterCode;
            entity.EditDate = DateTime.Now;
            entity.EditUser = info.EditUser;

            bll.Update(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 获取计量表列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="projectId"></param>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public List<StoreMeterInfo> GetMeterList(int type, int projectId, int? buildingId)
        {
            var list = new List<StoreMeterInfo>();
            var query = from sm in Context.Merchant_StoreMeter
                        join ms in Context.Merchant_Store on sm.StoreID equals ms.StoreID
                        join m in Context.Merchant on ms.MerchantID equals m.MerchantID
                        where sm.MeterType == type
                        select new
                        {
                            m.MerchantID,
                            m.MerchantName,
                            ms.StoreID,
                            ms.StoreName,
                            sm.MeterCode,
                            sm.MeterAttr,
                            sm.LastReading,
                            sm.LastNumber,
                            ms.RentContractID,
                        };

            query = query.Where(item => (from c in Context.Contract
                                         where c.Status == 1 && c.ProjectID == projectId && c.MerchantID == item.MerchantID
                                         select c.ContractCode).Any());

            if (buildingId.HasValue)
            {
                query = query.Where(item => (from cu in Context.Contract_Unit
                                             join pu in Context.Project_Unit on cu.UnitID equals pu.UnitID
                                             where cu.Status == 1 && pu.Status == 1 && cu.ContractID == item.RentContractID && pu.BuildingID == buildingId.Value
                                             select cu.ContractUnitID
                                             ).Any());
            }

            #region 铺位列表
            var unitQuery = from pu in Context.Project_Unit
                            join cu in Context.Contract_Unit on pu.UnitID equals cu.UnitID
                            where cu.Status == 1 && pu.Status == 1
                            select new
                            {
                                pu.UnitNumber,
                                cu.ContractID,
                            };

            unitQuery = unitQuery.Where(item => (from e in query
                                                 where e.RentContractID == item.ContractID
                                                 select e.RentContractID).Any());
            var unitList = unitQuery.ToList();
            #endregion

            var entityList = query.ToList();

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var info = new StoreMeterInfo
                    {
                        MerchantID = entity.MerchantID,
                        MerchantName = entity.MerchantName,
                        StoreID = entity.StoreID,
                        StoreName = entity.StoreName,
                        MeterAttr = entity.MeterAttr,
                        MeterCode = entity.MeterCode,
                        LastNumber = entity.LastNumber,
                        LastReading = entity.LastReading,
                    };
                    //铺位编号
                    info.UnitNumber = string.Join("、", (from unit in unitList
                                                        where unit.ContractID == entity.RentContractID
                                                        select unit.UnitNumber).ToArray());
                    list.Add(info);
                });
            }

            //按铺位排序
            list = (from item in list
                    orderby item.UnitNumber
                    select item).ToList();

            return list;
        }
    }
}
