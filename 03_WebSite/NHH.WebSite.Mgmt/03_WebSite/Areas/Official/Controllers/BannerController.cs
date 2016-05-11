using System.Web.Mvc;
using NHH.Framework.Web;
using NHH.Models.Official.Banner;
using NHH.Models.Official.Project;
using NHH.Service.Common.IService;
using NHH.Service.Official.IService;

namespace NHH.WebSite.Management.Areas.Official.Controllers
{
    public class BannerController : NHHController
    {
        private readonly IBannerService service;
        public BannerController()
        {
            service = GetService<IBannerService>();
        }

        public ActionResult List(BannerQueryInfo queryInfo)
        {
            var model = service.GetBannerList(queryInfo);
            model.CrumbInfo.AddCrumb("广告位管理");

            ICommonService commonService = GetService<ICommonService>();
            var bannerTargetList = commonService.GetCommonFieldList("BannerTarget");
            ViewData["BannerTargetList"] = new SelectList(bannerTargetList, "FieldValue", "FieldName",
                queryInfo.BannerTarget ?? 0);

            var bannerTypeList = commonService.GetCommonFieldList("BannerType");
            ViewData["BannerTypeList"] = new SelectList(bannerTypeList, "FieldValue", "FieldName",
                queryInfo.BannerType ?? 0);

            //唐小二
            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "ID", "Name", queryInfo.ProjectID ?? 0);

            //官网
            var nhhProjectList = GetService<INHHCGService>().GetProjectList();
            ViewData["NhhProjectList"] = new SelectList(nhhProjectList, "ProjectID", "ProjectName", queryInfo.ProjectID ?? 0);

            return View(model);
        }

        public ActionResult Add()
        {
            var model = new BannerModel();
            model.CrumbInfo.AddCrumb("广告位管理", Url.Action("List", "Banner", new { area = "Official" }));
            model.CrumbInfo.AddCrumb("创建广告位");

            var commonService = GetService<ICommonService>();

            var bannerTargetList = commonService.GetCommonFieldList("BannerTarget");
            ViewData["BannerTargetList"] = new SelectList(bannerTargetList, "FieldValue", "FieldName");
            var bannerTypeList = commonService.GetCommonFieldList("BannerType");
            ViewData["BannerTypeList"] = new SelectList(bannerTypeList, "FieldValue", "FieldName");
            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name");
            var locationList = service.GetLocationInfo();
            ViewData["LocationList"] = new SelectList(locationList, "LocationID", "LocationName");

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(BannerModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("广告位管理", Url.Action("List", "Banner", new { area = "Official" }));
                model.CrumbInfo.AddCrumb("创建广告位");

                var commonService = GetService<ICommonService>();

                var bannerTargetList = commonService.GetCommonFieldList("BannerTarget");
                ViewData["BannerTargetList"] = new SelectList(bannerTargetList, "FieldValue", "FieldName");
                var bannerTypeList = commonService.GetCommonFieldList("BannerType");
                ViewData["BannerTypeList"] = new SelectList(bannerTypeList, "FieldValue", "FieldName");
                var projectList = GetService<IProjectCommonService>().GetProjectList();
                ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name");
                var locationList = service.GetLocationInfo();
                ViewData["LocationList"] = new SelectList(locationList, "LocationID", "LocationName");

                return View(model);
            }

            model.UserID = NHHWebContext.Current.UserID;
            int bannerID = service.AddBanner(model);
            return RedirectToAction("Edit", "Banner", new { area = "Official", bannerID });
        }

