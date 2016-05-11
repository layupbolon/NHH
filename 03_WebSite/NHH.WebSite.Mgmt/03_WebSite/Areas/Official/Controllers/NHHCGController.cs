using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Official.ADPosition;
using NHH.Models.Official.Common;
using NHH.Models.Official.Feedback;
using NHH.Models.Official.Kiosk;
using NHH.Models.Official.Project;
using NHH.Models.Official.Unit;
using NHH.Service.Common.IService;
using NHH.Service.Official.IService;
using NPOI.SS.Formula.Functions;

namespace NHH.WebSite.Management.Areas.Official.Controllers
{
    
    public class NHHCGController : NHHController
    {
        private readonly INHHCGService service;

        List<SelectListItemInfo> statusList = new List<SelectListItemInfo>();
        public NHHCGController()
        {
            service = GetService<INHHCGService>();

            statusList.Add(new SelectListItemInfo { Text = "待出租", Value = "1" });
            statusList.Add(new SelectListItemInfo { Text = "意向洽谈中", Value = "2" });
            statusList.Add(new SelectListItemInfo { Text = "已出租", Value = "3" });
        }

        #region 项目

        private string Crumb_ProjectMgmt = "官网项目管理";
        private string Crumb_ProjectAdd = "创建官网项目";
        private string Crumb_ProjectEdit = "编辑官网项目";
        private string Crumb_ProjectView = "查看官网项目";
        /// <summary>
        /// 官网项目列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult ProjectList(NHHCGProjectQueryModel queryInfo)
        {
            var model = service.GetNHHCGProjectList(queryInfo);
            model.CrumbInfo.AddCrumb(Crumb_ProjectMgmt);

            return View(model);
        }
        /// <summary>
        /// 官网项目添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddProject()
        {
            var model = new NHHCGProjectModel();
            model.CrumbInfo.AddCrumb(Crumb_ProjectMgmt, Url.Action("ProjectList", "NHHCG", new { area = "Official" }));
            model.CrumbInfo.AddCrumb(Crumb_ProjectAdd);

            var businessTypeList = GetService<ICommonService>().GetCommonFieldList("NHHCG_BusinessType");
            ViewData["BusinessTypeList"] = new SelectList(businessTypeList, "FieldValue", "FieldName");

            return View(model);
        }
        /// <summary>
        /// 官网项目新增操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProject(NHHCGProjectModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb(Crumb_ProjectMgmt, Url.Action("ProjectList", "NHHCG", new { area = "Official" }));
                model.CrumbInfo.AddCrumb(Crumb_ProjectAdd);

                var businessTypeList = GetService<ICommonService>().GetCommonFieldList("NHHCG_BusinessType");
                ViewData["BusinessTypeList"] = new SelectList(businessTypeList, "FieldValue", "FieldName");

                return View(model);
            }

