using NHH.Entities;
using NHH.Framework.Service;
using NHH.Service.Estate.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Estate;
using NHH.Models.Common;
using NHH.Framework.Utility;
using NHH.Message.Models.Wechat;
using NHH.Framework.Wechat;
using Newtonsoft.Json;
using NHH.Models.Message;
namespace NHH.Service.Estate
{
    /// <summary>
    /// 维修服务
    /// </summary>
    public class RepairService : NHHService<NHHEntities>, IRepairService
    {
        /// <summary>
        /// 获取维修列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public RepairListModel GetRepairList(RepairListQueryInfo queryInfo)
        {
            var model = new RepairListModel();
            model.QueryInfo = queryInfo;
            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };


            #region 获取当前用户所在公司所管辖下的所有项目

            var projectList = new List<int>();
            var projectQuery = from su in Context.Sys_User
                               join em in Context.Employee on su.EmployeeID equals em.EmployeeID
                               join p in Context.Project on em.CompanyID equals p.ManageCompanyID
                               where su.UserID == queryInfo.CurrentUserId
                               select p.ProjectID;

            var projectEntityList = projectQuery.ToList();
            if (projectEntityList != null)
            {
                foreach (var projectId in projectEntityList)
                {
                    projectList.Add(projectId);
                }
            }

            #endregion

            #region 查询条件
            var query = from r in Context.Repair
                        join ds in Context.Dictionary on r.RepairStatus equals ds.FieldValue
                        join dt in Context.Dictionary on r.RepairType equals dt.FieldValue
                        where r.Status == 1 && ds.FieldType == "RepairStatus" && dt.FieldType == "RepairType" && projectList.Contains(r.ProjectID)
                        select new
                        {
                            r.RepairID,
                            r.RepairType,
                            r.RepairUserID,
                            r.RepairUserName,
                            r.Location,
                            r.RepairStartTime,
                            r.RepairFinishTime,
                            r.RepairStatus,
                            RepairStatusName = ds.FieldName,
                            r.RequestTime,
                            RepairTypeContent = dt.FieldName,
                            r.Important,
                            r.EstimatedFinishTime
                        };
            if (queryInfo.RepairId.HasValue)
            {
                query = query.Where(item => item.RepairID == queryInfo.RepairId.Value);
            }

            if (queryInfo.Type.HasValue)
            {
                query = query.Where(item => item.RepairType == queryInfo.Type.Value);
            }
            if (queryInfo.RepairUserId.HasValue)
            {
                query = query.Where(item => item.RepairUserID == queryInfo.RepairUserId.Value);
            }
            if (queryInfo.Status.HasValue)
            {
                query = query.Where(item => item.RepairStatus == queryInfo.Status);
            }
            if (queryInfo.FromDate.HasValue)
            {
                query = query.Where(item => item.RequestTime >= queryInfo.FromDate.Value);
            }
            if (queryInfo.ToDate.HasValue)
            {
                query = query.Where(item => item.RequestTime <= queryInfo.ToDate.Value);
            }
            #endregion
            model.PagingInfo.TotalCount = query.Count();
            model.RepairList = new List<RepairInfo>();

            var entityList = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (entityList != null && entityList.Count > 0)
            {
                entityList.ForEach(entity =>
                {
                    var repair = new RepairInfo
                    {
                        RepairID = entity.RepairID,
                        RepairType = entity.RepairType,
                        RepairUser = entity.RepairUserName,
                        RepairUserId = entity.RepairUserID,
                        RepairStartTime = entity.RepairStartTime,
                        RepairStatusName = entity.RepairStatusName,
                        RepairStatus = entity.RepairStatus,
                        Important = entity.Important,
                        EstimatedFinishTime = entity.EstimatedFinishTime,
                        RequestTime = entity.RequestTime,
                        RepairTypeName = entity.RepairTypeContent,
                    };
                    model.RepairList.Add(repair);
                });
            }

            return model;
        }

