using NHH.Framework.Web;
using NHH.WebSite.Image.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Image.Controllers
{
    /// <summary>
    /// 商户
    /// </summary>
    [AllowAnonymous]
    public class MerchantController : NHHController
    {
        /// <summary>
        /// 上传证件照
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload(int merchantId)
        {
            var result = new FileUploadResult();
            HttpPostedFileBase file = Request.Files["Filedata"];
            int fileLength = file.ContentLength;
            if (file == null || fileLength == 0)
            {
                result.Message = "请选择上传文件";
                return Json(result);
            }

            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileExt = fileInfo.Extension.ToLower();
            //if (fileExt != ".jpg" && fileExt != ".png")
            //{
            //    result.Message = string.Format("不支持上传{0}类型文件", fileExt);
            //    return Json(result);
            //}
            result.FileName = fileInfo.Name;


            //文件原始目录
            var path = BuildDirectory(merchantId, 0);

            Random random = new Random(Guid.NewGuid().GetHashCode());
            string fileName = string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyMMddHHmmss"), random.Next(0, 9), fileExt);
            string filePath = Path.Combine(path, fileName);
            file.SaveAs(filePath);

            if (fileExt == ".jpg" || fileExt == ".png")
            {
                //100
                path = BuildDirectory(merchantId, 100);
                PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 100, 0, "W");
                //800
                path = BuildDirectory(merchantId, 800);
                PictureUtil.CutPicture(filePath, Path.Combine(path, fileName), 800, 0, "W");
                result.FilePath = string.Format("Merchant/Original/{0}/{1}", merchantId, fileName);
                result.FileUrl = string.Format("{0}{1}", result.Domain, result.FilePath.Replace("Original", "S100"));
            }
            else
            {
                result.FilePath = string.Format("Merchant/{0}/{1}", merchantId, fileName);
                result.FileUrl = string.Format("{0}{1}", result.Domain, result.FilePath);
            }
            result.Result = true;
            result.Message = "文件上传成功";
            return Json(result);
        }
        
        /// <summary>
        /// 生成目录
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        private string BuildDirectory(int merchantId, int fileSize)
        {
            //商户文件夹
            string path = HttpContext.Server.MapPath("/Merchant/");
            if (fileSize > 0)
            {
                path = DirectoryUtil.BuildDir(path, string.Format("S{0}", fileSize));
            }
            else
            {
                path = DirectoryUtil.BuildDir(path, "Original");
            }
            path = DirectoryUtil.BuildDir(path, merchantId.ToString());
            return path;
        }
    }
}
