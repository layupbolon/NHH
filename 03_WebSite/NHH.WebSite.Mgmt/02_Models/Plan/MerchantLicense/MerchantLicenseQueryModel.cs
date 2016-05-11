using NHH.Models.Common;

namespace NHH.Models.Plan.MerchantLicense
{
    public class MerchantLicenseQueryModel : QueryInfo
    {
        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName { get; set; }

        //默认显示已审核
        private int? _status = 1;

        /// <summary>
        /// 状态
        /// -1作废 1待审核 2审核未通过 3已审核
        /// </summary>
        public int? Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}
