using System.Runtime.Remoting.Contexts;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Estate;
using NHH.Service.Estate.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Estate
{
    /// <summary>
    /// 投诉服务
    /// </summary>
    public class ComplaintService : NHHService<NHHEntities>, IComplaintService
    {
        /// <summary>
        /// 获取投诉各状态数量
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="merchantId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public ComplaintStatusInfo GetComplaintStatusQty(int roleId, int merchantId, int storeId)
        {
            ComplaintStatusInfo result = new ComplaintStatusInfo();
            result.StoreID = storeId;
            result.MerchantID = merchantId;
            
            var entityList = (from cpt in Context.Complaint
                where cpt.Status == 1 && cpt.MerchantID == merchantId
                select new
                {
                    cpt.StoreID,
                    cpt.MerchantID,
                    cpt.ComplaintStatus,
                    CommentCount =
                        (Context.Complaint_Comment.Count(
                            item => cpt.ComplaintStatus == 3 && item.ComplaintID == cpt.ComplaintID && item.Status == 1))
                }).ToList();
            if (roleId == 2) //操作员
            {
                entityList = entityList.Where(item => item.StoreID == storeId).ToList();
            }
            result.PendingQty = entityList.Count(item => item.ComplaintStatus == 1);
            result.ProcessingQty = entityList.Count(item => item.ComplaintStatus == 2);
            result.NoCommentsQty = entityList.Count(item => item.ComplaintStatus == 3 && item.CommentCount == 0);
            return result;
        }

        /// <summary>
        /// 获取投诉任务处理列表
        /// </summary>
        /// <returns></returns>
        public ComplaintListModel GetComplaintList(ComplaintListQuery queryInfo)
        {
            var model = new ComplaintListModel();
            model.ComplaintListInfo = new List<ComplaintInfo>();
            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page;
            model.PagingInfo.PageSize = queryInfo.Size;

            #region 查询信息

            var query = from cpt in Context.Complaint
                        join dcType in Context.Dictionary on cpt.ComplaintType equals dcType.FieldValue
                        join dcStatus in Context.Dictionary on cpt.ComplaintStatus equals dcStatus.FieldValue
                        join mu in Context.Merchant_User on cpt.RequestUserID equals mu.UserID into t_mu
                        from mu in t_mu.DefaultIfEmpty()
                        where
                            dcType.FieldType == "ComplaintType" && dcStatus.FieldType == "ComplaintStatus" && cpt.Status == 1 &&
                            cpt.MerchantID == queryInfo.MerchantID
                        select new
                        {
                            cpt.StoreID,
                            cpt.MerchantID,
                            cpt.ComplaintID,
                            cpt.ComplaintType,
                            cpt.RequestUserID,
                            //mu.UserName,
                            RequestUserName = mu.UserName,
                            RequestUserNickName = mu.NickName,
                            cpt.RequestTime,
                            cpt.RequestDesc,
                            cpt.ComplaintStatus,
                            cpt.RequestTarget,
                            CommentCount =
                                (Context.Complaint_Comment.Count(item => item.ComplaintID == cpt.ComplaintID && item.Status == 1))
                        };
            #endregion

            #region 查询条件

            if (queryInfo.Status.HasValue)
            {
                switch (queryInfo.Status.Value)
                {
                    case 3:
                        query = query.Where(item => item.CommentCount > 0 && item.ComplaintStatus == 3); //已完成
                        break;
                    case 4:
                        query = query.Where(item => item.CommentCount == 0 && item.ComplaintStatus == 3); //已完成未点评
                        break;
                    default:
                        query = query.Where(item => item.ComplaintStatus == queryInfo.Status.Value); //其它
                        break;
                }
            }
            if (queryInfo.BeginTime.HasValue)
                query = query.Where(item => item.RequestTime >= queryInfo.BeginTime.Value);
            if (queryInfo.EndTime.HasValue)
                query = query.Where(item => item.RequestTime <= queryInfo.EndTime.Value);
            if (queryInfo.RoleID == 2)
                query = query.Where(item => item.StoreID == queryInfo.StoreID);
            #endregion 查询条件

            model.PagingInfo.TotalCount = query.Count();

            #region 排序
            switch (queryInfo.Sort)
            {
                case 1:
                    query = query.OrderByDescending(item => item.RequestTime).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
                    break;
                case 2:
                    query = query.OrderBy(item => item.ComplaintStatus).ThenBy(item => item.CommentCount).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
                    break;
                default:
                    query = query.OrderByDescending(item => item.RequestTime).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
                    break;
            }
            #endregion 排序

            var entityList = query.ToList();
            entityList.ForEach(entity =>
            {
                var info = new ComplaintInfo();

                info.ComplaintID = entity.ComplaintID;
                info.ComplaintType = entity.ComplaintType;
                info.RequestUserID = entity.RequestUserID.HasValue ? entity.RequestUserID.Value : 0;
                info.RequestUserName = !string.IsNullOrEmpty(entity.RequestUserNickName)
                    ? entity.RequestUserNickName
                    : entity.RequestUserName;
                info.RequestTarget = entity.RequestTarget;
                info.RequestDesc = entity.RequestDesc;
                info.RequestTime = entity.RequestTime;
                info.ComplaintStatus = entity.ComplaintStatus;
                if (info.ComplaintStatus == 3 && entity.CommentCount == 0) //如果状态为已完成，并且未点评，则将状态改为未点评
                    info.ComplaintStatus = 4;
                model.ComplaintListInfo.Add(info);
            });
            return model;
        }



        ///// <summary>
        ///// 受理投诉请求
        ///// </summary>
        ///// <param name="complaintId"></param>
        ///// <returns></returns>
        //public bool AcceptedRequest(int complaintId)
        //{
        //    var instance = CreateBizObject<Complaint>();
        //    var entity = instance.GetBySysNo(complaintId);
        //    if (entity != null)
        //    {
        //        entity.AcceptUserID = 1;
        //        entity.AcceptUserName = "";
        //        entity.ComplaintStatus = 2;//改成已受理
        //        entity.AcceptTime = DateTime.Now;
        //        entity.EditDate = DateTime.Now;
        //        entity.EditUser = 0;

        //        instance.Update(entity);
        //        this.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// 获取投诉详情
        ///// </summary>
        ///// <param name="complaintId"></param>
        ///// <returns></returns>
        //public ComplaintDetailModel GetComplaintDetail(int complaintId)
        //{
        //    var model = new ComplaintDetailModel();

        //    //var query = from cpt in Context.Complaint
        //    //            join dcType in Context.Dictionary on cpt.ComplaintType equals dcType.FieldValue
        //    //            join dcStatus in Context.Dictionary on cpt.ComplaintStatus equals dcStatus.FieldValue
        //    //            join atc in Context.Complaint_Attachment on cpt.ComplaintID equals atc.ComplaintID into JoinedComplaintAttachment
        //    //            from atc in JoinedComplaintAttachment.DefaultIfEmpty()
        //    //            where dcType.FieldType == "ComplaintType" && dcStatus.FieldType == "ComplaintStatus" && cpt.ComplaintID == complaintId
        //    //            select new
        //    //            {
        //    //                cpt.ComplaintID,
        //    //                cpt.RequestTime,
        //    //                cpt.RequestDesc,
        //    //                cpt.ServiceUserID,
        //    //                cpt.ComplaintStatus,
        //    //                cpt.InvestigationDesc,
        //    //                cpt.ServiceDesc,
        //    //                atc.AttachmentName,
        //    //                atc.AttachmentPath,
        //    //                cpt.AcceptTime,
        //    //                cpt.ServiceStartTime,
        //    //                ComplaintTypeName = dcType.FieldName,
        //    //                ComplaintStatusName = dcStatus.FieldName,
        //    //                cpt.Complaint_Attachment
        //    //            };
        //    //var entity = query.FirstOrDefault();
        //    //if (entity != null)
        //    //{
        //    //    model.ComplaintId = entity.ComplaintID;
        //    //    model.ComplaintTypeName = entity.ComplaintTypeName;
        //    //    model.RequestTime = entity.RequestTime;
        //    //    model.RequestDesc = entity.RequestDesc;
        //    //    model.ServiceUserId = entity.ServiceUserID;
        //    //    model.ComplaintStatus = entity.ComplaintStatus;
        //    //    model.ComplaintStatusName = entity.ComplaintStatusName;
        //    //    model.InvestigationDesc = entity.InvestigationDesc;
        //    //    model.ServiceDesc = entity.ServiceDesc;
        //    //    model.AcceptTime = entity.AcceptTime;
        //    //    model.StartTime = entity.ServiceStartTime;
        //    //    //   model.AttachmentInfo.AttachmentName = entity.AttachmentName;
        //    //    //  model.AttachmentInfo.AttachmentPath = entity.AttachmentPath;
        //    //}

        //    /////查询出投诉证据相应附件信息
        //    //if (entity.Complaint_Attachment != null)
        //    //{
        //    //    model.AttachmentInfoList = new List<AttachmentInfo>();
        //    //    var attachList = entity.Complaint_Attachment.ToList();

        //    //    attachList.ForEach(item =>
        //    //    {
        //    //        var attachmentInfo = new AttachmentInfo()
        //    //        {
        //    //            AttachmentPath = item.AttachmentPath
        //    //        };
        //    //        model.AttachmentInfoList.Add(attachmentInfo);

        //    //    });
        //    //}

        //    /////投诉表中的客服人员名字从员工表中更新
        //    //if (model.ServiceUserId.HasValue)
        //    //{
        //    //    model.ServiceUserName = (from e in Context.Employee
        //    //                             where e.EmployeeID == model.ServiceUserId.Value
        //    //                             select e.EmployeeName).FirstOrDefault();
        //    //}

        //    return model;
        //}

        /// <summary>
        /// 更新投诉信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateComplaint(ComplaintInfo model)
        {
            var instance = CreateBizObject<Complaint>();
            var entity = instance.GetBySysNo(model.ComplaintID);
            if (entity != null)
            {
                entity.RequestUserID = model.RequestUserID;
                entity.RequestUserName = model.RequestUserName;
                entity.RequestTarget = model.RequestTarget;
                entity.RequestDesc = model.RequestDesc;
                entity.EditDate = DateTime.Now;
                entity.EditUser = entity.EditUser;

                instance.Update(entity);
                this.SaveChanges();
                return  true;
            }
            return false;
        }

        /// <summary>
        /// 作废投诉单
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        public bool CancelComplaint(int complaintId)
        {
            var instance = CreateBizObject<Complaint>();
            var entity = instance.GetBySysNo(complaintId);
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
        /// 增加投诉单 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddComplaint(ComplaintInfo model)
        {
            var instance = CreateBizObject<Complaint>();
            var entity = new Complaint()
            {
                ComplaintType = 1,
                ProjectID = model.ProjectID,
                MerchantID = model.MerchantID,
                StoreID = model.StoreID,
                RequestUserID = model.RequestUserID,
                RequestUserName = model.RequestUserName,
                RequestTarget = model.RequestTarget,
                RequestDesc = model.RequestDesc,
                RequestTime = DateTime.Now,
                RequestSrcType = 1,
                ComplaintStatus = 1,
                Status = 1,
                InDate = DateTime.Now,
                InUser = model.InUser,
                EditDate = DateTime.Now,
                EditUser = model.InUser
            };
            instance.Insert(entity);
            this.SaveChanges();
            model.ComplaintID = entity.ComplaintID;
            return model.ComplaintID > 0;
        }

        /// <summary>
        /// 获取指定的投诉信息（精简）
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        public ComplaintInfo GetComplaintSimple(int complaintId)
        {
            var model = new ComplaintInfo();
            var query = from c in Context.Complaint
                        join pj in Context.Project on c.ProjectID equals pj.ProjectID
                        join m in Context.Merchant on c.MerchantID equals m.MerchantID
                        //join s in Context.Merchant_Store on c.StoreID equals s.StoreID
                        join mu in Context.Merchant_User on c.RequestUserID equals mu.UserID
                        where c.ComplaintID == complaintId && c.Status == 1
                        select new
                        {
                            c.ComplaintID,
                            c.ComplaintType,
                            c.ProjectID,
                            pj.ProjectName,
                            c.MerchantID,
                            m.MerchantName,
                            //c.StoreID,
                            //s.StoreName,
                            c.RequestUserID,
                            RequestUserName = mu.UserName,
                            RequestUserNickName = mu.NickName,
                            c.RequestTarget,
                            c.RequestDesc,
                            c.RequestTime,
                            c.ComplaintStatus,
                            CommentCount =
                                (Context.Complaint_Comment.Count(item => item.ComplaintID == c.ComplaintID && item.Status == 1))
                        };
            var entity = query.FirstOrDefault();
            if (null != entity)
            {
                model.ComplaintID = entity.ComplaintID;
                model.ComplaintType = entity.ComplaintType;
                model.ProjectID = entity.ProjectID;
                model.ProjectName = entity.ProjectName;
                model.MerchantID = entity.MerchantID.HasValue ? entity.MerchantID.Value : 0;
                model.MerchantName = entity.MerchantName;
                //model.StoreID = entity.StoreID.HasValue ? entity.StoreID.Value : 0;
                //model.StoreName = entity.StoreName;
                model.RequestUserID = entity.RequestUserID.HasValue ? entity.RequestUserID.Value : 0;
                //model.RequestUserName = entity.RequestUserName;
                model.RequestUserName = !string.IsNullOrEmpty(entity.RequestUserNickName)
                    ? entity.RequestUserNickName
                    : entity.RequestUserName;
                model.RequestTarget = entity.RequestTarget;
                model.RequestDesc = entity.RequestDesc;
                model.RequestTime = entity.RequestTime;
                model.ComplaintStatus = entity.ComplaintStatus;
                if (model.ComplaintStatus == 3 && entity.CommentCount == 0) //如果状态为已完成，并且未点评，则将状态改为未点评
                    model.ComplaintStatus = 4;
            }
            return model;
        }

        /// <summary>
        /// 获取指定的投诉信息（详细）
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        public ComplaintInfo GetComplaintDetail(int complaintId)
        {
            var model = new ComplaintInfo();
            var query = from c in Context.Complaint
                        join pj in Context.Project on c.ProjectID equals pj.ProjectID
                        join m in Context.Merchant on c.MerchantID equals m.MerchantID
                        //join s in Context.Merchant_Store on c.StoreID equals s.StoreID into t_s from s in t_s.DefaultIfEmpty()
                        join mu in Context.Merchant_User on c.RequestUserID equals mu.UserID
                        join au in Context.Sys_User on c.AcceptUserID equals au.UserID into t_au
                        from au in t_au.DefaultIfEmpty()
                        join su in Context.Sys_User on c.ServiceUserID equals su.UserID into t_su
                        from su in t_su.DefaultIfEmpty()
                        where c.ComplaintID == complaintId && c.Status == 1
                        select new
                        {
                            c.ComplaintID,
                            c.ComplaintType,
                            c.ProjectID,
                            pj.ProjectName,
                            c.MerchantID,
                            m.MerchantName,
                            //c.StoreID,
                            //s.StoreName,
                            c.RequestUserID,
                            //RequestUserName = mu.UserName,
                            RequestUserName=mu.UserName,
                            RequestUserNickName=mu.NickName,
                            c.RequestTarget,
                            c.RequestDesc,
                            c.RequestTime,
                            c.AcceptUserID,
                            AcceptUserName = au.UserName,
                            c.AcceptTime,
                            c.InvestigationDesc,
                            c.ServiceUserID,
                            ServiceUserName = su.UserName,
                            c.ServiceStartTime,
                            c.ServiceFinishTime,
                            c.ServiceDesc,
                            c.ComplaintStatus,
                            CommentCount =
                                (Context.Complaint_Comment.Count(item => item.ComplaintID == c.ComplaintID && item.Status == 1)),
                            CommentList = (from cc in Context.Complaint_Comment
                                           where cc.ComplaintID == c.ComplaintID && cc.Status == 1
                                           select new
                                           {
                                               cc.CommentID,
                                               cc.Speed,
                                               cc.Quality,
                                               cc.Attitude,
                                               cc.Overall,
                                               cc.Comment,
                                               cc.Additional,
                                               cc.InDate
                                           }),
                            AttachmentList = (from ca in Context.Complaint_Attachment
                                              join mu2 in Context.Merchant_User on ca.InUser equals mu2.UserID into t_mu2
                                              from mu2 in t_mu2.DefaultIfEmpty()
                                              where ca.ComplaintID == c.ComplaintID && ca.Status == 1
                                              select new
                                              {
                                                  ca.AttachmentID,
                                                  ca.AttachmentName,
                                                  ca.AttachmentPath,
                                                  ca.InDate,
                                                  InUserName = mu2.UserName
                                              }),
                            ComplaintLogList = (from cl in Context.Complaint_Log
                                                where cl.ComplaintID == c.ComplaintID
                                                select new
                                                {
                                                    cl.LogID,
                                                    cl.ComplaintID,
                                                    cl.LogText,
                                                    cl.LogTime
                                                })
                        };

            var entity = query.FirstOrDefault();
            if (null != entity)
            {
                model.ComplaintID = entity.ComplaintID;
                model.ComplaintType = entity.ComplaintType;
                model.ProjectID = entity.ProjectID;
                model.ProjectName = entity.ProjectName;
                model.MerchantID = entity.MerchantID.HasValue ? entity.MerchantID.Value : 0;
                model.MerchantName = entity.MerchantName;
                //model.StoreID = entity.StoreID.HasValue ? entity.StoreID.Value : 0;
                //model.StoreName = entity.StoreName;
                model.RequestUserID = entity.RequestUserID.HasValue ? entity.RequestUserID.Value : 0;
                //model.RequestUserName = entity.RequestUserName;
                model.RequestUserName = !string.IsNullOrEmpty(entity.RequestUserNickName)
                    ? entity.RequestUserNickName
                    : entity.RequestUserName;
                model.RequestTarget = entity.RequestTarget;
                model.RequestDesc = entity.RequestDesc;
                model.RequestTime = entity.RequestTime;
                model.AcceptUserID = entity.AcceptUserID.HasValue ? entity.AcceptUserID.Value : 0;
                model.AcceptUserName = entity.AcceptUserName;
                model.AcceptTime = entity.AcceptTime;
                model.InvestigationDesc = entity.InvestigationDesc;
                model.ServiceUserID = entity.ServiceUserID.HasValue ? entity.ServiceUserID.Value : 0;
                model.ServiceUserName = entity.ServiceUserName;
                model.ServiceStartTime = entity.ServiceStartTime;
                model.ServiceFinishTime = entity.ServiceFinishTime;
                model.ServiceDesc = entity.ServiceDesc;
                model.ComplaintStatus = entity.ComplaintStatus;
                if (model.ComplaintStatus == 3 && entity.CommentCount == 0) //如果状态为已完成，并且未点评，则将状态改为未点评
                    model.ComplaintStatus = 4;
                //评论列表
                if (entity.CommentList != null)
                {
                    model.CommentList = new List<ComplaintCommentInfo>();
                    entity.CommentList.ToList().ForEach(item => model.CommentList.Add(new ComplaintCommentInfo
                    {
                        CommentID = item.CommentID,
                        Speed = item.Speed.HasValue ? item.Speed.Value : 0,
                        Quality = item.Quality.HasValue ? item.Quality.Value : 0,
                        Attitude = item.Attitude.HasValue ? item.Attitude.Value : 0,
                        Overall = item.Overall.HasValue ? item.Overall.Value : 0,
                        Comment = item.Comment,
                        Additional = item.Additional,
                        InDate = item.InDate
                    }));
                }
                //附件列表
                if (entity.AttachmentList != null)
                {
                    model.AttachmentList = new List<ComplaintAttachmentInfo>();
                    entity.AttachmentList.ToList()
                        .ForEach(item => model.AttachmentList.Add(new ComplaintAttachmentInfo
                        {
                            AttachmentID = item.AttachmentID,
                            AttachmentName = item.AttachmentName,
                            AttachmentPath = item.AttachmentPath,
                            InDate = item.InDate,
                            InUserName = item.InUserName
                        }));
                }
                //日志列表
                if (entity.ComplaintLogList != null)
                {
                    model.ComplaintLogList = new List<ComplaintLogInfo>();
                    entity.ComplaintLogList.ToList().ForEach(item => model.ComplaintLogList.Add(new ComplaintLogInfo
                    {
                        ComplaintID = item.ComplaintID,
                        LogID = item.LogID,
                        LogText = item.LogText,
                        LogTime = item.LogTime
                    }));
                }
            }
            return model;
        }

        /// <summary>
        /// 添加投诉附件
        /// </summary>
        public bool AddAttachment(ComplaintAttachmentInfo model)
        {
            var instance = CreateBizObject<Complaint_Attachment>();
            var complaints = CreateBizObject<Complaint>();
            DateTime date = DateTime.Now;
            var entity = new Complaint_Attachment()
            {
                ComplaintID = model.ComplaintID,
                AttachmentName = model.AttachmentName,
                AttachmentPath =
                    string.Format("Complaint/Original/{0}/{1}/{2}/{3}/{4}",
                        complaints.TopOne(r => r.ComplaintID == model.ComplaintID).ProjectID, date.Year, date.Month, date.Day,
                        model.AttachmentName),
                Status = 1,
                InDate = date,
                InUser = model.InUser
            };
            instance.Insert(entity);
            this.SaveChanges();
            model.AttachmentID = entity.AttachmentID;
            return model.AttachmentID > 0;
        }

        /// <summary>
        /// 获取指定的投诉附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        public ComplaintAttachmentInfo GetAttachment(int attachmentId)
        {
            var model = new ComplaintAttachmentInfo();
            var query = from ca in Context.Complaint_Attachment
                        join mu in Context.Merchant_User on ca.InUser equals mu.UserID into t_mu
                        from mu in t_mu.DefaultIfEmpty()
                        where ca.AttachmentID == attachmentId && ca.Status == 1
                        select new
                        {
                            ca.AttachmentID,
                            ca.ComplaintID,
                            ca.AttachmentName,
                            ca.AttachmentPath,
                            ca.InUser,
                            ca.InDate,
                            InUserName = mu.UserName
                        };
            var entity = query.FirstOrDefault();
            if (null != entity)
            {
                model.AttachmentID = entity.AttachmentID;
                model.ComplaintID = entity.ComplaintID;
                model.AttachmentName = entity.AttachmentName;
                model.AttachmentPath = NHH.Framework.Utility.UrlHelper.GetImageUrl(model.AttachmentPath, 100);
                model.InDate = entity.InDate;
                model.InUser = entity.InUser;
                model.InUserName = entity.InUserName;
            }
            return model;
        }

        /// <summary>
        /// 获取指定投诉单附件列表
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        public List<ComplaintAttachmentInfo> GetAttachmentList(int complaintId)
        {
            var model = new List<ComplaintAttachmentInfo>();
            var query = from ca in Context.Complaint_Attachment
                        join mu in Context.Merchant_User on ca.InUser equals mu.UserID into t_mu
                        from mu in t_mu.DefaultIfEmpty()
                        where ca.ComplaintID == complaintId && ca.Status == 1
                        select new
                        {
                            ca.AttachmentID,
                            ca.ComplaintID,
                            ca.AttachmentName,
                            ca.AttachmentPath,
                            ca.InUser,
                            ca.InDate,
                            InUserName = mu.UserName
                        };
            var entityList = query.OrderBy(item => item.InDate).ToList();
            if (null != entityList)
            {
                entityList.ForEach(entity =>
                {
                    var contract = new ComplaintAttachmentInfo()
                    {
                        AttachmentID = entity.AttachmentID,
                        ComplaintID = entity.ComplaintID,
                        AttachmentName = entity.AttachmentName,
                        AttachmentPath = NHH.Framework.Utility.UrlHelper.GetImageUrl( entity.AttachmentPath,100),
                        InDate = entity.InDate,
                        InUser = entity.InUser,
                        InUserName = entity.InUserName
                    };
                    model.Add(contract);
                });
            }
            return model;
        }

        /// <summary>
        /// 作废投诉附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        public bool CancelAttachment(int attachmentId)
        {
            var instance = CreateBizObject<Complaint_Attachment>();
            var entity = instance.GetBySysNo(attachmentId);
            if (entity != null)
            {
                entity.Status = -1;
                instance.Update(entity);
                this.SaveChanges();
                return  true;
            }
            return false;
        }

        /// <summary>
        /// 添加投诉单评论
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddComment(ComplaintCommentInfo model)
        {
            var instance = CreateBizObject<Complaint_Comment>();
            var entity = new Complaint_Comment
            {
                ComplaintID = model.ComplaintID,
                Speed = model.Speed,
                Quality = model.Quality,
                Attitude = model.Attitude,
                Overall = model.Overall,
                Comment = model.Comment,
                Status = 1,
                InDate = DateTime.Now,
                InUser = model.InUser,
                EditUser = model.InUser,
                EditDate = DateTime.Now
            };
            instance.Insert(entity);
            this.SaveChanges();
            model.CommentID = entity.CommentID;
            return model.CommentID > 0;
        }

        /// <summary>
        /// 获取指定的评论
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public ComplaintCommentInfo GetComment(int commentId)
        {
            var model = new ComplaintCommentInfo();
            var query = from cc in Context.Complaint_Comment
                        join mu in Context.Merchant_User on cc.InUser equals mu.UserID into t_mu
                        from mu in t_mu.DefaultIfEmpty()
                        where cc.CommentID == commentId && cc.Status == 1
                        select new
                        {
                            cc.CommentID,
                            cc.ComplaintID,
                            cc.Speed,
                            cc.Quality,
                            cc.Attitude,
                            cc.Overall,
                            cc.Comment,
                            cc.Additional,
                            cc.InDate,
                            cc.InUser,
                            InUserName = mu.UserName
                        };
            var entity = query.FirstOrDefault();
            if (null != entity)
            {
                model.CommentID = entity.CommentID;
                model.ComplaintID = entity.ComplaintID;
                model.Speed = entity.Speed.HasValue ? entity.Speed.Value : 0;
                model.Quality = entity.Quality.HasValue ? entity.Quality.Value : 0;
                model.Attitude = entity.Attitude.HasValue ? entity.Attitude.Value : 0;
                model.Overall = entity.Overall.HasValue ? entity.Overall.Value : 0;
                model.Comment = entity.Comment;
                model.Additional = entity.Additional;
                model.InDate = entity.InDate;
                model.InUser = entity.InUser;
                model.InUserName = entity.InUserName;
            }
            return model;
        }

        /// <summary>
        /// 更新追加评论
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateCommentAdditional(ComplaintCommentInfo model)
        {
            var instance = CreateBizObject<Complaint_Comment>();
            var entity = instance.GetBySysNo(model.CommentID);
            if (entity != null)
            {
                entity.Additional = model.Additional;
                entity.EditUser = model.EditUser;
                entity.EditDate = DateTime.Now;
                instance.Update(entity);
                this.SaveChanges();
                return  true;
            }
            return false;
        }
    }
}
