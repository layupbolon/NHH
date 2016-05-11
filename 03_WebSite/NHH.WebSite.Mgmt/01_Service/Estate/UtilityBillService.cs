using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Estate;
using NHH.Service.Estate.IService;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Estate
{
    public class UtilityBillService : NHHService<NHHEntities>, IUtilityBillService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public UtilityBillListModel GetList(UtilityBillQueryInfo queryInfo)
        {
            var model = new UtilityBillListModel();
            model.QueryInfo = queryInfo;
            model.UtilityBillList = new List<UtilityBillInfo>();
            model.PagingInfo = new Models.Common.PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };

            var query = from u in Context.Utility_Bill
                        join msm in Context.Merchant_StoreMeter on u.MeterID equals msm.MeterID
                        join ms in Context.Merchant_Store on msm.StoreID equals ms.StoreID
                        join m in Context.Merchant on ms.MerchantID equals m.MerchantID
                        join d in Context.Dictionary on msm.MeterType equals d.FieldValue
                        where u.Status == 1 && d.FieldType == "MeterType"
                        select new
                        {
                            u.BillID,
                            m.BriefName,
                            m.MerchantID,
                            m.MerchantName,
                            ms.StoreID,
                            ms.StoreName,
                            u.PrevNumber,
                            u.CurNumber,
                            u.UseNumber,
                            u.UnitPrice,
                            u.BillAmount,
                            u.Remark,
                            u.OperatorID,
                            u.OperatorName,
                            u.StartDate,
                            u.EndDate,
                            msm.MeterType,
                            msm.MeterCode,
                            msm.MeterAttr,
                            MeterTypeName = d.FieldName,
                        };
            if (!string.IsNullOrEmpty(queryInfo.MerchantName) && queryInfo.MerchantName.Length > 0)
            {
                query = query.Where(item => item.MerchantName.Contains(queryInfo.MerchantName) || item.BriefName.Contains(queryInfo.MerchantName));
            }

            if (queryInfo.Type.HasValue && queryInfo.Type.Value > 0)
            {
                query = query.Where(item => item.MeterType == queryInfo.Type);
            }

            if (queryInfo.StartDate.HasValue)
            {
                query = query.Where(item => item.StartDate >= queryInfo.StartDate.Value);
            }

            if (queryInfo.EndDate.HasValue)
            {
                query = query.Where(item => item.StartDate <= queryInfo.EndDate.Value);
            }

            model.PagingInfo.TotalCount = query.Count();
            var entityList = query.OrderByDescending(item => item.BillID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var info = new UtilityBillInfo()
                    {
                        MerchantID = entity.MerchantID,
                        MerchantName = entity.BriefName,
                        StoreID = entity.StoreID,
                        StoreName = entity.StoreName,
                        BillID = entity.BillID,
                        MeterCode = entity.MeterCode,
                        MeterAttr = entity.MeterAttr,
                        MeterTypeName = entity.MeterTypeName,
                        PrevNumber = entity.PrevNumber,
                        CurNumber = entity.CurNumber,
                        UseNumber = entity.UseNumber,
                        UnitPrice = entity.UnitPrice,
                        BillAmount = entity.BillAmount,
                        Remark = entity.Remark,
                        OperatorID = entity.OperatorID,
                        OperatorName = entity.OperatorName,
                        StartDate = entity.StartDate,
                        EndDate = entity.EndDate,
                    };
                    model.UtilityBillList.Add(info);
                });
            }

            return model;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="model"></param>
        public void Add(UtilityBillListModel model)
        {
            if (model == null ||
                model.UtilityBillList == null || model.UtilityBillList.Count == 0)
                return;

            var bll = CreateBizObject<Utility_Bill>();
            var meterBll = CreateBizObject<Merchant_StoreMeter>();

            model.UtilityBillList.ForEach(item =>
            {
                if (string.IsNullOrEmpty(item.MeterCode) || item.MeterCode.Length == 0)
                    return;

                //计量表编号
                item.MeterID = (from sm in Context.Merchant_StoreMeter
                                where sm.Status == 1 && sm.StoreID == item.StoreID && sm.MeterCode == item.MeterCode
                                select sm.MeterID).FirstOrDefault();

                if (item.MeterID <= 0)
                    return;

                var entity = new Utility_Bill
                {
                    MeterID = item.MeterID,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    PrevNumber = item.PrevNumber,
                    CurNumber = item.CurNumber,
                    UseNumber = item.UseNumber,
                    UnitPrice = item.UnitPrice,
                    BillAmount = item.BillAmount,
                    OperatorID = item.OperatorID,
                    OperatorName = item.OperatorName,
                    Remark = item.Remark,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = item.InUser,
                    EditDate = DateTime.Now,
                };
                bll.Insert(entity);
                this.SaveChanges();

                //更新计量表信息
                var meterEntity = meterBll.GetBySysNo(item.MeterID);
                if (meterEntity != null)
                {
                    meterEntity.LastNumber = item.CurNumber;
                    meterEntity.LastReading = item.EndDate;
                    meterBll.Update(meterEntity);
                    this.SaveChanges();
                }
            });
        }

    }
}
