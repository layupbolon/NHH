using NHH.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common.IService
{
    /// <summary>
    /// 报表定置信息
    /// </summary>
    public interface IReportConfigService
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reportCode"></param>
        /// <param name="fieldList"></param>
        /// <returns></returns>
        MessageBag<bool> Save(int userId, string reportCode, string fieldList);
    }
}
