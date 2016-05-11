using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace NhhMessageService
{
    public partial class NhhMessageService : ServiceBase
    {
        public NhhMessageService()
        {
            InitializeComponent();

            this.ServiceName = "NhhMessageService";
            this.CanStop = true;
            this.AutoLog = true;

            //间隔秒数
            int interval = 1;
            int.TryParse(ConfigurationManager.AppSettings["Interval"].ToString(), out interval);

            Timer timer = new Timer();
            timer.Elapsed += timer_Elapsed;
            timer.Interval = interval * 1000;
            timer.Enabled = true;
        }

        /// <summary>
        /// 定时任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //发送邮件
            new MailTask().SendMessage();
            //发送微信
            new WechatTask().SendMessage();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