        /// <summary>
        /// 根据单号查找Detail信息
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        public RepairDetailModel GetRepairDetail(int repairId)
        {
            var model = new RepairDetailModel();
            model.AttachmentInfos = new List<AttachmentInfo>();
            model.RepairLogInfoList = new List<RepairLogInfo>();
            model.RepairCommentDetailModel = new RepairCommentDetailModel();
            #region 查询条件
            var query = from r in Context.Repair
                        join d1 in Context.Dictionary on r.RepairType equals d1.FieldValue
                        join d2 in Context.Dictionary on r.RepairStatus equals d2.FieldValue
                        join e in Context.Employee on r.RepairUserID equals e.EmployeeID into RepairUser
                        from ru in RepairUser.DefaultIfEmpty()
                        where r.RepairID == repairId && d1.FieldType == "RepairType" && d2.FieldType == "RepairStatus" && r.Status == 1
                        select new
                       {
                           r.RepairID,
                           r.RepairType,
                           r.RepairUserName,
                           r.RepairUserID,
                           r.Location,
                           r.RepairStartTime,
                           r.RepairFinishTime,
                           r.RepairStatus,
                           r.RepairDesc,
                           r.InDate,
                           r.RequestTime,
                           r.RequestUserName,
                           r.RequestUserID,
                           r.RequestDesc,
                           r.QuoteAmount,
                           r.RequestSrcType,
                           r.Repair_Attachment,
                           r.Repair_Log,
                           r.Repair_Comment,
                           r.ProjectID,
                           r.AcceptTime,
                           ru.Moblie,
                           ru.Tag,
                           RepairTypeName = d1.FieldName,
                           RepairStatusName = d2.FieldName,
                           r.Important,
                           r.EstimatedFinishTime
                       };
            #endregion
            var entity = query.FirstOrDefault();
            if (null != entity)
            {
                model.RepairID = repairId;
                model.ProjectId = entity.ProjectID;
                model.Location = entity.Location;
                model.RepairTypeName = entity.RepairTypeName;
                model.RepairDesc = entity.RepairDesc;
                model.RequestDesc = entity.RequestDesc;
                model.RequestTime = entity.RequestTime;
                model.RequestUserName = entity.RequestUserName;
                model.RequestUserId = entity.RequestUserID.HasValue ? entity.RequestUserID.Value : 0;
                model.QuoteAmount = entity.QuoteAmount.HasValue ? entity.QuoteAmount.Value : 0;
                model.RequestSrcType = entity.RequestSrcType;

                if (model.RequestSrcType == 2)//1:商户端 2：智能平台端
                {
                    var requestContact = (from su in Context.Sys_User
                                          join em in Context.Employee on su.EmployeeID equals em.EmployeeID
                                          where su.UserID == model.RequestUserId && em.Status == 1
                                          select em.Moblie).FirstOrDefault();
                    model.RequestContact = string.IsNullOrEmpty(requestContact) != true ? requestContact : string.Empty;
                }
                if (model.RequestSrcType == 1)
                {
                    var requestContact = (from mu in Context.Merchant_User
                                          where mu.UserID == model.RequestUserId && mu.Status == 1
                                          select mu.Moblie).FirstOrDefault();
                    model.RequestContact = string.IsNullOrEmpty(requestContact) != true ? requestContact : string.Empty;
                }

                model.RepairUserId = entity.RepairUserID.HasValue ? entity.RepairUserID.Value : 0;
                model.RepairUserName = entity.RepairUserName;
                model.RepairMobile = string.IsNullOrEmpty(entity.Moblie) ? string.Empty : entity.Moblie;
                model.Tag = entity.Tag;
                model.RepairStatusName = entity.RepairStatusName;
                model.RepairStatus = entity.RepairStatus;
                model.RepairStartTime = entity.RepairStartTime;
                model.RepairFinishTime = entity.RepairFinishTime;
                model.InDate = entity.InDate;
                model.AcceptTime = entity.AcceptTime;
                model.Important = entity.Important;
                model.EstimatedFinishTime = entity.EstimatedFinishTime;

                #region 附件信息
                if (null != entity.Repair_Attachment)
                {
                    entity.Repair_Attachment.ToList().ForEach(m =>
                    {
                        var attachmentInfo = new AttachmentInfo
                        {
                            AttchmentId = m.AttachmentID,
                            AttachmentPath = m.AttachmentPath,
                            AttachmentName = m.AttachmentName
                        };
                        model.AttachmentInfos.Add(attachmentInfo);
                    });
                }
                #endregion

                #region 维修记录
                if (entity.Repair_Log != null)
                {
                    foreach (var item in entity.Repair_Log)
                    {
                        var repairLogInfo = new RepairLogInfo()
                        {
                            LogId = item.LogID,
                            RepairId = item.RepairID,
                            LogTime = item.LogTime,
                            LogUserId = item.LogUserID,
                            LogUserName = item.LogUserName,
                            LogText = item.LogText
                        };
                        model.RepairLogInfoList.Add(repairLogInfo);
                    }
                }
                #endregion

                #region 服务评价
                if (entity.Repair_Comment != null)
                {
                    var info = entity.Repair_Comment.FirstOrDefault();
                    if (info != null)
                    {
                        var comment = new RepairCommentDetailModel();
                        comment.Speed = info.Speed;
                        comment.Quality = info.Quality;
                        comment.Attitude = info.Attitude;
                        comment.AllComment = info.Comment + " " + info.Additional;

                        model.RepairCommentDetailModel = comment;
                    }
                }
                #endregion


            }
            return model;
        }

