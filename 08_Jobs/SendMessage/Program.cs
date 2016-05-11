using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            //发送邮件
            MailSender.SendMail();
        }        
    }
}
