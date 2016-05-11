using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common.Enum.CommonEnums;

namespace NHH.Models.Common
{
    public class MessageInfo
    {
        public MessageInfo()
        {
        }

        public MessageInfo(MessageTypeEnum messageType, int priority, string recipient, string subject, string content)
        {
            this.Priority = priority;
            this.Recipient = recipient;
            this.Subject = subject;
            this.Content = content;
            this.MessageType = (int) messageType;
        }

        public int MessageID { get; set; }
        public int MessageType { get; set; }
        public int Priority { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public DateTime InDate { get; set; }
        public int InUser { get; set; }
    }
}
