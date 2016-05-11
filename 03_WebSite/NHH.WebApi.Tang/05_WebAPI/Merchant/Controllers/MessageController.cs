using System.Web.Mvc;
using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Service.Common.IService;
using NHH.WebAPI.Merchant.Common;

namespace NHH.WebAPI.Merchant.Controllers
{
    public class MessageController : NHHController
    {
        #region Service
        private ICommonService m_service;
        public ICommonService Service
        {
            get
            {
                if (m_service == null)
                {
                    m_service = this.GetService<ICommonService>();
                }
                return m_service;
            }
        }
        #endregion

        /// <summary>
        /// 获取消息内容
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("message/{messageId}")]
        //[NHHActionLog]
        public JsonResult GetMessage(int messageId)
        {
            MessageInfo result = Service.GetMessage(messageId);
            return Json(result);
        }
    }
}
