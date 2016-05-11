using NHH.Framework.Service;
using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Common.Employee;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Common.Controllers
{
    /// <summary>
    /// 员工管理
    /// </summary>
    public class EmployeeController : NHHController
    {
        IEmployeeService iService;


        public EmployeeController()
        {
            this.iService = this.GetService<IEmployeeService>();
        }

        /// <summary>
        /// 员工首页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(EmployeeListQueryInfo queryInfo, DepartmentCommonInfo deptInfo)
        {
            EmployeeListModel model = iService.GetEmployeeList(queryInfo, deptInfo);

            model.CrumbInfo.AddCrumb("公司管理", Url.Action("List", "Company", new { area = "Common" }));
            model.CrumbInfo.AddCrumb("部门信息", Url.Action("List", "Department", new { area = "Common", companyId = model.CompanyInfo.Id }));
            model.CrumbInfo.AddCrumb("员工管理");

            var genderTypeList = GetService<ICommonService>().GetGenderTypeList();
            ViewData["GenderTypeList"] = new SelectList(genderTypeList, "Id", "Name");


            return View(model);
        }

        /// <summary>
        /// 员工详情
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public ActionResult Detail(int employeeId)
        {
            var model = iService.GetEmployeeDetail(employeeId);
            model.CrumbInfo.AddCrumb("公司管理", Url.Action("List", "Company", new { area = "Common" }));
            model.CrumbInfo.AddCrumb("部门信息", Url.Action("List", "Department", new { area = "Common", companyId = model.CompanyId }));
            model.CrumbInfo.AddCrumb("员工信息", Url.Action("List", "Employee", new { area = "Common", DepartmentID = model.DepartmentId }));
            model.CrumbInfo.AddCrumb("员工详情");

            return View(model);

        }

        /// <summary>
        /// 新增页面
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>

        public ActionResult Add(int DepartmentID)
        {
            var model = new EmployeeDetailModel();//iService.GetEmployeeDetail(DepartmentID);

            var deptService = GetService<IDepartmentService>();
            var department = deptService.GetDeptDetail(DepartmentID);
            model.DepartmentId = department.DepartmentID;
            model.CompanyId = department.CompanyID;

            model.CrumbInfo.AddCrumb("公司管理", Url.Action("List", "Company", new { area = "Common" }));
            model.CrumbInfo.AddCrumb("部门信息", Url.Action("List", "Department", new { area = "Common", companyId = model.CompanyId }));
            model.CrumbInfo.AddCrumb("员工信息", Url.Action("List", "Employee", new { area = "Common", DepartmentID = DepartmentID }));
            model.CrumbInfo.AddCrumb("新增员工");



            return View(model);
        }

        /// <summary>
        /// 提交新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(EmployeeDetailModel model)
        {
            model.CompanyId = GetService<IDepartmentService>().GetCompanyId(model.DepartmentId);
            if (ModelState.IsValid)
            {
                var result = iService.AddEmployee(model);
                if (!result.IsSuccess())
                {
                    ModelState.AddModelError(result.Key, result.Desc);
                }
            }

            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("公司管理", Url.Action("List", "Company", new { area = "Common" }));
                model.CrumbInfo.AddCrumb("部门信息", Url.Action("List", "Department", new { area = "Common", companyId = model.CompanyId }));
                model.CrumbInfo.AddCrumb("员工信息", Url.Action("List", "Employee", new { area = "Common", DepartmentID = model.DepartmentId }));
                model.CrumbInfo.AddCrumb("新增员工");
                return View(model);
            }
            return RedirectToAction("List", "Employee", new { area = "Common", DepartmentID = model.DepartmentId });
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public ActionResult Del(int employeeId, int deptId)
        {
            iService.DeleteEmployee(employeeId);
            return RedirectToAction("List", "Employee", new { area = "Common", DepartmentID = deptId });
        }

        /// <summary>
        /// 编辑员工信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public ActionResult Edit(int employeeId)
        {
            var model = iService.GetEmployeeDetail(employeeId);
            model.CrumbInfo.AddCrumb("公司管理", Url.Action("List", "Company", new { area = "Common" }));
            model.CrumbInfo.AddCrumb("部门信息", Url.Action("List", "Department", new { area = "Common", companyId = model.CompanyId }));
            model.CrumbInfo.AddCrumb("员工信息", Url.Action("List", "Employee", new { area = "Common", DepartmentID = model.DepartmentId }));
            model.CrumbInfo.AddCrumb("编辑员工");

            return View(model);
        }

        /// <summary>
        /// 保存员工编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                var detailModel = iService.GetEmployeeDetail(model.EmployeeId);
                detailModel.CrumbInfo.AddCrumb("公司管理", Url.Action("List", "Company", new { area = "Common" }));
                detailModel.CrumbInfo.AddCrumb("部门信息", Url.Action("List", "Department", new { area = "Common", companyId = model.CompanyId }));
                detailModel.CrumbInfo.AddCrumb("员工信息", Url.Action("List", "Employee", new { area = "Common", DepartmentID = model.DepartmentId }));
                detailModel.CrumbInfo.AddCrumb("编辑员工");

                return View(detailModel);
            }


            iService.UpdateEmployee(model);
            return RedirectToAction("List", "Employee", new { area = "Common", DepartmentID = model.DepartmentId });
        }
    }
}