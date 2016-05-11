using NHH.Models.Project.Adpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project.IService
{
    /// <summary>
    /// 广告位服务接口
    /// </summary>
    public interface IAdPointService
    {
        /// <summary>
        /// 获取广告位查询信息
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        AdPointListModel GetAdPointList(AdPointQueryInfo queryInfo);

        /// <summary>
        /// 根据广告位编号获取广告位信息
        /// </summary>
        /// <param name="adPointId"></param>
        /// <returns></returns>
        AdPointDetailModel GetAdPointDetail(int adPointId);

        /// <summary>
        /// 保存广告位信息
        /// </summary>
        /// <param name="model"></param>
        void SaveAdPoint(AdPointDetailModel model);
    }
}
