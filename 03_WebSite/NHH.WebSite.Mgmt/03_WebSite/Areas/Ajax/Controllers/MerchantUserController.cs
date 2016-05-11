using NHH.Framework.Web;
using NHH.Models.Merchant;
using NHH.Service.Merchant.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NHH.WebSite.Management.Areas.Ajax.Controllers
{
    /// <summary>
    /// 商户用户
    /// </summary>
    [AllowAnonymous]
    public class MerchantUserController : NHHController
    {
        /// <summary>
        /// 获取商户用户分组列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public JsonResult GetUserGroupList(MerchantUserGroupQueryInfo queryInfo)
        {
            var result = GetService<IMerchantUserService>().GetUserGroupList(queryInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}