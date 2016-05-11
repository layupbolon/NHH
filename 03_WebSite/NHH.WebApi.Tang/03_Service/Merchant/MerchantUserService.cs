using NHH.Framework.Caching;
using NHH.Framework.Security.Cryptography;
using NHH.Framework.Service;
using NHH.Framework.Utility;
using NHH.Models.Common.Common;
using NHH.Models.Common.Enum.CommonEnums;
using NHH.Models.Merchant;
using NHH.Service.Common.IService;
using NHH.Service.Merchant.IService;
using NHH.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Service.Merchant
{
    /// <summary>
    /// 商家用户服务
    /// </summary>
    public class MerchantUserService : NHHService<NHHEntities>, IMerchantUserService
    {
        private ICommonService commonService;

        public MerchantUserService()
        {
            commonService = NHHServiceFactory.Instance.CreateService<ICommonService>();
        }

        /// <summary>
        /// 根据WechatOpenID获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public MerchantUserInfo GetMerchantUserDetailByWechatOpenID(string wechatOpenId)
        {
            MerchantUserInfo model = null;
            var query = from mu in Context.Merchant_User
                        where mu.WechatOpenID == wechatOpenId
                        select new
                        {
                            mu.UserID,
                            mu.StoreID,
                            mu.MerchantID,
                            mu.RoleID,
                            mu.UserName,
                            mu.Password,
                            mu.NickName,
                            mu.Email,
                            mu.Moblie,
                            mu.WechatOpenID,
                            mu.Status,
                            mu.PhotoFile,
                            mu.IDNumber
                        };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model = new MerchantUserInfo();
                model.UserId = entity.UserID;
                model.StoreId = entity.StoreID ?? 0;
                model.MerchantId = entity.MerchantID;
                model.RoleId = entity.RoleID;
                model.RoleName = entity.RoleID == 1 ? "Admin" : "Operation";
                model.UserName = entity.UserName;
                model.Password = entity.Password;
                model.NickName = entity.NickName;
                model.Email = entity.Email;
                model.Mobile = entity.Moblie;
                model.WechatOpenId = entity.WechatOpenID;
                model.Status = entity.Status;
                model.PhotoFile = UrlHelper.GetImageUrl(entity.PhotoFile, 100);
                model.IDNumber = entity.IDNumber;
            }

            return model;
        }

        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public MerchantUserInfo GetMerchantUserDetail(string userName)
        {
            MerchantUserInfo model = null;
            var query = from mu in Context.Merchant_User
                where mu.UserName == userName
                select new
                {
                    mu.UserID,
                    mu.StoreID,
                    mu.MerchantID,
                    mu.RoleID,
                    mu.UserName,
                    mu.Password,
                    mu.NickName,
                    mu.Email,
                    mu.Moblie,
                    mu.WechatOpenID,
                    mu.Status,
                    mu.PhotoFile,
                    mu.IDNumber
                };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model = new MerchantUserInfo();
                model.UserId = entity.UserID;
                model.StoreId = entity.StoreID ?? 0;
                model.MerchantId = entity.MerchantID;
                model.RoleId = entity.RoleID;
                model.RoleName = entity.RoleID == 1 ? "Admin" : "Operation";
                model.UserName = entity.UserName;
                model.Password = entity.Password;
                model.NickName = entity.NickName;
                model.Email = entity.Email;
                model.Mobile = entity.Moblie;
                model.WechatOpenId = entity.WechatOpenID;
                model.Status = entity.Status;
                model.PhotoFile = UrlHelper.GetImageUrl(entity.PhotoFile, 100);
                model.IDNumber = entity.IDNumber;
            }

            return model;
        }

        /// <summary>
        /// 根据手机号或邮箱取用户
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MerchantUserInfo GetUserByRecipient(string recipient,int userId)
        {
            MerchantUserInfo model = null;
            var query = from mu in Context.Merchant_User
                where( mu.Moblie == recipient || mu.Email == recipient) && mu.UserID != userId
                select new
                {
                    mu.UserID,
                    mu.StoreID,
                    mu.MerchantID,
                    mu.RoleID,
                    mu.UserName,
                    mu.Password,
                    mu.NickName,
                    mu.Email,
                    mu.Moblie,
                    mu.WechatOpenID,
                    mu.Status,
                    mu.PhotoFile,
                    mu.IDNumber
                };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model = new MerchantUserInfo();
                model.UserId = entity.UserID;
                model.StoreId = entity.StoreID ?? 0;
                model.MerchantId = entity.MerchantID;
                model.RoleId = entity.RoleID;
                model.RoleName = entity.RoleID == 1 ? "Admin" : "Operation";
                model.UserName = entity.UserName;
                model.Password = entity.Password;
                model.NickName = entity.NickName;
                model.Email = entity.Email;
                model.Mobile = entity.Moblie;
                model.WechatOpenId = entity.WechatOpenID;
                model.Status = entity.Status;
                model.PhotoFile = UrlHelper.GetImageUrl(entity.PhotoFile, 100);
                model.IDNumber = entity.IDNumber;
            }

            return model;
        }


        /// <summary>
        /// 获取用户详细情况
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MerchantUserInfo GetMerchantUserDetail(int userId)
        {
            var model = new MerchantUserInfo();
            var query = from mu in Context.Merchant_User
                        join m in Context.Merchant on mu.MerchantID equals m.MerchantID
                        join ms in Context.Merchant_Store on mu.StoreID equals ms.StoreID into t_ms
                        from ms in t_ms.DefaultIfEmpty()
                        join cu in Context.Contract_Unit on ms.RentContractID equals cu.ContractID into t_cu from cu in t_cu.DefaultIfEmpty()
                        join pu in Context.Project_Unit on cu.UnitID equals pu.UnitID into t_pu from pu in t_pu.DefaultIfEmpty()
                        where mu.UserID == userId
                        select new
                        {
                            mu.UserID,
                            mu.MerchantID,
                            m.MerchantName,
                            mu.RoleID,
                            mu.UserName,
                            mu.Password,
                            mu.NickName,
                            mu.Email,
                            mu.Moblie,
                            mu.WechatOpenID,
                            ms.StoreName,
                            mu.StoreID,
                            pu.UnitNumber,
                            mu.Status,
                            mu.PhotoFile,
                            mu.IDNumber
                        };

            var entity = query.FirstOrDefault(); 

            if (entity != null)
            {
                model.UserId = entity.UserID;
                model.MerchantId = entity.MerchantID;
                model.MerchantName = entity.MerchantName;
                model.RoleId = entity.RoleID;
                model.RoleName = entity.RoleID == 1 ? "Admin" : "Operation";
                model.UserName = entity.UserName;
                model.Password = entity.Password;
                model.NickName = entity.NickName;
                model.Email = entity.Email;
                model.Mobile = entity.Moblie;
                model.WechatOpenId = entity.WechatOpenID;
                model.StoreName = entity.StoreName;
                model.StoreId = entity.StoreID??0;
                model.UnitNumber = entity.UnitNumber;
                model.Status = entity.Status;
                model.PhotoFile = UrlHelper.GetImageUrl(entity.PhotoFile, 100);
                model.IDNumber = entity.IDNumber;
            }

            return model;
        }

        /// <summary>
        /// 根据商户ID获取该商户的所有用户列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public List<MerchantUserInfo> GetMerchantUserList(int merchantId)
        {
            var model = new List<MerchantUserInfo>();
            var query = from mu in Context.Merchant_User
                join m in Context.Merchant on mu.MerchantID equals m.MerchantID
                join s in Context.Merchant_Store on mu.StoreID equals s.StoreID into temp
                from t in temp.DefaultIfEmpty()
                where mu.MerchantID == merchantId && mu.Status > 0
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
                    t.StoreName,
                    mu.StoreID,
                    mu.PhotoFile
                };

            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var item = new MerchantUserInfo
                    {
                        UserId = entity.UserID,
                        MerchantId = entity.MerchantID,
                        MerchantName = entity.MerchantName,
                        RoleId = entity.RoleID,
                        RoleName = entity.RoleID == 1 ? "Admin" : "Operation",
                        UserName = entity.UserName,
                        NickName = entity.NickName,
                        Email = entity.Email,
                        Mobile = entity.Moblie,
                        StoreName = entity.StoreName,
                        StoreId = entity.StoreID == null ? 0 : entity.StoreID.Value,
                        PhotoFile = UrlHelper.GetImageUrl(entity.PhotoFile, 100)
                    };
                    model.Add(item);
                });
            }
            return model;
        }

        /// <summary>
        ///  新增商户用户信息
        /// </summary>
        /// <param name="detailModel"></param>
        /// <returns></returns>
        public bool AddMerchantUser(MerchantUserInfo detailModel)
        {
            var instance = CreateBizObject<Merchant_User>();
            var entity = new Merchant_User()
            {
                MerchantID = detailModel.MerchantId,
                StoreID = detailModel.StoreId,
                UserName = detailModel.UserName,
                Password = Cryptographer.SHA1.Encrypt(detailModel.Password),
                RoleID = detailModel.RoleId,
                NickName = detailModel.NickName,
                Email = detailModel.Email,
                Moblie = detailModel.Mobile,
               // WechatOpenID = detailModel.WechatOpenId,
                Status = 1,
                InDate = DateTime.Now,
                InUser = 1,
                EditDate=DateTime.Now,
                EditUser=1
            };

            instance.Insert(entity);
            this.SaveChanges();
            detailModel.UserId = entity.UserID;
            return detailModel.UserId > 0;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="detailModel"></param>
        /// <returns></returns>
        public bool UpdateMerchantUser(MerchantUserInfo detailModel)
        {
            var instance = CreateBizObject<Merchant_User>();
            var entity = instance.GetBySysNo(detailModel.UserId);
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(detailModel.NickName))
                    entity.NickName = detailModel.NickName;
                if (!string.IsNullOrEmpty(detailModel.Email))
                    entity.Email = detailModel.Email;
                if (!string.IsNullOrEmpty(detailModel.Mobile))
                    entity.Moblie = detailModel.Mobile;
                if (!string.IsNullOrEmpty(detailModel.IDNumber))
                    entity.IDNumber = detailModel.IDNumber;
                //if (detailModel.RoleId != 0)
                //    entity.RoleID = detailModel.RoleId;
                //if (detailModel.StoreId != 0)
                //    entity.StoreID = detailModel.StoreId;
                entity.EditDate = DateTime.Now;
                entity.EditUser = detailModel.UserId;

                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateUserPhoto(MerchantUserInfo model)
        {
            var instance = CreateBizObject<Merchant_User>();
            var entity = instance.GetBySysNo(model.UserId);
            if (entity != null)
            {
                entity.PhotoFile = model.PhotoFile;

                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteMerchantUser(int userId)
        {
            var instance = CreateBizObject<Merchant_User>();
            instance.Delete(userId);
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool MerchantUserUpdatePassword(MerchantUserPassword model)
        {
            var instance = CreateBizObject<Merchant_User>();
            var entity = instance.GetBySysNo(model.userID);
            if (entity != null)
            {
                //旧密码输入正确才能更新成新密码
                if (entity.Password.Equals(Cryptographer.SHA1.Encrypt(model.OldPassword)))
                {
                    entity.Password = Cryptographer.SHA1.Encrypt(model.NewPassword);
                    instance.Update(entity);
                    this.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ResetMerchantUserPassword(ResetPasswordInfo model)
        {
            var instance = CreateBizObject<Merchant_User>();
            var entity = instance.GetBySysNo(model.UserID);
            if (entity != null)
            {
                entity.Password = Cryptographer.SHA1.Encrypt(model.Password);
                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取用户未读的消息数量
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public int GetNoReadMerchantMessageQty(int userId, int merchantId)
        {
            int result =
                (from mm in Context.Merchant_Message
                    where mm.UserID == userId && mm.MerchantID == merchantId && mm.Status == 1
                    select 1).Count();
            return result;
        }

        /// <summary>
        /// 获取用户的消息列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public MerchantMessageListModel GetMessageList(int userId,int page, int size)
        {
            var model = new MerchantMessageListModel();
            model.MerchantMessageInfo = new List<MerchantMessageInfo>();
            model.PagingInfo=new PagingInfo();
            model.PagingInfo.PageIndex = page;
            model.PagingInfo.PageSize = size;
            #region Query

            var query = from mm in Context.Merchant_Message
                join su in Context.Sys_User on mm.InUser equals su.UserID into temp
                from t in temp.DefaultIfEmpty()
                where mm.Status >= 1 && mm.UserID == userId
                select new
                {
                    mm.MessageID,
                    mm.Subject,
                    mm.InUser,
                    mm.InDate,
                    mm.Status,
                    t.UserName
                };
            #endregion Query

            model.PagingInfo.TotalCount = query.Count();
            query =
                query.OrderByDescending(m => m.MessageID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);
            var entityList = query.ToList();
            entityList.ForEach(entity =>
            {
                var info = new MerchantMessageInfo();
                info.MessageID = entity.MessageID;
                info.Subject = entity.Subject;
                info.InUser = entity.InUser;
                info.InDate = entity.InDate;
                info.Status = entity.Status;
                info.InUserName = entity.UserName;
                model.MerchantMessageInfo.Add(info);
            });
            return model;
        }

        /// <summary>
        /// 获取单个通知消息信息
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public MerchantMessageInfo GetMessageDetail(int messageId)
        {
            var model = new MerchantMessageInfo();
            var query = from mm in Context.Merchant_Message
                        join su in Context.Sys_User on mm.InUser equals su.UserID into temp
                        from su in temp.DefaultIfEmpty()
                        join e in Context.Employee on su.EmployeeID equals e.EmployeeID into t_e
                        from e in t_e.DefaultIfEmpty()
                        where mm.MessageID == messageId && mm.Status >= 1
                        select new
                        {
                            mm.MessageID,
                            mm.Subject,
                            mm.Content,
                            mm.SourceType,
                            mm.SourceRefID,
                            mm.InUser,
                            mm.InDate,
                            mm.Status,
                            e.EmployeeName
                        };

            var entity = query.FirstOrDefault(); ;

            if (entity != null)
            {
                model.MessageID = entity.MessageID;
                model.Subject = entity.Subject;
                model.Content = entity.Content;
                model.SourceType = entity.SourceType;
                model.SourceRefID = entity.SourceRefID.Value;
                model.InUser = entity.InUser;
                model.InDate = entity.InDate;
                model.Status = entity.Status;
                model.InUserName = string.IsNullOrEmpty(entity.EmployeeName) ? "唐小二" : entity.EmployeeName;

                UpdateMessageReadState(model.MessageID);
            }

            return model;
        }

        /// <summary>
        /// 更新通知消息为已读
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public bool UpdateMessageReadState(int messageId)
        {
            var instance = CreateBizObject<Merchant_Message>();
            var entity = instance.GetBySysNo(messageId);
            if (entity != null)
            {
                entity.Status = 2;
                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更新商户用户状态
        /// </summary>
        /// <param name="userId">商户用户编号</param>
        /// <param name="status">状态 -1作废 1有效 2可登陆</param>
        /// <returns></returns>
        public bool UpdateMerchantUserStatus(int userId, int status)
        {
            var instance = CreateBizObject<Merchant_User>();
            var entity = instance.GetBySysNo(userId);
            if (entity != null)
            {
                entity.Status = status;
                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 用户是否绑定过微信 
        /// </summary>
        /// <param name="wechatOpenId"></param>
        /// <returns></returns>
        public bool IsExistsOpenID(string wechatOpenId)
        {
            int result = (from mu in Context.Merchant_User where mu.WechatOpenID == wechatOpenId select 1).Count();
            return result > 0;
        }

        /// <summary>
        /// 更新商户用户的微信OpenID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="wechatOpenId"></param>
        /// <returns></returns>
        public bool UpdateMerchantUserOpenId(int userId, string wechatOpenId)
        {
            var instance = CreateBizObject<Merchant_User>();
            var entity = instance.GetBySysNo(userId);
            if (null != entity)
            {
                entity.WechatOpenID = wechatOpenId;
                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 发送重置密码验证码，并存入Cache
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="recipient"></param>
        /// <returns></returns>
        public bool SendForgetValidateCode(MessageTypeEnum messageType, string recipient)
        {
            string valiCode = RandomHelper.RandomNumberString(6);
            string cacheKey = CommonString.ForgetPasswordCacheKeyPrefix + recipient;
            CacheManager cacheManager = new CacheManager() {SlidingExpiration = new TimeSpan(0, 5, 0)};
            cacheManager.Add(cacheKey, valiCode);

            return commonService.SendMessage(new MessageInfo(messageType, 1, recipient,
                    CommonString.ForgetPasswordSendMessageSubject,
                    string.Format(CommonString.ForgetPasswordSendMessageContent, valiCode)));
        }
        
    }
}
