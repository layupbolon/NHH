using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Common.Logging;
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
    public class ContractRemindJob : IJob
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(ContractRemindJob));

        public void Execute(IJobExecutionContext context)
        {
            #region 微信、唐小二操作

            var list = GetMerchantInfo();
            if (list.Any())
            {
                try
                {
                    list.ForEach(info =>
                    {
                        //微信
                        if (!string.IsNullOrEmpty(info.WechatOpenId))
                        {
                            var message = new MessageInfo()
                            {
                                MessageType = 3,
                                Recipient = info.WechatOpenId,
                                Subject = "商户合同到期提醒",
                                Content = GetWeChatContent(info),
                                Priority = 2,
                                Link = GetLink(info.ContractID)
                            };
                            MessageDA.Add(message);

                            var entity = new ContractRemindInfo()
                            {
                                ContractID = info.ContractID,
                                ContractEndDate = DateTime.Parse(info.ContractEndDate)
                            };

                            if (!IsExist(entity))
                                InsertContractRemind(entity);
                        }
                    });
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message);
                }

                try
                {
                    list.Select(a => new { a.MerchantId, a.StoreId, a.UserId, a.ContractCode, a.UserName, a.ContractID, a.ContractEndDate }).Distinct().ToList().ForEach(info =>
                    {
                        //唐小二信息
                        var merchantMessage = new MerchantMessageInfo
                        {
                            MerchantId = info.MerchantId,
                            StoreId = info.StoreId,
                            UserId = info.UserId,
                            Subject = "合同到期提醒",
                            Content =
                                string.Format("亲爱的{0}，您有一份合同（编号：<a style='color:#63a8eb' href='{1}'>{2}</a>）即将于{3}到期。如有服务需要，欢迎联系我们的营运人员，谢谢！",
                                    info.UserName,
                                    GetLink(info.ContractID),
                                    info.ContractCode,
                                    info.ContractEndDate),
                            SourceType = 11,
                            SourceRefId = info.ContractID
                        };

                        MerchantMessageDA.Add(merchantMessage);
                    });
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message);
                }
            }

            #endregion

            #region 短信、智能平台操作

            var phoneAndMgmtList = GetContractRemindSmsInfo();
            if (phoneAndMgmtList.Any())
            {
                try
                {
                    foreach (var info in phoneAndMgmtList)
                    {
                        //短信
                        if (!string.IsNullOrEmpty(info.Mobile))
                        {
                            var content = new MerchantContractNotify(info.StoreName, info.ContractCode, info.ContractEndDate);

                            SmsMessage sms = new SmsMessage
                            {
                                TemplateCode = MerchantContractNotify.TemplateCode,
                                SignName = "唐小二",
                                Param = JsonConvert.SerializeObject(content)
                            };

                            var message = new MessageInfo()
                            {
                                MessageType = 2,
                                Recipient = info.Mobile,
                                Subject = "合同到期提醒",
                                Content = JsonConvert.SerializeObject(sms),
                                Priority = 2,
                                Link = ""
                            };

                            MessageDA.Add(message);

                            var entity = new ContractRemindInfo()
                            {
                                ContractID = info.ContractId,
                                ContractEndDate = DateTime.Parse(info.ContractEndDate)
                            };

                            if (!IsExist(entity))
                                InsertContractRemind(entity);
                        }

                        //管理平台
                        var userMessage = new UserMessageInfo()
                        {
                            UserId = info.UserId,
                            Subject = "商户合同到期提醒",
                            Content =
                                string.Format("亲，{0}的合同（编号：<a style='color:#63a8eb' href='{1}'>{2}</a>）即将于{3}到期，请及时沟通处理，",
                                    info.StoreName,
                                    string.Format("/Contract/ContractInfo/Detail?contractId={0}", info.ContractId)
                                    , info.ContractCode
                                    , info.ContractEndDate),
                            SourceType = 10,
                            SourceRefId = info.ContractId,
                        };

                        UserMessageDA.Add(userMessage);
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message);
                }
            }

            #endregion

            #region 去除合同提醒表中已到期的记录

            var listInfo = GetList();
            if (listInfo.Any())
            {
                var db = new Entities.NHHEntities();
                listInfo.ForEach(info =>
                {
                    var entity = db.Contract_RemindRecord.Find(info.RemindID);
                    entity.Status = -1;
                    entity.EditDate = DateTime.Now;
                    entity.EditUser = 1;

                    db.Entry(entity).State = EntityState.Modified;
                });
                db.SaveChanges();
            }

            #endregion
        }

        private List<MerchantUserInfoForContract> GetMerchantInfo()
        {
            var list = new List<MerchantUserInfoForContract>();

            string strCmd = @"SELECT C.ContractID
                                    ,C.ContractCode
                                    ,C.ContractEndDate
                                    ,MS.MerchantID
                                    ,MS.StoreID
                                    ,MS.StoreName
		                            ,MU.UserID
		                            ,MU.NickName
		                            ,MU.UserName
		                            ,MU.WechatOpenID
	                            FROM dbo.Contract(NOLOCK) AS C
	                            INNER JOIN dbo.Merchant_Store(NOLOCK) AS MS ON C.MerchantID = MS.MerchantID
	                            INNER JOIN dbo.Merchant_User(NOLOCK) as MU ON C.MerchantID = MU.MerchantID
	                            WHERE C.ContractStatus = 3 And MU.Status = 1 And MU.RoleID = 1 AND DATEDIFF(WEEK,GETDATE(),C.ContractEndDate) <= 8 AND C.ContractID not in (SELECT ContractID FROM dbo.Contract_RemindRecord WHERE Status = 1)";

            var table = SqlHelper.ExecuteDataTable(strCmd);
            if (table == null || table.Rows == null || table.Rows.Count == 0)
                return list;

            list.AddRange(table.Rows.Cast<DataRow>().Select(row => new MerchantUserInfoForContract
            {
                ContractID = ConvertHelper.ToInt(row["ContractID"]),
                ContractCode = ConvertHelper.ToString(row["ContractCode"]),
                ContractEndDate = DateTime.Parse(row["ContractEndDate"].ToString()).ToString("yyyy年MM月dd日"),
                MerchantId = ConvertHelper.ToInt(row["MerchantID"]),
                StoreId = ConvertHelper.ToInt(row["StoreID"]),
                StoreName = ConvertHelper.ToString(row["StoreName"]),
                UserId = ConvertHelper.ToInt(row["UserID"]),
                NickName = ConvertHelper.ToString(row["NickName"]),
                UserName = ConvertHelper.ToString(row["UserName"]),
                WechatOpenId = ConvertHelper.ToString(row["WechatOpenID"])
            }));
            return list;
        }

        /// <summary>
        /// 获取微信内容
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private string GetWeChatContent(MerchantUserInfoForContract info)
        {
            var msg = new MessageData
            {
                Type = "MerchantContractRemind",
                TemplateId = ParamHelper.GetString("MerchantContractTemplateId")
            };

            var content = new MerchantContractMessage()
            {
                first = new requestFieldDetail
                {
                    value = string.Format("亲爱的{0}，您有一份合同即将到期。", info.UserName),
                    color = "#173177"
                },
                keyword1 = new requestFieldDetail
                {
                    value = info.ContractCode,
                    color = "#173177"
                },
                keyword2 = new requestFieldDetail
                {
                    value = info.ContractEndDate,
                    color = "#173177"
                },
                remark = new requestFieldDetail
                {
                    value = "如有服务需要，欢迎联系我们的营运人员。谢谢！",
                    color = "#173177"
                }
            };

            msg.Content = JsonConvert.SerializeObject(content);

            return JsonConvert.SerializeObject(msg);
        }

        private string GetLink(int contractID)
        {
            return string.Format(ParamHelper.GetString("MerchantContractLink"), contractID);
        }

        private List<ContractRemindSmsInfo> GetContractRemindSmsInfo()
        {
            var list = new List<ContractRemindSmsInfo>();

            var sb = new StringBuilder();
            sb.Append(
                "SELECT DISTINCT con.ContractID,ms.StoreName,con.ContractCode,con.ContractEndDate,e.Moblie,u.UserID ");
            sb.Append("FROM dbo.Contract con ");
            sb.Append("INNER JOIN dbo.Merchant_Store ms ON con.ContractID = ms.RentContractID ");
            sb.Append("INNER JOIN dbo.Project p ON p.ProjectID = con.ProjectID ");
            sb.Append("INNER JOIN dbo.Employee e ON p.ManageCompanyID = e.CompanyID ");
            sb.Append("INNER JOIN dbo.Sys_User u ON e.EmployeeID = u.EmployeeID ");
            sb.Append("INNER JOIN dbo.Sys_User_Role ur ON u.UserID = ur.UserID ");
            sb.AppendFormat("WHERE DATEDIFF(WEEK,GETDATE(),ContractEndDate) <=8 AND ur.RoleID IN ({0}) ",
                string.IsNullOrEmpty(ParamHelper.GetString("MerchantContractRemindRoleIDs"))
                    ? "0"
                    : ParamHelper.GetString("MerchantContractRemindRoleIDs"));
            sb.Append(
                " AND con.ContractID NOT IN (SELECT ContractID FROM dbo.Contract_RemindRecord WHERE Status = 1)");

            var table = SqlHelper.ExecuteDataTable(sb.ToString());
            if (table == null || table.Rows == null || table.Rows.Count == 0)
                return list;

            list.AddRange(table.Rows.Cast<DataRow>().Select(row => new ContractRemindSmsInfo
            {
                ContractId = ConvertHelper.ToInt(row["ContractID"]),
                UserId = ConvertHelper.ToInt(row["UserID"]),
                ContractEndDate = DateTime.Parse(row["ContractEndDate"].ToString()).ToString("yyyy年MM月dd日"),
                StoreName = ConvertHelper.ToString(row["StoreName"]),
                Mobile = ConvertHelper.ToString(row["Moblie"]),
                ContractCode = ConvertHelper.ToString(row["ContractCode"])
            }));
            return list;
        }

        private bool InsertContractRemind(ContractRemindInfo info)
        {
            var db = new Entities.NHHEntities();
            var entity = new Entities.Contract_RemindRecord()
            {
                ContractID = info.ContractID,
                ContractEndDate = info.ContractEndDate,
                Status = 1,
                InUser = 1,
                InDate = DateTime.Now
            };

            db.Entry(entity).State = EntityState.Added;
            return db.SaveChanges() > 0;
        }

        private bool IsExist(ContractRemindInfo info)
        {
            var db = new Entities.NHHEntities();
            return
                db.Contract_RemindRecord.Any(
                    c =>
                        c.ContractID == info.ContractID &&
                        c.ContractEndDate.Equals(info.ContractEndDate) &&
                        c.Status == 1);
        }

        /// <summary>
        /// 获取提醒表中合同还未到期的记录
        /// </summary>
        /// <returns></returns>
        private List<ContractRemindInfo> GetList()
        {
            var list = new List<ContractRemindInfo>();
            const string sqlcmd = @" SELECT cr.RemindID,cr.ContractID,cr.ContractEndDate,cr.Status
                            FROM dbo.Contract con 
                            INNER JOIN dbo.Contract_RemindRecord cr ON con.ContractID = cr.ContractID
                            WHERE con.ContractEndDate >= GETDATE() AND cr.Status = -1";

            var table = SqlHelper.ExecuteDataTable(sqlcmd);
            if (table == null || table.Rows == null || table.Rows.Count == 0)
                return list;

            list.AddRange(table.Rows.Cast<DataRow>().Select(row => new ContractRemindInfo
            {
                RemindID = ConvertHelper.ToInt(row["RemindID"]),
                ContractID = ConvertHelper.ToInt(row["ContractID"]),
                ContractEndDate = DateTime.Parse(row["ContractEndDate"].ToString()),
                Status = ConvertHelper.ToInt(row["Status"])
            }));
            return list;
        }
    }
}
