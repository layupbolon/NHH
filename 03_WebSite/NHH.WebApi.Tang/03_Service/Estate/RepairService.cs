using NHH.Entities;
using NHH.Framework.Service;
using NHH.Service.Estate.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Estate;
using NHH.Models.Common;
using NHH.Service.Project.IService;

namespace NHH.Service.Estate
{
    /// <summary>
    /// 维修服务
    /// </summary>
    public class RepairService : NHHService<NHHEntities>, IRepairService
    {
        #region Service

        private IProjectUnitService unitService;
        public IProjectUnitService UnitService
        {
            get
            {
                if (unitService == null)
                {
                    unitService = NHHServiceFactory.Instance.CreateService<IProjectUnitService>();
                }
                return unitService;
            }
        }
        #endregion
        /// <summary>
        /// 获取维修单各状态数量
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public RepairStatusInfo GetRepairStatusQty(int roleId, int merchantId, int userId)
        {
            RepairStatusInfo result=new RepairStatusInfo();
            result.MerchantID = merchantId;
            var entityList = (from r in Context.Repair
                              join ms in Context.Merchant_Store on r.StoreID equals ms.StoreID into t_ms
                              from ms in t_ms.DefaultIfEmpty()
                              where ((r.IsCommon == 1 && ms.MerchantID == merchantId) || (r.IsCommon == 2 && r.InUser == userId)) && r.Status == 1
                select new
                {
                    r.RepairStatus,
                    CommentCount =
                        (Context.Repair_Comment.Count(
                            item => r.RepairStatus == 3 && item.RepairID == r.RepairID && item.Status == 1))
                }).ToList();
            result.PendingQty = entityList.Count(item => item.RepairStatus == 1);
            result.ProcessingQty = entityList.Count(item => item.RepairStatus == 2);
            result.NoCommentsQty = entityList.Count(item => item.RepairStatus == 3 && item.CommentCount == 0);
            return result;
        }
        /// <summary>
        /// 获取维修列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public RepairListModel GetRepairList(RepairListQuery queryInfo)
        {
            var model = new RepairListModel();
            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page;
            model.PagingInfo.PageSize = queryInfo.Size;
            model.RepairList = new List<RepairInfo>();

            var query = from r in Context.Repair
                join ms in Context.Merchant_Store on r.StoreID equals ms.StoreID into t_ms
                from ms in t_ms.DefaultIfEmpty()
                where ((r.IsCommon == 1 && ms.MerchantID == queryInfo.MerchantID) || (r.IsCommon == 2 && r.InUser == queryInfo.UserID)) && r.Status == 1
                select new
                {
                    r.StoreID,
                    //MerchantID = (ms.MerchantID == null ? ms.MerchantID : 0),
                    r.RepairID,
                    r.RepairType,
                    r.Location,
                    r.RequestDesc,
                    r.RequestTime,
                    r.RequestUserName,
                    r.RepairStatus,
                    r.IsCommon,
                    CommentCount =
                        (Context.Repair_Comment.Count(item => item.RepairID == r.RepairID && item.Status == 1))
                };
            #region 查询条件

            //if (queryInfo.RoleID == 1) //管理员
            //    query = query.Where(item => item.MerchantID == queryInfo.MerchantID || item.IsCommon == 2);
            //if (queryInfo.RoleID == 2) //操作员
            //    query = query.Where(item => item.StoreID == queryInfo.StoreID || item.IsCommon == 2);

            if (queryInfo.Status.HasValue)
            {
                switch (queryInfo.Status.Value)
                {
                    case 3:
                        query = query.Where(item => item.CommentCount > 0 && item.RepairStatus == 3); //已完成
                        break;
                    case 4:
                        query = query.Where(item => item.CommentCount == 0 && item.RepairStatus == 3); //已完成未点评
                        break;
                    default:
                        query = query.Where(item => item.RepairStatus == queryInfo.Status.Value); //其它
                        break;
                }
            }
            if (queryInfo.BeginTime.HasValue)
            {
                query = query.Where(item => item.RequestTime >= queryInfo.BeginTime.Value);
            }
            if (queryInfo.EndTime.HasValue)
            {
                query = query.Where(item => item.RequestTime <= queryInfo.EndTime.Value);
            }
            #endregion
            model.PagingInfo.TotalCount = query.Count();
            
            switch (queryInfo.Sort)
            {
                case 1:
                    query = query.OrderByDescending(item => item.RequestTime).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
                    break;
                case 2:
                    query = query.OrderBy(item => item.RepairStatus).ThenBy(item=>item.CommentCount).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
                    break;
                default:
                    query = query.OrderByDescending(item => item.RequestTime).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
                    break;
            }

            var entityList = query.ToList();

            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var contract = new RepairInfo();

                    contract.RepairID = entity.RepairID;
                    contract.RepairType = entity.RepairType;
                    contract.Location = entity.Location;
                    contract.RequestDesc = entity.RequestDesc;
                    contract.RequestTime = entity.RequestTime;
                    contract.RequestUserName = entity.RequestUserName;
                    contract.RepairStatus = entity.RepairStatus;
                    if (contract.RepairStatus == 3 && entity.CommentCount == 0) //如果状态为已完成，并且未点评，则将状态改为未点评
                        contract.RepairStatus = 4;
                    
                    model.RepairList.Add(contract);
                });
            }

            return model;
        }

        /// <summary>
        /// 获取维修详情(精简)
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        public RepairInfo GetRepairSimple(int repairId)
        {
            var model = new RepairInfo();
            #region 查询条件
            var query = from r in Context.Repair
                        join mu in Context.Merchant_User on r.RequestUserID equals mu.UserID
                        where r.RepairID == repairId && r.Status == 1
                        select new
                       {
                           r.RepairID,
                           r.RepairType,
                           r.ProjectID,
                           r.StoreID,
                           r.FloorID,
                           r.UnitID,
                           r.IsCommon,
                           r.Location,
                           r.RepairStatus,
                           r.RequestDesc,
                           r.RequestTime,
                           r.RequestUserID,
                           r.RequestUserName,
                           RequestUserNickName = mu.NickName,
                           CommentCount = (Context.Repair_Comment.Count(item => item.RepairID == r.RepairID && item.Status == 1))
                       };
            #endregion
            var entity = query.FirstOrDefault();
            if (null != entity)
            {
                if (entity.UnitID > 0 && entity.IsCommon == 1)
                {
                    var unitInfo = UnitService.GetUnitInfo(entity.UnitID.Value);
                    model.Location = unitInfo.UnitNumber;
                }
                else
                {
                    model.Location = entity.Location;
                }
                model.RepairID = repairId;
                model.RepairType = entity.RepairType;
                model.ProjectID = entity.ProjectID;
                model.StoreID = entity.StoreID.HasValue ? entity.StoreID.Value : 0;
                model.FloorID = entity.FloorID.HasValue ? entity.FloorID.Value : 0;
                model.UnitID = entity.UnitID.HasValue ? entity.UnitID.Value : 0;
                model.IsCommon = entity.IsCommon;
                model.RequestDesc = entity.RequestDesc;
                model.RequestUserID = entity.RequestUserID.HasValue ? entity.RequestUserID.Value : 0;
                model.RequestUserName = !string.IsNullOrEmpty(entity.RequestUserNickName)
                    ? entity.RequestUserNickName
                    : entity.RequestUserName;
                model.RequestTime = entity.RequestTime;
                model.RepairStatus = entity.RepairStatus;
                if (model.RepairStatus == 3 && entity.CommentCount == 0) //如果状态为已完成，并且未点评，则将状态改为未点评
                    model.RepairStatus = 4;

            }
            return model;
        }

        /// <summary>
        /// 获取维修详情(详细)
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        public RepairInfo GetRepairDetail(int repairId)
        {
            var model = new RepairInfo();
            var query = from r in Context.Repair
                        join mu in Context.Merchant_User on r.RequestUserID equals mu.UserID
                where r.RepairID == repairId && r.Status == 1
                select new
                {
                    r.RepairID,
                    r.RepairType,
                    r.ProjectID,
                    r.StoreID,
                    r.FloorID,
                    r.UnitID,
                    r.IsCommon,
                    r.Location,
                    r.RequestDesc,
                    r.RequestUserID,
                    r.RequestUserName,
                    RequestUserNickName = mu.NickName,
                    r.RequestTime,
                    r.AcceptUserID,
                    r.AcceptUserName,
                    r.AcceptTime,
                    r.RepairDesc,
                    r.RepairUserID,
                    r.RepairUserName,
                    r.RepairStartTime,
                    r.RepairFinishTime,
                    r.RepairStatus,
                    CommentCount =
                        (Context.Repair_Comment.Count(item => item.RepairID == r.RepairID && item.Status == 1)),
                    CommentList = (from c in Context.Repair_Comment
                        where c.RepairID == r.RepairID && c.Status == 1
                        select new
                        {
                            c.CommentID,
                            c.Speed,
                            c.Quality,
                            c.Attitude,
                            c.Overall,
                            c.Comment,
                            c.Additional,
                            c.InDate
                        }),
                    AttachmentList = (from a in Context.Repair_Attachment
                        where a.RepairID == r.RepairID && a.Status == 1
                        select new
                        {
                            a.AttachmentID,
                            a.AttachmentName,
                            a.AttachmentPath,
                            a.InDate
                        }),
                    RepairLogList = (from rl in Context.Repair_Log
                        where rl.RepairID == r.RepairID
                        select new
                        {
                            rl.LogID,
                            rl.RepairID,
                            rl.LogText,
                            rl.LogTime
                        })
                };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                if (entity.UnitID > 0 && entity.IsCommon == 1)
                {
                    var unitInfo = UnitService.GetUnitInfo(entity.UnitID.Value);
                    model.Location = unitInfo.UnitNumber;
                }
                else
                {
                    model.Location = entity.Location;
                }
                model.RepairID = entity.RepairID;
                model.RepairType = entity.RepairType;
                model.ProjectID = entity.ProjectID;
                model.StoreID = entity.StoreID.HasValue ? entity.StoreID.Value : 0;
                model.FloorID = entity.FloorID.HasValue ? entity.FloorID.Value : 0;
                model.UnitID = entity.UnitID.HasValue ? entity.UnitID.Value : 0;
                model.IsCommon = entity.IsCommon;
                model.RequestDesc = entity.RequestDesc;
                model.RequestUserID = entity.RequestUserID.HasValue ? entity.RequestUserID.Value : 0;
                model.RequestUserName = !string.IsNullOrEmpty(entity.RequestUserNickName)
                    ? entity.RequestUserNickName
                    : entity.RequestUserName;
                model.RequestTime = entity.RequestTime;
                model.AcceptUserID = entity.AcceptUserID.HasValue ? entity.AcceptUserID.Value : 0;
                model.AcceptUserName = entity.AcceptUserName;
                model.AcceptTime = entity.AcceptTime;
                model.RepairDesc = entity.RepairDesc;
                model.RepairUserID = entity.RepairUserID.HasValue ? entity.RepairUserID.Value : 0;
                model.RepairUserName = entity.RepairUserName;
                model.RepairStartTime = entity.RepairStartTime;
                model.RepairFinishTime = entity.RepairFinishTime;
                model.RepairStatus = entity.RepairStatus;
                if (model.RepairStatus == 3 && entity.CommentCount == 0) //如果状态为已完成，并且未点评，则将状态改为未点评
                    model.RepairStatus = 4;

                //评论列表
                if (entity.CommentList != null)
                {
                    model.RepairCommentList = new List<RepairCommentInfo>();
                    entity.CommentList.ToList().ForEach(item => model.RepairCommentList.Add(new RepairCommentInfo
                    {
                        CommentId = item.CommentID,
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
                    model.RepairAttachmentList = new List<RepairAttachmentInfo>();
                    entity.AttachmentList.ToList()
                        .ForEach(item => model.RepairAttachmentList.Add(new RepairAttachmentInfo
                        {
                            AttachmentID = item.AttachmentID,
                            AttachmentName = item.AttachmentName,
                            AttachmentPath = item.AttachmentPath,
                            InDate = item.InDate
                        }));
                }
                //日志列表
                if (entity.RepairLogList != null)
                {
                    model.RepairLogList=new List<RepairLogInfo>();
                    entity.RepairLogList.ToList().ForEach(item=>model.RepairLogList.Add(new RepairLogInfo
                    {
                        LogID = item.LogID,
                        LogText = item.LogText,
                        LogTime = item.LogTime,
                        RepairID = item.RepairID
                    }));
                }
            }
            return model;
        }



        ///// <summary>
        ///// 获取维修人员信息
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <returns></returns>
        //public RepairDetailModel GetRepairUserById(int userId)
        //{
        //    var model = new RepairDetailModel();
        //    var entity = Context.Employee.Where(m => m.EmployeeID == userId).FirstOrDefault();
        //    if (null != entity)
        //    {
        //        model.RepairUserId = entity.EmployeeID;
        //        model.RepairUserName = entity.EmployeeName;
        //        model.RepairMobile = entity.Moblie;
        //    }
        //    return model;
        //}


        /// <summary>
        /// 添加报修
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddRepair(RepairInfo model)
        {
            var instance = CreateBizObject<NHH.Entities.Repair>();
            using (var trans = this.Context.Database.BeginTransaction())
            {
                var entity = new NHH.Entities.Repair
                {
                    ProjectID = model.ProjectID,
                    RepairStatus = 1,
                    RepairType = model.RepairType,
                    RequestTime = DateTime.Now,
                    IsCommon = model.IsCommon,
                    RequestDesc = model.RequestDesc,
                    RequestUserID = model.RequestUserID,
                    RequestUserName = model.RequestUserName,
                    RequestSrcType = 1,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = model.InUser,
                    EditDate = DateTime.Now,
                    EditUser = model.InUser,
                };
                if (entity.IsCommon == 1) //我的铺内
                {
                    entity.StoreID = model.StoreID;
                    entity.UnitID = model.UnitID;
                }
                if (entity.IsCommon == 2) //公共区域
                {
                    entity.FloorID = model.FloorID;
                    entity.Location = model.Location;
                }
                instance.Insert(entity);
                this.SaveChanges();
                model.RepairID = entity.RepairID;
                //添加附件
                if (model.RepairID > 0 && model.RepairAttachmentList != null && model.RepairAttachmentList.Count > 0)
                {
                    foreach (var attachment in model.RepairAttachmentList)
                    {
                        attachment.RepairID = model.RepairID;
                        attachment.InUser = model.InUser;
                        AddRepairAttachment(attachment);
                    }
                }
                trans.Commit();
            }
            return model.RepairID > 0;
        }

        /// <summary>
        /// 更新指定的报修信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateRepair(RepairInfo model)
        {
            var instance = CreateBizObject<Repair>();
            var entity = instance.GetBySysNo(model.RepairID);
            if (entity != null)
            {
                entity.RepairType = model.RepairType;
                entity.ProjectID = model.ProjectID;
                entity.StoreID = model.StoreID;
                entity.FloorID = model.FloorID;
                entity.UnitID = model.UnitID;
                entity.IsCommon = model.IsCommon;
                entity.Location = model.Location;
                entity.RequestDesc = model.RequestDesc;
                entity.EditDate = DateTime.Now;
                entity.EditUser = model.EditUser;

                instance.Update(entity);
                this.SaveChanges();
                return  true;
            }
            return false;
        }

        /// <summary>
        /// 作废报修
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        public bool CancelRepair(int repairId)
        {
            var instance = CreateBizObject<Repair>();
            var entity = instance.GetBySysNo(repairId);
            if (entity != null)
            {
                entity.Status = -1;
                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }

        ///// <summary>
        ///// 根据维修单号指派维修人
        ///// </summary>
        ///// <param name="info"></param>
        //public bool RepairAssign(RepairAssignInfo info)
        //{
        //    var instance = CreateBizObject<NHH.Entities.Repair>();
        //    var entity = instance.GetBySysNo(info.RepairId);
        //    if (entity == null)
        //        return false;

        //    entity.RepairUserID = info.RepairUserId;
        //    entity.RepairUserName = (from e in Context.Employee
        //                             where e.EmployeeID == info.RepairUserId
        //                             select e.EmployeeName).FirstOrDefault();
        //    entity.RepairStatus = 2;
        //    entity.RepairStartTime = DateTime.Now;
        //    entity.AcceptTime = DateTime.Now;
        //    entity.AcceptUserID = 1;
        //    entity.EditDate = DateTime.Now;
        //    entity.EditUser = 1;

        //    instance.Update(entity);
        //    this.SaveChanges();
        //    return true;
        //}

        ///// <summary>
        ///// 维修结束
        ///// </summary>
        ///// <param name="repairId"></param>
        //public void RepairFinish(int repairId)
        //{
        //    var instance = CreateBizObject<NHH.Entities.Repair>();
        //    var entity = instance.GetBySysNo(repairId);
        //    if (entity == null)
        //        return;

        //    entity.RepairFinishTime = DateTime.Now;
        //    entity.RepairStatus = 3;
        //    entity.EditDate = DateTime.Now;
        //    entity.EditUser = 1;

        //    instance.Update(entity);
        //    this.SaveChanges();

        //}

        /// <summary>
        /// 添加维修附件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddRepairAttachment(RepairAttachmentInfo model)
        {
            var instance = CreateBizObject<Repair_Attachment>();
            var repairs = CreateBizObject<Repair>();
            DateTime date = DateTime.Now;
            var entity = new Repair_Attachment
            {
                RepairID = model.RepairID,
                AttachmentName = model.AttachmentName,
                AttachmentPath = model.AttachmentPath,
                Status = 1,
                InDate = date,
                InUser = model.InUser,
                EditDate = date,
                EditUser = model.InUser,
            };
            instance.Insert(entity);
            this.SaveChanges();
            model.AttachmentID = entity.AttachmentID;
            return model.AttachmentID > 0;
        }

        /// <summary>
        /// 获取指定的报修附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        public RepairAttachmentInfo GetRepairAttachment(int attachmentId)
        {
            var model = new RepairAttachmentInfo();
            var entity = Context.Repair_Attachment.FirstOrDefault(item => item.AttachmentID == attachmentId);
            if (null != entity)
            {
                model.AttachmentID = entity.AttachmentID;
                model.RepairID = entity.RepairID;
                model.AttachmentName = entity.AttachmentName;
                model.AttachmentPath = NHH.Framework.Utility.UrlHelper.GetImageUrl(model.AttachmentPath, 100);
                model.InDate = entity.InDate;
            }
            return model;
        }

        /// <summary>
        /// 作废指定报修附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        public bool CancelRepairAttachment(int attachmentId)
        {
            var instance = CreateBizObject<Repair_Attachment>();
            var entity = instance.GetBySysNo(attachmentId);
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
        /// 添加评价
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddComment(RepairCommentInfo model)
        {
            var instance = CreateBizObject<NHH.Entities.Repair_Comment>();
            var entity = new NHH.Entities.Repair_Comment
            {
                RepairID = model.RepairId,
                Speed = model.Speed,
                Quality = model.Quality,
                Attitude = model.Attitude,
                Overall = model.Overall,
                Comment = model.Comment,
                Status = 1,
                InDate = DateTime.Now,
                InUser = model.InUser,
                EditDate = DateTime.Now,
                EditUser = model.EditUser,
            };
            instance.Insert(entity);
            this.SaveChanges();
            model.CommentId = entity.CommentID;
            return model.CommentId > 0;
        }

        /// <summary>
        /// 获取维修点评详情
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public RepairCommentInfo GetCommentDetail(int commentId)
        {
            var model = new RepairCommentInfo();
            Expression<Func<Repair_Comment, bool>> where = m => m.CommentID == commentId && m.Status == 1;
            var entity = Context.Repair_Comment.Where(where).FirstOrDefault();
            if (null != entity)
            {
                model.CommentId = entity.CommentID;
                model.RepairId = entity.RepairID;
                model.Speed = entity.Speed.HasValue ? entity.Speed.Value : 0;
                model.Quality = entity.Quality.HasValue ? entity.Quality.Value : 0;
                model.Attitude = entity.Attitude.HasValue ? entity.Attitude.Value : 0;
                model.Overall = entity.Overall.HasValue ? entity.Overall.Value : 0;
                model.InDate = entity.InDate;
                model.Comment = entity.Comment;
                model.Additional = entity.Additional;
            }
            return model;
        }

        /// <summary>
        /// 追加评论
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddAdditional(RepairCommentInfo model)
        {
            var instance = CreateBizObject<Repair_Comment>();
            var entity = instance.GetBySysNo(model.CommentId);
            if (entity != null)
            {
                entity.Additional = model.Additional;
                entity.EditDate = DateTime.Now;
                entity.EditUser = model.EditUser;

                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
