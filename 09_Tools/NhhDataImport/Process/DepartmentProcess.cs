using NhhDataImport.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 部门处理
    /// </summary>
    public class DepartmentProcess : BaseDataProcess<DepartmentEntity>
    {
        public DepartmentProcess()
        {
            SheetName = "部门";
        }

        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected override DepartmentEntity ToEntity(DataRow row)
        {
            var entity = new DepartmentEntity();
            entity.CompanyName = GetValue<string>(row, "公司名称");
            entity.DepartmentName = GetValue<string>(row, "部门名称");
            entity.Phone = GetValue<string>(row, "联系电话");
            return entity;
        }


        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override void ValidEntity(DepartmentEntity entity)
        {
            
            if (string.IsNullOrEmpty(entity.DepartmentName) || entity.DepartmentName.Length == 0)
            {
                throw new NhhException("请输入部门名称");
            }

            if (string.IsNullOrEmpty(entity.Phone) || entity.Phone.Length == 0)
            {
                throw new NhhException("请输入联系电话");
            }

            entity.CompanyID = CompanyUtil.GetCompanyId(entity.CompanyName);
            if (entity.CompanyID == 0)
            {
                throw new NhhException("请输入公司名称");
            }
            if (entity.CompanyID < 0)
            {
                throw new NhhException("未找到相关公司 " + entity.CompanyName);
            }
        }

        protected override bool IsExists(DepartmentEntity entity)
        {
            var strCmd = string.Format("SELECT COUNT(0) AS Num FROM [dbo].[Department](Nolock) WHERE DepartmentName='{0}' AND CompanyID='{1}'", entity.DepartmentName, entity.CompanyID);
            return SqlHelper.ExecuteScalar(strCmd) > 0;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void SaveData(DepartmentEntity entity)
        {
            string strCmd = string.Format(@"INSERT INTO [dbo].[Department]
                                                   ([DepartmentName]
                                                   ,[Phone]
                                                   ,[CompanyID]
                                                   ,[Status]
                                                   ,[InDate]
                                                   ,[InUser]
                                                   ,[EditDate]
                                                   ,[EditUser])
                                             VALUES
                                                   (@DepartmentName
                                                   ,@Phone
                                                   ,@CompanyID
                                                   ,@Status
                                                   ,@InDate
                                                   ,@InUser
                                                   ,@EditDate
                                                   ,@EditUser)");
            var paramList = new SqlParameter[] 
            {
                new SqlParameter("@CompanyID", entity.CompanyID),
                new SqlParameter("@DepartmentName", entity.DepartmentName),
                new SqlParameter("@Phone", entity.Phone),
                new SqlParameter("@Status", entity.Status),
                new SqlParameter("@InDate", entity.InDate),
                new SqlParameter("@InUser", entity.InUser),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),            
            };
            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void UpdateData(DepartmentEntity entity)
        {
            string strCmd = string.Format(@"UPDATE [dbo].[Department] SET 
                                            [Phone] = @Phone
                                           ,[DepartmentName] = @DepartmentName
                                           ,[EditDate] = @EditDate
                                           ,[EditUser] = @EditUser
                                           WHERE DepartmentID=@DepartmentID");

            var paramList = new SqlParameter[]
            {
                new SqlParameter("@Phone", entity.Phone),
                new SqlParameter("@DepartmentName", entity.DepartmentName),
                new SqlParameter("@DepartmentID",CompanyUtil.GetDepartmentId(entity.CompanyName,entity.DepartmentName)),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser)
            };
            SqlHelper.ExecuteNonQuery(strCmd, paramList);

        }
    }
}
