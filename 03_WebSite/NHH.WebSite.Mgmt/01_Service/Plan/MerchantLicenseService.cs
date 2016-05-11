using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Newtonsoft.Json;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Framework.Utility;
using NHH.Framework.Wechat;
using NHH.Message.Models.Wechat;
using NHH.Models.Approve;
using NHH.Models.Common;
using NHH.Models.Plan.MerchantLicense;
using NHH.Service.Approve;
using NHH.Service.Plan.IService;

namespace NHH.Service.Plan
{
    public class MerchantLicenseService : NHHService<NHHEntities>, IMerchantLicenseService
    {
        private const string TXEMessage_Template_Key_Audit = "MerchantLicense_AuditNotify_TangMessage";

        private const string TXEMessage_Template_Key_Cancel = "MerchantLicense_CancelNotify_TangMessage";

        /// <summary>
        /// 获取商户证照列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public MerchantLicenseListModel GetMerchantLicenseList(MerchantLicenseQueryModel queryInfo)
        {
            var model = new MerchantLicenseListModel
            {
                PagingInfo = new PagingInfo { PageIndex = queryInfo.Page ?? 1 },
                QueryInfo = queryInfo,
                MerchantLicenseList = new List<MerchantLicenseModel>()
            };

            var query = from ma in Context.Merchant_Attachment
                        join m in Context.Merchant on ma.MerchantID equals m.MerchantID
                        join dc in Context.Dictionary.Where(a => a.FieldType == "MerchantAttachType") on ma.AttachmentType
                            equals dc.FieldValue
                        join dc2 in Context.Dictionary.Where(a => a.FieldType == "MerchantAttachStatus") on ma.Status equals
                            dc2.FieldValue
                        select new MerchantLicenseModel()
                        {
                            AttachmentID = ma.AttachmentID,
                            MerchantID = ma.MerchantID,
                            AttachmentType = ma.AttachmentType,
                            AttachmentName = ma.AttachmentName,
                            AttachmentPath = ma.AttachmentPath,
                            AttachmentCode = ma.AttachmentCode,
                            ExpireDate = ma.ExpiredDate,
                            ExpireDay = ma.ExpiredDate.HasValue ? -DbFunctions.DiffDays(ma.ExpiredDate, DateTime.Now) : 0,
                            Status = ma.Status,
                            MerchantName = m.MerchantName,
                            AttachmentTypeName = dc.FieldName,
                            StatusName = dc2.FieldName
                        };

            if (!string.IsNullOrEmpty(queryInfo.MerchantName))
            {
                query = query.Where(a => a.MerchantName.Contains(queryInfo.MerchantName));
            }
            if (queryInfo.Status.HasValue)
            {
                query = query.Where(a => a.Status == queryInfo.Status.Value);
            }

            model.PagingInfo.TotalCount = query.Count();

            var entityList =
                query.OrderBy(item => item.MerchantID)
                    .ThenBy(item => item.ExpireDate)
                    .Skip(model.PagingInfo.SkipNum)
                    .Take(model.PagingInfo.TakeNum)
                    .ToList();

            if (entityList.Any())
                model.MerchantLicenseList.AddRange(entityList);

            return model;
        }

        /// <summary>
        /// 获取商户证照
        /// </summary>
        /// <param name="attID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        public MerchantLicenseModel GetMerchantLicense(int attID, int configType = (int)ConfigTypeEnum.证照)
        {
            var query = from ma in Context.Merchant_Attachment
                        join m in Context.Merchant on ma.MerchantID equals m.MerchantID
                        join dc in Context.Dictionary.Where(a => a.FieldType == "MerchantAttachType") on ma.AttachmentType
                            equals dc.FieldValue
                        join dc2 in Context.Dictionary.Where(a => a.FieldType == "MerchantAttachStatus") on ma.Status equals
                            dc2.FieldValue
                        where ma.AttachmentID == attID
                        select new MerchantLicenseModel()
                        {
                            AttachmentID = ma.AttachmentID,
                            MerchantID = ma.MerchantID,
                            AttachmentType = ma.AttachmentType,
                            AttachmentName = ma.AttachmentName,
                            AttachmentPath = ma.AttachmentPath,
                            AttachmentCode = ma.AttachmentCode,
                            ExpireDate = ma.ExpiredDate,
                            ExpireDay = ma.ExpiredDate.HasValue ? DbFunctions.DiffDays(ma.ExpiredDate, DateTime.Now) : 0,
                            Status = ma.Status,
                            MerchantName = m.MerchantName,
                            AttachmentTypeName = dc.FieldName,
                            StatusName = dc2.FieldName
                        };

            var model = query.FirstOrDefault();

            model.ApproveRecordInfos = new ApproveRecordListInfo
            {
                ApproveRecordInfos = new ApproveService().GetDocumentApproveProcess(attID, configType)
            };
            model.CurrentApprover = GetCurrentApprover(attID);

            return model;
        }

