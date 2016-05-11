using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    public class CommonToken
    {
        public string Token { get; set; }

        public MerchantUserInfo UserInfo { get; set; }
    }

    public class CommonWechatAccess
    {
        public string Token { get; set; }

        public string OpenID { get; set; }
    }
}
