using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Report;
using NHH.Service.Report.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Report
{
    /// <summary>
    /// 租金报表服务
    /// </summary>
    public class RentReportService : NHHService<NHHEntities>, IRentReportService
    {
        /// <summary>
        /// 租金预估
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public RentPreviewModel Preview(RentPreviewQueryInfo queryInfo)
        {
            var model = new RentPreviewModel();
            model.QueryInfo = queryInfo;

            model.UnitList = new List<RentPreviewUnitItemInfo>();

            var query = from pu in Context.View_Project_Unit
                        join up in Context.Project_UnitPlan on pu.UnitID equals up.UnitID
                        join bt in Context.BizType on up.BizTypeID equals bt.BizTypeID
                        join ut in Context.View_UnitType on pu.UnitType equals ut.UnitTypeID
                        join us in Context.View_UnitStatus on pu.UnitStatus equals us.UnitStatusID
                        where pu.Status == 1 && pu.ProjectID == queryInfo.ProjectId
                        select new
                        {
                            pu.UnitID,
                            pu.UnitNumber,
                            pu.UnitAera,
                            pu.BuildingID,
                            pu.BuildingName,
                            pu.FloorID,
                            pu.FloorNumber,
                            up.StandardRent,
                            up.StandardMgmtFee,
                            up.BizTypeID,
                            bt.BizTypeName,
                            pu.UnitType,
                            ut.UnitTypeName,
                            pu.UnitStatus,
                            us.UnitStatusName,
                            ContractPrice = (from c in Context.Contract
                                             join cu in Context.Contract_Unit on c.ContractID equals cu.ContractID
                                             where pu.UnitStatus == 4 && c.Status == 1 && cu.UnitID == pu.UnitID
                                             select new
                                             {
                                                 c.ContractUnitRent,
                                                 c.ContractMgmtFee,
                                             }).FirstOrDefault()
                        };
            if (queryInfo.BuildingId.HasValue)
            {
                query = query.Where(item => item.BuildingID == queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                query = query.Where(item => item.FloorID == queryInfo.FloorId.Value);
            }
            if (queryInfo.UnitType.HasValue)
            {
                query = query.Where(item => item.UnitType == queryInfo.UnitType.Value);
            }
            if (queryInfo.BizTypeId.HasValue)
            {
                query = query.Where(item => item.BizTypeID == queryInfo.BizTypeId.Value);
            }

            var entityList = query.OrderBy(item => item.FloorID).OrderBy(item => item.UnitID).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var unit = new RentPreviewUnitItemInfo
                    {
                        UnitID = entity.UnitID,
                        UnitArea = entity.UnitAera,
                        UnitNumber = entity.UnitNumber,
                        BizTypeName = entity.BizTypeName,
                        UnitTypeName = entity.UnitTypeName,
                        BuildingName = entity.BuildingName,
                        FloorNumber = entity.FloorNumber,
                        UnitStatus = entity.UnitStatus,
                        UnitStatusName = entity.UnitStatusName,
                        StandardRent = entity.StandardRent.HasValue ? entity.StandardRent.Value : 0,
                        StandardMgmtFee = entity.StandardMgmtFee.HasValue ? entity.StandardMgmtFee.Value : 0,
                    };
                    //合同价
                    if (entity.ContractPrice != null)
                    {
                        unit.StandardRent = entity.ContractPrice.ContractUnitRent;
                        unit.StandardMgmtFee = entity.ContractPrice.ContractMgmtFee;
                    }
                    model.UnitList.Add(unit);
                });
            }

            return model;
        }
    }
}
