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
        ContractListModel GetContractList(ContractListQuery queryInfo);

        /// <summary>
        /// 获取合同详情
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        ContractInfo GetContractDetail(int contractId);

        ///// <summary>
        ///// 作废指定合约（合约状态必须为1【待签约】才可作废）
        ///// </summary>
        ///// <param name="contractId"></param>
        ///// <returns></returns>
        //bool CancelContract(int contractId);

        ///// <summary>
        ///// 添加租赁合约附件
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //bool AddContractAppendix(ContractAppendixInfo model);

        /// <summary>
        /// 查询指定的租赁合约附件
        /// </summary>
        /// <param name="appendixId"></param>
        /// <returns></returns>
        ContractAppendixInfo GetContractAppendix(int appendixId);

        ///// <summary>
        ///// 作废租赁合约附件
        ///// </summary>
        ///// <param name="appendixId"></param>
        ///// <returns></returns>
        //bool CancelContractAppendix(int appendixId);
    }
}
