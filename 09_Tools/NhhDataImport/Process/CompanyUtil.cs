using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 公司
    /// </summary>
    public class CompanyUtil
    {
        /// <summary>
        /// 获取公司ID
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public static int GetCompanyId(string companyName)
        {
            if (string.IsNullOrEmpty(companyName) || companyName.Length == 0)
                return 0;

            string strCmd = string.Format(@"SELECT TOP 1 [CompanyID]
                                          FROM [dbo].[Company](Nolock)
                                          Where CompanyName='{0}'", companyName);
            return SqlHelper.ExecuteScalar(strCmd);
        }

        /// <summary>
        /// 获取部门ID
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public static int GetDepartmentId(string companyName, string departmentName)
        {
            if (string.IsNullOrEmpty(companyName) || companyName.Length == 0 ||
                string.IsNullOrEmpty(departmentName) || departmentName.Length == 0)
                return 0;

            string strCmd = string.Format(@"SELECT TOP 1 [DepartmentID]
                                          FROM [dbo].[Department](Nolock) as A
                                          Inner join dbo.Company(Nolock) as B on A.CompanyID=B.CompanyID
                                          Where CompanyName='{0}' And DepartmentName='{1}'", companyName, departmentName);
            return SqlHelper.ExecuteScalar(strCmd);
        }

        /// <summary>
        /// 获取员工ID
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="departmentName"></param>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        public static int GetEmployeeId(string companyName, string departmentName, string employeeName)
        {
            if (string.IsNullOrEmpty(companyName) || companyName.Length == 0 ||
                string.IsNullOrEmpty(departmentName) || departmentName.Length == 0 ||
                string.IsNullOrEmpty(employeeName) || employeeName.Length == 0)
                return 0;

            string strCmd = string.Format(@"SELECT TOP 1 [EmployeeID]
                                          FROM [dbo].[Employee](Nolock) as A
                                          Inner join dbo.Department(Nolock) as B on A.DepartmentID=B.DepartmentID
                                          Inner join dbo.Company(Nolock) as C on B.CompanyID=C.CompanyID
                                          Where CompanyName='{0}' And DepartmentName='{1}' And EmployeeName='{2}'", companyName, departmentName, employeeName);
            return SqlHelper.ExecuteScalar(strCmd);
        }

        /// <summary>
        /// 获取公司类型ID
        /// </summary>
        /// <param name="companyType"></param>
        /// <returns></returns>
        public static int GetCompanyTypeId(string companyType)
        {
            if (string.IsNullOrEmpty(companyType) || companyType.Length == 0)
                return 0;
            string strCmd = string.Format(@"SELECT TOP 1 [FieldValue]
                                            FROM [dbo].[Dictionary](Nolock)
                                            Where FieldType='CompanyType' And FieldName='{0}'", companyType);
            return SqlHelper.ExecuteScalar(strCmd);
        }
    }
}
