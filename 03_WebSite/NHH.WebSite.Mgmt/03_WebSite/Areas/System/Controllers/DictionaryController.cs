using NHH.Framework.Web;
using NHH.Models.Configure;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.System.Controllers
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public class DictionaryController : NHHController
    {
        /// <summary>
        /// 列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(DictionaryListQueryInfo queryInfo)
        {
            var model = GetService<IDictionaryService>().GetList(queryInfo);
            model.CrumbInfo.AddCrumb("数据字典");

            return View(model);
        }

        /// <summary>
        /// 新增页
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            var model = new DictionaryDetailModel();
            model.CrumbInfo.AddCrumb("数据字典", Url.Action("List", "Dictionary", new { area = "System" }));
            model.CrumbInfo.AddCrumb("新增");

            return View(model);
        }

        /// <summary>
        /// 提交新增页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(DictionaryDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("数据字典", Url.Action("List", "Dictionary", new { area = "System" }, null));
                model.CrumbInfo.AddCrumb("新增");
                return View(model);
            }


            GetService<IDictionaryService>().Add(model);
            return RedirectToAction("List", "Dictionary", new { area = "System" });
        }

        /// <summary>
        /// 编辑页
        /// </summary>
        /// <param name="fieldId"></param>
        /// <returns></returns>
        public ActionResult Edit(int fieldId)
        {
            var model = GetService<IDictionaryService>().GetDetail(fieldId);
            model.CrumbInfo.AddCrumb("数据字典", Url.Action("List", "Dictionary", new { area = "System" }, null));
            model.CrumbInfo.AddCrumb("编辑");

            return View(model);
        }

        /// <summary>
        /// 提交编辑信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(DictionaryDetailModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CrumbInfo.AddCrumb("数据字典", Url.Action("List", "Dictionary", new { area = "System" }, null));
                model.CrumbInfo.AddCrumb("编辑");
                return View(model);
            }

            GetService<IDictionaryService>().Edit(model);
            return RedirectToAction("List", "Dictionary", new { area = "System" });
        }
    }
}