
namespace NHH.Framework.Baidu.Push
{
    public class BaiduPushQueryBinds
    {
        public string channel_id { get; set; }
        public string user_id { get; set; }
        public string device_id { get; set; }
        public uint device_type { get; set; }
        public string device_name { get; set; }
        public string bind_name { get; set; }
        public string bind_time { get; set; }
        public string info { get; set; }
        public int bind_status { get; set; }
        public string online_status { get; set; }
        public int online_timestamp { get; set; }
        public int online_expires { get; set; }
    }
}