        /// <summary>
        /// 获取维修人员详细信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public RepairDetailModel GetRepairUserById(int userId)
        {
            var model = new RepairDetailModel();
            var query = from e in Context.Employee
                        where e.EmployeeID == userId
                        select new
                        {
                            e.EmployeeID,
                            e.EmployeeName,
                            e.Moblie,
                            e.IDNumber,
                            e.Tag
                        };
            var entity = query.FirstOrDefault();
            if (null != entity)
            {
                model.RepairUserId = entity.EmployeeID;
                model.RepairUserName = entity.EmployeeName;
                model.RepairMobile = entity.Moblie;
                model.CertificationId = entity.IDNumber;
                model.Tag = entity.Tag;
            }
            return model;
        }

        /// <summary>
        /// 添加
        /// RequestSrcType:
        /// 1:唐小二报修
        /// 2：运营报修
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddRepair(RepairDetailModel model)
        {
            var instance = CreateBizObject<NHH.Entities.Repair>();
            var entity = new NHH.Entities.Repair
            {
                ProjectID = model.ProjectId,
                RepairStatus = 1,
                RepairType = model.RepairType.Value,
                RequestUserID = model.UserInfo.UserId,
                RequestUserName = model.UserInfo.UserName,
                RequestSrcType = 2,
                RequestTime = DateTime.Now,
                FloorID = model.FloorId,
                IsCommon = model.IsCommon,
                RequestDesc = model.RequestDesc,
                Location = model.Location,
                Status = 1,
                InDate = DateTime.Now,
                InUser = model.UserInfo.UserId,
                EditDate = DateTime.Now,
                EditUser = model.UserInfo.UserId,
            };
            instance.Insert(entity);
            this.SaveChanges();

            #region 报修附件
            if (!string.IsNullOrEmpty(model.AttchmentImgPathList))
            {
                var attachmentImgPathList = model.AttchmentImgPathList.Split(',').ToList();
                entity.Repair_Attachment = new List<Repair_Attachment>();
                var attBll = CreateBizObject<NHH.Entities.Repair_Attachment>();

                attachmentImgPathList.ForEach(item =>
                {
                    if (item != "")
                    {
                        var imgNameList = item.Split('/').ToList();
                        var imgName = imgNameList[imgNameList.Count - 1];
                        var repairAttachment = new Repair_Attachment
                        {
                            RepairID = entity.RepairID,
                            AttachmentPath = item,
                            AttachmentName = imgName,
                            Status = 1,
                            InDate = DateTime.Now,
                            InUser = model.UserInfo.UserId,
                            EditDate = DateTime.Now,
                            EditUser = model.UserInfo.UserId,
                        };
                        attBll.Insert(repairAttachment);
                        this.SaveChanges();
                    }

                });
            }
            #endregion

            #region 记录日志
            var log = new RepairLogInfo();

            log.LogTime = DateTime.Now;
            log.RepairId = entity.RepairID;
            log.LogUserId = model.UserInfo.UserId;
            log.LogUserName = model.UserInfo.UserName;
            log.LogText = model.UserInfo.UserName + "提交维修单";

            AddRepairLog(log);
            #endregion

            #region 发送提报消息到智能平台
            if (model.ProjectConfig != null)
            {
                int employeeId = 0;
                int.TryParse(model.ProjectConfig.Param3, out employeeId);
                if (employeeId > 0)
                {
                    var governorUserId = (from u in Context.Sys_User
                                          where u.EmployeeID == employeeId
                                          select u.UserID).FirstOrDefault();
                    if (governorUserId > 0)
                    {
                        var userMessage = new UserMessage();
                        userMessage.UserID = governorUserId;//提报人的主管
                        userMessage.Subject = "维修提报";
                        userMessage.Content = "你好！您收到一条新的报修";
                        userMessage.DetailUrl = model.RepairDetailUrl;
                        userMessage.SourceType = 6;//报修
                        userMessage.SourceRefID = entity.RepairID;
                        userMessage.UserInfo = new UserInfo();
                        userMessage.UserInfo.UserId = model.UserInfo.UserId;
                        AddUserMessage(userMessage);
                    }
                }
            }
            #endregion


            return entity.RepairID;
        }

