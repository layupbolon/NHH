using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using Newtonsoft.Json;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Message.Models.Sms;
using NHH.Models.Common;
using NHH.Models.Common.Enum.CommonEnums;
using NHH.Models.Documents;
using NHH.Service.Common.IService;
using NHH.Service.Documents.IService;
using NHH.Service.Store.IService;

namespace NHH.Service.Documents
{
    public class DocumentService:NHHService<NHHEntities>,IDocumentService
    {
        #region Service

        private IWorkflowService workflowService;
        public IWorkflowService WorkflowService
        {
            get
            {
                if (workflowService == null)
                {
                    workflowService = NHHServiceFactory.Instance.CreateService<IWorkflowService>();
                }
                return workflowService;
            }
        }

        private IMerchantStoreService storeService;
        public IMerchantStoreService StoreService
        {
            get
            {
                if (storeService == null)
                {
                    storeService = NHHServiceFactory.Instance.CreateService<IMerchantStoreService>();
                }
                return storeService;
            }
        }

        private ICommonService commonService;
        public ICommonService CommonService
        {
            get
            {
                if (commonService == null)
                {
                    commonService = NHHServiceFactory.Instance.CreateService<ICommonService>();
                }
                return commonService;
            }
        }
        #endregion
        /// <summary>
        /// 商户特殊单据列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public MerchantDocumentListModel GetMerchantDocumentList(MerchantDocumentListQuery queryInfo)
        {
            var model = new MerchantDocumentListModel(queryInfo.Page, queryInfo.Size);
            model.MerchantDocumentList = new List<MerchantDocumentInfo>();

            var query = from md in Context.Merchant_Documents
                join ms in Context.Merchant_Store on md.MerchantStoreID equals ms.StoreID
                join mdgor in Context.Merchant_Documents_GetOutRequest on md.DocumentID equals mdgor.DocumentID into t_mdgor
                from mdgor in t_mdgor.DefaultIfEmpty()
                join mddr in Context.Merchant_Documents_DecorateRequest on md.DocumentID equals mddr.DocumentID into t_mddr
                from mddr in t_mddr.DefaultIfEmpty()
                join mddor in Context.Merchant_Documents_DelayOperateRequest on md.DocumentID equals mddor.DocumentID into t_mddor
                from mddor in t_mddor.DefaultIfEmpty()
                join type in Context.Dictionary on md.DocumentType equals type.FieldValue into t_type
                from type in t_type.DefaultIfEmpty()
                join status in Context.Dictionary on md.Status equals status.FieldValue into t_status
                from status in t_status.DefaultIfEmpty()
                where
                    type.FieldType == "DocumentType" && status.FieldType == "DocumentStatus" &&
                    ms.MerchantID == queryInfo.MerchantID && md.DocumentType == queryInfo.DocumentType && md.Status > 0
                select new
                {
                    md.DocumentID,
                    md.Status,
                    StatusStr = status.FieldName,
                    md.MerchantStoreID,
                    ms.MerchantID,
                    md.InDate,
                    md.DocumentType,
                    mdgorRemark = mdgor.Remark,
                    mddrRemark = mddr.Remark,
                    mddorRemark = mddor.Reason,
                    md.InUser,
                    md.RequestUserName,
                    md.ContactPhone
                };
            
            //if (queryInfo.Status.HasValue)
            //{
            //    if (queryInfo.Status.Value == 2)
            //        query = query.Where(item => item.Status == 1 || item.Status == 2);
            //    else
            //    query = query.Where(item => item.Status == queryInfo.Status.Value);
            //}
            if (queryInfo.BeginTime.HasValue)
            {
                query = query.Where(item => item.InDate >= queryInfo.BeginTime.Value);
            }
            if (queryInfo.EndTime.HasValue)
            {
                query = query.Where(item => item.InDate <= queryInfo.EndTime.Value);
            }
            if (queryInfo.RoleID == 2)  //如果是操作员，只能看当前店铺的
            {
                query = query.Where(item => item.MerchantStoreID == queryInfo.StoreID);
            }

            model.PagingInfo.TotalCount = query.Count();

            query = query.OrderByDescending(item => item.InDate).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);

            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var contract = new MerchantDocumentInfo();
                    contract.DocumentID = entity.DocumentID;
                    contract.DocumentType = entity.DocumentType;
                    contract.Status = entity.Status;
                    contract.InDate = entity.InDate;
                    switch (entity.DocumentType)
                    {
                        case 1:
                            contract.Remark = entity.mdgorRemark;
                            break;
                        case 2:
                            contract.Remark = entity.mddrRemark;
                            break;
                        case 3:
                            contract.Remark = entity.mddorRemark;
                            break;
                        default:
                            contract.Remark = "";
                            break;
                    }
                    model.MerchantDocumentList.Add(contract);
                });
            }

            return model;
        }
        /// <summary>
        /// 创建装修申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddDecorateRequest(DecorateRequestInfo model)
        {

            var documentInstance = CreateBizObject<Merchant_Documents>();
            var decorateRequestInstance = CreateBizObject<Merchant_Documents_DecorateRequest>();
            using (var trans = this.Context.Database.BeginTransaction())
            {
                var documentEntity = new Merchant_Documents()
                {
                    ContactPhone = model.ContactPhone,
                    DocumentType = model.DocumentType,
                    InDate = DateTime.Now,
                    InUser = model.InUser,
                    MerchantStoreID = model.MerchantStoreID,
                    RequestUserName = model.RequestUserName,
                    Status = 1
                };
                documentInstance.Insert(documentEntity);
                this.SaveChanges();
                model.DocumentID = documentEntity.DocumentID;
                if (model.DocumentID < 1)
                    return false;

                var decorateRequestEntity = new Merchant_Documents_DecorateRequest()
                {
                    DecorationCompanyName = model.DecorationCompanyName,
                    DecorationCompanyAddress = model.DecorationCompanyAddress,
                    DocumentID = model.DocumentID,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    PersonInCharge = model.PersonInCharge,
                    Phone = model.Phone,
                    Remark = model.Remark,
                    InUser = model.InUser,
                    InDate = DateTime.Now,
                    ElectricityConsumption = model.ElectricityConsumption
                };
                decorateRequestInstance.Insert(decorateRequestEntity);
                this.SaveChanges();
                model.ID = decorateRequestEntity.ID;
                trans.Commit();
            }
            if (model.ID < 1)
                return false;

            //启用工作流程审批并发送审批人通知信息
            BeginWorkflowProcessAndSendMessage("init", ApproveConfigTypeEnum.DecorateRequest, model.DocumentID, model.MerchantStoreID);
            return true;
        }

        

        /// <summary>
        /// 创建出门申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddGetOutRequest(GetOutRequestInfo model)
        {
            var documentInstance = CreateBizObject<Merchant_Documents>();
            var getOutRequestInstance = CreateBizObject<Merchant_Documents_GetOutRequest>();
            using (var trans = this.Context.Database.BeginTransaction())
            {
                var documentEntity = new Merchant_Documents()
                {
                    ContactPhone = model.ContactPhone,
                    DocumentType = model.DocumentType,
                    InDate = DateTime.Now,
                    InUser = model.InUser,
                    MerchantStoreID = model.MerchantStoreID,
                    RequestUserName = model.RequestUserName,
                    Status = 1
                };
                documentInstance.Insert(documentEntity);
                this.SaveChanges();
                model.DocumentID = documentEntity.DocumentID;
                if (model.DocumentID < 1)
                    return false;

                var getOutRequestEntity = new Merchant_Documents_GetOutRequest()
                {
                    DocumentID = model.DocumentID,
                    GetOutTime = model.GetOutTime,
                    Qty = model.Qty,
                    Reason = model.Reason,
                    Remark = model.Remark,
                    InUser = model.InUser,
                    InDate = DateTime.Now,
                    IsTemporary = model.IsTemporary,
                    ReturnTime = model.ReturnTime
                };
                getOutRequestInstance.Insert(getOutRequestEntity);
                this.SaveChanges();
                model.ID = getOutRequestEntity.ID;
                trans.Commit();
            }
            if (model.ID < 1)
                return false;

            //启用工作流程审批并发送审批人通知信息
            BeginWorkflowProcessAndSendMessage("init", ApproveConfigTypeEnum.GetOutRequest, model.DocumentID, model.MerchantStoreID);
            return true;
        }
        /// <summary>
        /// 创建延时运营申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddDelayOperateRequest(DelayOperateRequestInfo model)
        {

            var documentInstance = CreateBizObject<Merchant_Documents>();
            var DelayRequestInstance = CreateBizObject<Merchant_Documents_DelayOperateRequest>();
            using (var trans = this.Context.Database.BeginTransaction())
            {
                var documentEntity = new Merchant_Documents()
                {
                    ContactPhone = model.ContactPhone,
                    DocumentType = model.DocumentType,
                    InDate = DateTime.Now,
                    InUser = model.InUser,
                    MerchantStoreID = model.MerchantStoreID,
                    RequestUserName = model.RequestUserName,
                    Status = 1
                };
                documentInstance.Insert(documentEntity);
                this.SaveChanges();
                model.DocumentID = documentEntity.DocumentID;
                if (model.DocumentID < 1)
                    return false;

                var delayRequestEntity = new Merchant_Documents_DelayOperateRequest()
                {
                    DocumentID = model.DocumentID,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    Reason = model.Reason,
                    InUser = model.InUser,
                    InDate = DateTime.Now,
                    NeedAC = model.NeedAC,
                    NeedLighting = model.NeedLighting,
                    MoreHour = model.MoreHour
                };
                DelayRequestInstance.Insert(delayRequestEntity);
                this.SaveChanges();
                model.ID = delayRequestEntity.ID;
                trans.Commit();
            }
            if (model.ID < 1)
                return false;

            //启用工作流程审批并发送审批人通知信息
            BeginWorkflowProcessAndSendMessage("init", ApproveConfigTypeEnum.DelayOperateRequest, model.DocumentID, model.MerchantStoreID);
            return true;
        }

        /// <summary>
        /// 更新申请单状态
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateDocumentStatus(int documentId, int status)
        {
            var instance = CreateBizObject<Merchant_Documents>();
            var entity = instance.GetBySysNo(documentId);
            if (entity != null)
            {
                entity.Status = status;

                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更新装修申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateDecorateRequest(DecorateRequestInfo model)
        {
            var instance = CreateBizObject<Merchant_Documents_DecorateRequest>();
            var entity =
                (from mddr in Context.Merchant_Documents_DecorateRequest
                    where mddr.DocumentID == model.DocumentID
                    select mddr).FirstOrDefault();
            if (entity != null)
            {
                entity.DecorationCompanyName = model.DecorationCompanyName;
                entity.DecorationCompanyAddress = model.DecorationCompanyAddress;
                entity.PersonInCharge = model.PersonInCharge;
                entity.Phone = model.Phone;
                entity.StartDate = model.StartDate;
                entity.EndDate = model.EndDate;
                entity.Remark = model.Remark;
                entity.ElectricityConsumption = model.ElectricityConsumption;

                instance.Update(entity);
                this.SaveChanges();

                //申请单状态重置为1 未审核
                UpdateDocumentStatus(model.DocumentID, 1);

                //重置工作流程审批并发送审批人通知信息
                BeginWorkflowProcessAndSendMessage("reset", ApproveConfigTypeEnum.DecorateRequest, model.DocumentID, model.MerchantStoreID);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更新出门申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateGetOutRequest(GetOutRequestInfo model)
        {
            var instance = CreateBizObject<Merchant_Documents_GetOutRequest>();
            var entity = (from mdgor in Context.Merchant_Documents_GetOutRequest
                where mdgor.DocumentID == model.DocumentID
                select mdgor).FirstOrDefault();
            if (entity != null)
            {
                entity.GetOutTime = model.GetOutTime;
                entity.Qty = model.Qty;
                entity.Reason = model.Reason;
                entity.Remark = model.Remark;
                entity.IsTemporary = model.IsTemporary;
                entity.ReturnTime = model.ReturnTime;

                instance.Update(entity);
                this.SaveChanges();

                //申请单状态重置为1 未审核
                UpdateDocumentStatus(model.DocumentID, 1);

                //重置工作流程审批并发送审批人通知信息
                BeginWorkflowProcessAndSendMessage("reset", ApproveConfigTypeEnum.GetOutRequest, model.DocumentID, model.MerchantStoreID);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 更新延时运营申请单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateDelayOperateRequest(DelayOperateRequestInfo model)
        {
            var instance = CreateBizObject<Merchant_Documents_DelayOperateRequest>();
            var entity = (from mddor in Context.Merchant_Documents_DelayOperateRequest
                where mddor.DocumentID == model.DocumentID
                select mddor).FirstOrDefault();
            if (entity != null)
            {
                entity.StartTime = model.StartTime;
                entity.EndTime = model.EndTime;
                entity.Reason = model.Reason;
                entity.NeedAC = model.NeedAC;
                entity.NeedLighting = model.NeedLighting;
                entity.MoreHour = model.MoreHour;

                instance.Update(entity);
                this.SaveChanges();

                //申请单状态重置为1 未审核
                UpdateDocumentStatus(model.DocumentID, 1);

                //重置工作流程审批并发送审批人通知信息
                BeginWorkflowProcessAndSendMessage("reset", ApproveConfigTypeEnum.DelayOperateRequest, model.DocumentID, model.MerchantStoreID);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取装修申请单
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public DecorateRequestInfo GetDecorateRequest(int documentId)
        {
            DecorateRequestInfo model = null;
            var query = from md in Context.Merchant_Documents
                join ms in Context.Merchant_Store on md.MerchantStoreID equals ms.StoreID
                join mddr in Context.Merchant_Documents_DecorateRequest on md.DocumentID equals mddr.DocumentID
                join type in Context.Dictionary on md.DocumentType equals type.FieldValue into t_type
                from type in t_type.DefaultIfEmpty()
                join status in Context.Dictionary on md.Status equals status.FieldValue into t_status
                from status in t_status.DefaultIfEmpty()
                where
                    type.FieldType == "DocumentType" && status.FieldType == "DocumentStatus" &&
                    md.DocumentID == documentId
                select new
                {
                    mddr.ID,
                    md.DocumentID,
                    md.Status,
                    StatusStr = status.FieldName,
                    md.MerchantStoreID,
                    ms.MerchantID,
                    ms.StoreName,
                    md.DocumentType,
                    md.InDate,
                    md.InUser,
                    md.RequestUserName,
                    md.ContactPhone,
                    mddr.DecorationCompanyName,
                    mddr.DecorationCompanyAddress,
                    mddr.PersonInCharge,
                    mddr.Phone,
                    mddr.StartDate,
                    mddr.EndDate,
                    mddr.Remark,
                    mddr.ElectricityConsumption,
                    UnitNumber = (from ct in Context.Contract
                        join cu in Context.Contract_Unit
                            on ct.ContractID equals cu.ContractID
                        join pu in Context.Project_Unit
                            on cu.UnitID equals pu.UnitID
                        join pf in Context.Project_Floor
                            on pu.FloorID equals pf.FloorID
                        join pb in Context.Project_Building
                            on pu.BuildingID equals pb.BuildingID
                        where ct.ContractID == ms.RentContractID
                        select pb.BuildingName + " " + pf.FloorName + " " + pu.UnitNumber).FirstOrDefault(),
                    RejectReason = (from ap in Context.Approve_Process
                                    join ac in Context.Approve_Config on ap.ConfigID equals ac.ConfigID
                                    orderby ap.ProcessID descending
                                    where ac.ConfigType == (int)ApproveConfigTypeEnum.DecorateRequest && ap.RefID == md.DocumentID && ap.Result == 2
                                    select ap.Reason).FirstOrDefault()
                };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model = new DecorateRequestInfo();
                //model.ID = entity.ID;
                model.DocumentID = entity.DocumentID;
                model.Status = entity.Status;
                model.StatusStr = entity.StatusStr;
                model.MerchantStoreID = entity.MerchantStoreID;
                model.MerchantStoreName = entity.StoreName;
                model.UnitNumber = entity.UnitNumber;
                model.DocumentType = entity.DocumentType;
                model.InDate = entity.InDate;
                model.InUser = entity.InUser;
                model.RequestUserName = entity.RequestUserName;
                model.ContactPhone = entity.ContactPhone;
                model.DecorationCompanyName = entity.DecorationCompanyName;
                model.DecorationCompanyAddress = entity.DecorationCompanyAddress;
                model.PersonInCharge = entity.PersonInCharge;
                model.Phone = entity.Phone;
                model.StartDate = entity.StartDate;
                model.EndDate = entity.EndDate;
                model.Remark = entity.Remark;
                model.ElectricityConsumption = entity.ElectricityConsumption;
                if (entity.Status == 3)  //如果状态为已驳回，要取驳回理由
                    model.RejectReason = entity.RejectReason;
            }
            return model;
        }

        /// <summary>
        /// 获取出门申请单
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public GetOutRequestInfo GetGetOutRequest(int documentId)
        {
            GetOutRequestInfo model = null;
            var query = from md in Context.Merchant_Documents
                        join ms in Context.Merchant_Store on md.MerchantStoreID equals ms.StoreID
                        join mdgor in Context.Merchant_Documents_GetOutRequest on md.DocumentID equals mdgor.DocumentID
                        join type in Context.Dictionary on md.DocumentType equals type.FieldValue into t_type
                        from type in t_type.DefaultIfEmpty()
                        join status in Context.Dictionary on md.Status equals status.FieldValue into t_status
                        from status in t_status.DefaultIfEmpty()
                        where
                            type.FieldType == "DocumentType" && status.FieldType == "DocumentStatus" &&
                            md.DocumentID == documentId
                        select new
                        {
                            mdgor.ID,
                            md.DocumentID,
                            md.Status,
                            StatusStr = status.FieldName,
                            md.MerchantStoreID,
                            ms.MerchantID,
                            ms.StoreName,
                            md.DocumentType,
                            md.InDate,
                            md.InUser,
                            md.RequestUserName,
                            md.ContactPhone,
                            mdgor.GetOutTime,
                            mdgor.Qty,
                            mdgor.Reason,
                            mdgor.Remark,
                            mdgor.IsTemporary,
                            mdgor.ReturnTime,
                            UnitNumber = (from ct in Context.Contract
                                          join cu in Context.Contract_Unit
                                              on ct.ContractID equals cu.ContractID
                                          join pu in Context.Project_Unit
                                              on cu.UnitID equals pu.UnitID
                                          join pf in Context.Project_Floor
                                              on pu.FloorID equals pf.FloorID
                                          join pb in Context.Project_Building
                                              on pu.BuildingID equals pb.BuildingID
                                          where ct.ContractID == ms.RentContractID
                                          select pb.BuildingName + " " + pf.FloorName + " " + pu.UnitNumber).FirstOrDefault(),
                            RejectReason = (from ap in Context.Approve_Process
                                               join ac in Context.Approve_Config on ap.ConfigID equals ac.ConfigID
                                               orderby ap.ProcessID descending 
                                               where ac.ConfigType == (int)ApproveConfigTypeEnum.GetOutRequest && ap.RefID == md.DocumentID && ap.Result == 2
                                               select ap.Reason).FirstOrDefault()
                        };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model = new GetOutRequestInfo();
                //model.ID = entity.ID;
                model.DocumentID = entity.DocumentID;
                model.Status = entity.Status;
                model.StatusStr = entity.StatusStr;
                model.MerchantStoreID = entity.MerchantStoreID;
                model.MerchantStoreName = entity.StoreName;
                model.UnitNumber = entity.UnitNumber;
                model.DocumentType = entity.DocumentType;
                model.InDate = entity.InDate;
                model.InUser = entity.InUser;
                model.RequestUserName = entity.RequestUserName;
                model.ContactPhone = entity.ContactPhone;
                model.GetOutTime = entity.GetOutTime;
                model.Qty = entity.Qty;
                model.Reason = entity.Reason;
                model.Remark = entity.Remark;
                model.IsTemporary = entity.IsTemporary;
                model.ReturnTime = entity.ReturnTime;
                if (entity.Status == 3)  //如果状态为已驳回，要取驳回理由
                    model.RejectReason = entity.RejectReason;
            }
            return model;
        }

        /// <summary>
        /// 获取延时运营申请单
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public DelayOperateRequestInfo GetDelayOperateRequest(int documentId)
        {
            DelayOperateRequestInfo model = null;
            var query = from md in Context.Merchant_Documents
                        join ms in Context.Merchant_Store on md.MerchantStoreID equals ms.StoreID
                        join mddor in Context.Merchant_Documents_DelayOperateRequest on md.DocumentID equals mddor.DocumentID
                        join type in Context.Dictionary on md.DocumentType equals type.FieldValue into t_type
                        from type in t_type.DefaultIfEmpty()
                        join status in Context.Dictionary on md.Status equals status.FieldValue into t_status
                        from status in t_status.DefaultIfEmpty()
                        where
                            type.FieldType == "DocumentType" && status.FieldType == "DocumentStatus" &&
                            md.DocumentID == documentId
                        select new
                        {
                            mddor.ID,
                            md.DocumentID,
                            md.Status,
                            StatusStr = status.FieldName,
                            md.MerchantStoreID,
                            ms.MerchantID,
                            ms.StoreName,
                            md.DocumentType,
                            md.InDate,
                            md.InUser,
                            md.RequestUserName,
                            md.ContactPhone,
                            mddor.StartTime,
                            mddor.EndTime,
                            mddor.Reason,
                            mddor.NeedAC,
                            mddor.NeedLighting,
                            mddor.MoreHour,
                            UnitNumber = (from ct in Context.Contract
                                          join cu in Context.Contract_Unit
                                              on ct.ContractID equals cu.ContractID
                                          join pu in Context.Project_Unit
                                              on cu.UnitID equals pu.UnitID
                                          join pf in Context.Project_Floor
                                              on pu.FloorID equals pf.FloorID
                                          join pb in Context.Project_Building
                                              on pu.BuildingID equals pb.BuildingID
                                          where ct.ContractID == ms.RentContractID
                                          select pb.BuildingName + " " + pf.FloorName + " " + pu.UnitNumber).FirstOrDefault(),
                            RejectReason = (from ap in Context.Approve_Process
                                            join ac in Context.Approve_Config on ap.ConfigID equals ac.ConfigID
                                            orderby ap.ProcessID descending
                                            where ac.ConfigType == (int)ApproveConfigTypeEnum.DelayOperateRequest && ap.RefID == md.DocumentID && ap.Result == 2
                                            select ap.Reason).FirstOrDefault()
                        };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model = new DelayOperateRequestInfo();
                //model.ID = entity.ID;
                model.DocumentID = entity.DocumentID;
                model.Status = entity.Status;
                model.StatusStr = entity.StatusStr;
                model.MerchantStoreID = entity.MerchantStoreID;
                model.MerchantStoreName = entity.StoreName;
                model.UnitNumber = entity.UnitNumber;
                model.DocumentType = entity.DocumentType;
                model.InDate = entity.InDate;
                model.InUser = entity.InUser;
                model.RequestUserName = entity.RequestUserName;
                model.ContactPhone = entity.ContactPhone;
                model.StartTime = entity.StartTime;
                model.EndTime = entity.EndTime;
                model.Reason = entity.Reason;
                model.NeedAC = entity.NeedAC;
                model.NeedLighting = entity.NeedLighting;
                model.MoreHour = entity.MoreHour;
                if (entity.Status == 3)  //如果状态为已驳回，要取驳回理由
                    model.RejectReason = entity.RejectReason;
            }
            return model;
        }
        /// <summary>
        /// 作废特殊单据
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public bool CancelDocumentRequest(int documentId)
        {
            var instance = CreateBizObject<Merchant_Documents>();
            var entity = instance.GetBySysNo(documentId);
            if (entity != null)
            {
                entity.Status = -1;
                instance.Update(entity);
                this.SaveChanges();
                //取当前店铺所在项目的管理公司编号
                int companyID = StoreService.GetManageCompanyIDByStoreID(entity.MerchantStoreID);
                var storeInfo = StoreService.GetSimpleMerchantStoreDetail(entity.MerchantStoreID);
                string documentTypeStr = "";
                string url = "";
                ApproveConfigTypeEnum configType = new ApproveConfigTypeEnum();
                switch (entity.DocumentType)
                {
                    case (int)MerchantDocumentTypeEnum.DecorateRequest:
                        configType = ApproveConfigTypeEnum.DecorateRequest;
                        documentTypeStr = "装修申请单";
                        url = "/Contract/Documents/DecorateDetail?documentID=";
                        break;
                    case (int)MerchantDocumentTypeEnum.GetOutRequest:
                        configType = ApproveConfigTypeEnum.GetOutRequest;
                        documentTypeStr = "出门申请单";
                        url = "/Contract/Documents/GetOutDetail?documentID=";
                        break;
                    case (int)MerchantDocumentTypeEnum.DelayOperateRequest:
                        configType = ApproveConfigTypeEnum.DelayOperateRequest;
                        documentTypeStr = "延时运营申请单";
                        url = "/Contract/Documents/DelayOperateDetail?documentID=";
                        break;
                }
                //取工作流审批当前已审批人员列表
                var userList = WorkflowService.GetNextApprovers((int)configType, entity.DocumentID, companyID);
                //发给管理平台审批人员通知审批消息
                List<SysUserMessage> messageList = new List<SysUserMessage>();
                var mgmtMessageTemplate = CommonService.GetMessageTemplate("MerchantDocuments_CancelNotify_MgmtMessage");
                foreach (var user in userList)
                {
                    SysUserMessage message = new SysUserMessage()
                    {
                        UserID = user.UserID,
                        Subject = mgmtMessageTemplate.Title.Replace("#STORENAME#", storeInfo.StoreName)
                                                           .Replace("#DOCUMENTTYPE#", documentTypeStr),
                        Content = mgmtMessageTemplate.Content.Replace("#STORENAME#", storeInfo.StoreName)
                                                             .Replace("#DOCUMENTTYPE#", documentTypeStr),
                        SourceType = (int)SysUserMessageSourceTypeEnum.MerchantDocuments,
                        SourceRefID = entity.DocumentID
                    };
                    messageList.Add(message);
                }
                commonService.SendSysUserMessageList(messageList);
                //发给审批人员短信通知审批
                //拼Params的Json实体
                MerchantDocumentsCancelNotify content = new MerchantDocumentsCancelNotify(storeInfo.StoreName, documentTypeStr);
                //拼Content的Json实体
                SmsMessage sms = new SmsMessage();
                sms.TemplateCode = MerchantDocumentsCancelNotify.TemplateCode;
                sms.SignName = "唐小二";
                sms.Param = JsonConvert.SerializeObject(content);

                List<MessageInfo> SmsList = new List<MessageInfo>();
                foreach (var user in userList)
                {
                    if (!string.IsNullOrEmpty(user.Mobile))
                    {
                        MessageInfo messageInfo = new MessageInfo(
                            MessageTypeEnum.SMS,
                            2,
                            user.Mobile,
                            storeInfo.StoreName + "提交的" + documentTypeStr + "被撤回",
                            JsonConvert.SerializeObject(sms)
                            );
                        SmsList.Add(messageInfo);
                    }
                }
                CommonService.SendMessageList(SmsList);
                return true;
            }
            return false;
        }

        #region Private
        /// <summary>
        /// 启用工作流程审批并发送审批人通知信息
        /// </summary>
        /// <param name="strInitOrReset">init|reset</param>
        /// <param name="approveConfigType"></param>
        /// <param name="refID">DocumentID</param>
        /// <param name="MerchantStoreID"></param>
        private void BeginWorkflowProcessAndSendMessage(string strInitOrReset, ApproveConfigTypeEnum approveConfigType, int refID, int MerchantStoreID)
        {
            if (strInitOrReset.ToLower() == "init")
                WorkflowService.InitProcess((int)approveConfigType, refID);
            if (strInitOrReset.ToLower() == "reset")
                WorkflowService.ResetProcess((int)approveConfigType, refID);
            string url = "";
            string documentTypeStr = "";
            switch (approveConfigType)
            {
                case ApproveConfigTypeEnum.DecorateRequest:
                    url = "/Contract/Documents/DecorateDetail?documentID=";
                    documentTypeStr = "装修申请单";
                    break;
                case ApproveConfigTypeEnum.GetOutRequest:
                    url = "/Contract/Documents/GetOutDetail?documentID=";
                    documentTypeStr = "出门申请单";
                    break;
                case ApproveConfigTypeEnum.DelayOperateRequest:
                    url = "/Contract/Documents/DelayOperateDetail?documentID=";
                    documentTypeStr = "延时运营申请单";
                    break;
            }
            
            //取当前店铺所在项目的管理公司编号
            int companyID = StoreService.GetManageCompanyIDByStoreID(MerchantStoreID);
            var storeInfo = StoreService.GetSimpleMerchantStoreDetail(MerchantStoreID);
            //取工作流审批下一批次人员列表
            var userList = WorkflowService.GetNextApprovers((int)approveConfigType, refID, companyID);
            //发给管理平台审批人员通知审批消息
            List<SysUserMessage> messageList = new List<SysUserMessage>();
            var mgmtMessageTemplate = CommonService.GetMessageTemplate("MerchantDocuments_AuditNotify_MgmtMessage");
            foreach (var user in userList)
            {
                SysUserMessage message = new SysUserMessage()
                {
                    UserID = user.UserID,
                    Subject = mgmtMessageTemplate.Title.Replace("#STORENAME#", storeInfo.StoreName)
                                                       .Replace("#DOCUMENTTYPE#", documentTypeStr),
                    Content = mgmtMessageTemplate.Content.Replace("#STORENAME#", storeInfo.StoreName)
                                                         .Replace("#DOCUMENTTYPE#", documentTypeStr)
                                                         .Replace("#URL#", url + refID),
                    SourceType = (int) SysUserMessageSourceTypeEnum.MerchantDocuments,
                    SourceRefID = refID
                };
                messageList.Add(message);
            }
            commonService.SendSysUserMessageList(messageList);
            //发给审批人员短信通知审批
            //拼Params的Json实体
            MerchantDocumentsAuditNotify content = new MerchantDocumentsAuditNotify(storeInfo.StoreName, documentTypeStr);
            //拼Content的Json实体
            SmsMessage sms = new SmsMessage();
            sms.TemplateCode = MerchantDocumentsAuditNotify.TemplateCode;
            sms.SignName = "唐小二";
            sms.Param = JsonConvert.SerializeObject(content);

            List<MessageInfo> SmsList = new List<MessageInfo>();
            foreach (var user in userList)
            {
                if (!string.IsNullOrEmpty(user.Mobile))
                {
                    MessageInfo messageInfo = new MessageInfo(
                        MessageTypeEnum.SMS,
                        2,
                        user.Mobile,
                        storeInfo.StoreName + "提交的" + documentTypeStr + "审批短信",
                        JsonConvert.SerializeObject(sms)
                        );
                    SmsList.Add(messageInfo);
                }
            }
            CommonService.SendMessageList(SmsList);
        }
        #endregion Private
    }
}
