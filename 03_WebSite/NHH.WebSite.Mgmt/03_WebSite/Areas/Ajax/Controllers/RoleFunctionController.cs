using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Service.Configure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax.Controllers
{

    /// <summary>
    /// 角色功能
    /// </summary>
    [AllowAnonymous]
    public class RoleFunctionController : NHHController
    {
        #region Service
        private ISysRoleFunctionService m_Service;
        protected ISysRoleFunctionService Service
        {
            get
            {
                if (m_Service == null)
                {
                    m_Service = this.GetService<ISysRoleFunctionService>();
                }

                return m_Service;
            }
        }
        #endregion

        /// <summary>
        /// 获取角色功能列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public JsonResult GetRoleFunctionList(int roleId)
        {
            var list = this.Service.GetRoleFunctionList(roleId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取系统功能列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public JsonResult GetSystemFunctionList(int? roleId, string keyword)
        {
            var list = this.Service.GetSystemFunctionList(roleId, keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加功能
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddFunction(int roleId, int functionId)
        {
            var result = new AjaxResult();
            result.Code = 0;
            result.Message = "添加成功";
            this.Service.AddFunction(roleId, functionId);
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 移除功能
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoveFunction(int roleId, int functionId)
        {
            var result = new AjaxResult();
            result.Code = 0;
            result.Message = "移除成功";
            this.Service.RemoveFunction(roleId, functionId);
            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}