        /// <summary>
        /// 更新维修报价
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateQuoteAmount(RepairDetailEditInfo info)
        {
            var instance = CreateBizObject<Repair>();
            var entity = instance.GetBySysNo(info.RepairId);
            if (entity != null)
            {
                entity.QuoteAmount = info.QuoteAmount.HasValue ? info.QuoteAmount.Value : 0;
                entity.EditDate = DateTime.Now;
                entity.EditUser = info.UserInfo.UserId;
                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据维修单号指派维修人
        /// </summary>
        /// <param name="info"></param>
        public bool RepairAssign(RepairAssignInfo info)
        {
            var instance = CreateBizObject<NHH.Entities.Repair>();
            var entity = instance.GetBySysNo(info.RepairId);
            if (entity == null)
                return false;

            entity.RepairUserID = info.RepairUserId;
            entity.RepairUserName = (from e in Context.Employee
                                     where e.EmployeeID == info.RepairUserId
                                     select e.EmployeeName).FirstOrDefault();
            entity.RepairStatus = 2;
            entity.RepairStartTime = DateTime.Now;
            entity.AcceptTime = DateTime.Now;
            entity.AcceptUserID = info.UserInfo.UserId;
            entity.AcceptUserName = info.UserInfo.UserName;

            entity.EditDate = DateTime.Now;
            entity.EditUser = info.UserInfo.UserId;
            entity.Important = info.Important == true ? 2 : 1;//1：非紧急 2：紧急

            var estimateHour = info.EstimatedHour.HasValue ? info.EstimatedHour.Value : 0;

            entity.EstimatedFinishTime = info.RequestTime.HasValue ? info.RequestTime.Value.AddHours(estimateHour) : DateTime.Now.AddHours(estimateHour);
            instance.Update(entity);
            this.SaveChanges();

            #region 记录日志
            var log = new RepairLogInfo();

            log.LogTime = DateTime.Now;
            log.LogUserId = info.UserInfo.UserId;
            log.LogUserName = info.UserInfo.UserName;
            log.LogText = "维修单已经指派给" + entity.RepairUserName;
            log.RepairId = info.RepairId;
            AddRepairLog(log);
            #endregion

            #region 发送消息到微信端
            var recipient = (from mu in Context.Merchant_User
                             where mu.UserID == info.RequestUserId
                             select mu.WechatOpenID).FirstOrDefault();//提报人oI_yCswwiV3MfSSoB0Qv2OV_NlLo
            if (info.RequestSrcType == 1 && recipient != null)//1：商户报修 2：运营报修
            {
                var msg = new MessageInfo();
                msg.Priority = 2;
                msg.Recipient = recipient;
                msg.Subject = "维修派单";
                msg.Link = string.Format(ParamManager.GetStringValue("RepairDetailLink"), info.RepairId);
                #region 微信端通知内容
                var content = new RepairWechatContentInfo();
                content.Title = "尊敬的" + info.RequestUserName + "先生/女士，您的报修有新的进展。";
                content.Location = info.Location;
                content.Reason = msg.Subject;
                content.RequestTime = info.RequestTime.HasValue ? info.RequestTime.Value : DateTime.Now;
                content.RepairStatus = "已指派给维修人员" + info.RepairUserName;
                content.EstimateTime = entity.EstimatedFinishTime.Value;
                content.Remarks = "上门前工作人员将提前与您预约，请保持电话畅通，谢谢。";
                msg.Content = GetRepairWechatContent(content);
                #endregion

                msg.UserInfo = new UserInfo();
                msg.UserInfo.UserId = info.UserInfo.UserId;
                AddMessage(msg);//发送给提报人
            }
            #endregion

            #region 发送完成维修消息到智能平台
            if (info.RequestSrcType == 2)//运营报修
            {
                var userMessage = new UserMessage();
                userMessage.UserID = info.RequestUserId;//提报人
                userMessage.Subject = "维修派单";
                userMessage.Content = "你好！你提交的报修单据" + entity.RepairID + "已经派单，维修人员将尽快安排维修工作";
                userMessage.DetailUrl = info.RepairDetailUrl;
                userMessage.SourceRefID = entity.RepairID;
                userMessage.UserInfo = new UserInfo();
                userMessage.UserInfo.UserId = info.UserInfo.UserId;
                AddUserMessage(userMessage);//发送给提报人
            }
            #endregion
            return true;
        }

        /// <summary>
        /// 重新指派
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool RepairReAssign(RepairAssignInfo info)
        {
            var instance = CreateBizObject<NHH.Entities.Repair>();
            var entity = instance.GetBySysNo(info.RepairId);
            if (entity == null)
                return false;

            entity.RepairUserID = info.RepairUserId;
            entity.RepairUserName = (from e in Context.Employee
                                     where e.EmployeeID == info.RepairUserId
                                     select e.EmployeeName).FirstOrDefault();

            entity.AcceptUserID = info.UserInfo.UserId;
            entity.AcceptUserName = info.UserInfo.UserName;

            entity.EditDate = DateTime.Now;
            entity.EditUser = info.UserInfo.UserId;
            entity.RepairStatus = 2;
            instance.Update(entity);
            this.SaveChanges();


            #region 记录日志
            var log = new RepairLogInfo();

            log.LogTime = DateTime.Now;
            log.LogUserId = info.UserInfo.UserId;
            log.LogUserName = info.UserInfo.UserName;
            log.LogText = "维修单重新指派给" + entity.RepairUserName + ",原因：" + info.ReAssignReason + " " + info.Remarks;
            log.RepairId = info.RepairId;
            AddRepairLog(log);
            #endregion

            #region 发送消息到微信端
            var recipient = (from mu in Context.Merchant_User
                             where mu.UserID == info.RequestUserId
                             select mu.WechatOpenID).FirstOrDefault();//提报人oI_yCswwiV3MfSSoB0Qv2OV_NlLo

            if (info.RequestSrcType == 1 && recipient != null)//商户报修
            {
                var msg = new MessageInfo();
                msg.Priority = 2;
                msg.Recipient = recipient;
                msg.Subject = "维修重新指派";
                msg.Link = string.Format(ParamManager.GetStringValue("RepairDetailLink"), info.RepairId);

                #region 微信端通知内容
                var content = new RepairWechatContentInfo();
                content.Title = "尊敬的" + info.RequestUserName + "先生/女士，您的报修有新的进展。";
                content.Location = info.Location;
                content.Reason = msg.Subject;
                content.RequestTime = info.RequestTime.HasValue ? info.RequestTime.Value : DateTime.Now;
                content.RepairStatus = "已重新指派给维修人员" + info.RepairUserName;
                content.EstimateTime = info.EstimatedFinishTime.Value;
                content.Remarks = "上门前工作人员将提前与您预约，请保持电话畅通，谢谢。";
                msg.Content = GetRepairWechatContent(content);
                #endregion

                msg.UserInfo = new UserInfo();
                msg.UserInfo.UserId = info.UserInfo.UserId;
                AddMessage(msg);//发送给提报人
            }
            #endregion

            #region 发送完成维修消息到智能平台
            if (info.RequestSrcType == 2)//运营报修
            {
                var userMessage = new UserMessage();
                userMessage.UserID = info.RequestUserId;//提报人
                userMessage.Subject = "维修重新指派";
                userMessage.Content = "你好！你提交的报修单据" + entity.RepairID + "重新指派，维修人员将尽快安排维修工作";
                userMessage.DetailUrl = info.RepairDetailUrl;
                userMessage.SourceRefID = entity.RepairID;
                userMessage.UserInfo = new UserInfo();
                userMessage.UserInfo.UserId = info.UserInfo.UserId;
                AddUserMessage(userMessage);//发送给提报人
            }
            #endregion
            return true;
        }

        /// <summary>
        /// 延迟维修时间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool RepairDelay(RepairDelayInfo model)
        {
            var instance = CreateBizObject<Repair>();
            var entity = instance.GetBySysNo(model.RepairId);
            if (entity == null)
            {
                return false;
            }
            var estimateHour = model.EstimatedHour.HasValue ? model.EstimatedHour.Value : 0;

            entity.EstimatedFinishTime = model.EstimatedFinishTime.HasValue ? model.EstimatedFinishTime.Value.AddHours(estimateHour) : DateTime.Now.AddHours(estimateHour);
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserInfo.UserId;

            #region 记录日志
            var log = new RepairLogInfo();
            log.LogTime = DateTime.Now;
            log.LogUserId = model.UserInfo.UserId;
            log.LogUserName = model.UserInfo.UserName;
            log.LogText = "延迟" + estimateHour + "个小时,延迟原因：" + model.DelayReason + " " + model.Remarks;
            log.RepairId = model.RepairId;
            AddRepairLog(log);
            #endregion

            return true;
        }

        /// <summary>
        /// 维修结束
        /// </summary>
        /// <param name="repairId"></param>
        public void RepairFinish(RepairFinishInfo info)
        {
            var instance = CreateBizObject<NHH.Entities.Repair>();
            var entity = instance.GetBySysNo(info.RepairId);
            if (entity == null)
                return;

            entity.RepairFinishTime = DateTime.Now;
            entity.RepairStatus = 3;
            entity.EditDate = DateTime.Now;
            entity.EditUser = info.UserInfo.UserId;

            instance.Update(entity);
            this.SaveChanges();

            #region 记录日志
            var log = new RepairLogInfo();

            log.LogTime = DateTime.Now;
            log.LogUserId = info.UserInfo.UserId;
            log.LogUserName = info.UserInfo.UserName;
            log.RepairId = info.RepairId;
            entity.RepairUserName = (from e in Context.Employee
                                     where e.EmployeeID == info.RepairUserId
                                     select e.EmployeeName).FirstOrDefault();

            log.LogText = "已完成维修";

            AddRepairLog(log);
            #endregion

            #region 发送消息到微信端
            var recipient = (from mu in Context.Merchant_User
                             where mu.UserID == info.RequestUserId
                             select mu.WechatOpenID).FirstOrDefault();//提报人oI_yCswwiV3MfSSoB0Qv2OV_NlLo
            if (info.RequestSrcType == 1 && recipient != null)//商户报修
            {
                var msg = new MessageInfo();
                msg.Priority = 2;
                msg.Recipient = recipient;
                msg.Subject = "完成维修";
                msg.Link = string.Format(ParamManager.GetStringValue("RepairDetailLink"), info.RepairId);

                #region 微信端通知内容
                var content = new RepairWechatContentInfo();
                content.Title = "尊敬的" + info.RequestUserName + "先生/女士，您的报修有新的进展。";
                content.Location = info.Location;
                content.Reason = msg.Subject;
                content.RequestTime = info.RequestTime.HasValue ? info.RequestTime.Value : DateTime.Now;
                content.RepairStatus = msg.Subject;
                content.EstimateTime = DateTime.Now;
                content.Remarks = "感谢您的选择！";
                msg.Content = GetRepairWechatContent(content);
                #endregion

                msg.UserInfo = new UserInfo();
                msg.UserInfo.UserId = info.UserInfo.UserId;
                AddMessage(msg);
            }
            #endregion

            #region 发送完成维修消息到智能平台
            if (info.RequestSrcType == 2)//运营报修
            {
                var userMessage = new UserMessage();
                userMessage.UserID = info.RequestUserId;//提报人
                userMessage.Subject = "完成维修";
                userMessage.Content = "你好！你提交的维修单据 " + entity.RepairID + " 已经完成维修";
                userMessage.DetailUrl = info.RepairDetailUrl;
                userMessage.SourceRefID = entity.RepairID;
                userMessage.UserInfo = new UserInfo();
                userMessage.UserInfo.UserId = info.UserInfo.UserId;
                AddUserMessage(userMessage);

            }
            #endregion
        }

        /// <summary>
        /// 维修搁置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool RepairShelve(RepairShelveInfo model)
        {
            var instance = CreateBizObject<Repair>();
            var entity = instance.GetBySysNo(model.RepairId);
            if (entity == null) return false;

            entity.RepairFinishTime = DateTime.Now;
            entity.RepairStatus = 4;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserInfo.UserId;

            instance.Update(entity);
            this.SaveChanges();

            #region 记录日志
            var log = new RepairLogInfo();

            log.LogTime = DateTime.Now;
            log.LogUserId = model.UserInfo.UserId;
            log.LogUserName = model.UserInfo.UserName;
            log.RepairId = model.RepairId;
            entity.RepairUserName = (from e in Context.Employee
                                     where e.EmployeeID == model.RepairUserId
                                     select e.EmployeeName).FirstOrDefault();
            log.LogText = "维修单已搁置" + ",原因：" + model.ShelveReason + " " + model.Remarks;

            AddRepairLog(log);
            #endregion

            #region 发送消息到微信端

            var recipient = (from mu in Context.Merchant_User
                             where mu.UserID == model.RequestUserId
                             select mu.WechatOpenID).FirstOrDefault();//提报人oI_yCswwiV3MfSSoB0Qv2OV_NlLo
            if (model.RequestSrcType == 1 && recipient != null)//商户报修
            {
                var msg = new MessageInfo();
                msg.Priority = 2;
                msg.Recipient = recipient;
                msg.Subject = "维修搁置";
                msg.Link = string.Format(ParamManager.GetStringValue("RepairDetailLink"), model.RepairId);

                #region 微信端通知内容
                var content = new RepairWechatContentInfo();
                content.Title = "尊敬的" + model.RequestUserName + "先生/女士，您的报修有新的进展。";
                content.Location = model.Location;
                content.Reason = msg.Subject;
                content.RequestTime = model.RequestTime.Value;
                content.RepairStatus = msg.Subject;
                content.EstimateTime = model.EstimatedFinishTime.HasValue ? model.EstimatedFinishTime.Value : DateTime.Now;
                content.Remarks = "感谢您的选择！";
                msg.Content = GetRepairWechatContent(content);
                #endregion

                msg.UserInfo = new UserInfo();
                msg.UserInfo.UserId = model.UserInfo.UserId;
                AddMessage(msg);
            }
            #endregion

            #region 发送完成维修消息到智能平台
            var userMessage = new UserMessage();
            userMessage.Subject = "维修搁置";
            userMessage.SourceRefID = entity.RepairID;
            userMessage.UserInfo = new UserInfo();
            userMessage.UserInfo.UserId = model.UserInfo.UserId;
            userMessage.DetailUrl = model.RepairDetailUrl;
            if (model.RequestSrcType == 2)//运营报修
            {
                userMessage.UserID = model.RequestUserId;//提报人
                userMessage.Content = "你好！你提交的维修单据" + entity.RepairID + "由于" + model.ShelveReason + "原因所以无法完成，进入搁置列表";
                AddUserMessage(userMessage);//发送给提报人
            }

            //第二个发给工程经理
            var projectManagerUserId = (from su in Context.Sys_User
                                        join em in Context.Employee on su.EmployeeID equals em.EmployeeID
                                        join ssu in Context.Sys_User on em.SupervisorID equals ssu.EmployeeID
                                        where su.UserID == model.UserInfo.UserId
                                        select ssu.UserID).FirstOrDefault();
            if (projectManagerUserId != 0)
            {
                userMessage.UserID = projectManagerUserId;//工程经理
                userMessage.Content = "你好！维修单据" + entity.RepairID + "由于" + model.ShelveReason + "原因所以无法完成，进入搁置列表。";
                AddUserMessage(userMessage);//发送给工程经理
            }

            //第三个发给项目总经理
            var projectGenernalManagerUserId = (from su in Context.Sys_User
                                                join em in Context.Employee on su.EmployeeID equals em.EmployeeID
                                                join ssu in Context.Sys_User on em.SupervisorID equals ssu.EmployeeID
                                                where su.UserID == projectManagerUserId
                                                select ssu.UserID).FirstOrDefault();
            if (projectGenernalManagerUserId != 0)
            {
                userMessage.UserID = projectGenernalManagerUserId;//项目总经理
                AddUserMessage(userMessage);//发送给项目总经理
            }
            #endregion

            return true;
        }

        /// <summary>
        /// 增加维修日志
        /// </summary>
        public void AddRepairLog(RepairLogInfo model)
        {
            var instance = CreateBizObject<Repair_Log>();

            var entity = new Repair_Log();
            entity.LogTime = model.LogTime;
            entity.LogUserID = model.LogUserId;
            entity.LogUserName = model.LogUserName;
            entity.LogText = model.LogText;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.LogUserId;
            entity.RepairID = model.RepairId;
            instance.Insert(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 发送消息到智能平台
        /// </summary>
        /// <param name="model"></param>
        public void AddUserMessage(UserMessage model)
        {
            var instance = CreateBizObject<Sys_User_Message>();
            var entity = new Sys_User_Message();

            entity.UserID = model.UserID;
            entity.Subject = model.Subject;
            entity.Content = model.Content;
            entity.Content += "<a href='" + model.DetailUrl + "?repairId=" + model.SourceRefID + "' class='blue'>,详情请点击此处>></a>";
            entity.SourceType = 6;//报修
            entity.SourceRefID = model.SourceRefID;
            entity.Status = 1;
            entity.InDate = DateTime.Now;
            entity.InUser = model.UserInfo.UserId;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserInfo.UserId;

            instance.Insert(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 发送消息到微信端
        /// </summary>
        /// <param name="model"></param>
        public void AddMessage(MessageInfo model)
        {
            var instance = CreateBizObject<NHH.Entities.Message>();
            var entity = new NHH.Entities.Message();
            entity.MessageType = 3;//微信端
            entity.Priority = model.Priority;
            entity.Recipient = model.Recipient;
            entity.Subject = model.Subject;
            entity.Content = model.Content;
            entity.Link = model.Link;
            entity.InDate = DateTime.Now;
            entity.InUser = model.UserInfo.UserId;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserInfo.UserId;
            entity.Status = 0;

            instance.Insert(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 序列化维修微信端content
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetRepairWechatContent(RepairWechatContentInfo info)
        {
            var msg = new MessageData();
            msg.Type = "Repair";
            msg.TemplateId = ParamManager.GetStringValue("RepairTemplateId");

            var content = new RepairRemindMessage();
            content.first = new requestFieldDetail();
            content.first.value = info.Title;
            content.first.color = info.Color;

            content.keyword1 = new requestFieldDetail();
            content.keyword1.value = info.Location;
            content.keyword1.color = info.Color;

            content.keyword2 = new requestFieldDetail();
            content.keyword2.value = info.Reason;
            content.keyword2.color = info.Color;

            content.keyword3 = new requestFieldDetail();
            content.keyword3.value = info.RequestTime.ToString();
            content.keyword3.color = info.Color;

            content.keyword4 = new requestFieldDetail();
            content.keyword4.value = info.RepairStatus;
            content.keyword4.color = info.Color;

            content.keyword5 = new requestFieldDetail();
            content.keyword5.value = info.EstimateTime.ToString();
            content.keyword5.color = info.Color;

            content.remark = new requestFieldDetail();
            content.remark.value = info.Remarks;
            content.remark.color = info.Color;

            msg.Content = JsonConvert.SerializeObject(content);

            return JsonConvert.SerializeObject(msg);
        }

        /// <summary>
        /// 获取项目下工程部所有人
        /// </summary>
        /// <param name="projectConfig"></param>
        /// <returns></returns>
        public List<EmployeeCommonInfo> GetRepairAssignUserList(ProjectBizConfigInfo projectConfig)
        {
            var empList = new List<EmployeeCommonInfo>();

            int companyId = 0;
            int departmentId = 0;

            int.TryParse(projectConfig.Param1, out companyId);
            int.TryParse(projectConfig.Param2, out departmentId);

            var query = from dept in Context.Department
                        join em in Context.Employee on dept.DepartmentID equals em.DepartmentID
                        where em.Status == 1 && dept.DepartmentID == departmentId
                        select new
                        {
                            em.EmployeeID,
                            em.EmployeeName,
                            em.Tag
                        };

            var entityList = query.ToList();
            if (entityList != null)
            {
                foreach (var entity in entityList)
                {
                    var info = new EmployeeCommonInfo();
                    info.Id = entity.EmployeeID;
                    info.Name = string.Format("{0}_{1}", entity.EmployeeName, entity.Tag);
                    empList.Add(info);
                }
            }
            return empList;
        }
    }
}
