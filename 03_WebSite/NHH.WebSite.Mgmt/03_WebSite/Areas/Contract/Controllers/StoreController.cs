using NHH.Framework.Web;
using NHH.Models.Merchant;
using NHH.Service.Common.IService;
using NHH.Service.Contract.IService;
using NHH.Service.Merchant.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Contract.Controllers
{
    /// <summary>
    /// 商铺管理
    /// </summary>
    public class StoreController : NHHController
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(MerchantStoreListQueryInfo queryInfo)
        {
            var model = GetService<IMerchantStoreService>().GetMerchantStoreList(queryInfo);
            model.CrumbInfo.AddCrumb("商铺管理");

            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public ActionResult Detail(int storeId)
        {
            var model = GetService<IMerchantStoreService>().GetDetail(storeId);
            model.CrumbInfo.AddCrumb("商铺管理", Url.Action("List", "Store", new { area = "Contract" }));
            model.CrumbInfo.AddCrumb("商铺详情");

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
            return PartialView("Store/_PartialUnitInfo", unitInfo);
        }

        /// <summary>
        /// 合同信息
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public PartialViewResult ContractInfo(int contractId)
        {
            var contractInfo = GetService<IContractInfoService>().GetContractInfo(contractId);
            return PartialView("Store/_PartialContractInfo", contractInfo);
        }
    }
}