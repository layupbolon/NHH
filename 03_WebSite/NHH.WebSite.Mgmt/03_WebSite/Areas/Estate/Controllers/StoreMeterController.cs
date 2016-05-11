using NHH.Framework.Web;
using NHH.Models.Merchant;
using NHH.Service.Common.IService;
using NHH.Service.Merchant.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Estate.Controllers
{
    /// <summary>
    /// 商铺计量表
    /// </summary>
    public class StoreMeterController : NHHController
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(StoreMeterListQueryInfo queryInfo)
        {
            var model = GetService<IStoreMeterService>().GetList(queryInfo);
            model.CrumbInfo.AddCrumb("商铺计量表管理");
            
            var projectList = GetService<IProjectCommonService>().GetProjectList(Context.UserID);
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 新增计量表
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public ActionResult Add(int storeId)
        {
            var model = new StoreMeterDetailModel();
            model.StoreID = storeId;
            model.StoreName = GetService<IMerchantStoreService>().GetStoreName(storeId);
            model.CrumbInfo.AddCrumb("商铺计量表", Url.Action("List", "StoreMeter", new { area = "Estate" }));
            model.CrumbInfo.AddCrumb("新增计量表");

            var meterTypeList = GetService<ICommonService>().GetCommonFieldList("MeterType");
            ViewData["MeterTypeList"] = new SelectList(meterTypeList, "FieldValue", "FieldName");

            return View(model);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(StoreMeterInfo info)
        {
            info.InUser = Context.UserID;
            GetService<IStoreMeterService>().Add(info);
            return RedirectToAction("List", "StoreMeter", new { area = "Estate", storeId = info.StoreID, storeName = info.StoreName });
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns></returns>
        public ActionResult Edit(int meterId)
        {
            return View();
        }
    }
}