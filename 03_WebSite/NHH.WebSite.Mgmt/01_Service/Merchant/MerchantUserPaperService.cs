using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Merchant;
using NHH.Service.Merchant.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Merchant
{
    /// <summary>
    /// 商户用户证件服务
    /// </summary>
    public class MerchantUserPaperService : NHHService<NHHEntities>, IMerchantUserPaperService
    {

        /// <summary>
        /// 根据userId查找paper
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public MerchantUserPaperListModel GetPaperList(int userId)
        {
            var model = new MerchantUserPaperListModel();
            model.UserId = userId;

            model.PaperList = new List<MerchantUserPaperInfo>();

            var query0 = (from mu in Context.Merchant_User
                          where mu.UserID == userId
                          select new
                          {
                              mu.MerchantID,
                              mu.NickName
                          }).FirstOrDefault();

            if (query0 != null)
            {
                model.MerchantId = query0.MerchantID;
                model.NickName = query0.NickName;
            }

            var query = from mup in Context.Merchant_UserPaper
                        join pt in Context.Dictionary on mup.PaperType equals pt.FieldValue
                        join at in Context.Dictionary on mup.AuditStatus equals at.FieldValue
                        where mup.Status == 1 && pt.FieldType == "MechantUserAttach" && at.FieldType == "AttAuditStatus" && mup.UserID == userId
                        select new
                        {
                            mup.PaperID,
                            mup.UserID,
                            mup.PaperNumber,
                            mup.PaperName,
                            mup.PaperPath,
                            mup.PaperType,
                            mup.ExpiredDate,
                            mup.AuditStatus,
                            PaperTypeName = pt.FieldName,
                            AuditStatusName = at.FieldName,
                        };
            var entityList = query.OrderBy(item => item.ExpiredDate).ToList();
            if (entityList != null && entityList.Count > 0)
            {
                foreach (var entity in entityList)
                {
                    var info = new MerchantUserPaperInfo();
                    info.PaperID = entity.PaperID;
                    info.UserID = entity.UserID;
                    info.PaperName = entity.PaperName;
                    info.PaperNumber = entity.PaperNumber;
                    info.PaperPath = entity.PaperPath;
                    info.PaperType = entity.PaperType;
                    info.PaperTypeName = entity.PaperTypeName;
                    info.AuditStatus = entity.AuditStatus;
                    info.AuditStatusName = entity.AuditStatusName;
                    info.ExpiredDate = entity.ExpiredDate;
                    model.PaperList.Add(info);
                }
            }
            return model;
        }

        /// <summary>
        /// 添加商户附件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddPaper(MerchantUserPaperInfo model)
        {
            var instance = CreateBizObject<Merchant_UserPaper>();
            var entity = new Merchant_UserPaper()
            {
                UserID = model.UserID,
                PaperType = model.PaperType,
                PaperNumber = model.PaperNumber,
                PaperName = model.PaperName,
                PaperPath = model.PaperPath,
                ExpiredDate = model.ExpiredDate,
                AuditStatus = 1,
                Status = 1,
                InDate = DateTime.Now,
                InUser = model.InUser,
                EditDate = DateTime.Now,
                EditUser = model.EditUser
            };
            instance.Insert(entity);
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 删除单张附件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paperId"></param>
        /// <returns></returns>
        public bool DelPaper(int userId, int paperId)
        {
            var instance = CreateBizObject<Merchant_UserPaper>();
            var entity = instance.TopOne(item => item.UserID == userId && item.PaperID == paperId);
            if (entity != null)
            {
                entity.Status = -1;
            }
            this.SaveChanges();
            return true;
        }
    }
}
