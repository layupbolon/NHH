using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Contract;
using NHH.Service.Contract.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Contract
{
    /// <summary>
    /// 补充协议
    /// </summary>
    public class SupplementaryService : NHHService<NHHEntities>, ISupplementaryService
    {
        /// <summary>
        /// 添加补充协议
        /// </summary>
        /// <param name="model"></param>
        public void AddSupplementary(SupplementaryModel model)
        {
            throw new NotImplementedException();
        }
    }
}
