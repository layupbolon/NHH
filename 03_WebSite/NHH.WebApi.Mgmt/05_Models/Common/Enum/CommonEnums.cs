using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common.Enum.CommonEnums
{
    /// <summary>
    /// Message表中的MessageType
    /// </summary>
    public enum MessageTypeEnum
    {
        [Description("邮件")]
        Email = 1,
        [Description("短信")]
        SMS = 2,
        [Description("微信")]
        Wechat = 3
    }
    /// <summary>
    /// 审核流程配置类型
    /// </summary>
    public enum ApproveConfigTypeEnum
    {
        [Description("出门申请单")]
        GetOutRequest = 101,
        [Description("装修申请单")]
        DecorateRequest = 102,
        [Description("新闻")]
        News = 103,
        [Description("证照")]
        MerchantAttachment = 104,
        [Description("延时经营申请单")]
        DelayOperateRequest = 105
    }
    /// <summary>
    /// Merchant_Documents表中的DocumentType
    /// </summary>
    public enum MerchantDocumentTypeEnum
    {
        [Description("出门申请单")]
        GetOutRequest = 1,
        [Description("装修申请单")]
        DecorateRequest = 2,
        [Description("延时经营申请单")]
        DelayOperateRequest = 3
    }
    /// <summary>
    /// 管理平台用户消息类型
    /// </summary>
    public enum SysUserMessageSourceTypeEnum
    {
        [Description("通知")]
        Notify = 1,
        [Description("租约")]
        Contract = 2,
        [Description("帐单")]
        Bill = 3,
        [Description("催款")]
        Reminders = 4,
        [Description("活动")]
        Campaign = 5,
        [Description("报修")]
        Repair = 6,
        [Description("投诉")]
        Complaint = 7,
        [Description("特殊单据")]
        MerchantDocuments = 8,
        [Description("新闻")]
        News = 9,
        [Description("商户")]
        Merchant = 10,
    }
    /// <summary>
    /// 商家附件类型
    /// </summary>
    public enum MerchantAttachTypeEnum
    {
        /// <summary>
        /// 营业执照
        /// </summary>
        [Description("营业执照")]
        BusinessLicense = 1,
        /// <summary>
        /// 组织机构代码证
        /// </summary>
        [Description("组织机构代码证")]
        OrganizationCodeCertificate = 2,
        /// <summary>
        /// 税务登记证
        /// </summary>
        [Description("税务登记证")]
        TaxRegistrationCertificate = 3,
        /// <summary>
        /// 身份证(正面)
        /// </summary>
        [Description("身份证(正面)")]
        IdCardFront=4,
        /// <summary>
        /// 品牌授权书
        /// </summary>
        [Description("品牌授权书")]
        BrandLicense = 5,
        /// <summary>
        /// 其它
        /// </summary>
        [Description("其它")]
        Other = 6,
        /// <summary>
        /// 身份证(背面)
        /// </summary>
        [Description("身份证(背面)")]
        IdCardBack = 7,
        /// <summary>
        /// 委托书
        /// </summary>
        [Description("委托书")]
        PowerofAttorney = 8
    }

    /// <summary>
    /// BannerLocationType
    /// </summary>
    public enum BannerLocationTypeEnum
    {
        [Description("顶部")]
        Top = 1,
        [Description("底部")]
        Botton = 2,
        [Description("中部")]
        Middle = 3,
        [Description("旺铺推荐")]
        HotUnit = 4,
        [Description("本周最受欢迎商家")]
        Top10Store = 5
    }
    /// <summary>
    /// BannerTarget
    /// </summary>
    public enum BannerTargetEnum
    {
        [Description("唐小二")]
        NHHtang = 1,
        [Description("官网")]
        NHHcg = 2
    }
    /// <summary>
    /// BannerPageID
    /// </summary>
    public enum BannerPageIDEnum
    {
        [Description("首页")]
        Index = 1
    }
    
}
