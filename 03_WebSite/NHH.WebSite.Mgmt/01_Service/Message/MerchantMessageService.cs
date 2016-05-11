using Newtonsoft.Json;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Common.Employee;
using NHH.Models.Estate;
using NHH.Models.Merchant;
using NHH.Models.Message;
using NHH.Models.Project;
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
    /// 商户消息服务
    /// </summary>
    public class MerchantMessageService : NHHService<NHHEntities>, IMerchantMessageService
    {

        /// <summary>
        /// 根据查询信息返回所有消息
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public MerchantMessageListModel GetList(MerchantMessageListQueryInfo queryInfo)
        {
            var model = new MerchantMessageListModel();
            model.QueryInfo = queryInfo;
            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1;
            model.MessageList = new List<MerchantMessageInfo>();

            #region 查询条件
            var query = from mm in Context.Merchant_Message
                        join mu in Context.Merchant_User on mm.UserID equals mu.UserID
                        join d in Context.Dictionary on mm.MessageLevel equals d.FieldValue
                        where mm.Status == 1 && d.FieldType == "MessageLevel"
                        select new
                        {
                            mm.MessageID,
                            mm.Subject,
                            mm.SourceType,
                            mm.InDate,
                            mm.MerchantID,
                            mm.Merchant.MerchantName,
                            mu.UserID,
                            mu.UserName,
                            mu.NickName,
                            mu.StoreID,
                            mm.MessageLevel,
                            MessageLevelText = d.FieldName,
                            mu.Merchant_Store.StoreName,
                            BuildingInfo = (from ms in Context.Merchant_Store
                                            join cu in Context.Contract_Unit on ms.RentContractID equals cu.ContractID
                                            join pu in Context.Project_Unit on cu.UnitID equals pu.UnitID
                                            where ms.Status == 1 && ms.MerchantID == mm.MerchantID
                                            select new
                                            {
                                                pu.ProjectID,
                                                pu.BuildingID
                                            }).FirstOrDefault()
                        };

            if (queryInfo.ProjectId.HasValue)
            {
                query = query.Where(item => item.BuildingInfo != null && item.BuildingInfo.ProjectID == queryInfo.ProjectId.Value);
            }

            if (queryInfo.BuildingId.HasValue)
            {
                query = query.Where(item => item.BuildingInfo != null && item.BuildingInfo.BuildingID == queryInfo.BuildingId.Value);
            }

            if (queryInfo.UserId.HasValue)
            {
                query = query.Where(item => item.UserID == queryInfo.UserId.Value);
            }

            if (queryInfo.MerchantId.HasValue)
            {
                query = query.Where(item => item.MerchantID == queryInfo.MerchantId.Value);
            }

            if (!string.IsNullOrEmpty(queryInfo.MerchantName) && queryInfo.MerchantName.Length > 0)
            {
                query = query.Where(item => item.MerchantName.IndexOf(queryInfo.MerchantName) >= 0);
            }

            if (queryInfo.StoreId.HasValue)
            {
                query = query.Where(item => item.StoreID == queryInfo.StoreId.Value);
            }

            if (queryInfo.StartTime.HasValue)
            {
                query = query.Where(item => (item.InDate >= queryInfo.StartTime.Value));
            }

            if (queryInfo.EndTime.HasValue)
            {
                query = query.Where(item => (item.InDate <= queryInfo.EndTime.Value));
            }
            #endregion

            model.PagingInfo.TotalCount = query.Count();
            var entityList = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();

            foreach (var entity in entityList)
            {
                var message = new MerchantMessageInfo();
                message.MessageID = entity.MessageID;
                message.MerchantID = entity.MerchantID;
                message.MerchantName = entity.MerchantName;
                message.MerchantUserName = string.IsNullOrEmpty(entity.NickName) ? entity.UserName : entity.NickName;
                message.Subject = entity.Subject;
                message.SourceType = entity.SourceType;
                message.MessageLevel = entity.MessageLevel;
                message.MessageLevelText = entity.MessageLevelText;
                message.InDate = entity.InDate;
                message.UserID = entity.UserID;
                message.NickName = string.IsNullOrEmpty(entity.NickName) ? entity.UserName : entity.NickName;
                message.StroeName = entity.StoreName;
                message.BuildingId = entity.BuildingInfo == null ? 0 : entity.BuildingInfo.BuildingID;
                model.MessageList.Add(message);
            }
            return model;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public MerchantMessageDetailModel GetDetail(int messageId)
        {
            var model = new MerchantMessageDetailModel();
            var query = from mm in Context.Merchant_Message
                        where mm.MessageID == messageId && mm.Status == 1
                        select mm;

            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.MessageID = entity.MessageID;
                model.Subject = entity.Subject;
                model.Content = entity.Content;
                model.UserID = entity.UserID;
                model.NickName = string.IsNullOrEmpty(entity.Merchant_User.NickName) ? entity.Merchant_User.UserName : entity.Merchant_User.NickName;
                model.InDate = entity.InDate;
                model.InUser = entity.InUser;
                model.MerchantID = entity.MerchantID;
                model.MerchantName = entity.Merchant.MerchantName;
                model.MessageLevel = entity.MessageLevel;
                model.MessageLevelText = (from d in Context.Dictionary
                                          where d.FieldType == "MessageLevel" && d.FieldValue == entity.MessageLevel
                                          select d.FieldName).FirstOrDefault();
            }

            return model;
        }

        /// <summary>
        /// 添加商户消息
        /// </summary>
        /// <param name="info"></param>
        public void Add(MerchantMessageInfo info)
        {
            var userIdArr = info.UserIdList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (userIdArr == null || userIdArr.Length == 0)
                return;
            var query = from mu in Context.Merchant_User
                        where mu.Status == 1
                        select new
                        {
                            mu.UserID,
                            mu.StoreID,
                            mu.MerchantID,
                        };

            var userIdList = userIdArr.ToList();
            query = query.Where(item => (from id in userIdList
                                         where id == item.UserID.ToString()
                                         select id).Any());

            var userList = query.ToList();


            var bll = CreateBizObject<Merchant_Message>();
            userList.ForEach(user =>
            {
                var message = new Merchant_Message
                {
                    MerchantID = user.MerchantID,
                    StoreID = user.StoreID,
                    UserID = user.UserID,
                    SourceRefID = 0,
                    SourceType = 1,
                    MessageLevel= info.MessageLevel,
                    Subject = info.Subject,
                    Content = info.Content,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = info.InUser,
                    EditDate = DateTime.Now,
                    EditUser = info.InUser,
                };
                bll.Insert(message);
                this.SaveChanges();
            });
        }
        
        /// <summary>
        /// 获取员工详情
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public EmployeeDetailModel GetEmployeeDetail(int employeeId)
        {
            var model = new EmployeeDetailModel();
            model.CompanyInfo = new CompanyCommonInfo();
            model.DeptInfo = new DepartmentCommonInfo();

            var query = from e in Context.Employee
                        join d in Context.Department on e.DepartmentID equals d.DepartmentID
                        join c in Context.Company on d.CompanyID equals c.CompanyID
                        where e.EmployeeID == employeeId && e.Status == 1
                        select new
                        {
                            e.EmployeeID,
                            e.EmployeeCode,
                            e.EmployeeName,
                            e.Gender,
                            e.Title,
                            e.Email,
                            e.Moblie,
                            e.IDNumber,
                            e.Phone,
                            e.Ext,
                            e.Birthday,
                            d.DepartmentID,
                            d.DepartmentName,
                            e.CompanyID,
                            c.BriefName,
                        };

            var entity = query.FirstOrDefault();

            if (entity == null)
            {
                return model;
            }
            model.EmployeeId = entity.EmployeeID;
            model.EmployeeCode = entity.EmployeeCode;
            model.EmployeeName = entity.EmployeeName;
            model.IDNumber = entity.IDNumber;
            model.Title = entity.Title;
            model.Email = entity.Email;
            model.Moblie = entity.Moblie;
            model.Phone = entity.Phone;
            model.Ext = entity.Ext;
            model.DepartmentName = entity.DepartmentName;
            model.Birthday = entity.Birthday;
            model.DepartmentId = entity.DepartmentID;
            model.DeptInfo.DepartmentName = entity.DepartmentName;
            model.CompanyId = entity.CompanyID;
            model.CompanyInfo.Name = entity.BriefName;
            model.GenderId = entity.Gender;

            return model;
        }

    }
}
