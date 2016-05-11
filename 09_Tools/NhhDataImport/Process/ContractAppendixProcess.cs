using NhhDataImport.Entity;
using NhhDataImport.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 合同附件
    /// </summary>
    public class ContractAppendixProcess : MultPictureProcess<ContractEntity>
    {
        public ContractAppendixProcess()
        {
            DefaultFileType = "租约扫描件";
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void ValidData(ContractEntity entity)
        {
            if (entity.ContractID <= 0)
            {
                throw new NhhException("合同附件上传，合同ID无效");
            }
        }

        /// <summary>
        /// 获取文件名称
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override string GetFileNames(ContractEntity entity)
        {
            return entity.AttachmentList;
        }

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        protected override int GetFileType(string fileType)
        {
            return CommonUtil.GetDictionaryValue("ContractAppendixType", fileType);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileType"></param>
        /// <param name="filePath"></param>
        protected override void UploadPicture(ContractEntity entity, int fileType, string filePath)
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

            var newFileName = string.Format("Contract/S100/{0}/{1}", entity.MerchantID, fileName);

            SaveData(entity, fileType, fileInfo.Name, newFileName);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileType"></param>
        /// <param name="filePath"></param>
        protected override void UploadFile(ContractEntity entity, int fileType, string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            string path = GetUploadPath(entity, 0);
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string fileName = string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyMMddHHmmss"), random.Next(0, 9), fileInfo.Extension);
            string newFilePath = Path.Combine(path, fileName);
            File.Move(filePath, newFilePath);

            var newFileName = string.Format("Contract/Original/{0}/{1}", entity.MerchantID, fileName);

            SaveData(entity, fileType, fileInfo.Name, newFileName);
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void ClearData(ContractEntity entity)
        {
            string strCmd = string.Format(@"UPDATE [dbo].[Contract_Appendix]
                                               SET [Status] = -1
                                             WHERE [ContractID] = {0}", entity.ContractID);
            SqlHelper.ExecuteNonQuery(strCmd);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileType"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        protected override void SaveData(ContractEntity entity, int fileType, string fileName, string filePath)
        {
            string strCmd = @"INSERT INTO [dbo].[Contract_Appendix]
                                   ([ContractID]
                                   ,[AppendixType]
                                   ,[AppendixTemplate]
                                   ,[AppendixName]
                                   ,[AppendixPath]
                                   ,[Status]
                                   ,[InDate]
                                   ,[InUser]
                                   ,[EditDate]
                                   ,[EditUser])
                             VALUES
                                   (@ContractID
                                   ,@AppendixType
                                   ,@AppendixTemplate
                                   ,@AppendixName
                                   ,@AppendixPath
                                   ,@Status
                                   ,@InDate
                                   ,@InUser
                                   ,@EditDate
                                   ,@EditUser)";

            var paramList = new SqlParameter[]
            {
                new SqlParameter("@ContractID", entity.ContractID),
                new SqlParameter("@AppendixType", fileType),
                new SqlParameter("@AppendixTemplate", 1),
                new SqlParameter("@AppendixName", fileName),
                new SqlParameter("@AppendixPath", filePath),
                new SqlParameter("@Status", entity.Status),
                new SqlParameter("@InDate", entity.InDate),
                new SqlParameter("@InUser", entity.InUser),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
            };
            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }

        /// <summary>
        /// 获取上传路径
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        protected override string GetUploadPath(ContractEntity entity, int fileSize)
        {
            string path = DirectoryUtil.BuildDir(Processed, "Contract");
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
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newFileNames"></param>
        protected override void UpdateData(ContractEntity entity, List<string> newFileNames)
        {
        }

        /// <summary>
        /// 获取新文件路径
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected override string GetNewFilePath(ContractEntity entity, string fileName)
        {
            return string.Empty;
        }

        /// <summary>
        /// 导入租约
        /// </summary>
        /// <param name="table"></param>
        /// <param name="file"></param>
        /// <param name="worker"></param>
        /// <returns></returns>
        public void ImportAppendix(DataTable table, FileInfo file, BackgroundWorker worker)
        {
            worker.ReportProgress(0, string.Format("正在处理{0}", file.FullName));
            var fileName = file.Name;
            fileName = file.Name.Substring(0, fileName.IndexOf("."));

            DataRow row = null;

            foreach (DataRow dr in table.Rows)
            {
                if (fileName.Contains(dr["ContractCode"].ToString()))
                {
                    row = dr;
                    break;
                }
            }

            if (row == null)
            {
                worker.ReportProgress(10, string.Format("未找到相关租约，{0}", file.FullName));
                return;
            }

            int contractId = Convert.ToInt32(row["ContractID"]);
            int merchantId = Convert.ToInt32(row["MerchantID"]);

            string strCmd = string.Format(@"SELECT TOP 1 [AppendixID]
                                          FROM [dbo].[Contract_Appendix](Nolock)
                                          Where ContractID={0} And AppendixName='{1}'", contractId, fileName);

            if (SqlHelper.ExecuteScalar(strCmd) > 0)
            {
                worker.ReportProgress(10, string.Format("该附件已存在，{0}", file.FullName));
                return;
            }
            var contract = new ContractEntity
            {
                ContractID = contractId,
                MerchantID = merchantId
            };

            string path = GetUploadPath(contract, 0);
            Random random = new Random(Guid.NewGuid().GetHashCode());
            fileName = string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyMMddHHmmss"), random.Next(0, 9), file.Extension);
            string newFilePath = Path.Combine(path, fileName);
            if (File.Exists(newFilePath))
                return;
            File.Move(file.FullName, newFilePath);

            var newFileName = string.Format("Contract/Original/{0}/{1}", contract.MerchantID, fileName);
            int fileType = GetFileType("租约扫描件");
            SaveData(contract, fileType, file.Name, newFileName);
            worker.ReportProgress(0, string.Format("处理完成，{0}", file.FullName));
        }
    }
}
