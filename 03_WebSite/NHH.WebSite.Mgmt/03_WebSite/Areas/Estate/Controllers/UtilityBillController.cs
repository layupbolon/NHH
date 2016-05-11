using Newtonsoft.Json;
using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Estate;
using NHH.Service.Common.IService;
using NHH.Service.Configure.IService;
using NHH.Service.Estate.IService;
using NHH.Service.Merchant.IService;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Estate.Controllers
{
    /// <summary>
    /// 水电煤气费抄表
    /// </summary>
    public class UtilityBillController : NHHController
    {
        /// <summary>
        /// 列表页
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult List(UtilityBillQueryInfo queryInfo)
        {
            var model = GetService<IUtilityBillService>().GetList(queryInfo);
            model.CrumbInfo.AddCrumb("水电煤气抄表");


            var meterTypeList = GetService<ICommonService>().GetCommonFieldList("MeterType");
            ViewData["MeterTypeList"] = new SelectList(meterTypeList, "FieldValue", "FieldName", queryInfo.Type.HasValue ? queryInfo.Type.Value : 0);

            int projectId = 0;
            var projectList = GetService<IProjectCommonService>().GetProjectList(Context.UserID);
            if (projectList != null && projectList.Count > 0)
            {
                projectId = projectList[0].Id;
            }
            projectId = queryInfo.ProjectId.HasValue ? queryInfo.ProjectId.Value : projectId;
            ViewData["ProjectList"] = new SelectList(projectList, "Id", "Name", projectId);

            var buildingList = GetService<IProjectCommonService>().GetBuildingList(projectId);
            ViewData["BuildingList"] = new SelectList(buildingList, "Id", "Name", queryInfo.BuildingId.HasValue ? queryInfo.BuildingId.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 下载模板
        /// </summary>
        /// <param name="type"></param>
        /// <param name="projectId"></param>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public ActionResult Download(int type,int projectId, int? buildingId)
        {
            var meterList = GetService<IStoreMeterService>().GetMeterList(type, projectId, buildingId);
            var book = new HSSFWorkbook();

            var sheetName = GetService<IDictionaryService>().GetFieldName("MeterType", type);

            #region 生成水费Excel模板
            // 新建一个Excel页签
            var sheet1 = book.CreateSheet(sheetName);

            IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("商户ID");
            row1.CreateCell(1).SetCellValue("商户名称");
            row1.CreateCell(2).SetCellValue("商铺ID");
            row1.CreateCell(3).SetCellValue("商铺名称");
            row1.CreateCell(4).SetCellValue("铺位编号");
            row1.CreateCell(5).SetCellValue("计量表编号");
            row1.CreateCell(6).SetCellValue("参数");
            row1.CreateCell(7).SetCellValue("上期抄表时间");
            row1.CreateCell(8).SetCellValue("上期抄表数");
            row1.CreateCell(9).SetCellValue("本期抄表时间");
            row1.CreateCell(10).SetCellValue("本期抄表数");
            row1.CreateCell(11).SetCellValue("实用表数");
            row1.CreateCell(12).SetCellValue("单价");
            row1.CreateCell(13).SetCellValue("抄表金额");
            row1.CreateCell(14).SetCellValue("备注");

            int num = 1;
            meterList.ForEach(meter =>
            {
                IRow row = sheet1.CreateRow(num);
                row.CreateCell(0).SetCellValue(meter.MerchantID);
                row.CreateCell(1).SetCellValue(meter.MerchantName);
                row.CreateCell(2).SetCellValue(meter.StoreID);
                row.CreateCell(3).SetCellValue(meter.StoreName);
                row.CreateCell(4).SetCellValue(meter.UnitNumber);
                row.CreateCell(5).SetCellValue(meter.MeterCode);
                row.CreateCell(6).SetCellValue(meter.MeterAttr);
                row.CreateCell(7).SetCellValue(meter.LastReading.HasValue ? meter.LastReading.Value.ToString("yyyy-MM-dd") : "");
                row.CreateCell(8).SetCellValue((double)meter.LastNumber);
                row.CreateCell(9).SetCellValue(DateTime.Now.ToString("yyyy-MM-dd"));
                row.CreateCell(10).SetCellValue(0);
                row.CreateCell(11).SetCellValue(0);
                row.CreateCell(12).SetCellValue(0);
                row.CreateCell(13).SetCellValue(0);
                row.CreateCell(14).SetCellValue("");
                num += 1;
            });
            #endregion
            // 输出Excel
            HttpContext.Response.Clear();
            string filename = string.Format("Project_{0}_{1}_{2}.xls", projectId, Context.UserID, DateTime.Now.ToString("yyyyMMdd"));
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
        /// 上传
        /// </summary>
        /// <returns></returns>
        public ContentResult Upload()
        {
            var result = new AjaxResult();
            var model = new UtilityBillListModel();
            model.UtilityBillList = new List<UtilityBillInfo>();

            HttpPostedFileBase file = Request.Files["ExcelFile"];
            if (file.FileName.IndexOf(".xls") < 0)
            {
                result.Code = -1;
                result.Message = "文件类型错误，请上传Excel格式的文件";
                return Content(JsonConvert.SerializeObject(result));
            }

            #region 读取Excel内容
            result.Message = "";
            using (var stream = file.InputStream)
            {
                var workbook = new HSSFWorkbook(stream);
                var sheetName = workbook.GetSheetName(0);
                var meterType = GetService<IDictionaryService>().GetFieldValue("MeterType", sheetName);
                //处理数据
                GetUtilityBillList(model, meterType, workbook.GetSheetAt(0));                
                workbook = null;
                stream.Close();
            }
            #endregion
            GetService<IUtilityBillService>().Add(model);
            result.Code = 0;
            result.Message += string.Format("已成功导入{0}条数据", model.UtilityBillList.Count);
            return Content(JsonConvert.SerializeObject(result));//此处不用Json
        }

        /// <summary>
        /// 读取Excel表中的水电煤气费信息列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="meterType"></param>
        /// <param name="sheet"></param>
        private void GetUtilityBillList(UtilityBillListModel model, int meterType, ISheet sheet)
        {
            if (sheet == null)
                return;

            int storeId = 0;
            string meterCode = string.Empty;
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            decimal preNumber = 0m;
            decimal curNumber = 0m;
            decimal useNumber = 0m;
            decimal unitPrice = 0m;
            decimal billAmount = 0m;
            
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);

                try
                {
                    #region 数据校验
                    var dataValidate = int.TryParse(row.GetCell(2).NumericCellValue.ToString(), out storeId) &&
                                       decimal.TryParse(row.GetCell(8).NumericCellValue.ToString(), out preNumber) &&
                                       decimal.TryParse(row.GetCell(10).NumericCellValue.ToString(), out curNumber) &&
                                       decimal.TryParse(row.GetCell(11).NumericCellValue.ToString(), out useNumber) &&
                                       decimal.TryParse(row.GetCell(12).NumericCellValue.ToString(), out unitPrice) &&
                                       decimal.TryParse(row.GetCell(13).NumericCellValue.ToString(), out billAmount);

                    if (!dataValidate || storeId <= 0)
                        continue;

                    //excel表格时间cell编辑过，它的字段类型会自动转换为numeric格式

                    var cell = row.GetCell(7);

                    dataValidate = cell.CellType == CellType.Numeric ? DateTime.TryParse(cell.DateCellValue.ToString(), out startDate) : DateTime.TryParse(cell.StringCellValue, out startDate);
                    if (!dataValidate)
                    {
                        continue;
                    }
                    cell = row.GetCell(9);
                    dataValidate = cell.CellType == CellType.Numeric ? DateTime.TryParse(cell.DateCellValue.ToString(), out endDate) : DateTime.TryParse(cell.StringCellValue, out endDate);
                    if (!dataValidate)
                    {
                        continue;
                    }
                    #endregion

                    meterCode = row.GetCell(5).StringCellValue;

                    var info = new UtilityBillInfo();
                    info.StoreID = storeId;
                    info.MeterType = meterType;
                    info.MeterCode = meterCode;
                    info.StartDate = startDate;
                    info.EndDate = endDate;
                    info.PrevNumber = preNumber;
                    info.CurNumber = curNumber;
                    info.UseNumber = useNumber;
                    info.UnitPrice = unitPrice;
                    info.BillAmount = billAmount;
                    info.Remark = row.GetCell(14).StringCellValue;
                    info.InUser = Context.UserID;
                    info.OperatorID = Context.UserID;
                    info.OperatorName = Context.UserName;
                    model.UtilityBillList.Add(info);
                }
                catch
                {
                }
            }
        }

    }
}