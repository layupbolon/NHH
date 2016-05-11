using NhhDataImport.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Framework.Security.Cryptography;
namespace NhhDataImport.Process
{
    /// <summary>
    /// 员工处理
    /// </summary>
    public class EmployeeProcess : BaseDataProcess<EmployeeEntity>
    {
        public EmployeeProcess()
        {
            SheetName = "员工";
        }

        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected override EmployeeEntity ToEntity(DataRow row)
        {
            var entity = new EmployeeEntity();
            entity.EmployeeCode = GetValue<string>(row, "员工编码");
            entity.EmployeeName = GetValue<string>(row, "员工姓名");
            entity.LoginAccountOrNot = GetValue<string>(row, "登录授权") == "是" ? true : false;
            entity.Title = GetValue<string>(row, "职位");
            entity.Email = GetValue<string>(row, "邮箱");
            entity.Moblie = GetValue<string>(row, "手机");
            entity.Phone = GetValue<string>(row, "座机");
            entity.Ext = GetValue<string>(row, "分机");
            entity.Gender = GetValue<string>(row, "性别") == "男" ? 1 : 2;
            entity.Birthday = GetValue<DateTime>(row, "生日");
            entity.IDNumber = GetValue<string>(row, "身份证号");
            entity.CompanyName = GetValue<string>(row, "公司名称");
            entity.DepartmentName = GetValue<string>(row, "部门名称");

            entity.UserType = 1;
            entity.Password = Cryptographer.SHA1.Encrypt(CommonUtil.GetStringValue("InitPassword"));

            return entity;
        }

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override void ValidEntity(EmployeeEntity entity)
        {
            if (string.IsNullOrEmpty(entity.EmployeeName) || entity.EmployeeName.Length == 0)
            {
                throw new NhhException("员工姓名为空");
            }

            entity.CompanyID = CompanyUtil.GetCompanyId(entity.CompanyName);
            if (entity.CompanyID == 0)
            {
                throw new NhhException("公司名称为空");
            }
            if (entity.CompanyID < 0)
            {
                throw new NhhException(string.Format("公司名称 {0} 输入错误", entity.CompanyName));
            }

            entity.DepartmentID = CompanyUtil.GetDepartmentId(entity.CompanyName, entity.DepartmentName);
            if (entity.DepartmentID == 0)
            {
                throw new NhhException("部门名称为空");
            }
            if (entity.DepartmentID < 0)
            {
                throw new NhhException(string.Format("部门名称 {0} 输入错误", entity.DepartmentName));
            }

            if (string.IsNullOrEmpty(entity.Email) || entity.Email.Length == 0)
            {
                throw new NhhException("员工邮箱为空");
            }

            if (string.IsNullOrEmpty(entity.EmployeeCode) || entity.EmployeeCode.Length == 0)
            {
                throw new NhhException("员工编码为空");
            }

            //if (string.IsNullOrEmpty(entity.IDNumber) || entity.IDNumber.Length == 0)
            //{
            //    throw new NhhException("身份证号为空");
            //}

            if (string.IsNullOrEmpty(entity.Title) || entity.Title.Length == 0)
            {
                throw new NhhException("职位为空");
            }

            if (string.IsNullOrEmpty(entity.Moblie) || entity.Moblie.Length == 0)
            {
                throw new NhhException("手机为空");
            }

            if (entity.Ext == @"\")
            {
                entity.Ext = "";
            }
        }

        /// <summary>
        /// 是否已存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool IsExists(NhhDataImport.Entity.EmployeeEntity entity)
        {
            var strCmd = string.Format("SELECT COUNT(0) AS Num FROM [dbo].[Employee](Nolock) WHERE EmployeeCode='{0}'", entity.EmployeeCode);
            return SqlHelper.ExecuteScalar(strCmd) > 0;

        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void SaveData(EmployeeEntity entity)
        {
            string strCmd = string.Format(@"INSERT INTO [dbo].[Employee]
                                           ([EmployeeCode]
                                           ,[EmployeeName]
                                           ,[Title]
                                           ,[Email]
                                           ,[Moblie]
                                           ,[Phone]
                                           ,[Ext]
                                           ,[Gender]
                                           ,[Birthday]
                                           ,[IDNumber]
                                           ,[DepartmentID]
                                           ,[CompanyID]
                                           ,[Status]
                                           ,[InDate]
                                           ,[InUser]
                                           ,[EditDate]
                                           ,[EditUser])
                                     VALUES
                                           (@EmployeeCode
                                           ,@EmployeeName
                                           ,@Title
                                           ,@Email
                                           ,@Moblie
                                           ,@Phone
                                           ,@Ext
                                           ,@Gender
                                           ,@Birthday
                                           ,@IDNumber
                                           ,@DepartmentID
                                           ,@CompanyID
                                           ,@Status
                                           ,@InDate
                                           ,@InUser
                                           ,@EditDate
                                           ,@EditUser);SELECT SCOPE_IDENTITY()");

            #region SQL2
            string strCmd2 = string.Format(@"INSERT INTO [dbo].[Sys_User]
                                   ([EmployeeID]
                                   ,[UserType]
                                   ,[UserName]
                                   ,[Password]
                                   ,[Status]
                                   ,[InDate]
                                   ,[InUser]
                                   ,[EditDate]
                                   ,[EditUser])
                             VALUES
                                   (@EmployeeID
                                   ,@UserType
                                   ,@UserName
                                   ,@Password
                                   ,@Status
                                   ,@InDate
                                   ,@InUser
                                   ,@EditDate
                                   ,@EditUser)");
            #endregion

            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@EmployeeCode", entity.EmployeeCode),
                new SqlParameter("@EmployeeName", entity.EmployeeName),
                new SqlParameter("@Title", entity.Title),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@Moblie", entity.Moblie),
                new SqlParameter("@Phone", entity.Phone),
                new SqlParameter("@Ext", entity.Ext),
                new SqlParameter("@Gender", entity.Gender),
                 new SqlParameter("@Birthday", entity.Birthday),
                new SqlParameter("@IDNumber", entity.IDNumber),
                new SqlParameter("@DepartmentID", entity.DepartmentID),
                new SqlParameter("@CompanyID", entity.CompanyID),
                new SqlParameter("@Status", entity.Status),
                new SqlParameter("@InDate", entity.InDate),
                new SqlParameter("@InUser", entity.InUser),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
            };
            entity.UserName = entity.Email.Substring(0, entity.Email.IndexOf("@"));
            var paramList2 = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", entity.EmployeeID),
                new SqlParameter("@UserType", entity.UserType),
                new SqlParameter("@UserName", entity.UserName),
                new SqlParameter("@Password", entity.Password),
                new SqlParameter("@Status", entity.Status),
                new SqlParameter("@InDate", entity.InDate),
                new SqlParameter("@InUser", entity.InUser),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
            };

            using (var conn = SqlHelper.GetConnection())
            {
                //打开
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                var trans = conn.BeginTransaction("Employee");
                entity.EmployeeID = SqlHelper.ExecuteScalar(trans, strCmd, paramList);
                paramList2[0].Value = entity.EmployeeID;
                SqlHelper.ExecuteNonQuery(trans, strCmd2, paramList2);
                trans.Commit();
                //关闭
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }

            //部门负责人
            if (entity.Title.IndexOf("经理") > 0)
            {
                string strCmd3 = @"UPDATE [dbo].[Department]
                                   SET [ManagerID] = @ManagerID
                                 WHERE DepartmentID=@DepartmentID";
                var paramList3 = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentID", entity.DepartmentID),
                    new SqlParameter("@ManagerID",entity.EmployeeID),
                };
                SqlHelper.ExecuteNonQuery(strCmd3, paramList3);
            }

            //员工的上级
            string strCmd4 = string.Format(@"Update Employee
                                            Set SupervisorID=(Select Top 1 ManagerID From Department(Nolock) Where DepartmentID=Employee.DepartmentID)
                                            Where EmployeeID={0}", entity.EmployeeID);
            SqlHelper.ExecuteNonQuery(strCmd4);
        }

        /// <summary>
        /// 更新员工数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void UpdateData(EmployeeEntity entity)
        {
            entity.EmployeeID = CompanyUtil.GetEmployeeId(entity.CompanyName, entity.DepartmentName, entity.EmployeeName);
            string strCmd = string.Format(@"UPDATE [dbo].[Employee] SET 
                                            [EmployeeCode] = @EmployeeCode
                                           ,[EmployeeName] = @EmployeeName
                                           ,[Title] = @Title
                                           ,[Email] = @Email
                                           ,[Moblie] = @Moblie
                                           ,[Phone] = @Phone
                                           ,[Ext] = @Ext
                                           ,[Gender] = @Gender
                                           ,[Birthday] = @Birthday
                                           ,[IDNumber] = @IDNumber
                                           ,[EditDate] = @EditDate
                                           ,[EditUser] = @EditUser
                                           WHERE EmployeeID=@EmployeeID");

            var paramList = new SqlParameter[] 
            {
                new SqlParameter("@EmployeeCode", entity.EmployeeCode),
                new SqlParameter("@EmployeeName", entity.EmployeeName),
                new SqlParameter("@Title", entity.Title),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@Moblie", entity.Moblie),
                new SqlParameter("@Phone", entity.Phone),
                new SqlParameter("@Ext", entity.Ext),
                new SqlParameter("@Gender", entity.Gender),
                new SqlParameter("@Birthday", entity.Birthday),
                new SqlParameter("@IDNumber", entity.IDNumber),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
                 new SqlParameter("@EmployeeID", entity.EmployeeID),
            };

            SqlHelper.ExecuteNonQuery(strCmd, paramList);

            //部门负责人
            if (entity.Title.IndexOf("经理") >= 0)
            {
                string strCmd3 = @"UPDATE [dbo].[Department]
                                   SET [ManagerID] = @ManagerID
                                 WHERE DepartmentID=@DepartmentID";
                var paramList3 = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentID", entity.DepartmentID),
                    new SqlParameter("@ManagerID",entity.EmployeeID),
                };
                SqlHelper.ExecuteNonQuery(strCmd3, paramList3);
            }

            //员工的上级
            string strCmd4 = string.Format(@"Update dbo.Employee
                                            Set SupervisorID=(Select Top 1 ManagerID From dbo.Department(Nolock) Where DepartmentID=Employee.DepartmentID)
                                            Where EmployeeID={0}", entity.EmployeeID);
            SqlHelper.ExecuteNonQuery(strCmd4);
        }

    }
}
