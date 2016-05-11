using NhhDataImport.Entity;
using NhhDataImport.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 图片处理
    /// </summary>
    public abstract class PictureProcess<T>
        where T : BaseEntity
    {
        protected BackgroundWorker _worker;
        protected string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
        protected string ReProcess = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReProcess/");
        protected string Processed = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Processed/");

        /// <summary>
        /// 处理图片
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="worker"></param>
        public virtual void ProcessPicture(T entity, BackgroundWorker worker)
        {
            if (worker == null)
                return;

            _worker = worker;

            try
            {
                ValidData(entity);
            }
            catch (NhhException ex)
            {
                _worker.ReportProgress(10, ex.Message);
                return;
            }

            var fileNames = GetFileNames(entity);
            if (string.IsNullOrEmpty(fileNames) || fileNames.Length == 0)
                return;

            string[] fileList = fileNames.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (fileList.Length == 0)
                return;

            var filePath = string.Empty;
            var newFileNames = new List<string>();
            var newFileName = string.Empty;
            foreach (string file in fileList)
            {
                filePath = ReProcess + file;

                if (!File.Exists(filePath))
                {
                    _worker.ReportProgress(10, string.Format("{0} 文件不存在", file));
                    continue;
                }
                newFileName = UploadPicture(entity, filePath);
                newFileNames.Add(newFileName);
            }
            UpdateData(entity, newFileNames);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected string UploadPicture(T entity, string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            string path = GetUploadPath(entity, 0);
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string fileName = string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyMMddHHmmss"), random.Next(0, 9), fileInfo.Extension);
            string newFilePath = Path.Combine(path, fileName);
            File.Move(filePath, newFilePath);

            path = GetUploadPath(entity, 800);
            PictureUtil.CutPicture(newFilePath, Path.Combine(path, fileName), 800, 0, "W");

            path = GetUploadPath(entity, 100);
            PictureUtil.CutPicture(newFilePath, Path.Combine(path, fileName), 100, 0, "W");

            return GetNewFilePath(entity, fileName);
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected abstract void ValidData(T entity);

        /// <summary>
        /// 获取文件名称
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected abstract string GetFileNames(T entity);
        
        /// <summary>
        /// 获取上传路径
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        protected abstract string GetUploadPath(T entity, int fileSize);

        /// <summary>
        /// 获取新的文件名
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected abstract string GetNewFilePath(T entity, string fileName);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newFileNames"></param>
        protected abstract void UpdateData(T entity, List<string> newFileNames);

    }
}
