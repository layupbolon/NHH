using log4net;
using Nhh.Jobs.Model;
using Nhh.Jobs.Utility;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Merchant
{
    /// <summary>
    /// 日营业额提醒
    /// </summary>
    public sealed class RevenueRemindJob : IJob
    {

        private readonly ILog _logger = LogManager.GetLogger(typeof(RevenueRemindJob));

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            var userList = GetMerchantUserList();
            if (userList == null || userList.Count == 0)
                return;

            _logger.Info(string.Format("共有{0}商铺尚未提交日营业额，将向商铺店长发送消息", userList.Count));

            //发消息
            foreach (var info in userList)
            {
                var message = new MerchantMessageInfo
                {
                    MerchantId = info.MerchantId,
                    StoreId = info.StoreId,
                    UserId = info.UserId,
                    Subject = string.Format("{0} 经营数据尚未提交，请尽快通过唐小二提交", DateTime.Now.ToString("yyyy-MM-dd")),
                    Content = string.Format("亲爱的 {0}，我们发现您有未提交的经营数据，请尽快通过唐小二提交，谢谢！<a href='/my/operatedata/new'>提交入口</a>", info.NickName),
                    SourceType = 1,
                };

                MerchantMessageDA.Add(message);
            }
        }

        /// <summary>
        /// 获取尚未提交日营业额数据的商铺用户列表
        /// </summary>
        /// <returns></returns>
        private List<MerchantUserInfo> GetMerchantUserList()
        {
            var list = new List<MerchantUserInfo>();
            string strCmd = @"Select MU.StoreID,MU.MerchantID
		                            ,MU.UserID
		                            ,MU.NickName
                                    ,MU.UserName
		                            ,MU.WechatOpenID
                              From dbo.Merchant_Store(Nolock) as MS
                              Inner join dbo.Merchant_User(Nolock) as MU ON MS.StoreID=MU.StoreID
                              Where MS.Status = 1 And Not Exists (Select Top 1 1 From dbo.Merchant_Revenue(Nolock) as MR 
                                                                  Where MR.Status=1 And DATEDIFF(DAY,StartDate,GETDATE())=0 And MS.StoreID=MR.StoreID)";

            var table = NHH.Framework.Utility.SqlHelper.ExecuteDataTable(strCmd);
            if (table == null || table.Rows == null || table.Rows.Count == 0)
                return list;

            foreach (DataRow row in table.Rows)
            {
                var info = new MerchantUserInfo
                {
                    UserId = ConvertHelper.ToInt(row["UserID"]),
                    MerchantId = ConvertHelper.ToInt(row["MerchantID"]),
                    StoreId = ConvertHelper.ToNullableInt(row["StoreID"]),
                    NickName = ConvertHelper.ToString(row["NickName"]),
                    UserName = ConvertHelper.ToString(row["UserName"]),
                    WechatOpenId = ConvertHelper.ToString(row["WechatOpenID"])
                };
                list.Add(info);
            }
            return list;
        }
    }
}
