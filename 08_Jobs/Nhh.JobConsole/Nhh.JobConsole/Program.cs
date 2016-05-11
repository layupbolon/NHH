using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Nhh.JobConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
            HostFactory.Run(x =>
            {
                x.UseLog4Net();

                x.Service<ServiceRunner>();

                x.SetDescription("立天唐人智能商业管理平台的Job服务宿主，管控智能平台所有JOB运行");
                x.SetDisplayName("Nhh.JobConsole");
                x.SetServiceName("Nhh.JobConsole");

                x.EnablePauseAndContinue();
            });
        }
    }
}
