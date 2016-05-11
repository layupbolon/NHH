using NHH.Models.Project.Kiosk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project.IService
{
    /// <summary>
    /// 多经点位服务接口
    /// </summary>
    public interface IKioskService
    {
        /// <summary>
        /// 根据筛选条件查询多经点位列表信息
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        KioskListModel GetKioskList(KioskQueryInfo queryInfo);

        /// <summary>
        /// 保存多经点位信息
        /// </summary>
        /// <param name="model"></param>
        void SaveKiosk(KioskDetailModel model); 

        
        /// <summary>
        /// 根据多经点位编号获取多经点位信息
        /// </summary>
        /// <param name="kioskId"></param>
        /// <returns></returns>
        KioskDetailModel GetKioskDetail(int kioskId);
    }
}
