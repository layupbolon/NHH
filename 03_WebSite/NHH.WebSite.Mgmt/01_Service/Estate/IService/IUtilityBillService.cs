using NHH.Models.Common;
using NHH.Models.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Estate.IService
{
    /// <summary>
    /// 水电煤气费抄表服务接口
    /// </summary>
    public interface IUtilityBillService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        UtilityBillListModel GetList(UtilityBillQueryInfo queryInfo);
        
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="model"></param>
        void Add(UtilityBillListModel model);
    }
}
