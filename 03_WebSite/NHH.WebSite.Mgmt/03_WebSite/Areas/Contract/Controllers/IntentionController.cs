using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Operations;
using NHH.Service.Common.IService;
using NHH.Service.Operations.IService;
using NPOI.SS.Formula.Functions;

namespace NHH.WebSite.Management.Areas.Contract.Controllers
{
    public sealed class IntentionController : NHHController
    {
        private IIntentionService intentionService;

        public IntentionController()
        {
            this.intentionService = GetService<IIntentionService>();
        }

        public ActionResult List(IntentionQuery queryInfo)
        {
            IntentionList model = intentionService.GetIntentionList(queryInfo);
            model.CrumbInfo.AddCrumb("入驻意向管理");

            //var projectList = GetService<IProjectCommonService>().GetProjectList();
            //官网会有多个项目，这里先写死
            List<CommonFieldInfo> items = new List<CommonFieldInfo>();
            items.Add(new CommonFieldInfo { FieldName = "大庆昆仑唐人中心" });
            items.Add(new CommonFieldInfo { FieldName = "宜宾唐人财富中心" });
            items.Add(new CommonFieldInfo { FieldName = "上海北蔡唐人生活广场" });
            items.Add(new CommonFieldInfo { FieldName = "章丘唐人中心" });
            items.Add(new CommonFieldInfo { FieldName = "德州唐人中心" });
            items.Add(new CommonFieldInfo { FieldName = "大庆奥莱城" });
            ViewData["ProjectList"] = new SelectList(items, "FieldName", "FieldName", model.QueryInfo.ProjectName);
            //ViewData["ProjectList"] = new SelectList(projectList, "Name", "Name", model.QueryInfo.ProjectName);

            var intentionTypeList = GetService<ICommonService>().GetCommonFieldList("IntentionType");
            ViewData["IntentionTypeList"] = new SelectList(intentionTypeList, "FieldValue", "FieldName",
                model.QueryInfo.IntentionType.HasValue ? model.QueryInfo.IntentionType.Value : 0);

            var intentionStatusList = GetService<ICommonService>().GetCommonFieldList("IntentionStatus");
            ViewData["IntentionStatusList"] = new SelectList(intentionStatusList, "FieldValue", "FieldName",
                model.QueryInfo.Status.HasValue ? model.QueryInfo.Status.Value : 0);
            return View(model);
        }

        public ActionResult Detail(int intentionId)
        {
            var model = intentionService.GetIntention(intentionId);
            model.CrumbInfo.AddCrumb("入驻意向管理", Url.Action("List", "Intention", new { area = "Contract" }));
            model.CrumbInfo.AddCrumb("入驻意向单据详情");
            return View(model);
        }

        public JsonResult Process(int intentionId)
        {
            var model = new IntentionInfo();
            model.ProcessUserID = Context.UserID;
            model.IntentionID = intentionId;
            return Json(intentionService.ProcessIntention(model));
            //if (intentionService.ProcessIntention(model))
            //    return RedirectToAction("Detail", "Intention", new { area = "Contract", intentionId = intentionId });
            //else
            //    return View();
        }
    }
}