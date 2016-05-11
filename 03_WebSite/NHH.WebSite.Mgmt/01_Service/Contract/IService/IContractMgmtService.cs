using NHH.Models.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Contract.IService
{
    /// <summary>
    /// 合同管理服务接口
    /// </summary>
    public interface IContractMgmtService
    {
        /// <summary>
        /// 更新铺位信息
        /// </summary>
        /// <param name="info"></param>
        void UpdateUnitInfo(ContractUnitInfo info);

        /// <summary>
        /// 更新合同信息
        /// </summary>
        /// <param name="info"></param>
        void UpdateContractInfo(ContractInfo info);

        /// <summary>
        /// 更新合同的交付标准和商户责任
        /// </summary>
        /// <param name="info"></param>
        void UpdateUnitSpec(ContractUnitSpecInfo info);

        /// <summary>
        /// 更新租金信息
        /// </summary>
        /// <param name="info"></param>
        void UpdateRentPaymentInfo(PaymentTermInfo info);

        /// <summary>
        /// 更新物业信息
        /// </summary>
        /// <param name="info"></param>
        void UpdateMgmtPaymentInfo(PaymentTermInfo info);
    }
}
