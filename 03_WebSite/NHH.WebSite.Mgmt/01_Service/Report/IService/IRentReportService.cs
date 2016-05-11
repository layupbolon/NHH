using NHH.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Report.IService
{
    /// <summary>
    /// 租金报表服务接口
    /// </summary>
    public interface IRentReportService
    {
        /// <summary>
        /// 租金预估
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        RentPreviewModel Preview(RentPreviewQueryInfo queryInfo);
    }
}
