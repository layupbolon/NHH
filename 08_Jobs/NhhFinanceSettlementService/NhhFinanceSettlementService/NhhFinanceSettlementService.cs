using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NhhFinanceSettlementService
{
    /// <summary>
    /// Nhh财务结算服务
    /// </summary>
    public partial class NhhFinanceSettlementService : ServiceBase
    {
        public NhhFinanceSettlementService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
