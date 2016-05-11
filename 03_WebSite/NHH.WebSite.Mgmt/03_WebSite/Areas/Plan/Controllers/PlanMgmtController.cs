using NHH.Framework.Web;
using NHH.Models.Common;
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
    /// <summary>
    /// 铺位筹划管理
    /// </summary>
    public class PlanMgmtController : NHHController
    {
        /// <summary>
        /// 导出铺位筹划列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ActionResult Export(ProjectPlanListQueryInfo queryInfo)
        {
            queryInfo.CurrentUserId = Context.UserID;
            var planList = GetService<IProjectPlanMgmtService>().GetUnitPlanList(queryInfo);

            #region 生成Excel模板
            var book = new HSSFWorkbook();
            var sheet1 = book.CreateSheet(DateTime.Now.ToString("yyyy-MM-dd"));

            IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("楼宇");
            row1.CreateCell(2).SetCellValue("楼层");
            row1.CreateCell(3).SetCellValue("铺位ID");
            row1.CreateCell(4).SetCellValue("铺位编号");
            row1.CreateCell(5).SetCellValue("计租面积");
            row1.CreateCell(6).SetCellValue("类型");
            row1.CreateCell(7).SetCellValue("业态");
            row1.CreateCell(8).SetCellValue("品类");
            row1.CreateCell(9).SetCellValue("招商批次");
            row1.CreateCell(10).SetCellValue("租金标准（元/天/平方米）");
            row1.CreateCell(11).SetCellValue("物业费标准（元/天/平方米）");
            row1.CreateCell(12).SetCellValue("月租金（元/月/平方米）");
            row1.CreateCell(13).SetCellValue("月物业费（元/月/平方米）");
            row1.CreateCell(14).SetCellValue("收益合计（万元）");


            int num = 1;
            double yearTotalAmount = 0d;
            planList.ForEach(plan =>
            {
                IRow row = sheet1.CreateRow(num);
                row.CreateCell(0).SetCellValue(num);
                row.CreateCell(1).SetCellValue(plan.BuindingName);
                row.CreateCell(2).SetCellValue(plan.FloorName);
                row.CreateCell(3).SetCellValue(plan.UnitId);
                row.CreateCell(4).SetCellValue(plan.UnitNumber);
                row.CreateCell(5).SetCellValue((double)plan.UnitArea);
                row.CreateCell(6).SetCellValue(plan.UnitTypeName);
                row.CreateCell(7).SetCellValue(plan.BizTypeName);
                row.CreateCell(8).SetCellValue(plan.BizCategoryName);
                row.CreateCell(9).SetCellValue(plan.BatchCode);
                row.CreateCell(10).SetCellValue((double)plan.StandardRent);
                row.CreateCell(11).SetCellValue((double)plan.StandardMgmtFee);
                row.CreateCell(12).SetCellValue(((double)plan.StandardRent * 30));
                row.CreateCell(13).SetCellValue(((double)plan.StandardMgmtFee * 30));
                yearTotalAmount = (double)(plan.StandardRent * plan.UnitArea * 360 + plan.StandardMgmtFee * plan.UnitArea * 360) / 10000;
                row.CreateCell(14).SetCellValue(Math.Round(yearTotalAmount, 2, MidpointRounding.AwayFromZero));
                num += 1;
            });

            #endregion

            // 输出Excel
            HttpContext.Response.Clear();
            string filename = string.Format("{0}_招商筹划_{1}.xls", queryInfo.ProjectName, DateTime.Now.ToString("yyyyMMdd"));
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
            var list = new List<ProjectUnitPlanInfo>();

            HttpPostedFileBase file = Request.Files["PlanExcelFile"];
            if (file.FileName.IndexOf(".xls") < 0)
            {
                return Content("文件类型错误，请上传Excel格式的文件");
            }

            #region 读取Excel内容
            int unitId = 0;
            decimal d = 0m;

            int count = 0;
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
                            var info = new ProjectUnitPlanInfo();
                            var dataValidate = int.TryParse(row.GetCell(3).NumericCellValue.ToString(), out unitId);

                            if (!dataValidate || unitId <= 0)
                                continue;


                            info.UnitId = unitId;
                            info.UnitArea = (decimal)row.GetCell(5).NumericCellValue;
                            info.UnitTypeName = row.GetCell(6).StringCellValue;
                            info.BizTypeName = row.GetCell(7).StringCellValue;
                            info.BizCategoryName = row.GetCell(8).StringCellValue;
                            info.BatchCode = row.GetCell(9).StringCellValue;
                            info.StandardRent = (decimal)row.GetCell(10).NumericCellValue;
                            info.StandardMgmtFee = (decimal)row.GetCell(11).NumericCellValue;

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
            var messageBag = GetService<IProjectPlanMgmtService>().Import(list);
            result += messageBag.Desc;
            return Content(result);
        }
    }
}