using NHH.Models.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Logging.IService
{
    public interface ILoggingService
    {

        Dictionary<string, string> GetActionNames();

        Dictionary<string, string> GetEntityNames();
        
        OptLogListModel QueryOptLogs(OptLogQueryInfo queryInfo);

        
    }
}
