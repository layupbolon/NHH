using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Merchant;
using NHH.Service.Common.IService;
using NHH.Service.Merchant.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Plan.Controllers
{
    /// <summary>
    /// 商户
    /// </summary>
    public class MerchantController : NHHController
    {
        IMerchantService iService;


        public MerchantController()
        {
            this.iService = this.GetService<IMerchantService>();
        }

        /// <summary>
        /// 商户列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(MerchantListQueryInfo queryInfo)
        {
            var model = new MerchantListModel();
            model.QueryInfo = queryInfo;
            model.CrumbInfo.AddCrumb("商户信息");

            return View(model);
        }

        /// <summary>
        /// 获取商户列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public JsonResult GetMerchantList(MerchantListQueryInfo queryInfo)
        {
            var model = iService.GetMerchantList(queryInfo);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 公司详情
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public ActionResult Detail(int merchantId)
        {
            var model = iService.GetMerchantDetail(merchantId);
            model.CrumbInfo.AddCrumb("商户信息", Url.Action("List", "Merchant", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("商户详情");

            //获取证照列表
            var listModel = GetService<IAttachmentService>().GetAttachmentList(merchantId);
            model.AttachmentList = listModel.AttachmentList;
            if (model.MerchantType == 2)
            {
                return View("Detail2", model);
            }

            return View(model);
        }

        /// <summary>
        /// 添加公司
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            var model = new MerchantDetailModel();
            model.CrumbInfo.AddCrumb("商户信息", Url.Action("List", "Merchant", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("新增商户");

            var provinceList = GetService<ICommonService>().GetProvinceList();
            var cityList = new List<CityInfo>();

            ViewData["MerchantProvince"] = new SelectList(provinceList, "Id", "Name");
            ViewData["MerchantCity"] = new SelectList(cityList, "Id", "Name");

            ViewData["MerchantRegisterProvince"] = new SelectList(provinceList, "Id", "Name");
            ViewData["MerchantRegisterCity"] = new SelectList(cityList, "Id", "Name");
            return View(model);
        }

        /// <summary>
        /// 添加自然人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Add2()
        {
            var model = new MerchantDetailModel();
            model.CrumbInfo.AddCrumb("商户信息", Url.Action("List", "Merchant", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("新增商户");

            var provinceList = GetService<ICommonService>().GetProvinceList();
            var cityList = new List<CityInfo>();

            ViewData["MerchantProvince"] = new SelectList(provinceList, "Id", "Name");
            ViewData["MerchantCity"] = new SelectList(cityList, "Id", "Name");
            return View(model);

        }

        /// <summary>
        /// 提交公司信息
        /// </summary>
        /// <param name="merchantModel"></param>
        /// <param name="finance"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(MerchantDetailModel merchantModel, MerchantFinanceModel financeModel)
        {
            var model = merchantModel;
            model.CrumbInfo.AddCrumb("商户信息", Url.Action("List", "Merchant", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("新增商户");
            model.FinanceInfo = financeModel;
            model.UserId = Context.UserID;

            if (ModelState.IsValid)
            {
                var message = iService.AddMerchant(model);
                return RedirectToAction("List", "Merchant", new { area = "Plan" });
            }

            var commonService = GetService<ICommonService>();
            var provinceList = commonService.GetProvinceList();
            var cityList = commonService.GetCityList(model.ProvinceId);

            ViewData["MerchantProvince"] = new SelectList(provinceList, "Id", "Name", model.ProvinceId);
            ViewData["MerchantCity"] = new SelectList(cityList, "Id", "Name", model.CityId);

            //当类型是公司时
            if (model.MerchantType == 1)
            {
                var registerCityList = commonService.GetCityList(model.RegisterProvinceId);
                ViewData["MerchantRegisterProvince"] = new SelectList(provinceList, "Id", "Name", model.RegisterProvinceId);
                ViewData["MerchantRegisterCity"] = new SelectList(registerCityList, "Id", "Name", model.RegisterCityId);
                return View(model);
            }

            return View("Add2", model);
        }

        /// <summary>
        /// 删除商户信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public ActionResult Del(int merchantId)
        {
            iService.DeleteMerchant(merchantId);
            return RedirectToAction("List", "Merchant", new { area = "Plan" });
        }

        /// <summary>
        /// 进去公司编辑页面
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public ActionResult Edit(int merchantId)
        {
            var model = iService.GetMerchantDetail(merchantId);
            model.CrumbInfo.AddCrumb("商户信息", Url.Action("List", "Merchant", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("编辑商户");

            model.MerchantType = 1;

            var commonService = GetService<ICommonService>();
            var provinceList = commonService.GetProvinceList();
            var cityList = new List<CityInfo>() { new CityInfo { Id = model.CityId, Name = model.CityInfo.Name, } };
            var RegisterCityList = new List<CityInfo>() { new CityInfo { Id = model.RegisterCityId, Name = model.RegisterCityInfo.Name, } };

            ViewData["MerchantProvince"] = new SelectList(provinceList, "Id", "Name", model.ProvinceId);
            ViewData["MerchantCity"] = new SelectList(cityList, "Id", "Name", model.CityId);
            ViewData["MerchantRegisterProvince"] = new SelectList(provinceList, "Id", "Name", model.RegisterProvinceId);
            ViewData["MerchantRegisterCity"] = new SelectList(RegisterCityList, "Id", "Name", model.RegisterCityId);


            return View(model);

        }

        /// <summary>
        /// 进去自然人编辑页面
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public ActionResult Edit2(int merchantId)
        {
            var model = iService.GetMerchantDetail(merchantId);
            model.CrumbInfo.AddCrumb("商户信息", Url.Action("List", "Merchant", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("编辑商户");

            var commonService = GetService<ICommonService>();
            var provinceList = commonService.GetProvinceList();
            var cityList = new List<CityInfo>() { new CityInfo { Id = model.CityId, Name = model.CityInfo.Name, } };


            ViewData["MerchantProvince"] = new SelectList(provinceList, "Id", "Name", model.ProvinceId);
            ViewData["MerchantCity"] = new SelectList(cityList, "Id", "Name", model.CityId);
            return View(model);
        }

        /// <summary>
        /// 提交公司更新信息
        /// </summary>
        /// <param name="merchant"></param>
        /// <param name="finance"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(MerchantDetailModel merchant, MerchantFinanceModel finance)
        {
            if (ModelState.IsValid)
            {
                merchant.UserId = Context.UserID;
                iService.UpdateMerchant(merchant, finance);
                return RedirectToAction("List", "Merchant", new { area = "Plan" });
            }

            #region 验证不通过
            var model = iService.GetMerchantDetail(merchant.MerchantId);
            model.CrumbInfo.AddCrumb("商户信息", Url.Action("List", "Merchant", new { area = "Merchant" }));
            model.CrumbInfo.AddCrumb("编辑商户");
            var commonService = GetService<ICommonService>();

            var provinceList = commonService.GetProvinceList();
            var cityList = new List<CityInfo>() { new CityInfo { Id = model.CityId, Name = model.CityInfo.Name, } };

            ViewData["MerchantProvince"] = new SelectList(provinceList, "Id", "Name", model.ProvinceId);
            ViewData["MerchantCity"] = new SelectList(cityList, "Id", "Name", model.CityId);

            //当商户是公司时
            if (model.MerchantType == 1)
            {
                var RegisterCityList = new List<CityInfo>() { new CityInfo { Id = model.RegisterCityId, Name = model.RegisterCityInfo.Name, } };
                ViewData["MerchantRegisterProvince"] = new SelectList(provinceList, "Id", "Name", model.RegisterProvinceId);
                ViewData["MerchantRegisterCity"] = new SelectList(RegisterCityList, "Id", "Name", model.RegisterCityId);
                return View(model);
            }

            return View("Edit2", model);

            #endregion

        }

    }
}