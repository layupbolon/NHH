using NHH.Framework.Web;
using NHH.Models.Project;
using NHH.Models.Project.ProjectUnit;
using NHH.Service.Common.IService;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using NHH.Models.Common;

namespace NHH.WebSite.Management.Areas.Project.Controllers
{
    /// <summary>
    /// 楼层平面图
    /// </summary>
    public class FloorMapController : NHHController
    {
        /// <summary>
        /// 楼层平面图
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult Info(FloorMapQueryInfo queryInfo)
        {
            var model = new FloorMapModel
            {
                QueryInfo = queryInfo
            };
            model.CrumbInfo.AddCrumb("项目管理", Url.Action("List", "ProjectInfo", new { area = "Project" }));
            model.CrumbInfo.AddCrumb("商场平面图");

            var projectService = GetService<IProjectCommonService>();

            //获取项目列表，楼层列表
            var projectList = projectService.GetProjectList();
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);
            var buildingList = projectService.GetBuildingList(queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0);
            ViewData["BuildingList"] = new SelectList(buildingList, "Id", "Name", queryInfo.BuildingId.HasValue ? queryInfo.BuildingId.Value : 0);


            model.FloorList = projectService.GetFloorList(queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : 0, queryInfo.BuildingId);
            model.FloorMapUnitList = GetService<IFloorMapService>().GetFloorMapList(queryInfo);
            int floorId = queryInfo.FloorId.HasValue ? queryInfo.FloorId.Value : 0;
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append(@"$('canvas').removeLayers();");
            sbScript.Append(@"$('canvas').drawText({layer: true, name: 'T000', fillStyle: '#000', x: 20, y: 20, maxWidth: 280, fontSize: 14, fontFamily: 'Verdana, sans-serif', fromCenter: false, align: 'left', lineHeight: 1.5, text: '平面图信息：',})");

            if (model.FloorMapUnitList.Count == 0)
            {
                sbScript.Append(@".drawText({layer: true, fillStyle: '#000', x: 550, y: 200, fontSize: 24, fontFamily: 'Verdana, sans-serif', text: '楼层平面图正在建设中'});");
            }
            else
            {
                model.FloorMapUnitList.ForEach(map =>
                {
                    sbScript.Append(".drawPath({layer: true,name:'A" + map.FloorMapUnitID.ToString() + "',fillStyle: '#515050',p1: { type: 'line', " + map.PathLine + "},");
                    if (!string.IsNullOrEmpty(map.PathQuad1))
                    {
                        sbScript.Append("p2: {type: 'quadratic'," + map.PathQuad1 + "},");
                    }
                    if (!string.IsNullOrEmpty(map.PathQuad2))
                    {
                        sbScript.Append("p3: {type: 'quadratic'," + map.PathQuad2 + "},");
                    }
                    if (!string.IsNullOrEmpty(map.PathQuad3))
                    {
                        sbScript.Append("p4: {type: 'quadratic'," + map.PathQuad3 + "},");
                    }
                    if (!string.IsNullOrEmpty(map.PathQuad4))
                    {
                        sbScript.Append("p5: {type: 'quadratic'," + map.PathQuad4 + "},");
                    }
                    sbScript.Append(@"mouseover: function() { $('canvas').setLayer('T000', {text: '平面图信息：ID:" + map.FloorMapUnitID.ToString() + ", 备注： " + map.Comments + "',});},");
                    sbScript.Append(@"mouseout: function() { $('canvas').setLayer('T000', {text: '平面图信息：',});},");
                    sbScript.Append(@"click: function() { getUnitDetail(" + map.FloorMapUnitID.ToString() + "," + floorId.ToString() + ");},");
                    sbScript.Append("})");

                    //店铺ID文字
                    sbScript.Append(@".drawText({ layer: true, fillStyle: '#000'," + map.TextPosition + @", fontSize: 12, fontFamily: 'Verdana, sans-serif', text: 'A" + map.FloorMapUnitID.ToString() + "'})");
                });
            }

            model.FloorMapScript = sbScript.ToString();
            return View(model);
        }

        /// <summary>
        /// 读取相信信息。重点扩展
        /// </summary>
        /// <param name="floorMapUnitId"></param>
        /// <param name="floorId"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult GetFloorMapUnit(int floorMapUnitId, int floorId)
        {
            var service = GetService<IFloorMapService>();
            var floorMapInfo = service.GetFloorMapInfo(floorMapUnitId);

            if (floorMapInfo != null)
            {
                return PartialView("FloorMap/_PartialFloorMapUnitInfo", floorMapInfo);
            }

            var list = service.GetUnitList(new FloorMapQueryInfo { FloorId = floorId });

            return PartialView("FloorMap/_PartialFloorMapUnitBinding", list);
        }

        /// <summary>
        /// 绑定平面图单元ID和商铺ID，解绑为将unitid更新成0
        /// </summary>
        /// <param name="floorMapUnitID"></param>
        /// <param name="unitId"></param>
        [HttpPost]
        public JsonResult BindingFloorMapUnit(int floorMapUnitID, int unitId)
        {
            var info = new FloorMapUnitInfo
            {
                FloorMapUnitID = floorMapUnitID,
                UnitID = unitId,
            };
            GetService<IFloorMapService>().UpdateFloorMapUnit(info);
            var result = new AjaxResult
            {
                Code = 0,
                Message = unitId > 0 ? "绑定成功" : "解绑成功",
            };
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 按照一定的条件选择商铺列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Layout(FloorMapQueryInfo queryInfo)
        {
            List<string> unitlist = new List<string>();
            var list = GetService<IFloorMapService>().GetUnitList(queryInfo);
            if (list != null)
            {
                list.ForEach(item =>
                {
                    if (item.FloorMapUnitID.HasValue)
                    {
                        unitlist.Add(string.Format("A{0}", item.FloorMapUnitID));
                    }
                });
            }
            return Json(new { unitlist = unitlist }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据类型列出按钮列表
        /// </summary>
        /// <param name="conditionName"></param>
        /// <returns></returns>
        [HttpGet]
        public ContentResult LayoutPickup(string conditionName)
        {
            string pickupList = "<dd><a onclick = \"floormap()\" class=\"button button-royal button-rounded button-normal\">初始店铺</a></dd>";
            string[] classlist = {"button button-rounded button-caution",
                                "button button-rounded button-highlight",
                                "button button-rounded button-action",
                                "button button-rounded button-inverse",
                                "button button-rounded button-caution",
                                "button button-rounded button-highlight",
                                "button button-rounded button-action",
                                "button button-rounded button-inverse",
                                "button button-rounded button-caution",
                                "button button-rounded button-highlight" };

            string[] colorlist = { "#22aa88", "#4488cc", "#e44734", "#d52b6f", "#5950ab", "#1493a1", "#a05214", "#851a92", "#da2c2c", "#776328" };

            var list = GetService<ICommonService>().GetCommonFieldList(conditionName);
            int i = 0;
            list.ForEach(item =>
            {
                pickupList += "<dd><a  onclick = \"changeLayoutColor('" + conditionName + "'," + item.FieldValue + ",'" + colorlist[i] + "')\" class='btnColor " + classlist[i] + "'>" + item.FieldName + "</a></dd>";
                i += 1;
            });

            return Content(pickupList);
        }
    }
}
