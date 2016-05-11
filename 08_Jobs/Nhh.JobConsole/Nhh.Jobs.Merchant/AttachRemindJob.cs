using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using log4net;
using Newtonsoft.Json;
using Nhh.Jobs.Model;
using Nhh.Jobs.Utility;
using NHH.Framework.Utility;
using NHH.Framework.Wechat;
using NHH.Message.Models.Sms;
using NHH.Message.Models.Wechat;
using Quartz;

namespace Nhh.Jobs.Merchant
{
    /// <summary>
    /// 商户证照有效期提醒
    /// </summary>
    public sealed class AttachRemindJob : IJob
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(AttachRemindJob));

        public void Execute(IJobExecutionContext context)
        {
            var merchantRoleUserList = GetWechatAndTangList();
            if (merchantRoleUserList.Any())
            {
                try
                {
                    foreach (var info in merchantRoleUserList)
                    {
                        //微信
                        if (!string.IsNullOrEmpty(info.WechatOpenID))
                        {
                            var message = new MessageInfo()
                            {
                                MessageType = 3,
                                Recipient = info.WechatOpenID,
                                Subject = "商户证件有效期提醒",
                                Content = GetWeiChatContent(info.AttachmentType),
                                Priority = 2,
                                Link = ParamHelper.GetString("MerchantLicenseLink")
                            };
                            MessageDA.Add(message);
                        }
                    }

                    foreach (var info in merchantRoleUserList.Select(a => new { a.MerchantID, a.StoreID, a.UserID }).Distinct())
                    {
                        //唐小二信息
                        var merchantMessage = new MerchantMessageInfo
                        {
                            MerchantId = info.MerchantID,
                            StoreId = info.StoreID,
                            UserId = info.UserID,
                            Subject = "商户证件有效期提醒",
                            Content =
                                string.Format("亲，你有部分证件即将过期或者已过期，<a style='color:#63a8eb' href='{0}'>请点击查看详情>>></a>",
                                    ParamHelper.GetString("MerchantLicenseLink")),
                            SourceType = 10,
                            SourceRefId = info.MerchantID
                        };

                        MerchantMessageDA.Add(merchantMessage);
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message);
                }

            }

            var phoneAndMgmtList = GetPhoneAndMgmtList();
            if (phoneAndMgmtList.Any())
            {
                try
                {
                    foreach (var info in phoneAndMgmtList)
                    {
                        //短信
                        if (!string.IsNullOrEmpty(info.Mobile))
                        {
                            var content = new MerchantAttachmentExpiredNotify(info.MerchantName);

                            SmsMessage sms = new SmsMessage
                            {
                                TemplateCode = MerchantAttachmentExpiredNotify.TemplateCode,
                                SignName = "唐小二",
                                Param = JsonConvert.SerializeObject(content)
                            };

                            var message = new MessageInfo()
                            {
                                MessageType = 2,
                                Recipient = info.Mobile,
                                Subject = "证件有效期提醒",
                                Content = JsonConvert.SerializeObject(sms),
                                Priority = 2,
                                Link = ""
                            };

                            MessageDA.Add(message);
                        }

                        //管理平台
                        var userMessage = new UserMessageInfo()
                        {
                            UserId = info.UserID,
                            Subject = "商户证件有效期提醒",
                            Content =
                                string.Format("亲，{0}有部分证件即将过期或者已过期，<a style='color:#63a8eb' href='{1}'>请点击查看详情>>></a>",
                                    info.MerchantName,
                                    string.Format("/Plan/Merchant/Detail?merchantId={0}", info.MerchantID)),
                            SourceType = 10,
                            SourceRefId = info.AttachmentID,
                        };

                        UserMessageDA.Add(userMessage);
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message);
                }

            }
        }

        /// <summary>
        /// 获取商户管理员信息
        /// </summary>
        private List<MerchantRoleUserInfo> GetWechatAndTangList()
        {
            var list = new List<MerchantRoleUserInfo>();

            var sb = new StringBuilder();
            sb.Append("SELECT ma.AttachmentID,ma.MerchantID,mu.WechatOpenID,ms.StoreID,mu.UserID,d.FieldName ");
            sb.Append("FROM dbo.Merchant_Attachment(Nolock) ma ");
            sb.Append(
                "INNER JOIN dbo.Dictionary(Nolock) d ON ma.AttachmentType = d.FieldValue AND d.FieldType = 'MerchantAttachType' ");
            sb.Append("INNER JOIN dbo.Merchant_User(Nolock) mu ON mu.MerchantID = ma.MerchantID ");
            sb.Append("INNER JOIN dbo.Merchant_Store(Nolock) ms ON ma.MerchantID = ms.MerchantID ");
            sb.Append("WHERE mu.RoleID = 1 AND ISNULL(DATEDIFF(MONTH,GETDATE(),ma.ExpiredDate),100) <=2 AND mu.Status = 1 AND ms.Status = 1");

            var table = SqlHelper.ExecuteDataTable(sb.ToString());
            if (table == null || table.Rows == null || table.Rows.Count == 0)
                return list;

            list.AddRange((from DataRow row in table.Rows
                           select new MerchantRoleUserInfo
                           {
                               AttachmentID = ConvertHelper.ToInt(row["AttachmentID"]),
                               MerchantID = ConvertHelper.ToInt(row["MerchantID"]),
                               WechatOpenID = ConvertHelper.ToString(row["WechatOpenID"]),
                               StoreID = ConvertHelper.ToInt(row["StoreID"]),
                               UserID = ConvertHelper.ToInt(row["UserID"]),
                               AttachmentType = ConvertHelper.ToString(row["FieldName"])
                           }).Select(info => info));
            return list;
        }

        /// <summary>
        /// 获取审核人信息
        /// </summary>
        /// <returns></returns>
        private List<AttachApproveInfo> GetPhoneAndMgmtList()
        {
            var list = new List<AttachApproveInfo>();

            var sb = new StringBuilder();
            sb.Append("SELECT ma.AttachmentID,u.UserID,e.Moblie,m.MerchantName,ma.MerchantID ");
            sb.Append("FROM dbo.Merchant_Attachment(Nolock) ma ");
            sb.Append("LEFT JOIN dbo.Merchant(Nolock) m ON m.MerchantID = ma.MerchantID ");
            sb.Append("LEFT JOIN dbo.Approve_Process(Nolock) ap ON ap.RefID = ma.AttachmentID ");
            sb.Append("INNER JOIN dbo.Approve_Config(Nolock) ac ON ac.ConfigID = ap.ConfigID ");
            sb.Append("LEFT JOIN dbo.Sys_User_Role(Nolock) ur ON ur.RoleID = ac.RoleID ");
            sb.Append("LEFT JOIN dbo.Sys_User(Nolock) u ON u.UserID = ur.UserID ");
            sb.Append("LEFT JOIN dbo.Employee(Nolock) e ON e.EmployeeID = u.EmployeeID ");
            sb.Append("WHERE ac.ConfigType = 104 AND ISNULL(DATEDIFF(MONTH,GETDATE(),ma.ExpiredDate),100) <= 2 ");

            var table = SqlHelper.ExecuteDataTable(sb.ToString());
            if (table == null || table.Rows == null || table.Rows.Count == 0)
                return list;

            list.AddRange((from DataRow row in table.Rows
                           select new AttachApproveInfo
                           {
                               MerchantID = ConvertHelper.ToInt(row["MerchantID"]),
                               AttachmentID = ConvertHelper.ToInt(row["AttachmentID"]),
                               UserID = ConvertHelper.ToInt(row["UserID"]),
                               Mobile = ConvertHelper.ToString(row["Moblie"]),
                               MerchantName = ConvertHelper.ToString(row["MerchantName"])
                           }).Select(info => info));
            return list;
        }

        /// <summary>
        /// 获取微信内容
        /// </summary>
        /// <param name="attachmentType"></param>
        /// <returns></returns>
        private string GetWeiChatContent(string attachmentType)
        {

            var msg = new MessageData
            {
                Type = "MerchantLicense",
                TemplateId = ParamHelper.GetString("MerchantLicenseTemplateId")
            };

            var content = new MerchantLicenseExpireMessage()
            {
                first = new requestFieldDetail
                {
                    value = "有部分证件即将到期或已过期：",
                    color = "#173177"
                },
                keyword1 = new requestFieldDetail
                {
                    value = attachmentType,
                    color = "#173177"
                },
                keyword2 = new requestFieldDetail
                {
                    value = "1",
                    color = "#173177"
                },
                remark = new requestFieldDetail
                {
                    value = "请及时更新并上传新的证件。",
                    color = "#173177"
                }
            };

            msg.Content = JsonConvert.SerializeObject(content);

            return JsonConvert.SerializeObject(msg);
        }
    }
}
