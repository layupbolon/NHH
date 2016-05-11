using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common
{
    /// <summary>
    /// 合同公共服务接口
    /// </summary>
    public class ContractCommonService : NHHService<NHHEntities>, IContractCommonService
    {
        /// <summary>
        /// 获取铺位列表
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public List<ProjectUnitCommonInfo> GetProjectUnitList(int contractId)
        {
            var list = new List<ProjectUnitCommonInfo>();
            var query = from cu in Context.Contract_Unit
                        join pu in Context.Project_Unit on cu.UnitID equals pu.UnitID
                        where cu.Status == 1 && cu.ContractID == contractId
                        select new
                        {
                            pu.UnitID,
                            pu.UnitNumber,
                            pu.Project_Floor.FloorName,
                            pu.Project_Building.BuildingName,
                        };

            var entityList = query.ToList();

            if (entityList == null)
                return list;

            entityList.ForEach(entity =>
            {
                var info = new ProjectUnitCommonInfo
                {
                    UnitId = entity.UnitID,
                    UnitNumber = entity.UnitNumber,
                };
                info.FloorName = entity.FloorName;
                info.BuildingName = entity.BuildingName;
                list.Add(info);
            });

            return list;
        }
    }
}
