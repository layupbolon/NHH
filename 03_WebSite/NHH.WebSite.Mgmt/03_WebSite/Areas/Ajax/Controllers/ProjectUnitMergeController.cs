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
    /// 铺位合并
    /// </summary>
    [AllowAnonymous]
    public class ProjectUnitMergeController : NHHController
    {

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult CheckNewNumber(int projectId, string unitNumber)
        {
            var ajaxResult = new AjaxResult
            {
                Code = -1,
                Message = string.Format("铺位：{0}不可用", unitNumber),
            };

            if (projectId <= 0)
            {
                ajaxResult.Message = "请选择项目";
                return Json(ajaxResult, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(unitNumber) || unitNumber.Length == 0)
            {
                ajaxResult.Message = "请输入铺位编号";
                return Json(ajaxResult, JsonRequestBehavior.AllowGet);
            }

            var result = GetService<IProjectUnitService>().IsExists(projectId, unitNumber);


            if (result)
            {
                ajaxResult.Message = string.Format("铺位：{0}已存在", unitNumber);
                return Json(ajaxResult, JsonRequestBehavior.AllowGet);
            }

            ajaxResult.Code = 0;
            ajaxResult.Message = string.Format("铺位：{0}可用", unitNumber);

            return Json(ajaxResult, JsonRequestBehavior.AllowGet);
        }

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
            ajaxResult.Code = -1;
            if (string.IsNullOrEmpty(unitNumber) || unitNumber.Length == 0)
            {
                ajaxResult.Message = "请输入铺位编号";
                return Json(ajaxResult, JsonRequestBehavior.AllowGet);
            }

            if (unitInfo.UnitId <= 0)
            {
                ajaxResult.Message = "未查询到相关铺位";
                return Json(ajaxResult, JsonRequestBehavior.AllowGet);
            }

            if (unitInfo.UnitStatus == 4)
            {
                ajaxResult.Message = string.Format("铺位：{0}，已出租", unitNumber);
                return Json(ajaxResult, JsonRequestBehavior.AllowGet);
            }

            ajaxResult.Code = 0;
            ajaxResult.Message = string.Format("铺位：{0}，{1}平方米", unitNumber, unitInfo.UnitArea.ToString("#,##0.00"));

            return Json(ajaxResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 合并铺位
        /// </summary>
        /// <param name="mergeData"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Merge(ProjectUnitMergeData mergeData)
        {
            mergeData.InUser = Context.UserID;
            var result = GetService<IProjectUnitAdjustService>().Merge(mergeData);
            var ajaxResult = new AjaxResult<int>(result.Data);
            ajaxResult.Code = result.Code;
            ajaxResult.Message = result.Desc;
            return Json(ajaxResult, JsonRequestBehavior.DenyGet);
        }
    }
}