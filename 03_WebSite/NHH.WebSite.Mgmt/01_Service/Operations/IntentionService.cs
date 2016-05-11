using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Operations;
using NHH.Service.Operations.IService;

namespace NHH.Service.Operations
{
    public class IntentionService : NHHService<NHHEntities>, IIntentionService
    {
        /// <summary>
        /// 获取入驻意向表单列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public IntentionList GetIntentionList(IntentionQuery queryInfo)
        {
            var model = new IntentionList();
            model.IntentionInfoList = new List<IntentionInfo>();
            model.QueryInfo = queryInfo;
            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1;

            var query = from i in Context.Intention
                join type in Context.Dictionary on i.IntentionType equals type.FieldValue
                join status in Context.Dictionary on i.Status equals status.FieldValue
                join u in Context.Sys_User on i.ProcessUserID equals u.UserID into t_u
                from u in t_u.DefaultIfEmpty()
                join e in Context.Employee on u.EmployeeID equals e.EmployeeID into t_e
                from e in t_e.DefaultIfEmpty()
                where type.FieldType == "IntentionType" && status.FieldType == "IntentionStatus"
                select new
                {
                    i.IntentionID,
                    i.IntentionType,
                    IntentionTypeString = type.FieldName,
                    i.ContactName,
                    i.ContactPhone,
                    i.InDate,
                    i.ProjectName,
                    i.Remark,
                    i.Source,
                    i.Status,
                    StatusString = status.FieldName,
                    i.ProcessUserID,
                    ProcessUserName = e.EmployeeName,
                    i.ProcessTime
                };
            if (!string.IsNullOrEmpty(queryInfo.ProjectName))
            {
                query = query.Where(item => item.ProjectName == queryInfo.ProjectName);
            }
            if (queryInfo.IntentionType.HasValue)
            {
                query = query.Where(item => item.IntentionType == queryInfo.IntentionType);
            }
            if (queryInfo.Status.HasValue)
            {
                query = query.Where(item => item.Status == queryInfo.Status);
            }
            if (queryInfo.StartDate.HasValue)
            {
                query = query.Where(item => item.InDate >= queryInfo.StartDate);
            }
            if (queryInfo.EndDate.HasValue)
            {
                query = query.Where(item => item.InDate <= queryInfo.EndDate);
            }

            model.PagingInfo.TotalCount = query.Count();
            var entityList =
                query.OrderByDescending(item => item.IntentionID)
                    .Skip(model.PagingInfo.SkipNum)
                    .Take(model.PagingInfo.TakeNum)
                    .ToList();
            if (entityList != null)
            {
                foreach (var entity in entityList)
                {
                    var info = new IntentionInfo();
                    info.ContactName = entity.ContactName;
                    info.ContactPhone = entity.ContactPhone;
                    info.InDate = entity.InDate;
                    info.IntentionID = entity.IntentionID;
                    info.IntentionType = entity.IntentionType;
                    info.IntentionTypeName = entity.IntentionTypeString;
                    info.ProcessTime = entity.ProcessTime;
                    info.ProcessUserID = entity.ProcessUserID;
                    info.ProcessUserName = entity.ProcessUserName;
                    info.ProjectName = entity.ProjectName;
                    info.Remark = entity.Remark;
                    info.Source = entity.Source;
                    info.Status = entity.Status;
                    info.StatusString = entity.StatusString;

                    model.IntentionInfoList.Add(info);
                }
            }
            return model;
        }

        /// <summary>
        /// 获取入驻意向表单
        /// </summary>
        /// <param name="intentionId"></param>
        /// <returns></returns>
        public IntentionInfo GetIntention(int intentionId)
        {
            var model = new IntentionInfo();
            var query = from i in Context.Intention
                join type in Context.Dictionary on i.IntentionType equals type.FieldValue
                join status in Context.Dictionary on i.Status equals status.FieldValue
                join u in Context.Sys_User on i.ProcessUserID equals u.UserID into t_u
                from u in t_u.DefaultIfEmpty()
                join e in Context.Employee on u.EmployeeID equals e.EmployeeID into t_e
                from e in t_e.DefaultIfEmpty()
                where
                    type.FieldType == "IntentionType" && status.FieldType == "IntentionStatus" &&
                    i.IntentionID == intentionId
                select new
                {
                    i.IntentionID,
                    i.IntentionType,
                    IntentionTypeString = type.FieldName,
                    i.ContactName,
                    i.ContactPhone,
                    i.InDate,
                    i.ProjectName,
                    i.Remark,
                    i.Source,
                    i.Status,
                    StatusString = status.FieldName,
                    i.ProcessUserID,
                    ProcessUserName = e.EmployeeName,
                    i.ProcessTime
                };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.ContactName = entity.ContactName;
                model.ContactPhone = entity.ContactPhone;
                model.InDate = entity.InDate;
                model.IntentionID = entity.IntentionID;
                model.IntentionType = entity.IntentionType;
                model.IntentionTypeName = entity.IntentionTypeString;
                model.ProcessTime = entity.ProcessTime;
                model.ProcessUserID = entity.ProcessUserID;
                model.ProcessUserName = entity.ProcessUserName;
                model.ProjectName = entity.ProjectName;
                model.Remark = entity.Remark;
                model.Source = entity.Source;
                model.Status = entity.Status;
                model.StatusString = entity.StatusString;
            }
            return model;
        }
        /// <summary>
        /// 更新接单人与接单状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ProcessIntention(IntentionInfo model)
        {
            var instance = CreateBizObject<Intention>();
            var entity = instance.GetBySysNo(model.IntentionID);
            entity.ProcessUserID = model.ProcessUserID;
            entity.ProcessTime = DateTime.Now;
            entity.Status = 3;
            instance.Update(entity);
            this.SaveChanges();
            return true;
        }
    }
}
