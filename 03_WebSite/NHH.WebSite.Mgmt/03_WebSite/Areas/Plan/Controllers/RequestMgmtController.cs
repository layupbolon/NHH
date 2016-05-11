using NHH.Framework.Web;
using NHH.Models.Plan;
using NHH.Service.Plan.IService;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Plan.Controllers
{
    public class RequestMgmtController : NHHController
    {
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult Export(ProjectUnitRequestListQueryInfo queryInfo)
        {
            queryInfo.CurrentUserId = Context.UserID;
            var requestList = GetService<IProjectUnitRequestService>().GetRequestList(queryInfo);

            #region 生成Excel模板
            var book = new HSSFWorkbook();
            var sheet1 = book.CreateSheet(DateTime.Now.ToString("yyyy-MM-dd"));

            IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("楼宇");
            row1.CreateCell(2).SetCellValue("楼层");
            row1.CreateCell(3).SetCellValue("铺位ID");
            row1.CreateCell(4).SetCellValue("铺位编号");
            row1.CreateCell(5).SetCellValue("铺位面积");
            row1.CreateCell(6).SetCellValue("铺位类型");
            row1.CreateCell(7).SetCellValue("业态");
            row1.CreateCell(8).SetCellValue("品类");
            row1.CreateCell(9).SetCellValue("品牌");
            row1.CreateCell(10).SetCellValue("租决状态");
            row1.CreateCell(11).SetCellValue("租金标准（元/天/平方米）");
            row1.CreateCell(12).SetCellValue("物业费标准（元/天/平方米）");
            row1.CreateCell(13).SetCellValue("月租金（元/月/平方米）");
            row1.CreateCell(14).SetCellValue("月物业费（元/月/平方米）");
            row1.CreateCell(15).SetCellValue("收益合计（万元）");


            int num = 1;
            double yearTotalAmount = 0d;
            //铺位面积
            decimal unitArea = 0m;

            requestList.ForEach(request =>
            {
                IRow row = sheet1.CreateRow(num);
                row.CreateCell(0).SetCellValue(num);
                row.CreateCell(1).SetCellValue(request.BuildingName);
                row.CreateCell(2).SetCellValue(request.FloorName);
                row.CreateCell(3).SetCellValue(request.UnitId);
                row.CreateCell(4).SetCellValue(request.UnitNumber);

                unitArea = request.RequestArea;
                if (unitArea <= 0)
                    unitArea = request.UnitArea;

                row.CreateCell(5).SetCellValue((double)unitArea);
                row.CreateCell(6).SetCellValue(request.UnitTypeName);
                row.CreateCell(7).SetCellValue(request.BizTypeName);
                row.CreateCell(8).SetCellValue(request.BizCategoryName);
                row.CreateCell(9).SetCellValue(request.BrandName);
                row.CreateCell(10).SetCellValue(request.RequestStatusName);
                row.CreateCell(11).SetCellValue((double)request.StandardRent);
                row.CreateCell(12).SetCellValue((double)request.StandardMgmtFee);
                row.CreateCell(13).SetCellValue(((double)request.StandardRent * 30));
                row.CreateCell(14).SetCellValue(((double)request.StandardMgmtFee * 30));
                yearTotalAmount = (double)(request.StandardRent * unitArea * 360 + request.StandardMgmtFee * unitArea * 360) / 10000;
                row.CreateCell(15).SetCellValue(Math.Round(yearTotalAmount, 2, MidpointRounding.AwayFromZero));
                num += 1;
            });

            #endregion

            // 输出Excel
            HttpContext.Response.Clear();
            string filename = string.Format("{0}_{1}_招商租决_{2}.xls", queryInfo.ProjectName, queryInfo.ProjectId.Value, DateTime.Now.ToString("yyyyMMdd"));
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
        /// 导入铺位筹划列表
        /// </summary>
        /// <returns></returns>
        public ContentResult Import()
        {
            var list = new List<ProjectUnitRequestInfo>();

            HttpPostedFileBase file = Request.Files["RequestExcelFile"];
            if (file.FileName.IndexOf(".xls") < 0)
            {
                return Content("文件类型错误，请上传Excel格式的文件");
            }

            #region 读取Excel内容
            int unitId, count = 0;
            var result = "";

            using (var stream = file.InputStream)
            {
                var workbook = new HSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet != null)
                {
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        var row = sheet.GetRow(i);

                        try
                        {
                            var info = new ProjectUnitRequestInfo();
                            var dataValidate = int.TryParse(row.GetCell(3).NumericCellValue.ToString(), out unitId);

                            if (!dataValidate || unitId <= 0)
                                continue;

                            info.UnitId = unitId;
                            info.RequestArea = (decimal)row.GetCell(5).NumericCellValue;
                            info.UnitTypeName = row.GetCell(6).StringCellValue;
                            info.BizTypeName = row.GetCell(7).StringCellValue;
                            info.BizCategoryName = row.GetCell(8).StringCellValue;
                            info.BrandName = row.GetCell(9).StringCellValue;
                            info.RequestStatusName = row.GetCell(10).StringCellValue;
                            info.StandardRent = (decimal)row.GetCell(11).NumericCellValue;
                            info.StandardMgmtFee = (decimal)row.GetCell(12).NumericCellValue;

                            list.Add(info);
                            count += 1;
                        }
                        catch (Exception ex)
                        {
                            result += string.Format("失败行号：{0}，原因：{1}\n", i, ex.Message);
                        }
                    }
                }
                sheet = null;
                workbook = null;
                stream.Close();
            }
            #endregion
            var messageBag = GetService<IProjectUnitRequestService>().Import(list);
            result += messageBag.Desc;
            return Content(result);
        }
    }
}