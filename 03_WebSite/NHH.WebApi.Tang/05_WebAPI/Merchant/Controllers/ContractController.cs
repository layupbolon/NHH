using NHH.Models.Common;
using NHH.Models.Common.Enum;
using NHH.Models.Contract;
using NHH.Service.Contract.IService;
using NHH.Framework.Web;
using System.Web.Mvc;
using NHH.WebAPI.Merchant.Common;

namespace NHH.WebAPI.Merchant.Controllers
{
    public class ContractController : NHHController
    {
        #region Service
        private IContractInfoService m_service;
        public IContractInfoService Service
        {
            get
            {
                if (m_service == null)
                {
                    m_service = this.GetService<IContractInfoService>();
                }
                return m_service;
            }
        }
        #endregion

        /// <summary>
        /// 获取当前商户租赁合约列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("merchant/contract/list")]
        //[NHHActionLog]
        public JsonResult GetContractList(ContractListQuery query)
        {
            query.MerchantID = NHHAPIContext.Current.MerchantID;
            ContractListModel result = this.Service.GetContractList(query);
            return Json(result);
        }

        /// <summary>
        /// 获取指定的合约详细
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/contract/detail/{contractId}")]
        //[NHHActionLog]
        public JsonResult GetContract(int contractId)
        {
            ContractInfo result = this.Service.GetContractDetail(contractId);
            return Json(result);
        }

        ///// <summary>
        ///// 作废指定商户租赁合约信息（待签约的合同方可作废）
        ///// </summary>
        ///// <param name="contractId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("merchant/contract/cancel/{contractId}")]
        ////[NHHActionLog]
        //public JsonResult CancelContract(int contractId)
        //{
        //    ApiResult result = null;
        //    if (this.Service.CancelContract(contractId))
        //        result = new ApiResult(ApiResultEnum.Success);
        //    else
        //        result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
        //    return Json(result);
        //}

        ///// <summary>
        ///// 添加租赁合约附件
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("merchant/contract/attch/add")]
        ////[NHHActionLog]
        //public JsonResult AddAppendix(ContractAppendixInfo model)
        //{
        //    ApiResult<ContractAppendixInfo> result = null;
        //    #region 验证
        //    if (model.ContractID < 1) { return Json(new ApiResult<ContractAppendixInfo>() { Code = 1000, Desc = "合约编号不能为空！" }); }
        //    if (model.AppendixType < 1) { return Json(new ApiResult<ContractAppendixInfo>() { Code = 1000, Desc = "附件类型不能为空！" }); }
        //    if (model.AppendixTemplate < 1) { return Json(new ApiResult<ContractAppendixInfo>() { Code = 1000, Desc = "附件模板不能为空！" }); }
        //    if (string.IsNullOrEmpty(model.AppendixName)) { return Json(new ApiResult<ContractAppendixInfo>() { Code = 1000, Desc = "附件名称不能为空！" }); }
        //    if (string.IsNullOrEmpty(model.AppendixPath)) { return Json(new ApiResult<ContractAppendixInfo>() { Code = 1000, Desc = "附件文件路径不能为空！" }); }
        //    #endregion 验证
        //    if (this.Service.AddContractAppendix(model))
        //    {
        //        result = new ApiResult<ContractAppendixInfo>(ApiResultEnum.Success);
        //        result.Data = this.Service.GetContractAppendix(model.AppendixID);
        //    }
        //    else
        //        result = new ApiResult<ContractAppendixInfo>(ApiResultEnum.NoUpdateAnyRows);
        //    return Json(result);
        //}

        /// <summary>
        /// 获取指定的租赁合约附件
        /// </summary>
        /// <param name="appendixId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("merchant/contract/attch/detail/{appendixId}")]
        //[NHHActionLog]
        public JsonResult GetAppendix(int appendixId)
        {
            ContractAppendixInfo result = this.Service.GetContractAppendix(appendixId);
            return Json(result);
        }

        ///// <summary>
        ///// 删除指定租赁合约附件（作废）
        ///// </summary>
        ///// <param name="appendixId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("merchant/contract/attch/delete/{appendixId}")]
        ////[NHHActionLog]

        //public JsonResult DeleteAppendix(int appendixId)
        //{
        //    ApiResult result = null;
        //    if (this.Service.CancelContractAppendix(appendixId))
        //        result = new ApiResult(ApiResultEnum.Success);
        //    else
        //        result = new ApiResult(ApiResultEnum.NoUpdateAnyRows);
        //    return Json(result);
        //}
    }
}
