using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NhhComplaintService
{
    /// <summary>
    /// Nhh投诉服务
    /// </summary>
    partial class NhhComplaintService : ServiceBase
    {
        public NhhComplaintService()
        {
            InitializeComponent();

            this.ServiceName = "NhhComplaintService";
            this.CanStop = true;
            this.AutoLog = true;

            //间隔秒数
            int interval = 1;
            int.TryParse(ConfigurationManager.AppSettings["Interval"].ToString(), out interval);

            Timer timer = new Timer();
            timer.Elapsed += timer_Elapsed;
            timer.Interval = interval * 1000 * 60;
            timer.Enabled = true;
        }

        /// <summary>
        /// 定时任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            new OvertimeTask().SendMessage();
        }

        protected override void OnStart(string[] args)
        {
            // TODO:  在此处添加代码以启动服务。
            ServiceLog.Log("ComplaintService", "Service Start...", "Service");
        }

        protected override void OnStop()
        {
            // TODO:  在此处添加代码以执行停止服务所需的关闭操作。
            ServiceLog.Log("ComplaintService", "Service Stop...", "Service");
        }
    }
}
