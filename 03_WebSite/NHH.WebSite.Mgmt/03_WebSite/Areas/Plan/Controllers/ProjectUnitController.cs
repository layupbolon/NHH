using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Plan;
using NHH.Service.Common.IService;
using NHH.Service.Plan.IService;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Plan.Controllers
{
    /// <summary>
    /// 商铺租决规划
    /// </summary>
    public class ProjectUnitController : NHHController
    {
        private IProjectPlanService iService;

        public ProjectUnitController()
        {
            iService = GetService<IProjectPlanService>();
        }

        /// <summary>
        /// 商铺规划信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ActionResult PlanInfo(int unitId)
        {
            var model = new ProjectPlanInfoModel();
            model.CrumbInfo.AddCrumb("招商筹划", Url.Action("List", "ProjectPlan", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("铺位规划信息");

            model.UnitId = unitId;
            model.UnitInfo = iService.GetUnitInfo(unitId);
            return View(model);
        }

        #region 租决规划
        /// <summary>
        /// 租赁信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public PartialViewResult UnitPlan(int unitId)
        {
            var model = iService.GetUnitPlan(unitId);

            return PartialView("ProjectPlan/_PartialUnitPlan", model);
        }

        /// <summary>
        /// 编辑租赁规划
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public PartialViewResult EditUnitPlan(int unitId)
        {
            var model = iService.GetUnitPlan(unitId);
            var commonService = GetService<ICommonService>();

            var unitTypeList = commonService.GetUnitTypeList();
            ViewData["UnitTypeList"] = new SelectList(unitTypeList, "Id", "Name", model.UnitTypeId);

            var bizTypeList = commonService.GetBizTypeList();
            ViewData["BizTypeList"] = new SelectList(bizTypeList, "Id", "Name", model.BizTypeId);

            var companyService = GetService<ICompanyCommonService>();
            var companyList = companyService.GetCompanyList();
            ViewData["CompanyList"] = new SelectList(companyList, "Id", "ShortName", model.CompanyId);

            var departmentList = companyService.GetDepartmentList(model.CompanyId);
            ViewData["DepartmentList"] = new SelectList(departmentList, "DepartmentID", "DepartmentName", model.DepartmentId);

            return PartialView("ProjectPlan/_PartialEditUnitPlan", model);
        }

        /// <summary>
        /// 编辑租赁规划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUnitPlan(ProjectUnitPlanModel model)
        {
            model.UserId = Context.UserID;
            iService.SaveUnitPlan(model);
            return RedirectToAction("UnitPlan", "ProjectUnit", new { area = "Plan", unitId = model.UnitId });
        }        
        #endregion

        #region 租赁信息
        /// <summary>
        /// 租赁信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public PartialViewResult RentInfo(int unitId)
        {
            var model = iService.GetRentInfo(unitId);

            model.RentChargeMode = string.Format("押 {0} 付 {1}", model.DepositMonthly, model.PaymentPeriod);
            model.IncreaseTypeString = string.Format("自第 {0} 年起，每 {1} 年", model.IncreaseStartYears, model.IncreaseStepYears);
            if (model.IncreaseType == 1)
            {
                model.IncreaseTypeString += "递增";
            }
            else if (model.IncreaseType == 2)
            {
                model.IncreaseTypeString += "递减";
            }
            model.IncreaseTypeString += model.IncreaseAmount.ToString();
            if (model.IncreaseAmountType == 1)
            {
                model.IncreaseTypeString += "%";
            }
            else if (model.IncreaseAmountType == 2)
            {
                model.IncreaseTypeString += "‰";
            }
            else if (model.IncreaseAmountType==3)
            {
                model.IncreaseTypeString += "元";
            }

            return PartialView("ProjectPlan/_PartialRentInfo", model);
        }

        /// <summary>
        /// 编辑租赁信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public PartialViewResult EditRentInfo(int unitId)
        {
            var model = iService.GetRentInfo(unitId);

            var commonService = GetService<ICommonService>();

            //押金帐期
            var depositType = new List<SelectListItemInfo>();
            depositType.Add(new SelectListItemInfo { Text = "押一付一", Value = "1.1" });
            depositType.Add(new SelectListItemInfo { Text = "押一付三", Value = "1.3" });
            depositType.Add(new SelectListItemInfo { Text = "押二付三", Value = "2.3" });
            depositType.Add(new SelectListItemInfo { Text = "押二付六", Value = "2.6" });
            model.RentChargeMode = string.Format("{0}.{1}", model.DepositMonthly, model.PaymentPeriod);
            ViewData["RentChargeModeList"] = new SelectList(depositType, "Value", "Text", model.RentChargeMode);

            //租金递增方式
            var increaseStartYears = commonService.GetSelectItemList(1, 4);
            ViewData["IncreaseStartYearsList"] = new SelectList(increaseStartYears, "Value", "Text", model.IncreaseStartYears);

            var increaseStepYears = commonService.GetSelectItemList(1, 3);
            ViewData["IncreaseStepYearsList"] = new SelectList(increaseStepYears, "Value", "Text", model.IncreaseStepYears);

            var increaseTypeList = new List<SelectListItemInfo>();
            increaseTypeList.Add(new SelectListItemInfo { Text = "递增", Value = "1" });
            increaseTypeList.Add(new SelectListItemInfo { Text = "递减", Value = "2" });
            ViewData["IncreaseTypeList"] = new SelectList(increaseTypeList, "Value", "Text", model.IncreaseType);

            var increaseAmountType = new List<SelectListItemInfo>();
            increaseAmountType.Add(new SelectListItemInfo { Text = "%", Value = "1" });
            increaseAmountType.Add(new SelectListItemInfo { Text = "‰", Value = "2" });
            increaseAmountType.Add(new SelectListItemInfo { Text = "元", Value = "3" });
            ViewData["IncreaseAmountTypeList"] = new SelectList(increaseAmountType, "Value", "Text", model.IncreaseAmountType);
            
            //免租上限
            var rentFreeLength = commonService.GetSelectItemList(1, 6);
            ViewData["RentFreeLengthList"] = new SelectList(rentFreeLength, "Value", "Text", model.RentFreeLength);
            //装修期上限
            var decorationLength = commonService.GetSelectItemList(1, 6);
            ViewData["DecorationLengthList"] = new SelectList(decorationLength, "Value", "Text", model.DecorationLength);
            //停车位及广告位
            var numList = commonService.GetSelectItemList(0, 3);
            ViewData["ParkingLotNumList"] = new SelectList(numList, "Value", "Text", model.ParkingLotNum);
            ViewData["AdPointNumList"] = new SelectList(numList, "Value", "Text", model.AdPointNum);

            return PartialView("ProjectPlan/_PartialEditRentInfo", model);
        }
        
        /// <summary>
        /// 编辑租赁信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditRentInfo(ProjectUnitInfoModel model)
        {
            var rentChargeModel = decimal.Parse(model.RentChargeMode);
            model.DepositMonthly = (int)(decimal.Floor(rentChargeModel));
            model.PaymentPeriod = (int)((rentChargeModel - (decimal)model.DepositMonthly) * 10);
            model.UserId = Context.UserID;
            iService.SaveRentInfo(model);
            return RedirectToAction("RentInfo", "ProjectUnit", new { area = "Plan", unitId = model.UnitId });
        }
        #endregion

        #region 招商信息
        /// <summary>
        /// 招商信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public PartialViewResult UnitLeasing(int unitId)
        {
            var model = iService.GetUnitLeasing(unitId);

            return PartialView("ProjectPlan/_PartialUnitLeasing", model);
        }

        /// <summary>
        /// 编辑招商信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public PartialViewResult EditUnitLeasing(int unitId)
        {
            var model = iService.GetUnitLeasing(unitId);

            var companyService = GetService<ICompanyCommonService>();
            var employeeList = companyService.GetEmployeeList(model.LeasingDepartmentID);
            ViewData["EmployeeList"] = new SelectList(employeeList, "EmployeeId", "EmployeeName", model.LeasingPersonID);

            return PartialView("ProjectPlan/_PartialEditUnitLeasing", model);
        }

        /// <summary>
        /// 编辑招商信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUnitLeasing(ProjectUnitLeasingModel model)
        {
            model.UserId = Context.UserID;
            iService.SaveUnitLeasing(model);
            return RedirectToAction("UnitLeasing", "ProjectUnit", new { area = "Plan", unitId = model.UnitId });
        }

        #endregion

        #region 交付条件与标准
        /// <summary>
        /// 交付条件与标准
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public PartialViewResult UnitSpec(int unitId)
        {
            var model = iService.GetUnitSpec(unitId);

            return PartialView("ProjectPlan/_PartialUnitSpec", model);
        }

        /// <summary>
        /// 编辑交付条件与标准
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public PartialViewResult EditUnitSpec(int unitId)
        {
            var model = iService.GetUnitSpec(unitId);

            var templateList = iService.GetProjectUnitSpecTemplateList();
            ViewData["TemplateList"] = new SelectList(templateList, "TemplateId", "TemplateName");

            return PartialView("ProjectPlan/_PartialEditUnitSpec", model);
        }

        /// <summary>
        /// 编辑交付条件与标准
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUnitSpec(ProjectUnitSpecModel model)
        {
            model.UserId = Context.UserID;
            iService.SaveUnitSpec(model);

            return RedirectToAction("UnitSpec", "ProjectUnit", new { area = "Plan", unitId = model.UnitId });
        }
        #endregion

        #region 商户责任
        /// <summary>
        /// 商户责任
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public PartialViewResult StoreSpec(int unitId)
        {
            var model = iService.GetStoreSpec(unitId);

            return PartialView("ProjectPlan/_PartialStoreSpec", model);
        }

        /// <summary>
        /// 编辑商户责任
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public PartialViewResult EditStoreSpec(int unitId)
        {
            var model = iService.GetStoreSpec(unitId);

            var templateList = iService.GetProjectUnitSpecTemplateList();
            ViewData["TemplateList"] = new SelectList(templateList, "TemplateId", "TemplateName");

            return PartialView("ProjectPlan/_PartialEditStoreSpec", model);
        }
        
        /// <summary>
        /// 编辑商户责任
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditStoreSpec(ProjectStoreSpecModel model)
        {
            model.UserId = Context.UserID;
            iService.SaveStoreSpec(model);
            return RedirectToAction("StoreSpec", "ProjectUnit", new { area = "Plan", unitId = model.UnitId });
        }

        #endregion        
    }
}
