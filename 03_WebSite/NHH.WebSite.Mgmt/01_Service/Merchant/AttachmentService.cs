using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Merchant;
using NHH.Service.Merchant.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHH.Framework.Utility;
namespace NHH.Service.Merchant
{
    /// <summary>
    /// 商户附件服务
    /// </summary>
    public class AttachmentService : NHHService<NHHEntities>, IAttachmentService
    {
        /// <summary>
        /// 获取商户附件列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public MerchantAttachmentListModel GetAttachmentList(int merchantId)
        {
            var model = new MerchantAttachmentListModel();
            model.MerchantId = merchantId;
            model.AttachmentList = new List<AttachmentInfo>();

            var query = from ma in Context.Merchant_Attachment
                        join mat in Context.Dictionary on ma.AttachmentType equals mat.FieldValue
                        join mas in Context.Dictionary on ma.AuditStatus equals mas.FieldValue
                        where  mat.FieldType == "MerchantAttachType" && mas.FieldType == "AttAuditStatus" && ma.MerchantID == merchantId
                        select new
                        {
                            ma.AttachmentID,
                            ma.AttachmentName,
                            ma.AttachmentCode,
                            ma.AttachmentPath,
                            ma.AttachmentType,
                            ma.ExpiredDate,
                            ma.Status,
                            AttTypeName = mat.FieldName,
                            AuditStatusName = mas.FieldName,
                        };

            var entityList = query.OrderBy(item => item.ExpiredDate).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var item = new AttachmentInfo
                    {
                        AttId = entity.AttachmentID,
                        AttName = entity.AttachmentName,
                        AttCode = entity.AttachmentCode,
                        AttPath = entity.AttachmentPath,
                        AttType = entity.AttachmentType,
                        AttTypeName = entity.AttTypeName,
                        ExpiredDate = entity.ExpiredDate,
                        AuditStatusName = entity.AuditStatusName
                    };

                    //到期天数
                    if (item.ExpiredDate.HasValue)
                    {
                        item.ExpiredDays = (int)((item.ExpiredDate.Value - DateTime.Now).TotalDays);
                    }
                    model.AttachmentList.Add(item);
                });
            }
            return model;
        }

        /// <summary>
        /// 删除证照
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        public MessageBag<bool> DelAttachment(int attachmentId)
        {
            var message = new MessageBag<bool>();
            message.Code = -1;
            message.Desc = "删除失败";
            var instance = CreateBizObject<Merchant_Attachment>();
            instance.Delete(attachmentId);
            this.SaveChanges();
            message.Code = 0;
            return message;
        }

        /// <summary>
        /// 商户证照附件上传
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="attachment"></param>
        public MessageBag<bool> AddAttachment(MerchantAttachmentDetailModel model)
        {
            var instance = CreateBizObject<Merchant_Attachment>();
            var message = new MessageBag<bool>();
            message.Code = -1;
            message.Desc = "新增失败";

            var entity = new Merchant_Attachment()
            {
                MerchantID = model.MerchantId,
                AttachmentType = model.AttachmentType,
                AttachmentName = model.AttachmentName,
                AttachmentPath = model.AttachmentPath,
                AttachmentCode = model.AttachmentCode,
                ExpiredDate = model.ExpiredDate,
                AuditStatus = 1,
                Status = 3,
                InDate = DateTime.Now,
                InUser = model.UserId,
                EditDate = DateTime.Now,
                EditUser = model.UserId
            };
            instance.Insert(entity);
            this.SaveChanges();
            message.Code = 0;
            return message;
        }
    }
}
