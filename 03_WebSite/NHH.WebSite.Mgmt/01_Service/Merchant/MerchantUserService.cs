using NHH.Framework.Service;
using NHH.Models.Merchant;
using NHH.Service.Merchant.IService;
using NHH.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;
using NHH.Framework.Security.Cryptography;

namespace NHH.Service.Merchant
{
    /// <summary>
    /// 商家用户服务
    /// </summary>
    public class MerchantUserService : NHHService<NHHEntities>, IMerchantUserService
    {
        /// <summary>
        /// 获取商户用户列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public MerchantUserListModel GetUserList(MerchantUserListQueryInfo queryInfo)
        {
            var model = new MerchantUserListModel();
            model.QueryInfo = queryInfo;
            model.MerchantUserList = new List<MerchantUserInfo>();
            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1,
            };

            var query = from mu in Context.Merchant_User
                        join m in Context.Merchant on mu.MerchantID equals m.MerchantID
                        join d in Context.Dictionary on mu.Status equals d.FieldValue
                        join ms in Context.Merchant_Store on mu.StoreID equals ms.StoreID into gms
                        from gmsi in gms.DefaultIfEmpty()
                        where d.FieldType == "MerchantUserStatus" && mu.MerchantID == queryInfo.MerchantId
                        select new
                        {
                            mu.UserID,
                            mu.MerchantID,
                            m.MerchantName,
                            mu.RoleID,
                            mu.UserName,
                            mu.NickName,
                            mu.Email,
                            mu.Moblie,
                            mu.WechatOpenID,
                            mu.Status,
                            StatusName = d.FieldName,
                            mu.StoreID,
                            gmsi.StoreName
                        };
            if (queryInfo.Status.HasValue)
            {
                query = query.Where(item => item.Status == queryInfo.Status.Value);
            }
            model.PagingInfo.TotalCount = query.Count();
            var entityList = query.OrderByDescending(m => m.UserID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();

            #region 商户信息
            model.QueryInfo.MerchantName = (from m in Context.Merchant
                                            where m.MerchantID == queryInfo.MerchantId
                                            select m.MerchantName).FirstOrDefault();

            #endregion

            entityList.ForEach(entity =>
            {
                var userInfo = new MerchantUserInfo();
                userInfo.UserId = entity.UserID;
                userInfo.MerchantId = entity.MerchantID;
                userInfo.MerchantInfo = new Models.Common.MerchantCommonInfo();
                userInfo.MerchantInfo.MerchantName = entity.MerchantName;
                userInfo.StoreId = entity.StoreID;
                userInfo.StoreName = entity.StoreName;
                userInfo.RoleInfo = new Models.Common.RoleCommonInfo();
                userInfo.RoleInfo.Name = entity.RoleID == 1 ? "管理员" : "操作员";
                userInfo.UserName = entity.UserName;
                userInfo.NickName = entity.NickName;
                userInfo.Email = entity.Email;
                userInfo.Mobile = entity.Moblie;
                userInfo.WechatOpenId = entity.WechatOpenID;
                userInfo.Status = entity.Status;
                userInfo.StatusName = entity.StatusName;
                model.MerchantUserList.Add(userInfo);
            });
            return model;
        }

