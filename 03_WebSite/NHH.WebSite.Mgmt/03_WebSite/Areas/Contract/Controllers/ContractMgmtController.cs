using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Contract;
using NHH.Service.Common.IService;
using NHH.Service.Contract.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Contract.Controllers
{
    /// <summary>
    /// 合同管理
    /// </summary>
    public class ContractMgmtController : NHHController
    {
        /// <summary>
        /// 创建合同
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 铺位信息
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public PartialViewResult UnitInfo(int contractId)
        {
            var unitInfo = GetService<IContractInfoService>().GetContractUnitInfo(contractId);
            return PartialView("ContractMgmt/_PartialUnitInfo", unitInfo);
        }

        /// <summary>
        /// 更新铺位信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult UpdateUnitInfo(ContractUnitInfo info)
        {
            GetService<IContractMgmtService>().UpdateUnitInfo(info);
            return RedirectToAction("UnitInfo", "ContractInfo", new { area = "Contract", contractId = info.ContractID });
        }

        /// <summary>
        /// 合同信息
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public PartialViewResult ContractInfo(int contractId)
        {
            var contractInfo = GetService<IContractInfoService>().GetContractInfo(contractId);
            return PartialView("ContractMgmt/_PartialContractInfo", contractInfo);
        }

        /// <summary>
        /// 更新合同信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult UpdateContractInfo(ContractInfo info)
        {
            GetService<IContractMgmtService>().UpdateContractInfo(info);
            return RedirectToAction("ContractInfo", "ContractInfo", new { area = "Contract", contractId = info.ContractID });
        }

        /// <summary>
        /// 租金
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public PartialViewResult RentPaymentInfo(int contractId)
        {
            var paymentInfo = GetService<IContractInfoService>().GetPaymentTermInfo(contractId, 1);

            var paymentTermsTypeList = GetService<ICommonService>().GetCommonFieldList("PaymentTermsType");
            ViewData["PaymentTermsTypeList"] = new SelectList(paymentTermsTypeList, "FieldValue", "FieldName", paymentInfo.PaymentTermsType);

            return PartialView("ContractMgmt/_PartialRentPaymentInfo", paymentInfo);
        }

        /// <summary>
        /// 更新租金
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult UpdateRentPaymentInfo(PaymentTermInfo info)
        {
            GetService<IContractMgmtService>().UpdateRentPaymentInfo(info);
            return RedirectToAction("RentPaymentInfo", "ContractInfo", new { area = "Contract", contractId = info.ContractID });
        }

        /// <summary>
        /// 物业费
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public PartialViewResult MgmtPaymentInfo(int contractId)
        {
            var paymentInfo = GetService<IContractInfoService>().GetPaymentTermInfo(contractId, 2);
            return PartialView("ContractMgmt/_PartialMgmtPaymentInfo", paymentInfo);
        }

        /// <summary>
        /// 更新物业费
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult UpdateMgmtPaymentInfo(PaymentTermInfo info)
        {
            GetService<IContractMgmtService>().UpdateMgmtPaymentInfo(info);
            return RedirectToAction("MgmtPaymentInfo", "ContractInfo", new { area = "Contract", contractId = info.ContractID });
        }

        /// <summary>
        /// 交付标准
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public PartialViewResult UnitSpec1(int contractId)
        {
            var unitSpec = GetService<IContractInfoService>().GetContractUnitSpec(contractId, 1);
            return PartialView("ContractMgmt/_PartialUnitSpec1", unitSpec);
        }

        /// <summary>
        /// 更新交付标准
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult UpdateUnitSpec1(ContractUnitSpecInfo info)
        {
            info.SpecType = 1;
            GetService<IContractMgmtService>().UpdateUnitSpec(info);
            return RedirectToAction("UnitSpec1", "ContractInfo", new { area = "Contract", contractId = info.ContractID });
        }

        /// <summary>
        /// 商家责任
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public PartialViewResult UnitSpec2(int contractId)
        {
            var unitSpec = GetService<IContractInfoService>().GetContractUnitSpec(contractId, 2);
            return PartialView("ContractMgmt/_PartialUnitSpec2", unitSpec);
        }

        /// <summary>
        /// 更新商家责任
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult UpdateUnitSpec2(ContractUnitSpecInfo info)
        {
            info.SpecType = 2;
            GetService<IContractMgmtService>().UpdateUnitSpec(info);
            return RedirectToAction("UnitSpec2", "ContractInfo", new { area = "Contract", contractId = info.ContractID }); 
        }
    }
}