using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHH.Service.Project.IService;
using NHH.Entities;
using NHH.Service.Common.IService;
using NHH.Models.Common;
using NHH.Models.Common.Company;
using NHH.Framework.Web;

namespace NHH.WebSite.Management.Areas.Common.Controllers
{
    /// <summary>
    /// 公司管理
    /// </summary>
    public class CompanyController : NHHController
    {
        ICompanyService iService;
        ICommonService iCommonService;


        public CompanyController()
        {
            this.iService = this.GetService<ICompanyService>();
            this.iCommonService = this.GetService<ICommonService>();
        }

        /// <summary>
        /// 关联公司首页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(CompanyListQueryInfo queryInfo)
        {
            CompanyListModel model = iService.GetCompanyList(queryInfo);
            model.CrumbInfo.AddCrumb("公司信息");

            var companyTypeList = GetService<ICompanyCommonService>().GetCompanyTypeList();
            ViewData["CompanyTypeList"] = new SelectList(companyTypeList, "Id", "Name");

            return View(model);
        }

        /// <summary>
        /// 关联公司详情
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public ActionResult Detail(int companyId)
        {
            var model = iService.GetCompanyDetail(companyId);
            model.CrumbInfo.AddCrumb("公司信息", Url.Action("List", "Company", new { area = "Common" }));
            model.CrumbInfo.AddCrumb("公司详情");
            return View(model);

        }

        // public ActionResult ListByCompany
        /// <summary>
        /// 关联公司新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            var model = new CompanyDetailModel();
            model.CrumbInfo.AddCrumb("公司信息", Url.Action("List", "Company", new { area = "Common" }));
            model.CrumbInfo.AddCrumb("新增公司");
            var companyTypeList = GetService<ICompanyCommonService>().GetCompanyTypeList();
            ViewData["CompanyTypeList"] = new SelectList(companyTypeList, "Id", "Name");

            var companyList = GetService<ICompanyCommonService>().GetCompanyList();
            ViewData["CompanyList"] = new SelectList(companyList, "Id", "Name");

            var provinceList = GetService<ICommonService>().GetProvinceList();
            var cityList = new List<CityInfo>();

            ViewData["ProvinceList"] = new SelectList(provinceList, "Id", "Name");
            ViewData["CityList"] = new SelectList(cityList, "Id", "Name");
            ViewData["RegisterProvinceList"] = new SelectList(provinceList, "Id", "Name");
            ViewData["RegisterCityList"] = new SelectList(cityList, "Id", "Name");



            return View(model);
        }

