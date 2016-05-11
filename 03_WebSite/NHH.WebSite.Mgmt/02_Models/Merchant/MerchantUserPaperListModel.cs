using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户用户证件列表
    /// </summary>
    public class MerchantUserPaperListModel : BaseModel
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 证件列表
        /// </summary>
        public List<MerchantUserPaperInfo> PaperList { get; set; }
    }
}
