using NHH.Models.Common;
using NHH.Models.Merchant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 租约详情
    /// </summary>
    public class ContractDetailModel : ContractInfo
    {
        private CrumbInfo _crumbInfo = new CrumbInfo();

        /// <summary>
        /// 面包屑信息
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get { return _crumbInfo; }
        }

        /// <summary>
        /// 品牌信息
        /// </summary>
        public List<MerchantBrandInfo> BrandList { get; set; }

        /// <summary>
        /// 商户信息
        /// </summary>
        public MerchantInfo MerchantInfo { get; set; }

        /// <summary>
        /// 交付标准
        /// </summary>
        public ContractUnitSpecInfo UnitSpec1 { get; set; }

        /// <summary>
        /// 商户责任
        /// </summary>
        public ContractUnitSpecInfo UnitSpec2 { get; set; }

        /// <summary>
        /// 租金支付条款
        /// </summary>
        public PaymentTermInfo RentPaymentTerm { get; set; }
        
        /// <summary>
        /// 物业费支付条款
        /// </summary>
        public PaymentTermInfo MgmtPaymentTerm { get; set; }        

        /// <summary>
        /// 合同附件
        /// </summary>
        public List<AppendixInfo> AppendixList { get; set; }

        /// <summary>
        /// 补充协议
        /// </summary>
        public List<SupplementaryInfo> SupplementaryList { get; set; }
    }
}