        /// <summary>
        /// 获取商户详细情况
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MerchantUserDetailModel GetUserDetail(int userId)
        {
            var model = new MerchantUserDetailModel();
            var query = from mu in Context.Merchant_User
                        join m in Context.Merchant on mu.MerchantID equals m.MerchantID
                        where mu.UserID == userId
                        select new
                        {
                            mu.UserID,
                            mu.MerchantID,
                            m.MerchantName,
                            mu.StoreID,
                            mu.RoleID,
                            mu.UserName,
                            mu.Password,
                            mu.NickName,
                            mu.Email,
                            mu.Moblie,
                            mu.IDNumber,
                            mu.PhotoFile,
                            mu.Gender,
                            mu.Birthday,
                            mu.Nation,
                            mu.Education,
                            mu.MaritalStatus,
                            mu.PoliticalStatus,
                            mu.Height,
                            mu.Weight,
                            mu.Address,
                            mu.ZipCode,
                            mu.EmergencyContact,
                            mu.EmergencyPhone,
                            mu.EducationExperience,
                            mu.WorkExperience,
                            mu.FamilyMembers,
                            mu.Status,
                            mu.WechatOpenID,
                            mu.InDate,
                            mu.InUser,
                        };

            var entity = query.FirstOrDefault(); ;

            if (entity != null)
            {
                model.UserId = entity.UserID;
                model.MerchantId = entity.MerchantID;
                model.MerchantInfo = new Models.Common.MerchantCommonInfo();
                model.MerchantInfo.MerchantName = entity.MerchantName;
                model.StoreId = entity.StoreID;
                model.RoleId = entity.RoleID;
                model.RoleInfo = new Models.Common.RoleCommonInfo();
                model.RoleInfo.Name = entity.RoleID == 1 ? "管理员" : "操作员";
                model.UserName = entity.UserName;
                model.PasswordMerchant = entity.Password;
                model.NickName = entity.NickName;
                model.Email = entity.Email;
                model.Mobile = entity.Moblie;
                model.WechatOpenId = entity.WechatOpenID;
                model.IDNumber = entity.IDNumber;
                model.PhotoFile = entity.PhotoFile;
                model.Gender = entity.Gender;
                if (entity.Birthday != null)
                {
                    model.Birthday = entity.Birthday.Value.Date;
                }
                model.Nation = entity.Nation;
                if (entity.Education != null)
                {
                    model.Education = entity.Education.Value;
                }
                if (entity.MaritalStatus != null)
                {
                    model.MaritalStatus = entity.MaritalStatus.Value;
                }
                model.PoliticalStatus = entity.PoliticalStatus;
                if (entity.Height != null)
                {
                    model.Height = entity.Height.Value;
                }
                if (entity.Weight != null)
                {
                    model.Weight = entity.Weight.Value;
                }
                model.Address = entity.Address;
                model.ZipCode = entity.ZipCode;
                model.EmergencyContact = entity.EmergencyContact;
                model.EmergencyPhone = entity.EmergencyPhone;
                model.EducationExperience = entity.EducationExperience;
                model.WorkExperience = entity.WorkExperience;
                model.FamilyMembers = entity.FamilyMembers;
                model.Status = entity.Status;
                model.InDate = entity.InDate;
                model.InUser = entity.InUser;
            }

            return model;
        }