        public bool Approve(ApproveModel approve)
        {
            ApproveService approveService = new ApproveService();

            using (var trans = Context.Database.BeginTransaction())
            {
                if (!approveService.UpdateApprove(approve))
                    return false;

                if (approve.Result == 1)
                {
                    if (approveService.IsApprovePass(approve))
                    {
                        //若审批全部通过，则给商户管理员发送微信和唐小二端信息
                        AddWechatMessage(approve.RefID, true, approve);
                        AddMerchantMessage(approve.RefID, true, approve.UserID);

                        //更改业务状态为已审批
                        UpdateMerchantLicenseStatus(approve.RefID, 3, approve.UserID);
                    }
                }
                else
                {
                    //若驳回，则给商户管理员发送微信和唐小二端信息
                    AddWechatMessage(approve.RefID, false, approve);
                    AddMerchantMessage(approve.RefID, false, approve.UserID);

                    //更改业务状态为已驳回
                    UpdateMerchantLicenseStatus(approve.RefID, 2, approve.UserID);
                }

                trans.Commit();
            }
            return true;
        }

        #region 私有方法

        /// <summary>
        /// 获取单据当前审核人
        /// </summary>
        /// <param name="attID"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        private List<int> GetCurrentApprover(int attID, int configType = 104)
        {
            var status = Context.Merchant_Attachment.Where(a => a.AttachmentID == attID).Select(a => a.Status).FirstOrDefault();
            if (status != 1)
                return new List<int>();

            var resultQuery = from ap in Context.Approve_Process
                              join ac in Context.Approve_Config on ap.ConfigID equals ac.ConfigID
                              where ap.RefID == attID && ac.ConfigType == configType
                              select ap.GroupNum;

            if (!resultQuery.Any())
                return new List<int>();

            int groupNum = resultQuery.Distinct().Max();

            var query = from ac in Context.Approve_Config
                        join ap in Context.Approve_Process on ac.ConfigID equals ap.ConfigID into queryResult
                        from ap in queryResult.DefaultIfEmpty()
                        where
                            ac.ConfigType == configType && ap.RefID == attID && ap.GroupNum == groupNum
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

            var result = from ur in Context.Sys_User_Role
                         where ur.RoleID == roleID
                         select ur.UserID;

            return result.ToList();
        }

        /// <summary>
        /// 发送微信消息
        /// </summary>
        /// <param name="attID"></param>
        /// <param name="pass"></param>
        /// <param name="approve"></param>
        /// <returns></returns>
        private bool AddWechatMessage(int attID, bool pass, ApproveModel approve)
        {
            var recipient = from u in Context.Merchant_User
                            join m in Context.Merchant_Attachment on u.MerchantID equals m.MerchantID
                            where m.AttachmentID == attID && u.RoleID == 1
                            select u.WechatOpenID;

            if (!recipient.Any())
                return false;

            var instance = CreateBizObject<Entities.Message>();

            bool needSave = false;

            recipient.ToList().ForEach(item =>
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var message = new Entities.Message()
                    {
                        MessageType = 3, //微信
                        Priority = 2,
                        Recipient = item,
                        Subject = pass ? "证照通过审批" : "证照审批被驳回",
                        Content = GetWeiChatContent(attID, pass, approve.Reason),
                        Link = ParamManager.GetStringValue("MerchantLicenseDetailLink"),
                        Status = 1,
                        InDate = DateTime.Now,
                        InUser = approve.UserID
                    };
                    instance.Insert(message);
                    needSave = true;
                }
            });

