using Newtonsoft.Json;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Framework.Utility;
using NHH.Framework.Wechat;
using NHH.Message.Models.Wechat;
using NHH.Models.Common;
using NHH.Models.Estate;
using NHH.Models.Message;
using NHH.Service.Estate.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
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
        /// 获取投诉任务处理列表
        /// </summary>
        /// <returns></returns>
        public ComplaintListModel GetComplaintList(ComplaintListQueryInfo queryInfo)
        {
            var model = new ComplaintListModel();
            model.ComplaintListInfo = new List<ComplaintInfo>();
            model.PagingInfo = new PagingInfo();
            model.QueryInfo = queryInfo;

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

            #region 查询信息
            var query = from cpt in Context.Complaint
                        join dcType in Context.Dictionary on cpt.ComplaintType equals dcType.FieldValue
                        join dcStatus in Context.Dictionary on cpt.ComplaintStatus equals dcStatus.FieldValue
                        where dcType.FieldType == "ComplaintType" && dcStatus.FieldType == "ComplaintStatus" && cpt.Status == 1 && projectList.Contains(cpt.ProjectID)
                        select new
                        {
                            cpt.ComplaintID,
                            cpt.ComplaintType,
                            ComplaintTypeName = dcType.FieldName,
                            cpt.RequestTime,
                            cpt.ComplaintStatus,
                            ComplaintStatusName = dcStatus.FieldName,
                            cpt.ServiceUserID,
                            cpt.ServiceUserName,
                            cpt.RequestTarget,
                            cpt.RequestDesc
                        };

            if (queryInfo != null)
            {
                if (queryInfo.ComplaintStatus.HasValue)
                {
                    query = query.Where(m => m.ComplaintStatus == queryInfo.ComplaintStatus.Value);
                }
                if (queryInfo.ComplaintId.HasValue)
                {
                    query = query.Where(m => m.ComplaintID == queryInfo.ComplaintId.Value);
                }
                if (queryInfo.ComplaintType.HasValue)
                {
                    query = query.Where(m => m.ComplaintType == queryInfo.ComplaintType.Value);
                }
                if (queryInfo.ServiceUserId.HasValue)
                {
                    query = query.Where(m => m.ServiceUserID == queryInfo.ServiceUserId.Value);
                }
                if (queryInfo.FromDate.HasValue)
                {
                    query = query.Where(m => m.RequestTime >= queryInfo.FromDate.Value);
                }
                if (queryInfo.ToDate.HasValue)
                {
                    query = query.Where(m => m.RequestTime <= queryInfo.ToDate.Value);
                }

            }
            #endregion

            model.PagingInfo.PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1;
            model.PagingInfo.TotalCount = query.Count();

            query = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);

            var entityList = query.ToList();
            entityList.ForEach(entity =>
            {
                var info = new ComplaintInfo();

                info.ComplaintId = entity.ComplaintID;
                info.ComplaintTypeName = entity.ComplaintTypeName;
                info.RequestTime = entity.RequestTime;
                info.ComplaintStatusName = entity.ComplaintStatusName;
                info.ServiceUserId = entity.ServiceUserID;
                info.ServiceUserName = entity.ServiceUserName;
                info.ComplaintStatus = entity.ComplaintStatus;
                info.RequestTarget = entity.RequestTarget;
                info.RequestDesc = entity.RequestDesc;
                model.ComplaintListInfo.Add(info);
            });

            return model;
        }

        /// <summary>
        /// 指派客服人员
        /// </summary>
        /// <param name="complaintInfo"></param>
        /// <returns></returns>
        public bool AssginServiceUser(ComplaintInfo complaintInfo)
        {
            var instance = CreateBizObject<Complaint>();
            var entity = instance.GetBySysNo(complaintInfo.ComplaintId);

            if (entity == null)
                return false;

            entity.ServiceUserID = complaintInfo.ServiceUserId;
            entity.ServiceUserName = (from em in Context.Employee
                                      where em.EmployeeID == complaintInfo.ServiceUserId
                                      select em.EmployeeName).FirstOrDefault();
            entity.AcceptTime = DateTime.Now;
            entity.ServiceStartTime = DateTime.Now;
            entity.ComplaintStatus = 2;//把进度改为处理中
            entity.EditDate = DateTime.Now;
            entity.EditUser = complaintInfo.UserInfo.UserId;
            entity.AcceptUserID = complaintInfo.UserInfo.UserId;
            entity.AcceptUserName = complaintInfo.UserInfo.UserName;

            instance.Update(entity);
            this.SaveChanges();

            complaintInfo.ServiceUserName = entity.ServiceUserName;

            new ComplaintLog().Assgin(complaintInfo);
            new ComplaintMessage().Assgin(complaintInfo);

            return true;
        }

        /// <summary>
        /// 重新指派客服人员
        /// </summary>
        /// <param name="complaintInfo"></param>
        /// <returns></returns>
        public bool ReAssginServiceUser(ComplaintInfo complaintInfo)
        {
            var instance = CreateBizObject<Complaint>();
            var entity = instance.GetBySysNo(complaintInfo.ComplaintId);

            if (entity == null)
                return false;
            entity.ServiceUserID = complaintInfo.ServiceUserId;
            entity.ServiceUserName = (from em in Context.Employee
                                      where em.EmployeeID == complaintInfo.ServiceUserId
                                      select em.EmployeeName).FirstOrDefault();
            entity.ComplaintStatus = 2;//把进度改为处理中
            entity.EditDate = DateTime.Now;
            entity.EditUser = complaintInfo.UserInfo.UserId;
            entity.AcceptUserID = complaintInfo.UserInfo.UserId;
            entity.AcceptUserName = complaintInfo.UserInfo.UserName;

            instance.Update(entity);
            this.SaveChanges();

            complaintInfo.ServiceUserName = entity.ServiceUserName;

            new ComplaintLog().ReAssgin(complaintInfo);
            new ComplaintMessage().ReAssgin(complaintInfo);
            return true;
        }

        /// <summary>
        /// 获取客服人员详细信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ComplaintDetailModel GetServiceUserById(int userId)
        {
            var model = new ComplaintDetailModel();
            var query = from e in Context.Employee
                        where e.EmployeeID == userId
                        select new
                        {
                            e.EmployeeID,
                            e.EmployeeName,
                            e.Moblie,
                            e.IDNumber,
                            e.Tag,
                        };
            var entity = query.FirstOrDefault();
            if (null != entity)
            {
                model.ServiceUserId = entity.EmployeeID;
                model.ServiceUserName = entity.EmployeeName;
                model.ServiceUserMobile = entity.Moblie;
                model.ServiceUserIDNumber = entity.IDNumber;
                model.Tag = entity.Tag;
            }
            return model;
        }

        /// <summary>
        /// 获取投诉详情
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        public ComplaintDetailModel GetComplaintDetail(int complaintId)
        {
            var model = new ComplaintDetailModel();
            model.AttachmentInfoList = new List<AttachmentInfo>();
            model.ComplaintLogInfoList = new List<ComplaintLogInfo>();
            model.ComplaintCommentDetail = new ComplaintCommentDetailModel();

            var query = from cpt in Context.Complaint
                        join dcType in Context.Dictionary on cpt.ComplaintType equals dcType.FieldValue
                        join dcStatus in Context.Dictionary on cpt.ComplaintStatus equals dcStatus.FieldValue
                        where dcType.FieldType == "ComplaintType" && dcStatus.FieldType == "ComplaintStatus" && cpt.ComplaintID == complaintId
                        select new
                        {
                            cpt.ComplaintID,
                            cpt.ProjectID,
                            cpt.RequestTime,
                            cpt.RequestDesc,
                            cpt.RequestUserID,
                            cpt.RequestUserName,
                            cpt.RequestSrcType,
                            cpt.RequestTarget,
                            cpt.ServiceUserID,
                            cpt.ServiceFinishTime,
                            cpt.ComplaintStatus,
                            cpt.InvestigationDesc,
                            cpt.ServiceDesc,
                            cpt.AcceptTime,
                            cpt.ServiceStartTime,
                            ComplaintTypeName = dcType.FieldName,
                            ComplaintStatusName = dcStatus.FieldName,
                            cpt.Complaint_Attachment,
                            cpt.Complaint_Log,
                            cpt.Complaint_Comment,
                            cpt.Project
                        };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.ComplaintId = entity.ComplaintID;
                model.ProjectId = entity.ProjectID;
                model.ProjectName = entity.Project.ProjectName;
                model.ComplaintTypeName = entity.ComplaintTypeName;
                model.RequestTime = entity.RequestTime;
                model.RequestDesc = entity.RequestDesc;
                model.RequestUserId = entity.RequestUserID.HasValue ? entity.RequestUserID.Value : 0;
                model.RequestUserName = entity.RequestUserName;
                model.RequestTarget = entity.RequestTarget;
                model.RequestSrcType = entity.RequestSrcType;
                model.ServiceUserId = entity.ServiceUserID;
                model.ServiceFinishTime = entity.ServiceFinishTime;
                model.ComplaintStatus = entity.ComplaintStatus;
                model.ComplaintStatusName = entity.ComplaintStatusName;
                model.InvestigationDesc = entity.InvestigationDesc;
                model.ServiceDesc = entity.ServiceDesc;
                model.AcceptTime = entity.AcceptTime;
                model.StartTime = entity.ServiceStartTime;
            }

            ///查询出投诉证据相应附件信息
            if (entity.Complaint_Attachment != null)
            {
                var attachList = entity.Complaint_Attachment.ToList();
                attachList.ForEach(item =>
                {
                    var attachmentInfo = new AttachmentInfo()
                    {
                        AttachmentPath = item.AttachmentPath
                    };
                    model.AttachmentInfoList.Add(attachmentInfo);
                });
            }

            ///投诉表中的客服人员名字从员工表中更新
            if (model.ServiceUserId.HasValue)
            {
                var employee = (from e in Context.Employee
                                where e.EmployeeID == model.ServiceUserId.Value
                                select new
                                {
                                    e.EmployeeName,
                                    e.Tag

                                }).FirstOrDefault();
                model.ServiceUserName = employee.EmployeeName;
                model.Tag = employee.Tag;

            }

            #region 投诉记录
            if (entity.Complaint_Log != null)
            {
                foreach (var item in entity.Complaint_Log)
                {
                    var complaintLogInfo = new ComplaintLogInfo()
                    {
                        LogId = item.LogID,
                        ComplaintId = item.ComplaintID,
                        LogTime = item.LogTime,
                        LogUserId = item.LogUserID,
                        LogUserName = item.LogUserName,
                        LogText = item.LogText
                    };
                    model.ComplaintLogInfoList.Add(complaintLogInfo);
                }
            }
            #endregion

            #region 服务评价
            if (entity.Complaint_Comment != null)
            {
                var info = entity.Complaint_Comment.FirstOrDefault();
                if (info != null)
                {
                    var comment = new ComplaintCommentDetailModel();
                    comment.Speed = info.Speed;
                    comment.Quality = info.Quality;
                    comment.Attitude = info.Attitude;
                    comment.AllComment = info.Comment + " " + info.Additional;

                    model.ComplaintCommentDetail = comment;
                }
            }
            #endregion

            return model;
        }

        /// <summary>
        /// 更新投诉信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateComplaint(ComplaintDetailModel model)
        {
            var instance = CreateBizObject<Complaint>();
            var entity = instance.GetBySysNo(model.ComplaintId);
            if (entity == null)
            {
                return false;
            }
            entity.InvestigationDesc = model.InvestigationDesc;
            entity.ServiceDesc = model.ServiceDesc;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserInfo.UserId;


            if (model.ComplaintStatus == 3)
            {
                entity.ServiceFinishTime = DateTime.Now;
                new ComplaintLog().Finish(model);
            }
            instance.Update(entity);
            this.SaveChanges();

            new ComplaintLog().Update(model);
            return true;
        }

        /// <summary>
        /// 增加投诉单 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddComplaint(ComplaintDetailModel model)
        {
            var instance = CreateBizObject<Complaint>();
            Complaint entity = new Complaint()
            {
                ComplaintType = model.ComplaintType,
                RequestDesc = model.RequestDesc,
                RequestTarget = model.RequestTarget,
                ProjectID = model.ProjectId,
                ComplaintStatus = 1,
                RequestUserID = model.UserInfo.UserId,
                RequestUserName = model.UserInfo.UserName,
                RequestSrcType = 2,//智能端
                RequestTime = DateTime.Now,
                Status = 1,
                InDate = DateTime.Now,
                InUser = model.UserInfo.UserId,
                EditDate = DateTime.Now,
                EditUser = model.UserInfo.UserId
            };
            instance.Insert(entity);
            this.SaveChanges();

            model.ComplaintId = entity.ComplaintID;

            #region 投诉证据附件
            if (!string.IsNullOrEmpty(model.AttchmentImgPathList))
            {
                var attchmentImgPathList = model.AttchmentImgPathList.Split(',').ToList();

                foreach (var item in attchmentImgPathList)
                {
                    if (item != "")
                    {
                        var imgNameList = item.Split('/').ToList();
                        var imgName = imgNameList[imgNameList.Count - 1];
                        var attachmentInfo = new AttachmentInfo()
                        {
                            ComplaintId = entity.ComplaintID,
                            AttachmentName = imgName,
                            AttachmentPath = item
                        };
                        AddAttachment(attachmentInfo);
                    }

                }
            }
            #endregion
            //日志
            new ComplaintLog().Add(model);
            //发送提报消息到智能平台
            int employeeId = 0;
            if (model.ProjectConfig != null && int.TryParse(model.ProjectConfig.Param3, out employeeId))
            {
                if (employeeId > 0)
                {
                    model.ServiceUserId = (from u in Context.Sys_User
                                           where u.EmployeeID == employeeId
                                           select u.UserID).FirstOrDefault();
                    new ComplaintUserMessage().Add(model);
                }
            }

            return entity.ComplaintID;
        }

        /// <summary>
        /// 上传投诉证据图
        /// </summary>
        /// <param name="complaintId"></param>
        /// <param name="attachmentInfo"></param>
        public void AddAttachment(AttachmentInfo attachmentInfo)
        {
            var instance = CreateBizObject<Complaint_Attachment>();

            var complaintAttachment = new Complaint_Attachment()
            {
                ComplaintID = attachmentInfo.ComplaintId,
                AttachmentName = attachmentInfo.AttachmentName,
                AttachmentPath = attachmentInfo.AttachmentPath,
                Status = 1,
                InDate = DateTime.Now,
                InUser = 1,
                EditDate = DateTime.Now,
                EditUser = 1
            };
            instance.Insert(complaintAttachment);
            this.SaveChanges();
        }

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="complaintId"></param>
        public void Finish(ComplaintInfo model)
        {
            var bll = CreateBizObject<Complaint>();
            var entity = bll.TopOne(item => item.ComplaintID == model.ComplaintId);

            if (entity == null)
                return;

            entity.ServiceFinishTime = DateTime.Now;
            entity.ComplaintStatus = 3;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserInfo.UserId;

            bll.Update(entity);
            this.SaveChanges();

            new ComplaintLog().Finish(model);
            new ComplaintMessage().Finish(model);
        }

        /// <summary>
        /// 投诉搁置
        /// </summary>
        /// <param name="shelveInfo"></param>
        /// <returns></returns>
        public bool ComplaintShelve(ComplaintShelveInfo shelveInfo)
        {
            var instance = CreateBizObject<Complaint>();
            var entity = instance.GetBySysNo(shelveInfo.ComplaintId);
            if (entity == null)
                return false;

            entity.ServiceFinishTime = DateTime.Now;
            entity.ComplaintStatus = 4;
            entity.EditDate = DateTime.Now;
            entity.EditUser = shelveInfo.UserInfo.UserId;
            instance.Update(entity);
            this.SaveChanges();

            new ComplaintLog().Shelve(shelveInfo);
            new ComplaintMessage().Shelve(shelveInfo);
            return true;
        }

        /// <summary>
        /// 获取投诉指派员工信息
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ComplaintAssignUserListModel GetComplaintAssignUserList(ComplaintAssignUserQueryInfo queryInfo)
        {
            var model = new ComplaintAssignUserListModel();
            model.QueryInfo = queryInfo;
            model.ComplaintAssignUserList = new List<ComplaintAssignUserInfo>();

            var query = from em in Context.Employee
                        join dept in Context.Department on em.DepartmentID equals dept.DepartmentID
                        select new
                        {
                            em.EmployeeID,
                            em.EmployeeName,
                            em.Tag,
                            dept.DepartmentID,
                            dept.DepartmentName
                        };

            #region 查询条件
            if (!queryInfo.DeptId.HasValue)
            {
                //第一次开打弹出框时默认选择当前用户的部门
                // 获取当前用户所在部门
                queryInfo.DeptId = (from su in Context.Sys_User
                                    join em in Context.Employee on su.EmployeeID equals em.EmployeeID
                                    where su.UserID == queryInfo.UserInfo.UserId
                                    select em.DepartmentID).FirstOrDefault();
            }
            if (queryInfo.DeptId.HasValue && queryInfo.DeptId > 0)
            {
                query = query.Where(m => m.DepartmentID == queryInfo.DeptId);
            }
            if (!string.IsNullOrEmpty(queryInfo.Tag))
            {
                query = query.Where(m => m.Tag.Contains(queryInfo.Tag));
            }
            #endregion

            var entityList = query.ToList();
            if (entityList != null)
            {
                foreach (var item in entityList)
                {
                    var userInfo = new ComplaintAssignUserInfo();
                    userInfo.DeptName = item.DepartmentName;
                    userInfo.EmployeeId = item.EmployeeID;
                    userInfo.EmployeeName = item.EmployeeName;
                    userInfo.Tag = item.Tag;
                    model.ComplaintAssignUserList.Add(userInfo);
                }
            }
            return model;
        }

        /// <summary>
        /// 获取项目下**部(例如营运部)所有人
        /// </summary>
        /// <param name="projectConfig"></param>
        /// <returns></returns>
        public List<EmployeeCommonInfo> GetComplaintServiceUserList(ProjectBizConfigInfo projectConfig)
        {
            var empList = new List<EmployeeCommonInfo>();

            int departmentId = 0;

            int.TryParse(projectConfig.Param2, out departmentId);

            var query = from dept in Context.Department
                        join em in Context.Employee on dept.DepartmentID equals em.DepartmentID
                        where em.Status == 1 && dept.DepartmentID == departmentId
                        select new
                        {
                            em.EmployeeID,
                            em.EmployeeName,
                        };

            var entityList = query.ToList();
            if (entityList != null)
            {
                foreach (var entity in entityList)
                {
                    var info = new EmployeeCommonInfo();
                    info.Id = entity.EmployeeID;
                    info.Name = string.Format("{0}", entity.EmployeeName);
                    empList.Add(info);
                }
            }
            return empList;
        }
    }
}
