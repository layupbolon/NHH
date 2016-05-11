using Nhh.Jobs.Model;
using NHH.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhh.Jobs.Utility
{
    /// <summary>
    /// 商户用户
    /// </summary>
    public class MerchantUserDA
    {
        /// <summary>
        /// 获取项目下所有的商户用户的微信信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<MerchantUserInfo> GetMerchantUserList(int projectId)
        {
            var merchantUserList = new List<MerchantUserInfo>();
            var strCmd = string.Format(@"SELECT mu.[UserID]
                                               ,mu.[UserName]
                                               ,mu.[MerchantID]
                                               ,mu.[StoreID]
                                               ,mu.[WechatOpenID]
                                               FROM [dbo].[Merchant_User](Nolock) mu
					                           INNER JOIN [dbo].[Contract](Nolock) c on mu.MerchantID=c.MerchantID
					                           WHERE c.ProjectID={0} AND c.ContractStatus=3 AND mu.status=1 And c.status=1", projectId);

            var tabUserList = SqlHelper.ExecuteDataTable(strCmd);
            if (tabUserList.Rows.Count == 0 || tabUserList == null) 
                return null;

            foreach (DataRow row in tabUserList.Rows)
            {
                var info = new MerchantUserInfo();
                info.WechatOpenId = row["WechatOpenID"].ToString();
                info.UserId = ConvertHelper.ToInt(row["UserID"]);
                info.UserName = row["UserName"].ToString();
                info.MerchantId = ConvertHelper.ToInt(row["MerchantID"]);
                info.StoreId = ConvertHelper.ToNullableInt(row["StoreID"]);
                
                merchantUserList.Add(info);
            }
            return merchantUserList;
        }
    }
}