            if (needSave)
                SaveChanges();

            return true;
        }

        /// <summary>
        /// 获取微信内容
        /// </summary>
        /// <param name="attID"></param>
        /// <param name="pass"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        private string GetWeiChatContent(int attID, bool pass, string reason)
        {
            var attachmentTypeName = (from m in Context.Merchant_Attachment
                                      join dc in Context.Dictionary.Where(a => a.FieldType == "MerchantAttachType") on m.AttachmentType equals
                                          dc.FieldValue
                                      where m.AttachmentID == attID
                                      select dc.FieldName).FirstOrDefault();

            var msg = new MessageData
            {
                Type = "MerchantLicense",
                TemplateId = ParamManager.GetStringValue("MerchantLicenseTemplateId")
            };

            var content = new MerchantLicenseMessage()
            {
                first = new requestFieldDetail
                {
                    value = "你好，你提交的以下证照审核结果如下：",
                    color = "#173177"
                },
                keyword1 = new requestFieldDetail
                {
                    value = attachmentTypeName,
                    color = "#173177"
                },
                keyword2 = new requestFieldDetail
                {
                    value = pass ? "审核通过" : "审核被驳回",
                    color = "#173177"
                },
                keyword3 = new requestFieldDetail
                {
                    value = reason,
                    color = "#173177"
                },
                remark = new requestFieldDetail
                {
                    value = "请点击查看详情>>>",
                    color = "#173177"
                }
            };

            msg.Content = JsonConvert.SerializeObject(content);

            return JsonConvert.SerializeObject(msg);
        }

        /// <summary>
        /// 发送消息到唐小二端
        /// </summary>
        /// <param name="attID"></param>
        /// <param name="pass">审核是否通过</param>
        /// <param name="currentUserID">当前用户</param>
        /// <returns></returns>
        private bool AddMerchantMessage(int attID, bool pass, int currentUserID)
        {
            var recipient = from u in Context.Merchant_User
                            join m in Context.Merchant_Attachment on u.MerchantID equals m.MerchantID
                            where m.AttachmentID == attID && u.RoleID == 1
                            select u.UserID;

            if (!recipient.Any())
                return false;

            var query = from m in Context.Merchant_Attachment
                        join dc in Context.Dictionary.Where(a => a.FieldType == "MerchantAttachType") on m.AttachmentType equals
                            dc.FieldValue
                        join ms in Context.Merchant_Store on m.MerchantID equals ms.MerchantID
                        where m.AttachmentID == attID
                        select new
                        {
                            AttachTypeName = dc.FieldName,
                            m.MerchantID,
                            ms.StoreID
                        };

            var MessageTemplate =
                Context.Message_Template.Where(
                    a => a.TemplateKey == (pass ? TXEMessage_Template_Key_Audit : TXEMessage_Template_Key_Cancel))
                    .Select(a => a)
                    .FirstOrDefault();

            var entity = query.FirstOrDefault();

            var instance = CreateBizObject<Merchant_Message>();

            recipient.ToList().ForEach(userID =>
            {
                instance.Insert(new Merchant_Message()
                {
                    MerchantID = entity.MerchantID,
                    StoreID = entity.StoreID,
                    UserID = userID,
                    Subject = MessageTemplate.Title.Replace("#MerchantAttachment#", entity.AttachTypeName),
                    Content = MessageTemplate.Content.Replace("#URL", ParamManager.GetStringValue("MerchantLicenseDetailLink")),
                    SourceType = 10, //商户证照
                    SourceRefID = attID,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = currentUserID,
                    EditDate = DateTime.Now,
                    EditUser = currentUserID
                });
            });

            SaveChanges();
            return true;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="attID"></param>
        /// <param name="stauts"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        private bool UpdateMerchantLicenseStatus(int attID, int stauts, int currentUserID)
        {
            var instance = CreateBizObject<Merchant_Attachment>();
            var model = instance.TopOne(a => a.AttachmentID == attID);
            model.Status = stauts;
            model.EditUser = currentUserID;
            model.EditDate = DateTime.Now;
            instance.Update(model);
            SaveChanges();
            return true;
        }

        #endregion
    }
}
