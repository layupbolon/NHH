using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Merchant;

namespace NHH.Service.Merchant.IService
{
    /// <summary>
    /// 商户附件服务接口
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// 获取商户证照列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        /// <remarks>
        /// 由于唐小二端每个证照只取最新上传的，所以这里要分组取最新的AttachmentID
        /// 先取出IDS，如：
        /// SELECT MAX(AttachmentID) FROM nhh.dbo.Merchant_Attachment 
        /// WHERE MerchantID = 10002 AND [Status] = 1
        /// GROUP BY AttachmentType
        /// </remarks>
        List<MerchantAttachmentInfo> GetMerchantAttachmentList(int merchantId);

        /// <summary>
        /// 商户证照附件上传
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddMerchantAttachment(MerchantAttachmentInfo model);

        /// <summary>
        /// 更新商户证照
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateMerchantAttachment(MerchantAttachmentInfo model);

        /// <summary>
        /// 获取证照
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        MerchantAttachmentInfo GetAttachmentInfo(int attachmentId, int merchantId);

        /// <summary>
        /// 作废证照
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        bool CancelAttachment(int attachmentId, int merchantId);
    }
}
