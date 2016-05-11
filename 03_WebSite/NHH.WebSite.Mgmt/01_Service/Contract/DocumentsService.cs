using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using Newtonsoft.Json;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Framework.Utility;
using NHH.Framework.Web;
using NHH.Framework.Wechat;
using NHH.Message.Models.Sms;
using NHH.Models.Common;
using NHH.Models.Contract;
using NHH.Models.Approve;
using NHH.Service.Approve;
using NHH.Service.Approve.IService;
using NHH.Service.Contract.IService;
using NHH.Message.Models.Wechat;

namespace NHH.Service.Contract
{
    /// <summary>
    /// 特殊单据服务
    /// </summary>
    public class DocumentsService : NHHService<NHHEntities>, IDocumentsService
    {
        /// <summary>
        /// 消息模板Key（管理平台）
        /// </summary>
        private const string MgmtMessage_Template_Key = "MerchantDocuments_AuditNotify_MgmtMessage";

        /// <summary>
        /// 唐小二端审核通过消息模板KEY
        /// </summary>
        private const string TXEMessage_Template_Key_Audit = "MerchantDocuments_AuditNotify_TangMessage";

        /// <summary>
        /// 唐小二端审核驳回消息模板KEY
        /// </summary>
        private const string TXEMessage_Template_Key_Cancel = "MerchantDocuments_CancelNotify_TangMessage";

