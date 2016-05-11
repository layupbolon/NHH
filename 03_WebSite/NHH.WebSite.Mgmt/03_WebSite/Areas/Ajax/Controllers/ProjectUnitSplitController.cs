using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Project.ProjectUnit;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax.Controllers
{
    /// <summary>
    /// 铺位拆分
    /// </summary>
    [AllowAnonymous]
    public class ProjectUnitSplitController : NHHController
    {

        /// <summary>
        /// 获取铺位信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUnitInfo(int projectId, string unitNumber)
        {
            var unitInfo = GetService<IProjectUnitService>().GetCommonInfo(projectId, unitNumber);
            var ajaxResult = new AjaxResult<ProjectUnitCommonInfo>(unitInfo);
            if (unitInfo.UnitId <= 0)
            {
                ajaxResult.Code = -1;
                ajaxResult.Message = "未查询到相关铺位";
                return Json(ajaxResult, JsonRequestBehavior.AllowGet);
            }

            if (unitInfo.UnitStatus == 4)
            {
                ajaxResult.Code = -1;
                ajaxResult.Message = string.Format("铺位：{0}，已出租", unitNumber);
                return Json(ajaxResult, JsonRequestBehavior.AllowGet);
            }

            ajaxResult.Message = string.Format("铺位：{0}，{1}平方米", unitNumber, unitInfo.UnitArea.ToString("#,##0.00"));

            return Json(ajaxResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 铺位拆分
        /// </summary>
        /// <param name="splitData"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Split(ProjectUnitSplitData splitData)
        {
            splitData.InUser = Context.UserID;
            var result = GetService<IProjectUnitAdjustService>().Split(splitData);
            var ajaxResult = new AjaxResult
            {
                Code = result.Code,
                Message = result.Desc,
            };
            return Json(ajaxResult, JsonRequestBehavior.DenyGet);
        }
    }
}