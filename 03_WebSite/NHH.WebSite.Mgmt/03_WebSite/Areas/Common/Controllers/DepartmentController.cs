using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Common.Department;
using NHH.Service.Common.IService;
using NHH.Service.Project;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Common.Controllers
{
    /// <summary>
    /// 部门
    /// </summary>
    public class DepartmentController : NHHController
    {
        ICompanyCommonService iCompanyCommonService;
        ICompanyService iCompanyService;
        IDepartmentService iService;


        public DepartmentController()
        {
            this.iService = this.GetService<IDepartmentService>();
            this.iCompanyCommonService = GetService<ICompanyCommonService>();
            this.iCompanyService = GetService<ICompanyService>();
        }

        // GET: Project/ProjectCompany
        /// <summary>
        /// 部门首页
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public ActionResult List(int companyId)
        {
            var model = iService.GetDeptList(companyId);
            model.CrumbInfo.AddCrumb("公司管理", Url.Action("List", "Company", new { area = "Common" }));
            model.CrumbInfo.AddCrumb("部门信息");

            return View(model);
        }

        /// <summary>
        /// 部门详情
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public ActionResult Detail(int deptId)
        {
            var model = iService.GetDeptDetail(deptId);
            model.CrumbInfo.AddCrumb("公司管理", Url.Action("List", "Company", new { area = "Common" }));
            model.CrumbInfo.AddCrumb("部门信息");

            return View(model);
        }

        /// <summary>
        /// 提交新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add(DepartmentDetailModel model)
        {
            var result = new AjaxResult();
            if (!ModelState.IsValid)
            {
                result.Code = -1;
                result.Message = "请输入完整数据";
                return Json(result, JsonRequestBehavior.DenyGet);
            }
            model.InUser = Context.UserID;
            model.EditUser = Context.UserID;
            bool flag = iService.AddDept(model);
            if (!flag)
            {
                result.Code = -1;
                result.Message = "保存失败";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 获取部门新增页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult Add()
        {
            return PartialView("_DeptAddPartial");
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public ActionResult Del(int deptId)
        {
            var result = new AjaxResult();
            bool flag = iService.DeleteDept(deptId);
            if (!flag)
            {
                result.Code = -1;
                result.Message = "删除失败";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 页面编辑提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(DepartmentDetailModel model)
        {
            var result = new AjaxResult();
            if (!ModelState.IsValid)
            {
                result.Code = -1;
                result.Message = "请输入完整数据";
                return Json(result, JsonRequestBehavior.DenyGet);
            }

            model.EditUser = Context.UserID;
            bool flag = iService.UpdateDept(model);
            if (!flag)
            {
                result.Code = -1;
                result.Message = "保存失败";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 获取编辑弹出框
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public PartialViewResult Edit(int deptId)
        {
            var model = iService.GetDeptDetail(deptId);
            return PartialView("_DeptEditPartial", model);
        }
    }
}