
namespace NHH.Framework.Baidu.Push
{
    public class BaiduPushNotificationIOS
    {
        public string title { get; set; } //通知标题，可以为空；如果为空则设为appid对应的应用名;
        public string description { get; set; } //通知文本内容，不能为空;
        public string aps { get; set; }

        public BaiduPushNotificationIOS()
        {
        }
    }
}
