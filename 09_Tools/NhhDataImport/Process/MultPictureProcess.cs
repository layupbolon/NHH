using NhhDataImport.Entity;
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
    /// 多图片处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MultPictureProcess<T> : PictureProcess<T>
        where T : BaseEntity
    {
        protected string DefaultFileType = "";

        /// <summary>
        /// 处理图片
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="worker"></param>
        public override void ProcessPicture(T entity, BackgroundWorker worker)
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

            //清空之前的附件
            ClearData(entity);

            int fileType = 0;
            var fileTypeName = string.Empty;
            var fileName = string.Empty;
            foreach (string file in fileList)
            {
                var fileItem = file.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (fileItem.Length == 1)
                {
                    fileTypeName = DefaultFileType;
                    fileName = fileItem[0];
                }
                else
                {
                    fileTypeName = fileItem[0];
                    fileName = fileItem[1];
                }
                var filePath = ReProcess + fileName;

                if (!File.Exists(filePath))
                {
                    _worker.ReportProgress(10, string.Format("{0} 文件不存在", filePath));
                    continue;
                }

                fileType = GetFileType(fileTypeName);
                if (fileType <= 0)
                {
                    _worker.ReportProgress(10, string.Format("{0} 附件类型无效", fileTypeName));
                    continue;
                }

                FileInfo fileInfo = new FileInfo(filePath);
                var fileExt = fileInfo.Extension.ToLower();
                if (fileExt == ".jpg" || fileExt == ".png")
                {
                    UploadPicture(entity, fileType, filePath);
                }
                else
                {
                    UploadFile(entity, fileType, filePath);
                }
            }
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void ValidData(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取需要上传的文件名
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override string GetFileNames(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        protected override string GetUploadPath(T entity, int fileSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取新的文件名
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected override string GetNewFilePath(T entity, string fileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newFileNames"></param>
        protected override void UpdateData(T entity, List<string> newFileNames)
        {
        }

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        protected abstract int GetFileType(string fileType);

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileType"></param>
        /// <param name="filePath"></param>
        protected abstract void UploadPicture(T entity, int fileType, string filePath);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileType"></param>
        /// <param name="filePath"></param>
        protected abstract void UploadFile(T entity, int fileType, string filePath);

        /// <summary>
        /// 清除数据
        /// </summary>
        /// <param name="entity"></param>
        protected abstract void ClearData(T entity);

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileType"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        protected abstract void SaveData(T entity, int fileType, string fileName, string filePath);
    }
}
