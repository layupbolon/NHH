using NHH.Models.Merchant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Merchant.IService
{
    /// <summary>
    /// 商铺计量表服务接口
    /// </summary>
    public interface IStoreMeterService
    {
        /// <summary>
        /// 获取商铺计量表ID
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="meterCode"></param>
        /// <returns></returns>
        int GetMeterId(int storeId, string meterCode);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        StoreMeterListModel GetList(StoreMeterListQueryInfo queryInfo);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="info"></param>
        void Add(StoreMeterInfo info);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="info"></param>
        void Edit(StoreMeterInfo info);


        /// <summary>
        /// 获取计量表列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="projectId"></param>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        List<StoreMeterInfo> GetMeterList(int type, int projectId, int? buildingId);
    }
}
