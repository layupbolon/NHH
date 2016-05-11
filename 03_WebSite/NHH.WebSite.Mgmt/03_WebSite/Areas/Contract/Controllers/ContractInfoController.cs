using NHH.Framework.Web;
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
    /// 租约信息查询
    /// </summary>
    public class ContractInfoController : NHHController
    {
        private IContractInfoService service;
        private ICommonService commonService;
        private IProjectCommonService projectService;

        public ContractInfoController()
        {
            service = GetService<IContractInfoService>();
            commonService = GetService<ICommonService>();
            projectService = GetService<IProjectCommonService>();
        }

        /// <summary>
        /// 租约列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(ContractListQueryInfo queryInfo)
        {
            var model = service.GetContractList(queryInfo);
            model.CrumbInfo.AddCrumb("合同管理");

            var projectList = projectService.GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);

            var contractStatusList = commonService.GetContractStatusList();
            ViewData["ContractStatusList"] = new SelectList(contractStatusList, "Id", "Name", queryInfo.Status.HasValue ? queryInfo.Status.Value : 1);

            return View(model);
        }

        /// <summary>
        /// 合同详情页
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public ActionResult Detail(int contractId)
        {
            var model = service.GetContractDetail(contractId);
            model.CrumbInfo.AddCrumb("合同管理", Url.Action("List", "ContractInfo", new { area = "Contract" }));
            model.CrumbInfo.AddCrumb("合同详情");

            return View(model);
        }

        /// <summary>
        /// 铺位信息
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public PartialViewResult UnitInfo(int contractId)
        {
            var unitInfo = GetService<IContractInfoService>().GetContractUnitInfo(contractId);
            return PartialView("ContractInfo/_PartialUnitInfo", unitInfo);
        }

        /// <summary>
        /// 租约信息
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public PartialViewResult ContractInfo(int contractId)
        {
            var contractInfo = GetService<IContractInfoService>().GetContractInfo(contractId);
            return PartialView("ContractInfo/_PartialContractInfo", contractInfo);
        }

        /// <summary>
        /// 租金
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public PartialViewResult RentPaymentInfo(int contractId)
        {
            var paymentInfo = GetService<IContractInfoService>().GetPaymentTermInfo(contractId, 1);
            return PartialView("ContractInfo/_PartialRentPaymentInfo", paymentInfo);
        }

        /// <summary>
        /// 物业费
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public PartialViewResult MgmtPaymentInfo(int contractId)
        {
            var paymentInfo = GetService<IContractInfoService>().GetPaymentTermInfo(contractId, 2);
            return PartialView("ContractInfo/_PartialMgmtPaymentInfo", paymentInfo);
        }

        /// <summary>
        /// 交付标准
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public PartialViewResult UnitSpec1(int contractId)
        {
            var unitSpec = GetService<IContractInfoService>().GetContractUnitSpec(contractId, 1);
            return PartialView("ContractInfo/_PartialUnitSpec1", unitSpec);
        }

        /// <summary>
        /// 商家责任
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public PartialViewResult UnitSpec2(int contractId)
        {
            var unitSpec = GetService<IContractInfoService>().GetContractUnitSpec(contractId, 2);
            return PartialView("ContractInfo/_PartialUnitSpec2", unitSpec);
        }
    }
}