        /// <summary>
        /// 提交关联公司新增
        /// </summary>
        /// <param name="company"></param>
        /// <param name="finance"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CompanyDetailModel company, FinanceModel finance)
        {
            var model = new CompanyDetailModel();
            model.CrumbInfo.AddCrumb("公司信息", Url.Action("List", "Company", new { area = "Common" }));
            model.CrumbInfo.AddCrumb("新增公司");

            var companyTypeList = GetService<ICompanyCommonService>().GetCompanyTypeList();
            ViewData["CompanyTypeList"] = new SelectList(companyTypeList, "Id", "Name");

            var companyList = GetService<ICompanyCommonService>().GetCompanyList();
            ViewData["CompanyList"] = new SelectList(companyList, "Id", "Name");

            var provinceList = GetService<ICommonService>().GetProvinceList();
            var cityList = new List<CityInfo>();

            ViewData["ProvinceList"] = new SelectList(provinceList, "Id", "Name");
            ViewData["CityList"] = new SelectList(cityList, "Id", "Name");
            ViewData["RegisterProvinceList"] = new SelectList(provinceList, "Id", "Name");
            ViewData["RegisterCityList"] = new SelectList(cityList, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            company.InUser = Context.UserID;
            bool flag = iService.AddCompany(company, finance);
            if (!flag)
            {
                ModelState.AddModelError("CompanyName","公司名已存在");
                return View(model);
            }
            return RedirectToAction("List", "Company", new { area = "Common" });
        }

        /// <summary>
        /// 删除关联公司
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public ActionResult Del(int companyId)
        {
            iService.DeleteCompany(companyId);
            return RedirectToAction("List", "Company", new { area = "Common" });
        }

        /// <summary>
        /// 编辑公司
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public ActionResult Edit(int companyId)
        {
            CompanyDetailModel model = iService.GetCompanyDetail(companyId);
            model.CrumbInfo.AddCrumb("公司信息", Url.Action("List", "Company", new { area = "Common" }));
            model.CrumbInfo.AddCrumb("公司编辑");
            var companyTypeList = GetService<ICompanyCommonService>().GetCompanyTypeList();
            ViewData["CompanyTypeList"] = new SelectList(companyTypeList, "Id", "Name", model.CompanyType);

            // var resolver = new NhhNinjectDependencyResolver();
            var companyList = GetService<ICompanyCommonService>().GetCompanyList();
            ViewData["CompanyList"] = new SelectList(companyList, "Id", "Name", model.CompanyName);

            var commonService = GetService<ICommonService>();
            var provinceList = commonService.GetProvinceList();
            var cityList = new List<CityInfo>() { new CityInfo { Id = model.CityID, Name = model.CityName, } };

            var RegisterCityList = new List<CityInfo>() { new CityInfo { Id = model.RegisterCityID, Name = model.RegisterCityName, } };

            ViewData["CompanyProvince"] = new SelectList(provinceList, "Id", "Name", model.ProvinceID);
            ViewData["CompanyCity"] = new SelectList(cityList, "Id", "Name", model.CityID);
            ViewData["CompanyRegisterProvince"] = new SelectList(provinceList, "Id", "Name", model.RegisterProvinceID);
            ViewData["CompanyRegisterCity"] = new SelectList(RegisterCityList, "Id", "Name", model.RegisterCityID);
            return View(model);
        }

        /// <summary>
        /// 保存公司编辑
        /// </summary>
        /// <param name="company"></param>
        /// <param name="finance"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyDetailModel company, FinanceModel finance)
        {
            if (!ModelState.IsValid)
            {
                CompanyDetailModel model = iService.GetCompanyDetail(company.CompanyID);
                model.CrumbInfo.AddCrumb("公司信息", Url.Action("List", "Company", new { area = "Common" }));
                model.CrumbInfo.AddCrumb("公司编辑");
                var companyTypeList = GetService<ICompanyCommonService>().GetCompanyTypeList();
                ViewData["CompanyTypeList"] = new SelectList(companyTypeList, "Id", "Name", model.CompanyType);

                var companyList = GetService<ICompanyCommonService>().GetCompanyList();
                ViewData["CompanyList"] = new SelectList(companyList, "Id", "Name", model.CompanyName);

                var commonService = GetService<ICommonService>();
                var provinceList = commonService.GetProvinceList();
                var cityList = new List<CityInfo>() { new CityInfo { Id = model.CityID, Name = model.CityName, } };

                var RegisterCityList = new List<CityInfo>() { new CityInfo { Id = model.RegisterCityID, Name = model.RegisterCityName, } };

                ViewData["CompanyProvince"] = new SelectList(provinceList, "Id", "Name", model.ProvinceID);
                ViewData["CompanyCity"] = new SelectList(cityList, "Id", "Name", model.CityID);
                ViewData["CompanyRegisterProvince"] = new SelectList(provinceList, "Id", "Name", model.RegisterProvinceID);
                ViewData["CompanyRegisterCity"] = new SelectList(RegisterCityList, "Id", "Name", model.RegisterCityID);
                return View(model);
            }

            company.InUser = Context.UserID;
            iService.UpdateCompany(company, finance);
            return RedirectToAction("List", "Company", new { area = "Common" });
        }
    }
}