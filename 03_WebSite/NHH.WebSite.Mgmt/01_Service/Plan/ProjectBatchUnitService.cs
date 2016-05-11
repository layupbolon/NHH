using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Plan;
using NHH.Service.Plan.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Plan
{
    /// <summary>
    /// 招商批次铺位信息
    /// </summary>
    public class ProjectBatchUnitService : NHHService<NHHEntities>, IProjectBatchUnitService
    {
        /// <summary>
        /// 获取铺位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ProjectBatchUnitListModel GetUnitList(ProjectBatchUnitListQueryInfo queryInfo)
        {
            var model = new ProjectBatchUnitListModel();
            model.QueryInfo = queryInfo;
            model.UnitList = new List<ProjectBatchUnitInfo>();
            model.PagingInfo = new Models.Common.PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };

            var query = from bu in Context.Project_BatchUnit
                        join b in Context.Project_Batch on bu.BatchID equals b.BatchID
                        join pu in Context.Project_Unit on bu.UnitID equals pu.UnitID
                        join pue in Context.View_Project_UnitExt on pu.UnitID equals pue.UnitID
                        where bu.Status == 1 && b.Status == 1 && pu.Status == 1
                        select new
                        {
                            b.BatchID,
                            b.BatchCode,
                            pu.ProjectID,
                            pu.UnitID,
                            pu.UnitNumber,
                            pu.UnitAera,
                            pue.UnitTypeName,
                            pue.BizTypeName
                        };

            if (queryInfo.ProjectId.HasValue)
            {
                query = query.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }
            if (queryInfo.BatchId.HasValue)
            {
                query = query.Where(item => item.BatchID == queryInfo.BatchId.Value);
            }

            model.PagingInfo.TotalCount = query.Count();
            var entityList = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var info = new ProjectBatchUnitInfo
                    {
                        BatchID = entity.BatchID,
                        BatchCode = entity.BatchCode,
                        UnitId = entity.UnitID,
                        UnitNumber = entity.UnitNumber,
                        UnitArea = entity.UnitAera,
                        UnitType = entity.UnitTypeName,
                        BizType = entity.BizTypeName,
                    };
                    model.UnitList.Add(info);
                });
            }

            return model;
        }
    }
}
