using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Merchant
{
    /// <summary>
    /// 商户用户详细信息
    /// </summary>
    public class MerchantUserDetailModel : MerchantUserInfo
    {

        private CrumbInfo _crumbInfo = new CrumbInfo { };
        public bool JobFlag { get; set; }
        public int JobStatus { get; set; }
        /// <summary>
        /// 面包屑信息
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get
            { return _crumbInfo; }
        }

        [Required(ErrorMessage="请填写用户密码")]
        public string PasswordMerchant { get; set; }
        public DateTime InDate { get; set; }
        public int InUser { get; set; }
        public DateTime EditDate { get; set; }
        public int EditUser { get; set; }
        public List<MerchantUserPaperInfo> MerchantUserPaperInfoList { get; set; }
    }
}
