using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Merchant;
using NHH.Service.Common.IService;
using NHH.Service.Merchant.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Plan.Controllers
{
    /// <summary>
    /// 商户品牌管理
    /// </summary>
    public class BrandController : NHHController
    {
        private IMerchantBrandService service;

        public BrandController()
        {
            service = GetService<IMerchantBrandService>();
        }

        /// <summary>
        /// 列表页
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult List(int merchantId, int? page)
        {
            var model = service.GetBrandList(merchantId, page);
            model.CrumbInfo.AddCrumb("商户管理", Url.Action("List", "Merchant", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("商户品牌");

            return View(model);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public ActionResult Add(int merchantId)
        {
            var model = new MerchantBrandDetailModel();
            model.MerchantID = merchantId;
            model.CrumbInfo.AddCrumb("商户管理", Url.Action("List", "Merchant", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("新增商户品牌");

            var commonService = GetService<ICommonService>();

            var brandList = commonService.GetBrandList();
            ViewData["BrandList"] = new SelectList(brandList, "Value", "Text");

            var brandTypeList = commonService.GetCommonFieldList("BrandType");
            ViewData["BrandTypeList"] = new SelectList(brandTypeList, "FieldValue", "FieldName");

            return View(model);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(MerchantBrandDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("商户管理", Url.Action("List", "Merchant", new { area = "Plan" }));
                model.CrumbInfo.AddCrumb("新增商户品牌");

                var commonService = GetService<ICommonService>();

                var brandList = commonService.GetBrandList();
                ViewData["BrandList"] = new SelectList(brandList, "Value", "Text");

                var brandTypeList = commonService.GetCommonFieldList("BrandType");
                ViewData["BrandTypeList"] = new SelectList(brandTypeList, "FieldValue", "FieldName");

                return View(model);
            }
            model.InUser = Context.UserID;
            service.Add(model);
            return RedirectToAction("List", "Brand", new { area = "Plan", merchantId = model.MerchantID });
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="mbId"></param>
        /// <returns></returns>
        public ActionResult Edit(int mbId)
        {
            var model = service.GetBrandDetail(mbId);
            model.CrumbInfo.AddCrumb("商户管理", Url.Action("List", "Merchant", new { area = "Plan" }));
            model.CrumbInfo.AddCrumb("编辑商户品牌");

            var commonService = GetService<ICommonService>();

            var brandList = commonService.GetBrandList();
            ViewData["BrandList"] = new SelectList(brandList, "Value", "Text", model.BrandID);

            var brandTypeList = commonService.GetCommonFieldList("BrandType");
            ViewData["BrandTypeList"] = new SelectList(brandTypeList, "FieldValue", "FieldName", model.BrandTypeId);

            return View(model);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(MerchantBrandDetailModel model)
        {
            model.InUser = Context.UserID;
            service.Edit(model);
            return RedirectToAction("List", "Brand", new { area = "Plan", merchantId = model.MerchantID });
        }

        /// <summary>
        /// 作废商户品牌
        /// </summary>
        /// <param name="merchantBrandId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Del(int merchantBrandId)
        {
            var result = new AjaxResult();
            var message = service.DelelteMerchantBrand(merchantBrandId);

            result.Code = message.Code;
            result.Message = message.Desc;

            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}