        /// <summary>
        /// 获取位置列表
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public JsonResult GetLocationList(int target)
        {
            var locationList = service.GetLocationInfo(target);
            return Json(locationList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="bannerID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Cancel(int? bannerID)
        {
            var success = service.Cancel(bannerID,NHHWebContext.Current.UserID);
            return Json(success);
        }

        public ActionResult Edit(int bannerID)
        {
            var model = service.GetBanner(bannerID);
            model.CrumbInfo.AddCrumb("广告位管理", Url.Action("List", "Banner", new { area = "Official" }));
            model.CrumbInfo.AddCrumb("广告位编辑");

            var commonService = GetService<ICommonService>();

            var bannerTargetList = commonService.GetCommonFieldList("BannerTarget");
            ViewData["BannerTargetList"] = new SelectList(bannerTargetList, "FieldValue", "FieldName");
            var bannerTypeList = commonService.GetCommonFieldList("BannerType");
            ViewData["BannerTypeList"] = new SelectList(bannerTypeList, "FieldValue", "FieldName");
            //唐小二
            var projectList = GetService<IProjectCommonService>().GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "ID", "Name", model.ProjectID);

            //官网
            var nhhProjectList = GetService<INHHCGService>().GetProjectList();
            nhhProjectList.Insert(0, new NHHCGProjectModel() { ProjectID = 0, ProjectName = "全部" });
            ViewData["NhhProjectList"] = new SelectList(nhhProjectList, "ProjectID", "ProjectName", model.ProjectID);
            var locationList = service.GetLocationInfo(1);
            ViewData["LocationList"] = new SelectList(locationList, "LocationID", "LocationName");
            var nhhLocationList = service.GetLocationInfo(2);
            ViewData["nhhLocationList"] = new SelectList(nhhLocationList, "LocationID", "LocationName");

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(BannerModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("广告位管理", Url.Action("List", "Banner", new { area = "Official" }));
                model.CrumbInfo.AddCrumb("广告位编辑");

                var commonService = GetService<ICommonService>();

                var bannerTargetList = commonService.GetCommonFieldList("BannerTarget");
                ViewData["BannerTargetList"] = new SelectList(bannerTargetList, "FieldValue", "FieldName");
                var bannerTypeList = commonService.GetCommonFieldList("BannerType");
                ViewData["BannerTypeList"] = new SelectList(bannerTypeList, "FieldValue", "FieldName");
                //唐小二
                var projectList = GetService<IProjectCommonService>().GetProjectList();
                ViewData["ProjectList"] = new SelectList(projectList, "ID", "Name", model.ProjectID);

                //官网
                var nhhProjectList = GetService<INHHCGService>().GetProjectList();
                nhhProjectList.Insert(0, new NHHCGProjectModel() {ProjectID = 0, ProjectName = "全部"});
                ViewData["NhhProjectList"] = new SelectList(nhhProjectList, "ProjectID", "ProjectName", model.ProjectID);
                var locationList = service.GetLocationInfo();
                ViewData["LocationList"] = new SelectList(locationList, "LocationID", "LocationName");

                return View(model);
            }

            model.UserID = NHHWebContext.Current.UserID;
            var success = service.EditBanner(model);
            return RedirectToAction("List", "Banner", new { area = "Official" });
        }

        /// <summary>
        /// 广告位详细列表
        /// </summary>
        /// <param name="bannerID"></param>
        /// <returns></returns>
        public PartialViewResult BannerInfoList(int bannerID)
        {
            var model = service.GetBannerDetail(bannerID);
            return PartialView("_PartialBannerInfoList", model);
        }

        /// <summary>
        /// 删除广告位详细信息
        /// </summary>
        /// <param name="infoID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(int infoID)
        {
            var success = service.DeleteBannerInfo(infoID);
            return Json(success, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 添加广告位详细信息
        /// </summary>
        /// <param name="bannerID"></param>
        /// <returns></returns>
        public PartialViewResult AddBannerInfo(int bannerID)
        {
            var model = new BannerInfoModel { BannerID = bannerID };
            return PartialView("_PartialBannerInfoAdd", model);
        }

        /// <summary>
        /// 新增广告位信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddBannerInfo(BannerInfoModel model)
        {
            model.UserID = NHHWebContext.Current.UserID;
            bool success = service.AddBannerInfo(model);
            return Json(success, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 广告位详细调整顺序
        /// </summary>
        /// <param name="infoID"></param>
        /// <param name="seqType">1为上移  2为下移</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Seq(int infoID, int seqType)
        {
            var success = service.BannerSeq(infoID, seqType,NHHWebContext.Current.UserID);
            return Json(success, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 编辑广告位详细信息
        /// </summary>
        /// <param name="infoID"></param>
        /// <returns></returns>
        public PartialViewResult EditBannerInfo(int infoID)
        {
            var model = service.GetBannerInfo(infoID);
            return PartialView("_PartialBannerInfoEdit", model);
        }

        /// <summary>
        /// 编辑广告位详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditBannerInfo(BannerInfoModel model)
        {
            model.UserID = NHHWebContext.Current.UserID;
            bool success = service.EditBannerInfo(model);
            return Json(success, JsonRequestBehavior.DenyGet);
        }
    }
}