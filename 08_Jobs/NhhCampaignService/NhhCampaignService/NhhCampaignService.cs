using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Timers;
namespace NhhCampaignService
{
    public partial class NhhCampaignService : ServiceBase
    {
        public NhhCampaignService()
        {
            InitializeComponent();

            this.CanStop = true;
            this.AutoLog = true;

            int interval = 30;
            int.TryParse(ConfigurationManager.AppSettings["interval"].ToString(), out interval);

            Timer timer = new Timer();
            timer.Interval = interval * 1000*60;//分钟
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
            timer.Start();

        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            new CampaignTask().SendMessage();

        }

        protected override void OnStart(string[] args)
        {
            ServiceLog.Log("Campaign Service", "Service Start...", "Service");
        }

        protected override void OnStop()
        {
            ServiceLog.Log("Campaign Service", "Service Stop...", "Service");
        }
    }
}
