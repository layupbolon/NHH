using NHH.Framework.Web;
using NHH.Models.Common.Department;
using NHH.Service.Common.IService;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax.Controllers
{
    /// <summary>
    /// 公司
    /// </summary>
    [AllowAnonymous]
    public class CompanyController : NHHController
    {
        #region Service
        private ICompanyCommonService m_Service;
        protected ICompanyCommonService Service
        {
            get
            {
                if (m_Service == null)
                {
                    m_Service = this.GetService<ICompanyCommonService>();
                }

                return m_Service;
            }
        }
        #endregion

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public JsonResult GetDepartmentList(int companyId)
        {
            var departmentList = this.Service.GetDepartmentList(companyId);
            return Json(departmentList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public JsonResult GetEmployeeList(int departmentId)
        {
            var employeeList = this.Service.GetEmployeeList(departmentId);
            return Json(employeeList, JsonRequestBehavior.AllowGet);
        }
    }
}