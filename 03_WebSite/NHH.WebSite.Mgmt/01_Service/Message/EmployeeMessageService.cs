using Newtonsoft.Json;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Common.Employee;
using NHH.Models.Message;
using NHH.Service.Message.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Message
{
    /// <summary>
    /// 员工消息服务
    /// </summary>
    public class EmployeeMessageService : NHHService<NHHEntities>, IEmployeeMessageService
    {
        /// <summary>
        /// 插入发送给员工的通知
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SaveEmployeeMessage(EmployeeMessageInfo message)
        {
            List<string> userList = message.UserIdList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var instance = CreateBizObject<Sys_User_Message>();

            for (int i = 0; i < userList.Count; i++)
            {
                var entity = new Sys_User_Message();
                entity.SourceType = 1;
                entity.Subject = message.Subject;
                entity.Content = message.Content;
                entity.UserID = Convert.ToInt32(userList[i]);
                entity.InUser = message.UserInfo.UserId;
                entity.InDate = DateTime.Now;
                entity.EditUser = message.UserInfo.UserId;
                entity.EditDate = DateTime.Now;
                entity.Status = 1;
                instance.Insert(entity);
                this.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// 返回员工信息列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public EmployeeMessageListModel GetMessageList(EmployeeMessageListQueryInfo queryInfo)
        {
            var model = new EmployeeMessageListModel();
            model.QueryInfo = queryInfo;
            model.UserMessageList = new List<UserMessage>();
            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1,
            };

            var query = from sum in Context.Sys_User_Message
                        join su in Context.Sys_User on sum.UserID equals su.UserID
                        join ce in Context.Employee on su.EmployeeID equals ce.EmployeeID
                        join cp in Context.Department on ce.DepartmentID equals cp.DepartmentID
                        join cd in Context.Dictionary on sum.SourceType equals cd.FieldValue
                        where sum.Status == 1 && cd.FieldType == "SourceType" && sum.SourceType == 1
                        select new
                        {
                            sum.Content,
                            sum.Subject,
                            su.EmployeeID,
                            ce.EmployeeName,
                            ce.Title,
                            ce.DepartmentID,
                            cp.DepartmentName,
                            ce.CompanyID,
                            sum.SourceType,
                            sum.InDate,
                            sum.MessageID,
                            cd.FieldName
                        };
            if (queryInfo.CompanyId.HasValue)
            {
                query = query.Where(item => item.CompanyID == queryInfo.CompanyId.Value);
            }
            if (queryInfo.EmployeeName != null)
            {
                query = query.Where(item => item.EmployeeName == queryInfo.EmployeeName);
            }
            if (queryInfo.StartTime.HasValue)
            {
                query = query.Where(item => item.InDate >= queryInfo.StartTime.Value);
            }
            if (queryInfo.EndTime.HasValue)
            {
                query = query.Where(item => item.InDate <= queryInfo.EndTime.Value);
            }
            
            model.PagingInfo.TotalCount = query.Count();
            //降序写在query中了
            var entityList = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();

            if (entityList != null)
            {
                foreach (var entity in entityList)
                {
                    var message = new UserMessage();
                    message.Content = entity.Content;
                    message.EmployeeName = entity.EmployeeName;
                    message.InDate = entity.InDate;
                    message.Subject = entity.Subject;
                    message.SourceType = entity.SourceType;
                    message.Title = entity.Title;
                    message.SourceTypeName = entity.FieldName;
                    message.DepartmentName = entity.DepartmentName;
                    message.MessageID = entity.MessageID;
                    model.UserMessageList.Add(message);
                }
            }
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public EmployeeMessageDetailModel GetDetail(int messageId)
        {
            var model = new EmployeeMessageDetailModel();
            var query = from sum in Context.Sys_User_Message
                        join su in Context.Sys_User on sum.UserID equals su.UserID
                        join ce in Context.Employee on su.EmployeeID equals ce.EmployeeID
                        join cd in Context.Department on ce.DepartmentID equals cd.DepartmentID
                        where su.Status == 1 && sum.MessageID == messageId
                        select new
                        {
                            sum.Subject,
                            sum.InDate,
                            sum.Content,
                            cd.DepartmentName,
                            ce.EmployeeName
                        };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.EmployeeName = entity.EmployeeName;
                model.Subject = entity.Subject;
                model.DepartmentName = entity.DepartmentName;
                model.Content = entity.Content;
                model.InDate = entity.InDate;
            }
            return model;
        }

    }
}
