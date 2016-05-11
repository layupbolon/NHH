using System.Web.Mvc;
using NHH.Framework.Web;
using NHH.Models.Message;
using NHH.Service.Message.IService;
using NHH.Models.Common;
using NHH.Service.Common.IService;

namespace NHH.WebSite.Management.Areas.Message.Controllers
{
    public class UserMessageController : NHHController
    {
        NHH.Service.Message.IService.IUserMessageService iService;
        public UserMessageController()
        {
            iService = GetService<NHH.Service.Message.IService.IUserMessageService>();
        }

        /// <summary>
        /// 用户消息列表
        /// </summary>
        /// <param name="messageStatus"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult List(UserMessageQueryInfo queryInfo)
        {
            queryInfo.UserInfo = new UserInfo();
            queryInfo.UserInfo.UserId = Context.UserID;

            var model = iService.GetUserMessageList(queryInfo);
            model.CrumbInfo.AddCrumb("用户消息");

            var messageStatusList = GetService<ICommonService>().GetCommonFieldList("UserMessageStatus");
            ViewData["MessageStatusList"] = new SelectList(messageStatusList, "FieldValue", "FieldName", queryInfo.MessageStatus.HasValue ? queryInfo.MessageStatus.Value : 0);

            return View(model);
        }

        /// <summary>
        /// 消息详情页
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public ActionResult Detail(int messageId)
        {
            var model = iService.GetUserMessageDetail(messageId);
            if (model.Status == 1)
            {
                model.UserInfo = new UserInfo();
                model.UserInfo.UserId = Context.UserID;
                iService.UpdateUserMessageStatus(model);
                model = iService.GetUserMessageDetail(messageId);
            }

            model.CrumbInfo.AddCrumb("用户消息", Url.Action("List", "UserMessage", new { area = "Message" }));
            model.CrumbInfo.AddCrumb("消息内容");

            return View(model);
        }

        /// <summary>
        /// 获取未读消息总数
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMessageCount()
        {
            var count = iService.GetMessageCount(Context.UserID);
            return Json(count);
        }
    }
}