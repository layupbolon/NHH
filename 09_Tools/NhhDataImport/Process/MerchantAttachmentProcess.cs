using NhhDataImport.Entity;
using NhhDataImport.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 商户附件处理
    /// </summary>
    public class MerchantAttachmentProcess : MultPictureProcess<MerchantEntity>
    {
        /// <summary>
        /// 清空数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void ClearData(MerchantEntity entity)
        {
            string strCmd = string.Format(@"UPDATE [dbo].[Merchant_Attachment]
                                               SET [Status] = -1
                                             WHERE [MerchantID] = {0}", entity.MerchantID);
            SqlHelper.ExecuteNonQuery(strCmd);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileType"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        protected override void SaveData(MerchantEntity entity, int fileType, string fileName, string filePath)
        {
            string strCmd = @"INSERT INTO [dbo].[Merchant_Attachment]
                                   ([MerchantID]
                                   ,[AttachmentType]
                                   ,[AttachmentName]
                                   ,[AttachmentPath]
                                   ,[Status]
                                   ,[InDate]
                                   ,[InUser]
                                   ,[EditDate]
                                   ,[EditUser])
                             VALUES
                                   (@MerchantID
                                   ,@AttachmentType
                                   ,@AttachmentName
                                   ,@AttachmentPath
                                   ,@Status
                                   ,@InDate
                                   ,@InUser
                                   ,@EditDate
                                   ,@EditUser)";

            var paramList = new SqlParameter[]
            {
                new SqlParameter("@MerchantID", entity.MerchantID),
                new SqlParameter("@AttachmentType", fileType),
                new SqlParameter("@AttachmentName", fileName),
                new SqlParameter("@AttachmentPath", filePath),
                new SqlParameter("@Status", entity.Status),
                new SqlParameter("@InDate", entity.InDate),
                new SqlParameter("@InUser", entity.InUser),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
            };
            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        protected override int GetFileType(string fileType)
        {
            return CommonUtil.GetDictionaryValue("MerchantAttachType", fileType);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileType"></param>
        /// <param name="filePath"></param>
        protected override void UploadPicture(MerchantEntity entity, int fileType, string filePath)
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

            var newFileName = string.Format("Merchant/S100/{0}/{1}", entity.MerchantID, fileName);

            SaveData(entity, fileType, fileInfo.Name, newFileName);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileType"></param>
        /// <param name="filePath"></param>
        protected override void UploadFile(MerchantEntity entity, int fileType, string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            string path = GetUploadPath(entity, 0);
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string fileName = string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyMMddHHmmss"), random.Next(0, 9), fileInfo.Extension);
            string newFilePath = Path.Combine(path, fileName);
            File.Move(filePath, newFilePath);

            var newFileName = string.Format("Merchant/{0}/{1}", entity.MerchantID, fileName);

            SaveData(entity, fileType, fileInfo.Name, newFileName);
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void ValidData(MerchantEntity entity)
        {
            if (entity.MerchantID <= 0)
            {
                throw new NhhException("商户附件上传，商户ID无效");
            }
        }

        /// <summary>
        /// 获取附件文件列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override string GetFileNames(MerchantEntity entity)
        {
            return entity.AttachmentFiles;
        }

        /// <summary>
        /// 获取上传目录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        protected override string GetUploadPath(MerchantEntity entity, int fileSize)
        {
            string path = DirectoryUtil.BuildDir(Processed, "Merchant");
            if (fileSize > 0)
            {
                path = DirectoryUtil.BuildDir(path, string.Format("S{0}", fileSize));
            }
            else
            {
                path = DirectoryUtil.BuildDir(path, "Original");
            }
            path = DirectoryUtil.BuildDir(path, entity.MerchantID.ToString());
            return path;
        }

        /// <summary>
        /// 获取新的文件名
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected override string GetNewFilePath(MerchantEntity entity, string fileName)
        {
            return string.Empty;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newFileNames"></param>
        protected override void UpdateData(MerchantEntity entity, List<string> newFileNames)
        {
        }
    }
}
