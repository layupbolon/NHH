using NHH.Models.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Contract.IService
{
    /// <summary>
    /// 合同信息服务接口
    /// </summary>
    public interface IContractInfoService
    {
        /// <summary>
        /// 获取合同列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        ContractListModel GetContractList(ContractListQueryInfo queryInfo);

        /// <summary>
        /// 获取合同详情
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        ContractDetailModel GetContractDetail(int contractId);

        /// <summary>
        /// 获取合同铺位信息
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        ContractUnitInfo GetContractUnitInfo(int contractId);

        /// <summary>
        /// 获取合同信息
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        ContractInfo GetContractInfo(int contractId);

        /// <summary>
        /// 获取合同支付条款
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="paymentItemId"></param>
        /// <returns></returns>
        PaymentTermInfo GetPaymentTermInfo(int contractId, int paymentItemId);

        /// <summary>
        /// 获取合同的交付标准和商户责任
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="specType"></param>
        /// <returns></returns>
        ContractUnitSpecInfo GetContractUnitSpec(int contractId, int specType);

    }
}
