using NHH.Framework.Service;
using NHH.Models.Merchant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Merchant.IService
{
    /// <summary>
    /// 商户附件服务接口
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// 获取商户附件列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        MerchantAttachmentListModel GetAttachmentList(int merchantId);

        /// <summary>
        /// 删除证照
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <returns></returns>
        MessageBag<bool> DelAttachment(int attachmentId);

        /// <summary>
        /// 新增商户附件
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        MessageBag<bool> AddAttachment(MerchantAttachmentDetailModel attachment);
    }
}
