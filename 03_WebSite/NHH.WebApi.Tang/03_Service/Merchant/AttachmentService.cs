using Newtonsoft.Json;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Message.Models.Sms;
using NHH.Models.Common;
using NHH.Models.Common.Enum.CommonEnums;
using NHH.Models.Merchant;
using NHH.Service.Common.IService;
using NHH.Service.Merchant.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using NHH.Service.Project.IService;

namespace NHH.Service.Merchant
{
    /// <summary>
    /// 商户附件服务
    /// </summary>
    public class AttachmentService : NHHService<NHHEntities>, IAttachmentService
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

        private IMerchantService merchantService;
        public IMerchantService MerchantService
        {
            get
            {
                if (merchantService == null)
                {
                    merchantService = NHHServiceFactory.Instance.CreateService<IMerchantService>();
                }
                return merchantService;
            }
        }

        private IProjectInfoService projectService;
        public IProjectInfoService ProjectService
        {
            get
            {
                if (projectService == null)
                {
                    projectService = NHHServiceFactory.Instance.CreateService<IProjectInfoService>();
                }
                return projectService;
            }
        }
        #endregion

        /// <summary>
        /// 获取商户证照列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        /// <remarks>
        /// 由于唐小二端每个证照只取最新上传的，所以这里要分组取最新的AttachmentID
        /// 先取出IDS，如：
        /// SELECT MAX(AttachmentID) FROM nhh.dbo.Merchant_Attachment 
        /// WHERE MerchantID = 10002 AND [Status] = 1
        /// GROUP BY AttachmentType
        /// </remarks>
        public List<MerchantAttachmentInfo> GetMerchantAttachmentList(int merchantId)
        {
            var model = new List<MerchantAttachmentInfo>();

            List<int> IDs=new List<int>();
            var list =
                Context.Merchant_Attachment
                    .Where(p => p.MerchantID == merchantId && p.Status >= 1)
                    .GroupBy(p => p.AttachmentType)
                    .Select(g => g.Max(x => x.AttachmentID))
                    .ToList();
            if(list!=null)
                list.ForEach(IDs.Add);

            var query = from ma in Context.Merchant_Attachment
                join d in Context.Dictionary on ma.AttachmentType equals d.FieldValue
                where d.FieldType == "MerchantAttachType" && d.FieldValue > 0 && ma.MerchantID == merchantId && ma.Status >= 1
                    && IDs.Contains(ma.AttachmentID)
                select new
                {
                    ma.AttachmentID,
                    ma.MerchantID,
                    ma.AttachmentType,
                    ma.AttachmentPath,
                    ma.ExpiredDate,
                    ma.AttachmentCode,
                    d.Seq,
                    d.FieldName,
                    d.FieldValue,
                    ma.Status
                };
            
            query = query.OrderBy(item => item.Seq);

            var entityList = query.ToList();

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var contract = new MerchantAttachmentInfo();
                    contract.AttachmentID = entity.AttachmentID;
                    contract.MerchantID = entity.MerchantID;
                    contract.AttachmentPath = NHH.Framework.Utility.UrlHelper.GetImageUrl(entity.AttachmentPath, 100);
                    contract.ExpiredDate = entity.ExpiredDate;
                    contract.AttachmentCode = entity.AttachmentCode;
                    contract.AttachmentType = entity.FieldValue;
                    contract.AttachmentTypeName = entity.FieldName;
                    contract.Status = entity.Status;
                    //如果审核通过并且过期时间在30天之内，状态为即将过期
                    if (entity.Status == 3 && entity.ExpiredDate.HasValue && entity.ExpiredDate.Value.AddDays(-30) < DateTime.Now)
                        contract.Status = 99; //即将过期
                    model.Add(contract);
                });
            }

            return model;

        }
        /// <summary>
        /// 商户证照附件上传
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddMerchantAttachment(MerchantAttachmentInfo model)
        {
            var instance = CreateBizObject<Merchant_Attachment>();

            var entity = new Merchant_Attachment()
            {
                MerchantID = model.MerchantID,
                AttachmentType = model.AttachmentType,
                AttachmentName = model.AttachmentName,
                AttachmentPath = model.AttachmentPath,
                AttachmentCode = model.AttachmentCode,
                ExpiredDate = model.ExpiredDate,
                Status = 1,
                InDate = DateTime.Now,
                InUser = model.InUser,
                EditDate = DateTime.Now,
                EditUser = model.InUser
            };
            instance.Insert(entity);
            this.SaveChanges();
            if (entity.AttachmentID > 0)
            {
                model.AttachmentID = entity.AttachmentID;
                BeginWorkflowProcessAndSendMessage("init", ApproveConfigTypeEnum.MerchantAttachment, model.AttachmentID,
                    model.MerchantID, model.ProjectID, GetAttachmentInfo(model.AttachmentID,model.MerchantID).AttachmentTypeName);
            }
            return model.AttachmentID;
        }
        /// <summary>
        /// 更新商户证照
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMerchantAttachment(MerchantAttachmentInfo model)
        {
            var instance = CreateBizObject<Merchant_Attachment>();
            var entity = instance.GetBySysNo(model.AttachmentID);
            if (entity != null)
            {
                //不是同一商家的不能更改
                if (entity.MerchantID != model.MerchantID)
                    return false;

                if (!string.IsNullOrEmpty(entity.AttachmentName))
                    entity.AttachmentName = model.AttachmentName;
                if (!string.IsNullOrEmpty(entity.AttachmentPath))
                    entity.AttachmentPath = model.AttachmentPath;
                entity.ExpiredDate = model.ExpiredDate;
                entity.AttachmentCode = model.AttachmentCode;
                entity.Status = 1;
                entity.EditUser = model.EditUser;
                entity.EditDate = DateTime.Now;
                instance.Update(entity);
                this.SaveChanges();

                model.AttachmentType = entity.AttachmentType;
                BeginWorkflowProcessAndSendMessage("reset", ApproveConfigTypeEnum.MerchantAttachment, model.AttachmentID,
                    model.MerchantID, model.ProjectID, GetAttachmentInfo(model.AttachmentID,model.MerchantID).AttachmentTypeName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取证照
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public MerchantAttachmentInfo GetAttachmentInfo(int attachmentId,int merchantId)
        {
            var model = new MerchantAttachmentInfo();
            var query = from ma in Context.Merchant_Attachment
                        join d in Context.Dictionary on ma.AttachmentType equals d.FieldValue
                join su in Context.Sys_User on ma.InUser equals su.UserID into temp
                from t in temp.DefaultIfEmpty()
                where d.FieldType == "MerchantAttachType" && ma.AttachmentID == attachmentId && ma.Status >= 1 && ma.MerchantID == merchantId
                select new
                {
                    ma.AttachmentID,
                    ma.AttachmentName,
                    ma.AttachmentPath,
                    ma.AttachmentType,
                    ma.AttachmentCode,
                    ma.ExpiredDate,
                    ma.InUser,
                    t.UserName,
                    ma.InDate,
                    ma.Status,
                    d.FieldName
                };
            var entity = query.FirstOrDefault(); ;

            if (entity != null)
            {
                model.AttachmentID = entity.AttachmentID;
                model.AttachmentName = entity.AttachmentName;
                model.AttachmentPath = NHH.Framework.Utility.UrlHelper.GetImageUrl(entity.AttachmentPath, 100);
                model.AttachmentType = entity.AttachmentType;
                model.AttachmentTypeName = entity.FieldName;
                model.AttachmentCode = entity.AttachmentCode;
                model.ExpiredDate = entity.ExpiredDate;
                model.InUser = entity.InUser;
                model.InUserName = entity.UserName;
                model.InDate = entity.InDate;
                model.Status = entity.Status;
                //如果审核通过并且过期时间在30天之内，状态为即将过期
                if (entity.Status == 3 && entity.ExpiredDate.HasValue && entity.ExpiredDate.Value.AddDays(-30) < DateTime.Now)
                    model.Status = 99; //即将过期
            }
            return model;
        }

        /// <summary>
        /// 作废附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public bool CancelAttachment(int attachmentId,int merchantId)
        {
            var instance = CreateBizObject<Merchant_Attachment>();
            var entity = instance.GetBySysNo(attachmentId);
            if (entity != null)
            {
                //不是同一商家不能作废
                if (entity.MerchantID != merchantId)
                    return false;

                entity.Status = -1;
                instance.Update(entity);
                this.SaveChanges();
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
        /// <param name="refID">AttachmentID</param>
        /// <param name="merchantId"></param>
        /// <param name="projectID"></param>
        /// <param name="merchantAttachmentTypeName"></param>
        private void BeginWorkflowProcessAndSendMessage(string strInitOrReset, ApproveConfigTypeEnum approveConfigType, int refID, int merchantId,int projectID,string merchantAttachmentTypeName)
        {
            if (strInitOrReset.ToLower() == "init")
                WorkflowService.InitProcess((int)approveConfigType, refID);
            if (strInitOrReset.ToLower() == "reset")
                WorkflowService.ResetProcess((int)approveConfigType, refID);

            string url = "/Plan/MerchantLicense/Detail?attachmentID=" + refID;
            
            //取商家名
            string merchantName = MerchantService.GetSimpleMerchantDetail(merchantId).MerchantName;
            int companyID = ProjectService.GetProjectInfo(projectID).ManageCompanyId;
            //取工作流审批下一批次人员列表
            var userList = WorkflowService.GetNextApprovers((int)approveConfigType, refID, companyID);
            //发给管理平台审批人员通知审批消息
            List<SysUserMessage> messageList = new List<SysUserMessage>();
            var mgmtMessageTemplate = CommonService.GetMessageTemplate("MerchantAttachment_AuditNotify_MgmtMessage");
            foreach (var user in userList)
            {
                SysUserMessage message = new SysUserMessage()
                {
                    UserID = user.UserID,
                    Subject = mgmtMessageTemplate.Title.Replace("#MERCHANTNAME#", merchantName)
                                                       .Replace("#ATTACHMENTTYPE#", merchantAttachmentTypeName),
                    Content = mgmtMessageTemplate.Content.Replace("#MERCHANTNAME#", merchantName)
                                                         .Replace("#ATTACHMENTTYPE#", merchantAttachmentTypeName)
                                                         .Replace("#URL#", url),
                    SourceType = (int)SysUserMessageSourceTypeEnum.Merchant,
                    SourceRefID = refID
                };
                messageList.Add(message);
            }
            commonService.SendSysUserMessageList(messageList);
            //发给审批人员短信通知审批
            //拼Params的Json实体
            MerchantAttachmentAuditNotify content = new MerchantAttachmentAuditNotify(merchantName, merchantAttachmentTypeName);
            //拼Content的Json实体
            SmsMessage sms = new SmsMessage();
            sms.TemplateCode = MerchantAttachmentAuditNotify.TemplateCode;
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
                        merchantName + "提交的" + merchantAttachmentTypeName + "审批短信",
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
