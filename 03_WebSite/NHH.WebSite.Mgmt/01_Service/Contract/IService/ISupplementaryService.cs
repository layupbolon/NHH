using NHH.Models.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Contract.IService
{
    /// <summary>
    /// 补充协议
    /// </summary>
    public interface ISupplementaryService
    {
        /// <summary>
        /// 添加补充协议
        /// </summary>
        /// <param name="model"></param>
        void AddSupplementary(SupplementaryModel model);
    }
}
