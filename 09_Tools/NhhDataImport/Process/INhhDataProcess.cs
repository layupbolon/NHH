using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 立天唐人数据处理接口
    /// </summary>
    public interface INhhDataProcess
    {
        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="worker"></param>
        void CheckData(string fileName, BackgroundWorker worker);

        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="fileName"></param>
        void ProcessData(string fileName, BackgroundWorker worker);
    }
}
