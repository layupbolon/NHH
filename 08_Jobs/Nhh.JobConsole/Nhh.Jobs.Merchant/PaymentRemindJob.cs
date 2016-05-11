using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Logging;
using Newtonsoft.Json;
using Nhh.Jobs.Model;
using Nhh.Jobs.Utility;
using NHH.Message.Models.Sms;
using Quartz;

namespace Nhh.Jobs.Merchant
{
    public class PaymentRemindJob : IJob
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(PaymentRemindJob));

        public void Execute(IJobExecutionContext context)
        {
            var list = GetMerchantUserInfos();
            if (!list.Any())
                return;

            _logger.Info(string.Format("共有{0}条缴费提醒需要发送", list.Count));

            list.ForEach(info =>
            {
                //发短信
                if (!string.IsNullOrEmpty(info.Mobile))
                {
                    PaymentNotify content = new PaymentNotify(string.IsNullOrEmpty(info.NickName.Trim()) ? "用户" : info.NickName);

                    SmsMessage sms = new SmsMessage
                    {
                        TemplateCode = PaymentNotify.TemplateCode,
                        SignName = "唐小二",
                        Param = JsonConvert.SerializeObject(content)
                    };

                    MessageDA.Add(new MessageInfo()
                    {
                        MessageType = 2,
                        Priority = 2,
                        Recipient = info.Mobile,
                        Subject = "缴费提醒",
                        Content = JsonConvert.SerializeObject(sms),
                        Link = "",
                        Status = 1 //待发送
                    });
                }

                //唐小二信息
                MerchantMessageDA.Add(new MerchantMessageInfo
                {
                    MerchantId = info.MerchantId,
                    StoreId = info.StoreId,
                    UserId = info.UserId,
                    Subject = "亲，记得缴费哦",
                    Content = string.Format("亲爱的{0}，本月的账单已经生成啦！请在本月20日到财务处缴纳费用，谢谢！如您已经缴费，请忽略本信息。", string.IsNullOrEmpty(info.NickName.Trim()) ? "用户" : info.NickName),
                    SourceType = 1,
                });
            });
        }

        /// <summary>
        /// 获取商户信息
        /// </summary>
        /// <returns></returns>
        public List<MerchantUserInfo> GetMerchantUserInfos()
        {
            var list = new List<MerchantUserInfo>();

            string strCmd = @"Select MU.StoreID
                                    ,MU.MerchantID
		                            ,MU.UserID
		                            ,MU.NickName
                                    ,MU.UserName
		                            ,MU.WechatOpenID
                                    ,MU.Moblie
                              From dbo.Merchant_Store(Nolock) as MS
                              Inner join dbo.Merchant_User(Nolock) as MU ON MS.StoreID=MU.StoreID
                              Where MS.Status = 1 And MU.Status = 1 And MU.RoleID = 1";

            var table = NHH.Framework.Utility.SqlHelper.ExecuteDataTable(strCmd);
            if (table == null || table.Rows == null || table.Rows.Count == 0)
                return list;

            list.AddRange(from DataRow row in table.Rows
                          select new MerchantUserInfo
                          {
                              UserId = ConvertHelper.ToInt(row["UserID"]),
                              MerchantId = ConvertHelper.ToInt(row["MerchantID"]),
                              StoreId = ConvertHelper.ToNullableInt(row["StoreID"]),
                              NickName = ConvertHelper.ToString(row["NickName"]),
                              UserName = ConvertHelper.ToString(row["UserName"]),
                              WechatOpenId = ConvertHelper.ToString(row["WechatOpenID"]),
                              Mobile = ConvertHelper.ToString(row["Moblie"])
                          });
            return list;
        }
    }
}
