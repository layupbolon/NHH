using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHH.Service.Common.IService;
using NHH.Models.Common;
using NHH.Framework.Web;
namespace NHH.WebSite.Management.Areas.Common.Controllers
{
    /// <summary>
    /// 品牌管理
    /// </summary>
    public class BrandController : NHHController
    {
        IBrandService iService;


        public BrandController()
        {
            this.iService = this.GetService<IBrandService>();

        }

        /// <summary>
        /// 品牌列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(BrandListQueryInfo queryInfo)
        {
            var model = iService.GetBrandList(queryInfo);
            model.CrumbInfo.AddCrumb("品牌信息");
            return View(model);
        }

        /// <summary>
        /// 新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            var model = new BrandDetailModel();
            model.CrumbInfo.AddCrumb("品牌信息", Url.Action("List", "Brand", new { area = "Common" }));
            model.CrumbInfo.AddCrumb("新增品牌");

            var brandLevelList = GetService<ICommonService>().GetCommonFieldList("BrandLevel");
            ViewData["BrandLevelList"] = new SelectList(brandLevelList, "FieldValue", "FieldName");

            var bizTypeList = GetService<ICommonService>().GetBizTypeList();
            ViewData["BizTypeList"] = new SelectList(bizTypeList, "Id", "Name");

            var bizCategoryList = new List<BizCategoryInfo>();
            ViewData["BizCategoryList"] = new SelectList(bizCategoryList, "Id", "Name");

            return View(model);
        }

        /// <summary>
        /// 提交新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(BrandDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                var detailModel = new BrandDetailModel();
                detailModel.CrumbInfo.AddCrumb("品牌信息", Url.Action("List", "Brand", new { area = "Common" }));
                detailModel.CrumbInfo.AddCrumb("新增品牌");

                var brandLevelList = GetService<ICommonService>().GetCommonFieldList("BrandLevel");
                ViewData["BrandLevelList"] = new SelectList(brandLevelList, "FieldValue", "FieldName");

                var bizTypeList = GetService<ICommonService>().GetBizTypeList();
                ViewData["BizTypeList"] = new SelectList(bizTypeList, "Id", "Name");

                var bizCategoryList = new List<BizCategoryInfo>();
                ViewData["BizCategoryList"] = new SelectList(bizCategoryList, "Id", "Name");

                return View(detailModel);
            }

            model.InUser = Context.UserID;
            model.EditUser = Context.UserID;
            bool flag = iService.AddBrand(model);
            if (flag)
            {
                return RedirectToAction("List", "Brand", new { area = "Common" });
            }
            else
            {
                return RedirectToAction("Add", "Brand", new { area = "Common" });
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public ActionResult Edit(int brandId)
        {
            var model = iService.GetBrandDetail(brandId);
            model.CrumbInfo.AddCrumb("品牌信息", Url.Action("List", "Brand", new { area = "Common" }));
            model.CrumbInfo.AddCrumb("品牌编辑");
            var brandLevelList = GetService<ICommonService>().GetCommonFieldList("BrandLevel");
            ViewData["BrandLevelList"] = new SelectList(brandLevelList, "FieldValue", "FieldName", model.BrandLevel);

            var bizTypeList = GetService<ICommonService>().GetBizTypeList();
            ViewData["BizTypeList"] = new SelectList(bizTypeList, "Id", "Name", model.BizTypeId);

            var bizCategoryList = new List<BizCategoryInfo>() { new BizCategoryInfo() { Id = model.BizCategoryId, Name = model.BizCategoryInfo.Name } };
            ViewData["BizCategoryList"] = new SelectList(bizCategoryList, "Id", "Name", model.BizCategoryId);
            return View(model);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="detailInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BrandDetailModel detailInfo)
        {
            if (!ModelState.IsValid)
            {
                var model = iService.GetBrandDetail(detailInfo.BrandId);

                var brandLevelList = GetService<ICommonService>().GetCommonFieldList("BrandLevel");
                ViewData["BrandLevelList"] = new SelectList(brandLevelList, "FieldValue", "FieldName", model.BrandLevel);

                var bizTypeList = GetService<ICommonService>().GetBizTypeList();
                ViewData["BizTypeList"] = new SelectList(bizTypeList, "Id", "Name", model.BizTypeId);

                var bizCategoryList = new List<BizCategoryInfo>() { new BizCategoryInfo() { Id = model.BizCategoryId, Name = model.BizCategoryInfo.Name } };
                ViewData["BizCategoryList"] = new SelectList(bizCategoryList, "Id", "Name", model.BizCategoryId);
                return View(model);
            }

            detailInfo.EditUser = Context.UserID;
            iService.UpdateBrand(detailInfo);
            return RedirectToAction("List", "Brand", new { area = "Common" });
        }
    }
}
