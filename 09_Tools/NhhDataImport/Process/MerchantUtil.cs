using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 商户
    /// </summary>
    public class MerchantUtil
    {
        /// <summary>
        /// 获取商户ID
        /// </summary>
        /// <param name="merchantName"></param>
        /// <returns></returns>
        public static int GetMerchantId(string merchantName)
        {
            if (string.IsNullOrEmpty(merchantName) || merchantName.Length == 0)
                return 0;

            string strCmd = string.Format(@"SELECT TOP 1 [MerchantID]
                                          FROM [dbo].[Merchant](Nolock)
                                          Where MerchantName='{0}'", merchantName);
            return SqlHelper.ExecuteScalar(strCmd);
        }
    }
}