        /// <summary>
        ///  新增商户用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public MessageBag<bool> AddUser(MerchantUserDetailModel model)
        {
            var result = new MessageBag<bool>();
            result.Code = 0;
            result.Desc = "新增成功";

            var query = from mu in Context.Merchant_User
                        where mu.MerchantID == model.MerchantId && mu.UserName == model.UserName
                        select mu.UserName;

            if (query.Any())
            {
                result.Code = -1;
                result.Key = "UserName";
                result.Desc = "用户名不可用";
                return result;
            }

            if (model.StoreId.HasValue && model.StoreId <= 0)
            {
                model.StoreId = null;
            }

            var instance = CreateBizObject<Merchant_User>();
            var entity = new Merchant_User()
            {
                MerchantID = model.MerchantId,
                StoreID = model.StoreId,
                UserName = model.UserName,
                Password = Cryptographer.SHA1.Encrypt(model.PasswordMerchant),
                RoleID = model.RoleId,
                Gender = model.Gender,
                NickName = model.NickName,
                Email = model.Email,
                Moblie = model.Mobile,
                PhotoFile = model.PhotoFile,
                IDNumber = model.IDNumber,
                FamilyMembers = model.FamilyMembers,
                Birthday = model.Birthday,
                Nation = model.Nation,
                Education = model.Education,
                MaritalStatus = model.MaritalStatus,
                PoliticalStatus = model.PoliticalStatus,
                Height = model.Height,
                Weight = model.Weight,
                Address = model.Address,
                ZipCode = model.ZipCode,
                EmergencyContact = model.EmergencyContact,
                EmergencyPhone = model.EmergencyPhone,
                EducationExperience = model.EducationExperience,
                WorkExperience = model.WorkExperience,
                WechatOpenID = model.WechatOpenId,
                Status = 1,
                InDate = DateTime.Now,
                InUser = model.InUser,
                EditDate = DateTime.Now,
                EditUser = model.EditUser
            };

            instance.Insert(entity);
            this.SaveChanges();
            model.UserId = entity.UserID;

            return result;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUser(MerchantUserDetailModel model)
        {
            var instance = CreateBizObject<Merchant_User>();

            if (model.StoreId.HasValue && model.StoreId <= 0)
            {
                model.StoreId = null;
            }

            var entity = instance.GetBySysNo(model.UserId);
            if (entity == null)
            {
                return false;
            }
            entity.StoreID = model.StoreId;
            entity.RoleID = model.RoleId;
            entity.NickName = model.NickName;
            entity.Email = model.Email;
            entity.Moblie = model.Mobile;
            entity.PhotoFile = model.PhotoFile;
            entity.Gender = model.Gender;
            DateTime bir = DateTime.Parse("0001/1/1 0:00:00");
            if (model.Birthday == bir)
            {
                entity.Birthday = null;
            }
            else
            {
                entity.Birthday = model.Birthday;
            }
            entity.Nation = model.Nation;
            entity.Education = model.Education;
            entity.MaritalStatus = model.MaritalStatus;
            entity.PoliticalStatus = model.PoliticalStatus;
            entity.Height = model.Height;
            entity.Weight = model.Weight;
            entity.Address = model.Address;
            entity.ZipCode = model.ZipCode;
            entity.IDNumber = model.IDNumber;
            entity.EmergencyContact = model.EmergencyContact;
            entity.EmergencyPhone = model.EmergencyPhone;
            entity.EducationExperience = model.EducationExperience;
            entity.WorkExperience = model.WorkExperience;
            entity.WechatOpenID = model.WechatOpenId;
            entity.Status = model.Status;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.EditUser;
            model.MerchantId = entity.MerchantID;
            instance.Update(entity);
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 切换在职状态
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="jstatus"></param>
        /// <param name="inUserid"></param>
        /// <returns></returns>
        public bool UpdateMerchantUserJobStatus(int userid, int jstatus, int inUserid)
        {
            var instance = CreateBizObject<Merchant_User>();
            var entity = instance.GetBySysNo(userid);
            return true;
        }

        /// <summary>
        /// 判断用户是否上传过身份证
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool GetUserIdentityFlag(int userid)
        {
            var instance = CreateBizObject<Merchant_UserPaper>();
            var entity = instance.TopOne(item => (item.UserID == userid && item.PaperType == 1 && item.Status == 1));
            if (entity != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUser(int merchantId, int userId)
        {
            var instance = CreateBizObject<Merchant_User>();
            var entity = instance.TopOne(item => item.MerchantID == merchantId && item.UserID == userId);
            if (entity == null)
            {
                return false;
            }
            entity.Status = -1;
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateMerchantUserStatus(MerchantUserDetailModel model, int inUserid)
        {
            var instance = CreateBizObject<Merchant_User>();

            var entity = instance.GetBySysNo(model.UserId);
            if (entity != null)
            {
                entity.Status = model.Status;
                entity.EditDate = DateTime.Now;
                entity.EditUser = inUserid;
            }
            instance.Update(entity);
            this.SaveChanges();

            return true;
        }

        /// <summary>
        /// 按商铺分组列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public List<MerchantUserGroupInfo> GetUserGroupList(MerchantUserGroupQueryInfo queryInfo)
        {
            var list = new List<MerchantUserGroupInfo>();

            var query = from ms in Context.Merchant_Store
                        join m in Context.Merchant on ms.MerchantID equals m.MerchantID
                        where ms.Status == 1
                        select new
                        {
                            ms.StoreID,
                            ms.StoreName,
                            ms.RentContractID,
                            ms.BizTypeID,
                        };
            #region 查询条件
            query = query.Where(item => (from pu in Context.Project_Unit
                                         join cu in Context.Contract_Unit on pu.UnitID equals cu.UnitID
                                         where cu.ContractID == item.RentContractID && pu.ProjectID == queryInfo.ProjectId
                                         select cu.UnitID).Any());

            if (queryInfo.BuildingId.HasValue)
            {
                query = query.Where(item => (from pu in Context.Project_Unit
                                             join cu in Context.Contract_Unit on pu.UnitID equals cu.UnitID
                                             where cu.ContractID == item.RentContractID && pu.BuildingID == queryInfo.BuildingId.Value
                                             select cu.UnitID).Any());
            }
            if (queryInfo.FloorId.HasValue)
            {
                query = query.Where(item => (from pu in Context.Project_Unit
                                             join cu in Context.Contract_Unit on pu.UnitID equals cu.UnitID
                                             where cu.ContractID == item.RentContractID && pu.FloorID == queryInfo.FloorId.Value
                                             select cu.UnitID).Any());
            }
            if (queryInfo.UnitType.HasValue)
            {
                query = query.Where(item => (from pu in Context.Project_Unit
                                             join cu in Context.Contract_Unit on pu.UnitID equals cu.UnitID
                                             where cu.ContractID == item.RentContractID && pu.UnitType == queryInfo.UnitType.Value
                                             select cu.UnitID).Any());
            }
            if (queryInfo.BizType.HasValue)
            {
                query = query.Where(item => item.BizTypeID == queryInfo.BizType.Value);
            }
            if (!string.IsNullOrEmpty(queryInfo.UnitNumber) && queryInfo.UnitNumber.Length > 0)
            {
                query = query.Where(item => (from pu in Context.Project_Unit
                                             join cu in Context.Contract_Unit on pu.UnitID equals cu.UnitID
                                             where cu.ContractID == item.RentContractID && pu.UnitNumber.Contains(queryInfo.UnitNumber)
                                             select cu.UnitID).Any());
            }
            #endregion

            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var groupInfo = new MerchantUserGroupInfo
                    {
                        StoreId = entity.StoreID,
                        StoreName = entity.StoreName,
                        UnitNumberList = "",
                        UserList = new List<MerchantUserInfo>(),
                    };
                    #region 铺位编号
                    var unitNumberList = (from pu in Context.Project_Unit
                                          join cu in Context.Contract_Unit on pu.UnitID equals cu.UnitID
                                          where pu.Status == 1 && cu.ContractID == entity.RentContractID
                                          select pu.UnitNumber).ToArray();
                    if (unitNumberList != null)
                    {
                        groupInfo.UnitNumberList = string.Join("，", unitNumberList);
                    }
                    #endregion

                    #region 用户列表
                    var userList = (from mu in Context.Merchant_User
                                    where mu.Status == 1 && mu.StoreID == entity.StoreID
                                    select new
                                    {
                                        mu.UserID,
                                        mu.UserName,
                                        mu.NickName,
                                    }).ToList();

                    if (userList != null)
                    {
                        userList.ForEach(user =>
                        {
                            groupInfo.UserList.Add(new MerchantUserInfo
                            {
                                UserId = user.UserID,
                                UserName = user.UserName,
                                NickName = user.NickName,
                            });
                        });
                    }
                    #endregion

                    list.Add(groupInfo);
                });
            }

            list.OrderBy(item => item.UnitNumberList);
            return list;
        }
    }
}