        /// <summary>
        /// 获取特殊单据列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public DocumentsListModel GetDocumentsList(DocumentsQueryInfo queryInfo)
        {
            var model = new DocumentsListModel();
            model.QueryInfo = queryInfo;
            model.DocumentsInfos = new List<DocumentsInfo>();

            var approveProcess = from ap in Context.Approve_Process
                                 join ac in Context.Approve_Config on ap.ConfigID equals ac.ConfigID
                                 join u in Context.Sys_User on new { Approver = (Int32)ap.Approver } equals new { Approver = u.UserID }
                                 where
                                   (Int64)((Int32?)ap.Approver ?? (Int32?)0) > 0
                                 group new { ac, ap, u } by new
                                 {
                                     ac.ConfigType,
                                     ap.RefID
                                 } into g
                                 select new
                                 {
                                     ConfigType = g.Key.ConfigType == 101 ? 1 : g.Key.ConfigType == 102 ? 2 : g.Key.ConfigType == 103 ? 3 : 0,
                                     RefID = (Int32?)g.Key.RefID,
                                     //ApproverName = g.Max(p => p.u.UserName),
                                     ProcessID = (Int32?)g.Max(p => p.ap.ProcessID)
                                 };

            var query = from d in Context.Merchant_Documents
                        join mc in Context.Merchant_Store on d.MerchantStoreID equals mc.StoreID
                        join dc in Context.Dictionary on d.DocumentType equals dc.FieldValue
                        join dc2 in Context.Dictionary on d.Status equals dc2.FieldValue
                        where dc.FieldType == "DocumentType" && dc2.FieldType == "DocumentStatus"
                        select new
                        {
                            d.DocumentID,
                            d.DocumentType,
                            DocumentTypeName = dc.FieldName,
                            d.MerchantStoreID,
                            mc.StoreName,
                            d.RequestUserName,
                            d.ContactPhone,
                            d.Status,
                            DocumentStatusName = dc2.FieldName,
                            d.InUser,
                            d.InDate
                        };

            #region 查询条件

            if (queryInfo.DocumentType.HasValue)
            {
                query = query.Where(d => d.DocumentType == queryInfo.DocumentType.Value);
            }
            if (queryInfo.Status.HasValue)
            {
                query = query.Where(d => d.Status == queryInfo.Status.Value);
            }
            if (!string.IsNullOrEmpty(queryInfo.MerchantStoreName))
            {
                query = query.Where(d => d.StoreName.Contains(queryInfo.MerchantStoreName));
            }
            if (queryInfo.StartTime.HasValue)
            {
                query = query.Where(d => d.InDate >= queryInfo.StartTime.Value);
            }
            if (queryInfo.EndTime.HasValue)
            {
                query = query.Where(d => d.InDate <= queryInfo.EndTime.Value);
            }

            #endregion

            //分页信息
            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page ?? 1;
            model.PagingInfo.TotalCount = query.Count();

            query = query.OrderByDescending(d => d.DocumentID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
            var entityList = query.ToList();
            if (!entityList.Any())
            {
                return model;
            }

            entityList.ForEach(entity =>
            {
                model.DocumentsInfos.Add(new DocumentsInfo()
                {
                    DocumentID = entity.DocumentID,
                    DocumentType = entity.DocumentType,
                    DocumentTypeName = entity.DocumentTypeName,
                    MerchantStoreID = entity.MerchantStoreID,
                    MerchantStoreName = entity.StoreName,
                    RequestUserName = entity.RequestUserName,
                    ContactPhone = entity.ContactPhone,
                    Status = entity.Status,
                    StatusName = entity.DocumentStatusName,
                    InUser = entity.InUser,
                    InDate = entity.InDate,
                    ApproverName = (from ap in Context.Approve_Process
                                    join u in Context.Sys_User on ap.Approver equals u.UserID
                                    where
                                        ap.ProcessID ==
                                        approveProcess.Where(
                                            a => a.RefID == entity.DocumentID && a.ConfigType == entity.DocumentType)
                                            .Select(a => a.ProcessID)
                                            .FirstOrDefault()
                                    select u.UserName
                        ).FirstOrDefault()
                });
            });
            return model;
        }

        /// <summary>
        /// 获取出门申请单详细
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        public GetOutDetaiInfo GetOutDetaiInfo(int documentID, int configType = (int)ConfigTypeEnum.出门申请单)
        {
            var query = from d in Context.Merchant_Documents
                        join g in Context.Merchant_Documents_GetOutRequest on d.DocumentID equals g.DocumentID
                        join mc in Context.Merchant_Store on d.MerchantStoreID equals mc.StoreID
                        where d.DocumentID == documentID
                        select new
                        {
                            d.DocumentID,
                            d.RequestUserName,
                            d.ContactPhone,
                            d.MerchantStoreID,
                            MerchantStoreName = mc.StoreName,
                            g.GetOutTime,
                            g.Qty,
                            g.Reason,
                            g.Remark,
                            ProjectUnit = (from cu in Context.Contract_Unit
                                           join pu in Context.Project_Unit on cu.UnitID equals pu.UnitID
                                           join p in Context.Project on pu.ProjectID equals p.ProjectID
                                           join b in Context.Project_Building on pu.BuildingID equals b.BuildingID
                                           join f in Context.Project_Floor on pu.FloorID equals f.FloorID
                                           where cu.ContractID == mc.RentContractID
                                           select new ProjectUnitInfo()
                                           {
                                               UnitId = cu.UnitID,
                                               UnitNumber = pu.UnitNumber,
                                               ProjectName = p.ProjectName,
                                               FloorName = f.FloorName,
                                               BuildingName = b.BuildingName
                                           }).FirstOrDefault()
                        };

            var entity = query.FirstOrDefault();

            var model = new GetOutDetaiInfo
            {
                DocumentID = entity.DocumentID,
                RequestUserName = entity.RequestUserName,
                ContactPhone = entity.ContactPhone,
                MerchantStoreID = entity.MerchantStoreID,
                MerchantStoreName = entity.MerchantStoreName,
                GetOutTime = entity.GetOutTime,
                Qty = entity.Qty,
                Reason = entity.Reason,
                Remark = entity.Remark,
                ProjectUnitInfo = entity.ProjectUnit,
                ApproveRecordInfos = new ApproveRecordListInfo
                {
                    ApproveRecordInfos = new ApproveService().GetDocumentApproveProcess(documentID, configType)
                },
                CurrentApprover = GetCurrentApprover(documentID),
                CheckOptions = new ApproveService().GetCheckOptions(documentID, configType)
            };

            return model;
        }

        /// <summary>
        /// 获取装修申请单详细
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        public DecorateDetailInfo GetDecorateDetailInfo(int documentID, int configType = (int)ConfigTypeEnum.装修申请单)
        {
            var query = from d in Context.Merchant_Documents
                        join g in Context.Merchant_Documents_DecorateRequest on d.DocumentID equals g.DocumentID
                        join mc in Context.Merchant_Store on d.MerchantStoreID equals mc.StoreID
                        where d.DocumentID == documentID
                        select new
                        {
                            d.DocumentID,
                            d.RequestUserName,
                            d.ContactPhone,
                            d.MerchantStoreID,
                            MerchantStoreName = mc.StoreName,
                            g.DecorationCompanyName,
                            g.DecorationCompanyAddress,
                            g.PersonInCharge,
                            g.Phone,
                            g.StartDate,
                            g.EndDate,
                            g.Remark,
                            ProjectUnit = (from cu in Context.Contract_Unit
                                           join pu in Context.Project_Unit on cu.UnitID equals pu.UnitID
                                           join p in Context.Project on pu.ProjectID equals p.ProjectID
                                           join b in Context.Project_Building on pu.BuildingID equals b.BuildingID
                                           join f in Context.Project_Floor on pu.FloorID equals f.FloorID
                                           where cu.ContractID == mc.RentContractID
                                           select new ProjectUnitInfo()
                                           {
                                               UnitId = cu.UnitID,
                                               UnitNumber = pu.UnitNumber,
                                               ProjectName = p.ProjectName,
                                               FloorName = f.FloorName,
                                               BuildingName = b.BuildingName
                                           }).FirstOrDefault()
                        };

            var entity = query.FirstOrDefault();

            var model = new DecorateDetailInfo
            {
                DocumentID = entity.DocumentID,
                RequestUserName = entity.RequestUserName,
                ContactPhone = entity.ContactPhone,
                MerchantStoreID = entity.MerchantStoreID,
                MerchantStoreName = entity.MerchantStoreName,
                DecorationCompanyName = entity.DecorationCompanyName,
                DecorationCompanyAddress = entity.DecorationCompanyAddress,
                PersonInCharge = entity.PersonInCharge,
                Phone = entity.Phone,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Remark = entity.Remark,
                ProjectUnitInfo = entity.ProjectUnit,
                ApproveRecordInfos = new ApproveRecordListInfo
                {
                    ApproveRecordInfos = new ApproveService().GetDocumentApproveProcess(documentID, configType)
                },
                CurrentApprover = GetCurrentApprover(documentID),
                CheckOptions = new ApproveService().GetCheckOptions(documentID, configType)
            };

            return model;
        }

        /// <summary>
        /// 获取延时经营申请单详细
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        public DelayOperateDetailInfo GetDelayOperateDetailInfo(int documentID, int configType = (int)ConfigTypeEnum.延时经营申请单)
        {
            var query = from d in Context.Merchant_Documents
                        join g in Context.Merchant_Documents_DelayOperateRequest on d.DocumentID equals g.DocumentID
                        join mc in Context.Merchant_Store on d.MerchantStoreID equals mc.StoreID
                        where d.DocumentID == documentID
                        select new
                        {
                            d.DocumentID,
                            d.RequestUserName,
                            d.ContactPhone,
                            d.MerchantStoreID,
                            MerchantStoreName = mc.StoreName,
                            g.StartTime,
                            g.EndTime,
                            g.Reason,
                            ProjectUnit = (from cu in Context.Contract_Unit
                                           join pu in Context.Project_Unit on cu.UnitID equals pu.UnitID
                                           join p in Context.Project on pu.ProjectID equals p.ProjectID
                                           join b in Context.Project_Building on pu.BuildingID equals b.BuildingID
                                           join f in Context.Project_Floor on pu.FloorID equals f.FloorID
                                           where cu.ContractID == mc.RentContractID
                                           select new ProjectUnitInfo()
                                           {
                                               UnitId = cu.UnitID,
                                               UnitNumber = pu.UnitNumber,
                                               ProjectName = p.ProjectName,
                                               FloorName = f.FloorName,
                                               BuildingName = b.BuildingName
                                           }).FirstOrDefault()
                        };

            var entity = query.FirstOrDefault();

            var model = new DelayOperateDetailInfo
            {
                DocumentID = entity.DocumentID,
                RequestUserName = entity.RequestUserName,
                ContactPhone = entity.ContactPhone,
                MerchantStoreID = entity.MerchantStoreID,
                MerchantStoreName = entity.MerchantStoreName,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Reason = entity.Reason,
                ProjectUnitInfo = entity.ProjectUnit,
                ApproveRecordInfos = new ApproveRecordListInfo
                {
                    ApproveRecordInfos = new ApproveService().GetDocumentApproveProcess(documentID, configType)
                },
                CurrentApprover = GetCurrentApprover(documentID),
                CheckOptions = new ApproveService().GetCheckOptions(documentID, configType)
            };

            return model;
        }

        /// <summary>
        /// 添加审批意见
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        public bool Approve(ApproveModel approve)
        {
            ApproveService approveService = new ApproveService();

            using (var trans = Context.Database.BeginTransaction())
            {
                if (!approveService.UpdateApprove(approve))
                    return false;

                UpdateDocumentStatus(approve.RefID, 2);

                if (approve.Result == 1)
                {
                    if (approveService.IsApprovePass(approve))
                    {
                        //若审批全部通过，则往Merchant_Message表中插数据（唐小二端提醒）
                        AddMerchantMessage(approve.RefID, true, approve.UserID);
                        //发送微信
                        AddWechatMessage(approve.RefID, true, approve.UserID);
                        //更改业务状态为已审批
                        UpdateDocumentStatus(approve.RefID, 4);
                    }
                    else
                    {
                        //获取下一组审核人
                        List<int> nextApprovers = GetNextStepApprover(approve.ConfigType, approve.RefID, approve.GroupNum);

                        //若未全部通过，给下一组审核人发短信、智能平台提醒（往Message表(短信),Sys_User_Message表中插数据）
                        if (nextApprovers.Any())
                        {
                            nextApprovers.ForEach(userId =>
                            {
                                AddMobileMessage(userId, approve.RefID, approve.UserID);
                                AddSysMessage(userId, approve.RefID, approve.UserID);
                            });
                        }
                    }
                }
                else
                {
                    //若驳回，则往Merchant_Message表中插数据（唐小二端提醒）
                    AddMerchantMessage(approve.RefID, false, approve.UserID);
                    //发送微信
                    AddWechatMessage(approve.RefID, false, approve.UserID);
                    //更改业务状态为已驳回
                    UpdateDocumentStatus(approve.RefID, 3);
                }

                trans.Commit();
            }

            return true;
        }

        #region 私有方法

        /// <summary>
        /// 获取下一节点审核人列表
        /// </summary>
        /// <returns></returns>
        private List<int> GetNextStepApprover(int configType, int refID, int groupNum)
        {
            var query = from ac in Context.Approve_Config
                        join ap in Context.Approve_Process on ac.ConfigID equals ap.ConfigID into queryResult
                        from ap in queryResult.DefaultIfEmpty()
                        where
                            ac.ConfigType == configType && ap.RefID == refID && ap.GroupNum == groupNum
                        select new
                        {
                            ac.ConfigID,
                            ac.Step,
                            ap.ProcessID,
                            ap.Approver,
                            ap.Result
                        };

            int configID = query.Where(a => !a.Approver.HasValue).Min(a => a.ConfigID);
            int roleID = (from con in Context.Approve_Config
                          where con.ConfigID == configID
                          select con.RoleID).FirstOrDefault();

            var userIds = from ur in Context.Sys_User_Role
                          join u in Context.Sys_User on ur.Sys_User.UserID equals u.UserID
                          join e in Context.Employee on u.Employee.EmployeeID equals e.EmployeeID
                          join p in Context.Project on new { e.Company.CompanyID } equals
                              new { p.Company.CompanyID }
                          join c in Context.Contract on p.ProjectID equals c.Project.ProjectID
                          join ms in Context.Merchant_Store on new { c.ContractID } equals
                              new { ms.Contract.ContractID }
                          where
                              ms.StoreID ==
                              Context.Merchant_Documents.Where(md => md.DocumentID == refID)
                                  .Select(md => md.MerchantStoreID)
                                  .FirstOrDefault() && ur.RoleID == roleID
                          select ur.UserID;

            return userIds.ToList();
        }

        /// <summary>
        /// 更新单据状态
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private bool UpdateDocumentStatus(int documentID, int status)
        {
            var instance = CreateBizObject<Merchant_Documents>();
            var entity = instance.TopOne(a => a.DocumentID == documentID);
            entity.Status = status;
            instance.Update(entity);
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 获取单据当前审核人
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        private List<int> GetCurrentApprover(int documentID)
        {
            List<int> resultList = new List<int>();
            try
            {
                var result = (from d in Context.Merchant_Documents
                              where d.DocumentID == documentID
                              select new
                              {
                                  d.DocumentID,
                                  d.DocumentType,
                                  d.Status
                              }).FirstOrDefault();
                if (!new[] { 1, 2 }.Contains(result.Status)) return resultList;

                int configType = 0;
                switch (result.DocumentType)
                {
                    case 1:
                        configType = 101;
                        break;
                    case 2:
                        configType = 102;
                        break;
                    case 3:
                        configType = 105;
                        break;
                }

                int groupNum = (from ap in Context.Approve_Process
                                join ac in Context.Approve_Config on ap.ConfigID equals ac.ConfigID
                                where ap.RefID == documentID && ac.ConfigType == configType
                                select ap.GroupNum).Max();

                return GetNextStepApprover(configType, documentID, groupNum);
            }
            catch (Exception)
            {
                return resultList;
            }
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="documentID"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        private bool AddMobileMessage(int userID, int documentID, int currentUserID)
        {
            //获取用户手机
            var query = from u in Context.Sys_User
                        join e in Context.Employee on u.EmployeeID equals e.EmployeeID
                        where u.UserID == userID
                        select new
                        {
                            u.UserID,
                            e.Moblie
                        };

            var document =
                Context.Merchant_Documents.Where(a => a.DocumentID == documentID)
                    .Select(a => a)
                    .FirstOrDefault();

            var documentType = document.DocumentType;
            string documentTypeName = documentType == 1 ? "出门申请单" : documentType == 2 ? "装修申请单" : documentType == 3 ? "延时经营申请单" : string.Empty;

            var stroeName =
                Context.Merchant_Store.Where(a => a.StoreID == document.MerchantStoreID)
                    .Select(a => a.StoreName)
                    .FirstOrDefault();

            MerchantDocumentsAuditNotify content = new MerchantDocumentsAuditNotify(stroeName, documentTypeName);

            SmsMessage sms = new SmsMessage
            {
                TemplateCode = MerchantDocumentsAuditNotify.TemplateCode,
                SignName = "唐小二",
                Param = JsonConvert.SerializeObject(content)
            };

            var instance = CreateBizObject<Entities.Message>();
            instance.Insert(new Entities.Message
            {
                MessageType = 2, //短信
                Priority = 2,
                Recipient = query.Where(a => a.UserID == userID).Select(a => a.Moblie).FirstOrDefault(),
                Subject = string.Format("{0}提交的{1}审批短信", stroeName, documentType),
                Content = JsonConvert.SerializeObject(sms),
                Link = "",
                Status = 1, //待发送
                InDate = DateTime.Now,
                InUser = currentUserID,
                EditDate = DateTime.Now,
                EditUser = currentUserID
            });
            SaveChanges();
            return true;
        }

        /// <summary>
        /// 发送系统消息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="documentID"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        private bool AddSysMessage(int userID, int documentID, int currentUserID)
        {
            var document =
                Context.Merchant_Documents.Where(a => a.DocumentID == documentID)
                    .Select(a => a)
                    .FirstOrDefault();

            var documentType = document.DocumentType;
            string documentTypeName = documentType == 1 ? "出门申请单" : documentType == 2 ? "装修申请单" : documentType == 3 ? "延时经营申请单" : string.Empty;
            string actionName = documentType == 1 ? "GetOutDetail" : documentType == 2 ? "DecorateDetail" : documentType == 3 ? "DelayOperateDetail" : string.Empty;

            var MessageTemplate =
                Context.Message_Template.Where(a => a.TemplateKey == MgmtMessage_Template_Key)
                    .Select(a => a)
                    .FirstOrDefault();

            var stroeName =
                Context.Merchant_Store.Where(a => a.StoreID == document.MerchantStoreID)
                    .Select(a => a.StoreName)
                    .FirstOrDefault();

            var instance = CreateBizObject<Sys_User_Message>();
            instance.Insert(new Sys_User_Message()
            {
                UserID = userID,
                Subject = MessageTemplate.Title.Replace("#STORENAME#", stroeName).Replace("#DOCUMENTTYPE#", documentTypeName),
                Content = MessageTemplate.Content.Replace("#STORENAME#", stroeName).Replace("#DOCUMENTTYPE#", documentTypeName).Replace("#URL#", string.Format("/Contract/Documents/{0}?documentID={1}", actionName, documentID)),
                SourceType = 8,//特殊单据
                SourceRefID = documentID,
                Status = 1,
                InDate = DateTime.Now,
                InUser = currentUserID,
                EditDate = DateTime.Now,
                EditUser = currentUserID
            });
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 发送消息到唐小二端
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="pass">审核是否通过</param>
        /// <param name="currentUserID">审核是否通过</param>
        /// <returns></returns>
        private bool AddMerchantMessage(int documentID, bool pass,int currentUserID)
        {
            var query = from d in Context.Merchant_Documents
                        join ms in Context.Merchant_Store on d.MerchantStoreID equals ms.StoreID
                        where d.DocumentID == documentID
                        select new
                        {
                            d.DocumentType,
                            d.MerchantStoreID,
                            ms.MerchantID,
                            d.InUser,
                            d.InDate
                        };

            var MessageTemplate =
                Context.Message_Template.Where(
                    a => a.TemplateKey == (pass ? TXEMessage_Template_Key_Audit : TXEMessage_Template_Key_Cancel))
                    .Select(a => a)
                    .FirstOrDefault();

            var entity = query.FirstOrDefault();
            string documentTypeName = entity.DocumentType == 1 ? "出门申请单" : entity.DocumentType == 2 ? "装修申请单" : entity.DocumentType == 3 ? "延时经营申请单" : string.Empty;

            var instance = CreateBizObject<Merchant_Message>();
            instance.Insert(new Merchant_Message()
            {
                MerchantID = entity.MerchantID,
                StoreID = entity.MerchantStoreID,
                UserID = entity.InUser,
                Subject = MessageTemplate.Title.Replace("#DOCUMENTTYPE#", documentTypeName),
                Content =
                    MessageTemplate.Content.Replace("#INDATE#", entity.InDate.ToString())
                        .Replace("#DOCUMENTTYPE#", documentTypeName)
                        .Replace("#URL", GetURL(documentID, entity.DocumentType)),
                SourceType = 8, //特殊单据
                SourceRefID = documentID,
                Status = 1,
                InDate = DateTime.Now,
                InUser = currentUserID,
                EditDate = DateTime.Now,
                EditUser = currentUserID
            });
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 发送微信消息
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="pass"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        private bool AddWechatMessage(int documentID, bool pass,int currentUserID)
        {
            var query = from d in Context.Merchant_Documents
                        join ms in Context.Merchant_Store on d.MerchantStoreID equals ms.StoreID
                        where d.DocumentID == documentID
                        select new
                        {
                            d.DocumentType,
                            d.MerchantStoreID,
                            ms.MerchantID,
                            d.InUser,
                            d.InDate
                        };

            var entity = query.FirstOrDefault();

            var recipient = (from u in Context.Merchant_User
                             join d in Context.Merchant_Documents on u.UserID equals d.InUser
                             where d.DocumentID == documentID
                             select u.WechatOpenID).FirstOrDefault();

            if (string.IsNullOrEmpty(recipient))
                return false;

            var message = new Entities.Message()
            {
                MessageType = 3, //微信
                Priority = 2,
                Recipient = recipient,
                Subject = pass ? "特殊单据通过审批" : "特殊单据被驳回",
                Content = GetWeiChatContent(documentID, pass),
                Link = GetURL(documentID, entity.DocumentType),
                Status = 1,
                InDate = DateTime.Now,
                InUser = currentUserID
            };

            return InsertMessage(message);
        }

        /// <summary>
        /// 新增消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool InsertMessage(Entities.Message message)
        {
            var instance = CreateBizObject<NHH.Entities.Message>();
            instance.Insert(message);
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 获取微信内容
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        private string GetWeiChatContent(int documentID, bool pass)
        {
            var document =
                Context.Merchant_Documents.Where(a => a.DocumentID == documentID).Select(a => a).FirstOrDefault();

            var documentType = document.DocumentType == 1 ? "出门申请单" : document.DocumentType == 2 ? "装修申请单" : document.DocumentType == 3 ? "延时经营申请单" : string.Empty;

            var msg = new MessageData
            {
                Type = "Document",
                TemplateId = ParamManager.GetStringValue("DocumentTemplateId")
            };

            var content = new DocumentRemindMessage
            {
                first = new requestFieldDetail
                {
                    value = pass ? "特殊单据通过审批" : "特殊单据被驳回",
                    color = "#173177"
                },
                keyword1 = new requestFieldDetail
                {
                    value = pass ? "审批通过" : "申请驳回",
                    color = "#173177"
                },
                keyword2 = new requestFieldDetail
                {
                    value = documentType,
                    color = "#173177"
                },
                keyword3 = new requestFieldDetail
                {
                    value = document.InDate.ToString(CultureInfo.InvariantCulture),
                    color = "#173177"
                },
                remark = new requestFieldDetail
                {
                    value = "请点击查看结果详情>>>",
                    color = "#173177"
                }
            };

            msg.Content = JsonConvert.SerializeObject(content);

            return JsonConvert.SerializeObject(msg);
        }

        /// <summary>
        /// 获取唐小二页面URL
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="documentType">1：出门申请单  2：装修申请单</param>
        /// <returns></returns>
        private string GetURL(int documentID, int documentType)
        {
            switch (documentType)
            {
                case 1:
                    return string.Format(ParamManager.GetStringValue("DocumentOutLink"), documentID);
                case 2:
                    return string.Format(ParamManager.GetStringValue("DocumentDecoraterLink"), documentID);
                case 3:
                    return string.Format(ParamManager.GetStringValue("DocumentDelayLink"), documentID);
            }

            return "#";
        }

        #endregion
    }
}
