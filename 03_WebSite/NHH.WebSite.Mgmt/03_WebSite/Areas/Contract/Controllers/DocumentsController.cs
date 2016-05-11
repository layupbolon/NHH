using System;
using System.Web.Mvc;
using NHH.Framework.Web;
using NHH.Models.Approve;
using NHH.Models.Contract;
using NHH.Service.Common.IService;
using NHH.Service.Contract.IService;

namespace NHH.WebSite.Management.Areas.Contract.Controllers
{
    public class DocumentsController : NHHController
    {
        private readonly IDocumentsService documentsService;
        private readonly ICommonService commonService;

        public DocumentsController()
        {
            documentsService = GetService<IDocumentsService>();
            commonService = GetService<ICommonService>();
        }

        /// <summary>
        /// 单据列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(DocumentsQueryInfo queryInfo)
        {
            var model = documentsService.GetDocumentsList(queryInfo);
            model.CrumbInfo.AddCrumb("特殊单据");

            var documentTypeList = commonService.GetCommonFieldList("DocumentType");
            ViewData["DocumentTypeList"] = new SelectList(documentTypeList, "FieldValue", "FieldName", queryInfo.DocumentType ?? 0);

            var documentStatusList = commonService.GetCommonFieldList("DocumentStatus");
            ViewData["DocumentStatusList"] = new SelectList(documentStatusList, "FieldValue", "FieldName", queryInfo.Status ?? 0);

            return View(model);
        }

        /// <summary>
        /// 出门申请单
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public ActionResult GetOutDetail(int documentID)
        {
            var model = documentsService.GetOutDetaiInfo(documentID);
            model.CrumbInfo.AddCrumb("特殊单据", Url.Action("List", "Documents", new { area = "Contract" }));
            model.CrumbInfo.AddCrumb("出门申请单");

            return View(model);
        }

        /// <summary>
        /// 装修申请单
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public ActionResult DecorateDetail(int documentID)
        {
            var model = documentsService.GetDecorateDetailInfo(documentID);
            model.CrumbInfo.AddCrumb("特殊单据", Url.Action("List", "Documents", new { area = "Contract" }));
            model.CrumbInfo.AddCrumb("装修申请单");

            return View(model);
        }

        /// <summary>
        /// 延时经营申请单
        /// </summary>
        /// <param name="documentID"></param>
        /// <returns></returns>
        public ActionResult DelayOperateDetail(int documentID)
        {
            var model = documentsService.GetDelayOperateDetailInfo(documentID);
            model.CrumbInfo.AddCrumb("特殊单据", Url.Action("List", "Documents", new { area = "Contract" }));
            model.CrumbInfo.AddCrumb("延时经营申请单");

            return View(model);
        }

        /// <summary>
        /// 添加审批意见
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Approve(ApproveModel approve)
        {
            approve.ApproveTime = DateTime.Now;
            approve.UserID = Context.UserID;
            var result = documentsService.Approve(approve);
            return Json(result);
        }
    }
}