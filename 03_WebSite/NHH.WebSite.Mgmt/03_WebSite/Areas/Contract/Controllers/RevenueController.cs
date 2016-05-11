using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Merchant;
using NHH.Service.Merchant.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using NHH.Service.Common.IService;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using Newtonsoft.Json;
namespace NHH.WebSite.Management.Areas.Contract.Controllers
{
    /// <summary>
    /// 商户销售管理
    /// </summary>
    public class RevenueController : NHHController
    {
        /// <summary>
        /// 商铺销售列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(MerchantRevenueListQueryInfo queryInfo)
        {
            var model = GetService<IMerchantRevenueService>().GetRevenueReport(queryInfo);
            model.CrumbInfo.AddCrumb("日销售额管理");

            int projectId = 0;
            var projectService = GetService<IProjectCommonService>();
            var projectList = projectService.GetProjectList();
            if (projectList != null && projectList.Count > 0)
            {
                projectId = projectList[0].Id;
            }
            projectId = queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : projectId;
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", projectId);

            int buildingId = 0;
            var buildingList = projectService.GetBuildingList(projectId);
            if (buildingList != null && buildingList.Count > 0)
            {
                buildingId = buildingList[0].Id;
            }
            buildingId = queryInfo.BuildingId.HasValue ? queryInfo.BuildingId.Value : buildingId;
            ViewData["BuildingList"] = new SelectList(buildingList, "Id", "Name", buildingId);

            var floorList = projectService.GetFloorList(projectId, buildingId);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName", queryInfo.FloorId.HasValue ? queryInfo.FloorId.Value : 0);

            var commonService = GetService<ICommonService>();
            var unitTypeList = commonService.GetUnitTypeList();
            ViewData["UnitTypeList"] = new SelectList(unitTypeList, "Id", "Name", queryInfo.UnitType.HasValue ? queryInfo.UnitType.Value : 0);

            var bizTypeList = commonService.GetBizTypeList();
            ViewData["BizTypeList"] = new SelectList(bizTypeList, "Id", "Name", queryInfo.BizType.HasValue ? queryInfo.BizType.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 商铺列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult StoreList(MerchantRevenueListQueryInfo queryInfo)
        {
            var model = new MerchantRevenueListModel();
            model.QueryInfo = queryInfo;
            model.StoreList = GetService<IMerchantRevenueService>().GetRevenueTemplate(queryInfo);
            model.CrumbInfo.AddCrumb("日销售额管理");

            int projectId = 0;
            var projectService = GetService<IProjectCommonService>();
            var projectList = projectService.GetProjectList();
            if (projectList != null && projectList.Count > 0)
            {
                projectId = projectList[0].Id;
            }
            projectId = queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : projectId;
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", projectId);

            int buildingId = 0;
            var buildingList = projectService.GetBuildingList(projectId);
            if (buildingList != null && buildingList.Count > 0)
            {
                buildingId = buildingList[0].Id;
            }
            buildingId = queryInfo.BuildingId.HasValue ? queryInfo.BuildingId.Value : buildingId;
            ViewData["BuildingList"] = new SelectList(buildingList, "Id", "Name", buildingId);

            var floorList = projectService.GetFloorList(projectId, buildingId);
            ViewData["FloorList"] = new SelectList(floorList, "FloorId", "FloorName", queryInfo.FloorId.HasValue ? queryInfo.FloorId.Value : 0);

            var commonService = GetService<ICommonService>();
            var unitTypeList = commonService.GetUnitTypeList();
            ViewData["UnitTypeList"] = new SelectList(unitTypeList, "Id", "Name", queryInfo.UnitType.HasValue ? queryInfo.UnitType.Value : 0);

            var bizTypeList = commonService.GetBizTypeList();
            ViewData["BizTypeList"] = new SelectList(bizTypeList, "Id", "Name", queryInfo.BizType.HasValue ? queryInfo.BizType.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 提交商铺销售编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(MerchantRevenueDetailModel model)
        {
            var result = new AjaxResult();
            var flag = GetService<IMerchantRevenueService>().UpdateMerchantRevenue(model);
            if (!flag)
            {
                result.Code = -1;
                result.Message = "保存失败";
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 下载模板
        /// </summary>
        /// <param name="queryInfo">项目Id</param>
        /// <returns></returns>
        public ActionResult Download(MerchantRevenueListQueryInfo queryInfo)
        {
            queryInfo.CurrentUserId = Context.UserID;
            var storeList = GetService<IMerchantRevenueService>().GetRevenueTemplate(queryInfo);

            #region 生成Excel模板
            var book = new HSSFWorkbook();
            var sheet1 = book.CreateSheet(DateTime.Now.ToString("yyyy-MM-dd"));

            IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("业态");
            row1.CreateCell(2).SetCellValue("铺位编号");
            row1.CreateCell(3).SetCellValue("商铺ID");
            row1.CreateCell(4).SetCellValue("商铺名称");
            int cellNumber = 5;
            foreach (var day in queryInfo.DayList)
            {
                row1.CreateCell(cellNumber).SetCellValue(day);
                cellNumber += 1;
            }

            int num = 1;
            storeList.ForEach(store =>
            {
                IRow row = sheet1.CreateRow(num);
                row.CreateCell(0).SetCellValue(num);
                row.CreateCell(1).SetCellValue(store.BizTypeName);
                row.CreateCell(2).SetCellValue(store.ProjectUnitList);
                row.CreateCell(3).SetCellValue(store.StoreId);
                row.CreateCell(4).SetCellValue(store.StoreName);
                cellNumber = 5;
                foreach (var revenue in store.RevenueList)
                {
                    row.CreateCell(cellNumber).SetCellValue((double)revenue.Revenue);
                    cellNumber += 1;
                }

                num += 1;
            });

            #endregion
            // 输出Excel
            HttpContext.Response.Clear();
            string filename = string.Format("{0}.xls", DateTime.Now.ToString("yyyyMMdd"));
            MemoryStream file = new MemoryStream();
            book.Write(file);
            HttpContext.Response.BinaryWrite(file.GetBuffer());
            HttpContext.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(filename));
            HttpContext.Response.Flush();
            HttpContext.Response.End();
            return new EmptyResult();
        }

        /// <summary>
        /// 获取数据流
        /// </summary>
        /// <returns></returns>
        public ContentResult GetExeclStreamUpLoad()
        {
            var result = new AjaxResult();
            var model = new MerchantRevenueListModel();
            model.QueryInfo = new MerchantRevenueListQueryInfo();
            model.QueryInfo.CurrentUserId = Context.UserID;
            model.StoreList = new List<MerchantRevenueStoreInfo>();

            HttpPostedFileBase file = Request.Files["ReveExcelFile"];
            if (file.FileName.IndexOf(".xls") < 0)
            {
                result.Code = -1;
                result.Message = "文件类型错误，请上传Excel格式的文件";
                return Content(JsonConvert.SerializeObject(result));
            }

            #region 读取Excel内容
            int storeId = 0;
            decimal revenue = 0m;

            result.Message = "";
            int count = 0;
            using (var stream = file.InputStream)
            {
                var workbook = new HSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    //日期
                    var row0 = sheet.GetRow(0);
                    var cellNum = row0.Cells.Count;

                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        var row = sheet.GetRow(i);

                        try
                        {
                            var store = new MerchantRevenueStoreInfo();
                            var dataValidate = int.TryParse(row.GetCell(3).NumericCellValue.ToString(), out storeId);

                            if (!dataValidate || storeId <= 0)
                                continue;

                            store.StoreId = storeId;
                            store.StoreName = row.GetCell(4).StringCellValue;
                            store.RevenueList = new List<MerchantRevenueItem>();
                            //每个商铺每天的日销售额
                            for (int n = 5; n < cellNum; n++)
                            {
                                var revenueItem = new MerchantRevenueItem
                                {
                                    Date = Convert.ToDateTime(row0.GetCell(n).StringCellValue),
                                    TaxRate = 1
                                };

                                decimal.TryParse(row.GetCell(n).NumericCellValue.ToString(), out revenue);
                                revenueItem.Revenue = revenue;
                                store.RevenueList.Add(revenueItem);
                            }

                            model.StoreList.Add(store);
                            count += 1;
                        }
                        catch (Exception ex)
                        {
                            result.Message += string.Format("失败行号：{0}，原因：{1}\n", i, ex.Message);
                        }
                    }
                }
                sheet = null;
                workbook = null;
                stream.Close();
            }
            #endregion
            GetService<IMerchantRevenueService>().AddMerchantRevenue(model);
            result.Code = 0;
            result.Message += string.Format("已成功导入{0}条数据", count);
            return Content(JsonConvert.SerializeObject(result));//此处不用Json
        }

    }
}