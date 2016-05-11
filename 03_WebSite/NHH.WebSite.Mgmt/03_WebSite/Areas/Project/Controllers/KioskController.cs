using NHH.Framework.Web;
using NHH.Models.Project.Kiosk;
using NHH.Service.Common.IService;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Project.Controllers
{
    /// <summary>
    /// 多经点位
    /// </summary>
    public class KioskController : NHHController
    {
        private IKioskService iService;
        private IProjectCommonService projectService;


        public KioskController()
        {
            this.iService = this.GetService<IKioskService>();
            projectService = GetService<IProjectCommonService>();
        }

        /// <summary>
        /// 列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult List(KioskQueryInfo queryInfo)
        {
            var model = iService.GetKioskList(queryInfo);
            model.CrumbInfo.AddCrumb("项目管理", Url.Action("List", "ProjectInfo", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("多经点位");
            var projectList = projectService.GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);

            int projectId = projectList.Count > 0 ? projectList.FirstOrDefault().Id : 0;
            var floorList = GetService<IProjectCommonService>().GetAllFloor(projectId);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName", queryInfo.FloorId.HasValue ? queryInfo.FloorId.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 多经点创建
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            var model = new KioskDetailModel();
            model.CrumbInfo.AddCrumb("项目管理", Url.Action("List", "ProjectInfo", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("多经点位管理", Url.Action("List", "Kiosk", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("新增多经点位");

            var projectList = projectService.GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name");

            int projectId = projectList.Count > 0 ? projectList.FirstOrDefault().Id : 0;
            var floorList = GetService<IProjectCommonService>().GetAllFloor(projectId);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName");

            ///多经点位类型下拉框
            var kioskTypeList = GetService<ICommonService>().GetCommonFieldList("KioskType");
            ViewData["KioskTypeList"] = new SelectList(kioskTypeList,"FieldValue","FieldName");

            return View(model);
        }

        /// <summary>
        /// 保存多经点位信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(KioskDetailModel model)
        {
            if (ModelState.IsValid)
            {
                iService.SaveKiosk(model);
                return RedirectToAction("List", "Kiosk", new { area = "Project" });
            }
            model.CrumbInfo.AddCrumb("项目管理", Url.Action("List", "ProjectInfo", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("多经点位管理", Url.Action("List", "Kiosk", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("新增多经点位");

            var projectList = projectService.GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", model.ProjectID);

            int projectId = projectList.Count > 0 ? projectList.FirstOrDefault().Id : 0;
            var floorList = GetService<IProjectCommonService>().GetAllFloor(projectId);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName", model.FloorId);

            ///多经点位类型下拉框
            var kioskTypeList = GetService<ICommonService>().GetCommonFieldList("KioskType");
            ViewData["KioskTypeList"] = new SelectList(kioskTypeList, "FieldValue", "FieldName",model.KioskType);

            return View(model);

        }

        /// <summary>
        /// 编辑多经点位信息
        /// </summary>
        /// <param name="kioskId"></param>
        /// <returns></returns>
        public ActionResult Edit(int kioskId)
        {
            var model = iService.GetKioskDetail(kioskId);
            model.CrumbInfo.AddCrumb("项目管理", Url.Action("List", "ProjectInfo", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("多经点位管理", Url.Action("List", "Kiosk", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("编辑多经点位");

            var projectList = projectService.GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", model.ProjectID);

            var floorList = GetService<IProjectCommonService>().GetAllFloor(model.ProjectID);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName",model.FloorId);

            ///多经点位类型下拉框
            var kioskTypeList = GetService<ICommonService>().GetCommonFieldList("KioskType");
            ViewData["KioskTypeList"] = new SelectList(kioskTypeList, "FieldValue", "FieldName", model.KioskType);

            return View(model);
        }

        /// <summary>
        /// 编辑多经点位信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(KioskDetailModel model)
        {
            if (ModelState.IsValid)
            {
                iService.SaveKiosk(model);
                return RedirectToAction("List", "Kiosk", new { area = "Project" });
            }

            model.CrumbInfo.AddCrumb("项目管理", Url.Action("List", "ProjectInfo", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("多经点位管理", Url.Action("List", "Kiosk", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("编辑多经点位");

            var projectList = projectService.GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", model.ProjectID);

            var floorList = GetService<IProjectCommonService>().GetAllFloor(model.ProjectID);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName");

            return View(model);
        }
    }
}