            service.AddNHHCGProject(model);
            return RedirectToAction("EditProject", "NHHCG", new { area = "Official", model.ProjectID });
        }
        /// <summary>
        /// 编辑项目页面
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult EditProject(int projectId)
        {
            var model = service.GetNHHCGProject(projectId);
            model.CrumbInfo.AddCrumb(Crumb_ProjectMgmt, Url.Action("ProjectList", "NHHCG", new { area = "Official" }));
            model.CrumbInfo.AddCrumb(Crumb_ProjectEdit);

            var businessTypeList = GetService<ICommonService>().GetCommonFieldList("NHHCG_BusinessType");
            ViewData["BusinessTypeList"] = new SelectList(businessTypeList, "FieldValue", "FieldName");

            return View(model);
        }

        public ActionResult ViewProject(int projectId)
        {
            var model = service.GetNHHCGProject(projectId);
            model.CrumbInfo.AddCrumb(Crumb_ProjectMgmt, Url.Action("ProjectList", "NHHCG", new { area = "Official" }));
            model.CrumbInfo.AddCrumb(Crumb_ProjectView);
            return View(model);
        }
        /// <summary>
        /// 编辑项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProject(NHHCGProjectModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb(Crumb_ProjectMgmt, Url.Action("ProjectList", "NHHCG", new { area = "Official" }));
                model.CrumbInfo.AddCrumb(Crumb_ProjectEdit);

                var businessTypeList = GetService<ICommonService>().GetCommonFieldList("NHHCG_BusinessType");
                ViewData["BusinessTypeList"] = new SelectList(businessTypeList, "FieldValue", "FieldName");

                return View(model);
            }

            var success = service.UpdateNHHCGProject(model);
            return RedirectToAction("ProjectList", "NHHCG", new { area = "Official" });
        }

        /// <summary>
        /// 获取官网项目列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public JsonResult GetProjectList()
        {
            var projectList = service.GetProjectList();
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        #endregion 项目

        #region 铺位

        private string Crumb_UnitMgmt = "官网铺位管理";
        private string Crumb_UnitAdd = "创建官网铺位";
        private string Crumb_UnitEdit = "编辑官网铺位";
        private string Crumb_UnitView = "查看官网铺位";

        /// <summary>
        /// 官网铺位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult UnitList(NHHCGUnitQueryModel queryInfo)
        {
            var model = service.GetNHHCGUnitList(queryInfo);
            model.CrumbInfo.AddCrumb(Crumb_UnitMgmt);
            
            ViewData["StatusList"] = new SelectList(statusList, "Value", "Text", queryInfo.Status ?? 0);

            var bizTypeList = service.GetNHHCGBizTypeDropDownList();
            ViewData["BizTypeList"] = new SelectList(bizTypeList, "Value", "Text", queryInfo.BizType ?? 0);

            var buildingList = service.GetNGGCGBuildingDropDownList(NHHCGTypeEnum.Unit);
            ViewData["BuildingList"] = new SelectList(buildingList, "Value", "Text", queryInfo.Building);

            var areaScopeList = GetService<ICommonService>().GetCommonFieldList("UnitAreaScope");
            ViewData["UnitAreaScopeList"] = new SelectList(areaScopeList, "FieldValue", "FieldName", queryInfo.AreaScope ?? 0);

            var projectList =service.GetNHHCGProjectDropDownList();
            ViewData["ProjectList"] = new SelectList(projectList, "Value", "Text", queryInfo.ProjectID ?? 0);

            return View(model);
        }
        /// <summary>
        /// 官网铺位添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUnit()
        {
            var model = new NHHCGUnitModel();
            model.CrumbInfo.AddCrumb(Crumb_UnitMgmt, Url.Action("UnitList", "NHHCG", new { area = "Official" }));
            model.CrumbInfo.AddCrumb(Crumb_UnitAdd);
            
            var bizTypeList = service.GetNHHCGBizTypeDropDownList();
            ViewData["BizTypeList"] = new SelectList(bizTypeList, "Value", "Text");

            var floorList = service.GetNHHCGFloorDropDownList(NHHCGTypeEnum.Unit);
            ViewData["FloorList"] = new SelectList(floorList, "Value", "Text");

            var projectList = service.GetNHHCGProjectDropDownList();
            ViewData["ProjectList"] = new SelectList(projectList, "Value", "Text");

            return View(model);
        }
        /// <summary>
        /// 官网铺位新增操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddUnit(NHHCGUnitModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb(Crumb_UnitMgmt, Url.Action("UnitList", "NHHCG", new { area = "Official" }));
                model.CrumbInfo.AddCrumb(Crumb_UnitAdd);
                
                var bizTypeList = service.GetNHHCGBizTypeDropDownList();
                ViewData["BizTypeList"] = new SelectList(bizTypeList, "Value", "Text");
                
                var projectList = service.GetNHHCGProjectDropDownList();
                ViewData["ProjectList"] = new SelectList(projectList, "Value", "Text");

                return View(model);
            }
            model.BizTypes = string.Join(",", model.BizTypeList);
            service.AddNHHCGUnit(model);
            return RedirectToAction("EditUnit", "NHHCG", new { area = "Official" ,model.UnitID});
        }
        /// <summary>
        /// 编辑铺位页面
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ActionResult EditUnit(int unitId)
        {
            var model = service.GetNHHCGUnit(unitId);
            model.CrumbInfo.AddCrumb(Crumb_UnitMgmt, Url.Action("UnitList", "NHHCG", new { area = "Official" }));
            model.CrumbInfo.AddCrumb(Crumb_UnitEdit);

            var bizTypeList = service.GetNHHCGBizTypeDropDownList();
            ViewData["BizTypeList"] = new SelectList(bizTypeList, "Value", "Text");

            var floorList = service.GetNHHCGFloorDropDownList(NHHCGTypeEnum.Unit);
            ViewData["FloorList"] = new SelectList(floorList, "Value", "Text");

            var projectList = service.GetNHHCGProjectDropDownList();
            ViewData["ProjectList"] = new SelectList(projectList, "Value", "Text");

            ViewData["StatusList"] = new SelectList(statusList, "Value", "Text");

            return View(model);
        }

        public ActionResult ViewUnit(int unitId)
        {
            var model = service.GetNHHCGUnit(unitId);
            model.CrumbInfo.AddCrumb(Crumb_UnitMgmt, Url.Action("UnitList", "NHHCG", new { area = "Official" }));
            model.CrumbInfo.AddCrumb(Crumb_UnitView);
            return View(model);
        }
        /// <summary>
        /// 编辑铺位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditUnit(NHHCGUnitModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb(Crumb_UnitMgmt, Url.Action("UnitList", "NHHCG", new { area = "Official" }));
                model.CrumbInfo.AddCrumb(Crumb_UnitEdit);

                var bizTypeList = service.GetNHHCGBizTypeDropDownList();
                ViewData["BizTypeList"] = new SelectList(bizTypeList, "Value", "Text");

                var projectList = service.GetNHHCGProjectDropDownList();
                ViewData["ProjectList"] = new SelectList(projectList, "Value", "Text");

                ViewData["StatusList"] = new SelectList(statusList, "Value", "Text");

                return View(model);
            }
            model.BizTypes = string.Join(",", model.BizTypeList);
            var success = service.UpdateNHHCGUnit(model);
            return RedirectToAction("UnitList", "NHHCG", new { area = "Official" });
        }
        #endregion 铺位

        #region 多经点位

        private string Crumb_KioskMgmt = "官网多经点位管理";
        private string Crumb_KioskAdd = "创建官网多经点位";
        private string Crumb_KioskEdit = "编辑官网多经点位";
        private string Crumb_KioskView = "查看官网多经点位";
        /// <summary>
        /// 官网多经点位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult KioskList(NHHCGKioskQueryModel queryInfo)
        {
            var model = service.GetNHHCGKioskList(queryInfo);
            model.CrumbInfo.AddCrumb(Crumb_KioskMgmt);

            ViewData["StatusList"] = new SelectList(statusList, "Value", "Text", queryInfo.Status ?? 0);

            var buildingList = service.GetNGGCGBuildingDropDownList(NHHCGTypeEnum.Kiosk);
            ViewData["BuildingList"] = new SelectList(buildingList, "Value", "Text", queryInfo.Building);

            var regionList = GetService<ICommonService>().GetCommonFieldList("NHHCG_Region");
            ViewData["RegionList"] = new SelectList(regionList, "FieldValue", "FieldName", queryInfo.Region ?? 0);
            
            var areaScopeList = GetService<ICommonService>().GetCommonFieldList("KioskAreaScope");
            ViewData["KioskAreaScopeList"] = new SelectList(areaScopeList, "FieldValue", "FieldName", queryInfo.AreaScope ?? 0);

            var floorList = service.GetNHHCGFloorDropDownList(NHHCGTypeEnum.Kiosk);
            ViewData["FloorList"] = new SelectList(floorList, "Value", "Text", queryInfo.FloorID ?? 0);

            var projectList = service.GetNHHCGProjectDropDownList();
            ViewData["ProjectList"] = new SelectList(projectList, "Value", "Text", queryInfo.ProjectID ?? 0);

            return View(model);
        }
        /// <summary>
        /// 官网多经点位添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddKiosk()
        {
            var model = new NHHCGKioskModel();
            model.CrumbInfo.AddCrumb(Crumb_KioskMgmt, Url.Action("KioskList", "NHHCG", new { area = "Official" }));
            model.CrumbInfo.AddCrumb(Crumb_KioskAdd);

            var commonService = GetService<ICommonService>();
            
            var regionList = GetService<ICommonService>().GetCommonFieldList("NHHCG_Region");
            ViewData["RegionList"] = new SelectList(regionList, "FieldValue", "FieldName");

            var businessScopeList = GetService<ICommonService>().GetCommonFieldList("BusinessScope");
            ViewData["BusinessScopeList"] = new SelectList(businessScopeList, "FieldValue", "FieldName");

            var areaScopeList = GetService<ICommonService>().GetCommonFieldList("KioskAreaScope");
            ViewData["KioskAreaScopeList"] = new SelectList(areaScopeList, "FieldValue", "FieldName");

            var projectList = service.GetNHHCGProjectDropDownList();
            ViewData["ProjectList"] = new SelectList(projectList, "Value", "Text");

            return View(model);
        }
        /// <summary>
        /// 官网多经点位新增操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddKiosk(NHHCGKioskModel model)
        {
            var areaScopeList = GetService<ICommonService>().GetCommonFieldList("KioskAreaScope");
            ViewData["KioskAreaScopeList"] = new SelectList(areaScopeList, "FieldValue", "FieldName");

            model.BusinessScopes = string.Join(",", model.BusinessScopeList);
            service.AddNHHCGKiosk(model);
            return RedirectToAction("EditKiosk", "NHHCG", new { area = "Official", model.KioskID });
        }
        /// <summary>
        /// 编辑多经点位页面
        /// </summary>
        /// <param name="kioskId"></param>
        /// <returns></returns>
        public ActionResult EditKiosk(int kioskId)
        {
            var model = service.GetNHHCGKiosk(kioskId);
            model.CrumbInfo.AddCrumb(Crumb_KioskMgmt, Url.Action("KioskList", "NHHCG", new { area = "Official" }));
            model.CrumbInfo.AddCrumb(Crumb_KioskEdit);

            var businessScopeList = GetService<ICommonService>().GetCommonFieldList("BusinessScope");
            ViewData["BusinessScopeList"] = new SelectList(businessScopeList, "FieldValue", "FieldName");
            
            var regionList = GetService<ICommonService>().GetCommonFieldList("NHHCG_Region");
            ViewData["RegionList"] = new SelectList(regionList, "FieldValue", "FieldName");

            var areaScopeList = GetService<ICommonService>().GetCommonFieldList("KioskAreaScope");
            ViewData["KioskAreaScopeList"] = new SelectList(areaScopeList, "FieldValue", "FieldName");

            var projectList = service.GetNHHCGProjectDropDownList();
            ViewData["ProjectList"] = new SelectList(projectList, "Value", "Text");

            ViewData["StatusList"] = new SelectList(statusList, "Value", "Text");

            return View(model);
        }

        public ActionResult ViewKiosk(int kioskId)
        {
            var model = service.GetNHHCGKiosk(kioskId);
            model.CrumbInfo.AddCrumb(Crumb_KioskMgmt, Url.Action("KioskList", "NHHCG", new { area = "Official" }));
            model.CrumbInfo.AddCrumb(Crumb_KioskView);
            return View(model);
        }
        /// <summary>
        /// 编辑多经点位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditKiosk(NHHCGKioskModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb(Crumb_KioskMgmt, Url.Action("KioskList", "NHHCG", new { area = "Official" }));
                model.CrumbInfo.AddCrumb(Crumb_KioskEdit);

                var businessScopeList = GetService<ICommonService>().GetCommonFieldList("BusinessScope");
                ViewData["BusinessScopeList"] = new SelectList(businessScopeList, "FieldValue", "FieldName");

                var regionList = GetService<ICommonService>().GetCommonFieldList("NHHCG_Region");
                ViewData["RegionList"] = new SelectList(regionList, "FieldValue", "FieldName");

                var areaScopeList = GetService<ICommonService>().GetCommonFieldList("KioskAreaScope");
                ViewData["KioskAreaScopeList"] = new SelectList(areaScopeList, "FieldValue", "FieldName");

                var projectList = service.GetNHHCGProjectDropDownList();
                ViewData["ProjectList"] = new SelectList(projectList, "Value", "Text");

                ViewData["StatusList"] = new SelectList(statusList, "Value", "Text");

                return View(model);
            }

            model.BusinessScopes = string.Join(",", model.BusinessScopeList);
            var success = service.UpdateNHHCGKiosk(model);
            return RedirectToAction("KioskList", "NHHCG", new { area = "Official" });
        }

        #endregion 多经点位

        #region 广告位

        private string Crumb_ADPositionMgmt = "官网广告位管理";
        private string Crumb_ADPositionAdd = "创建官网广告位";
        private string Crumb_ADPositionEdit = "编辑官网广告位";
        private string Crumb_ADPositionView = "查看官网广告位";
        /// <summary>
        /// 官网广告位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult ADPositionList(NHHCGADPositionQueryModel queryInfo)
        {
            var model = service.GetNHHCGADPositionList(queryInfo);
            model.CrumbInfo.AddCrumb(Crumb_ADPositionMgmt);

            ViewData["StatusList"] = new SelectList(statusList, "Value", "Text", queryInfo.Status ?? 0);

            var buildingList = service.GetNGGCGBuildingDropDownList(NHHCGTypeEnum.ADPosition);
            ViewData["BuildingList"] = new SelectList(buildingList, "Value", "Text", queryInfo.Building);

            var electricityTypeList = GetService<ICommonService>().GetCommonFieldList("ElectricityType");
            ViewData["ElectricityTypeList"] = new SelectList(electricityTypeList, "FieldValue", "FieldName");

            var adTypeList = GetService<ICommonService>().GetCommonFieldList("AdPointMedia");
            ViewData["ADTypeList"] = new SelectList(adTypeList, "FieldValue", "FieldName", queryInfo.ADType ?? 0);

            var regionList = GetService<ICommonService>().GetCommonFieldList("NHHCG_Region");
            ViewData["RegionList"] = new SelectList(regionList, "FieldValue", "FieldName", queryInfo.Region ?? 0);

            var projectList = service.GetNHHCGProjectDropDownList();
            ViewData["ProjectList"] = new SelectList(projectList, "Value", "Text", queryInfo.ProjectID ?? 0);

            return View(model);
        }
        /// <summary>
        /// 官网广告位添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddADPosition()
        {
            var model = new NHHCGADPositionModel();
            model.CrumbInfo.AddCrumb(Crumb_ADPositionMgmt, Url.Action("ADPositionList", "NHHCG", new { area = "Official" }));
            model.CrumbInfo.AddCrumb(Crumb_ADPositionAdd);

            var commonService = GetService<ICommonService>();

            var electricityTypeList = GetService<ICommonService>().GetCommonFieldList("ElectricityType");
            ViewData["ElectricityTypeList"] = new SelectList(electricityTypeList, "FieldValue", "FieldName");

            var regionList = GetService<ICommonService>().GetCommonFieldList("NHHCG_Region");
            ViewData["RegionList"] = new SelectList(regionList, "FieldValue", "FieldName");

            var adTypeList = GetService<ICommonService>().GetCommonFieldList("AdPointMedia");
            ViewData["ADTypeList"] = new SelectList(adTypeList, "FieldValue", "FieldName");

            var projectList = service.GetNHHCGProjectDropDownList();
            ViewData["ProjectList"] = new SelectList(projectList, "Value", "Text");

            return View(model);
        }
        /// <summary>
        /// 官网广告位新增操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddADPosition(NHHCGADPositionModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb(Crumb_ADPositionMgmt, Url.Action("ADPositionList", "NHHCG", new { area = "Official" }));
                model.CrumbInfo.AddCrumb(Crumb_ADPositionAdd);

                var electricityTypeList = GetService<ICommonService>().GetCommonFieldList("ElectricityType");
                ViewData["ElectricityTypeList"] = new SelectList(electricityTypeList, "FieldValue", "FieldName");

                var adTypeList = GetService<ICommonService>().GetCommonFieldList("AdPointMedia");
                ViewData["ADTypeList"] = new SelectList(adTypeList, "FieldValue", "FieldName");

                var regionList = GetService<ICommonService>().GetCommonFieldList("NHHCG_Region");
                ViewData["RegionList"] = new SelectList(regionList, "FieldValue", "FieldName");

                var projectList = service.GetNHHCGProjectDropDownList();
                ViewData["ProjectList"] = new SelectList(projectList, "Value", "Text");

                return View(model);
            }

            service.AddNHHCGADPosition(model);
            return RedirectToAction("EditADPosition", "NHHCG", new { area = "Official", model.ADPositionID });
        }
        /// <summary>
        /// 编辑广告位页面
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ActionResult EditADPosition(int adPositionId)
        {
            var model = service.GetNHHCGADPosition(adPositionId);
            model.CrumbInfo.AddCrumb(Crumb_ADPositionMgmt, Url.Action("ADPositionList", "NHHCG", new { area = "Official" }));
            model.CrumbInfo.AddCrumb(Crumb_ADPositionEdit);

            var adTypeList = GetService<ICommonService>().GetCommonFieldList("AdPointMedia");
            ViewData["ADTypeList"] = new SelectList(adTypeList, "FieldValue", "FieldName");

            var regionList = GetService<ICommonService>().GetCommonFieldList("NHHCG_Region");
            ViewData["RegionList"] = new SelectList(regionList, "FieldValue", "FieldName");

            var electricityTypeList = GetService<ICommonService>().GetCommonFieldList("ElectricityType");
            ViewData["ElectricityTypeList"] = new SelectList(electricityTypeList, "FieldValue", "FieldName");

            var projectList = service.GetNHHCGProjectDropDownList();
            ViewData["ProjectList"] = new SelectList(projectList, "Value", "Text");

            ViewData["StatusList"] = new SelectList(statusList, "Value", "Text");

            return View(model);
        }

        public ActionResult ViewADPosition(int adPositionId)
        {
            var model = service.GetNHHCGADPosition(adPositionId);

            model.CrumbInfo.AddCrumb(Crumb_ADPositionMgmt, Url.Action("ADPositionList", "NHHCG", new { area = "Official" }));
            model.CrumbInfo.AddCrumb(Crumb_ADPositionView);
            return View(model);
        }
        /// <summary>
        /// 编辑广告位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditADPosition(NHHCGADPositionModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb(Crumb_ADPositionMgmt, Url.Action("ADPositionList", "NHHCG", new { area = "Official" }));
                model.CrumbInfo.AddCrumb(Crumb_ADPositionEdit);

                var electricityTypeList = GetService<ICommonService>().GetCommonFieldList("ElectricityType");
                ViewData["ElectricityTypeList"] = new SelectList(electricityTypeList, "FieldValue", "FieldName");

                var adTypeList = GetService<ICommonService>().GetCommonFieldList("AdPointMedia");
                ViewData["ADTypeList"] = new SelectList(adTypeList, "FieldValue", "FieldName");

                var regionList = GetService<ICommonService>().GetCommonFieldList("NHHCG_Region");
                ViewData["RegionList"] = new SelectList(regionList, "FieldValue", "FieldName");

                var projectList = service.GetNHHCGProjectDropDownList();
                ViewData["ProjectList"] = new SelectList(projectList, "Value", "Text");

                ViewData["StatusList"] = new SelectList(statusList, "Value", "Text");

                return View(model);
            }

            var success = service.UpdateNHHCGADPosition(model);
            return RedirectToAction("ADPositionList", "NHHCG", new { area = "Official" });
        }

        #endregion 广告位

        #region 图片
        /// <summary>
        /// 图片列表
        /// </summary>
        /// <param name="refID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public PartialViewResult PicInfoList(int refID,int type,string title)
        {
            ViewData["RefID"] = refID;
            ViewData["Type"] = type;
            ViewData["Title"] = title;
            var model = service.GetPicList(refID, type);
            return PartialView("_PartialPicListAdd", model);
        }
        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="refId"></param>
        /// <param name="type">1店铺 2广告位 3多经点位</param>
        /// <returns></returns>
        public ActionResult AddPic(int refId,int type)
        {
            var model = new NHHCGPicModel();

            model.RefID = refId;
            model.Type = type;

            return View(model);
        }
        /// <summary>
        /// 插入图片到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult InsertPic(NHHCGPicModel model)
        {
            bool result = service.AddNHHCGPic(model);
            //return RedirectToAction("EditUnit", "NHHCG", new { area = "Official", UnitID = model.RefID });
            return Json(result);
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="picId"></param>
        /// <returns></returns>
        public JsonResult DeletePic(int picId)
        {
            bool result = service.DeletePic(picId);
            return Json(result);
        }
        #endregion 图片

        #region 投诉建议

        /// <summary>
        /// 官网投诉建议列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult FeedbackList(FeedbackQueryModel queryInfo)
        {
            var model = service.GetFeedbackListModel(queryInfo);
            model.CrumbInfo.AddCrumb("官网用户反馈管理");

            var commonService = GetService<ICommonService>();

            var feedbackTypeList = commonService.GetCommonFieldList("FeedbackType");
            ViewData["FeedbackTypeList"] = new SelectList(feedbackTypeList, "FieldValue", "FieldName");

            var userTypeList = commonService.GetCommonFieldList("UserType");
            ViewData["UserTypeList"] = new SelectList(userTypeList, "FieldValue", "FieldName");

            var feedbackStatusList = commonService.GetCommonFieldList("FeedbackStatus");
            ViewData["FeedbackStatusList"] = new SelectList(feedbackStatusList, "FieldValue", "FieldName");

            return View(model);
        }

        public ActionResult ViewFeedback(int feedbackID)
        {
            var model = service.GetFeedbackModel(feedbackID);
            model.CrumbInfo.AddCrumb("官网用户反馈管理", Url.Action("FeedbackList", "NHHCG", new { area = "Official" }));
            model.CrumbInfo.AddCrumb("查看官网用户反馈");
            return View(model);
        }

        public JsonResult Process(FeedbackModel model)
        {
            model.Accepter = Context.UserID;
            model.AcceptTime = DateTime.Now;
            return Json(service.ProcessFeedback(model));
        }

        #endregion
    }
}