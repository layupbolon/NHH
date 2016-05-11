using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhCampaignService
{
    public class CampaignSendAllMessage
    {
        /// <summary>
        /// 用于设定图文消息的接收者
        /// </summary>
        public Filter filter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Mpnews mpnews { get; set; }

        public string msgtype { get; set; }
    }
}
