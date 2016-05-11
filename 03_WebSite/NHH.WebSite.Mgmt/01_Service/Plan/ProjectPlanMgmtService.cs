using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Plan;
using NHH.Service.Plan.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Plan
{
    /// <summary>
    /// 项目招商管理服务接口
    /// </summary>
    public class ProjectPlanMgmtService : NHHService<NHHEntities>, IProjectPlanMgmtService
    {
        /// <summary>
        /// 获取铺位筹划列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public List<ProjectUnitPlanInfo> GetUnitPlanList(ProjectPlanListQueryInfo queryInfo)
        {
            queryInfo.ProjectName = (from p in Context.Project
                                     where p.ProjectID == queryInfo.ProjectId.Value
                                     select p.ProjectName).FirstOrDefault();

            var list = new List<ProjectUnitPlanInfo>();

            var query = from pu in Context.View_Project_Unit
                        join pue in Context.View_Project_UnitExt on pu.UnitID equals pue.UnitID
                        join ut in Context.View_UnitType on pu.UnitType equals ut.UnitTypeID
                        join up in Context.Project_UnitPlan on pu.UnitID equals up.UnitID into tempPlan
                        from unitPlan in tempPlan.DefaultIfEmpty()
                        where pu.Status == 1 && pu.ProjectID == queryInfo.ProjectId.Value
                        select new
                        {
                            pu.BuildingID,
                            pu.BuildingName,
                            pu.FloorID,
                            pu.FloorName,
                            pu.UnitID,
                            pu.UnitNumber,
                            pu.UnitAera,
                            pu.BizTypeID,
                            pu.BizCategoryID,
                            pu.UnitType,
                            pu.UnitStatus,
                            ut.UnitTypeName,
                            pue.UnitStatusName,
                            pue.BizTypeName,
                            pue.BizCategoryName,
                            unitPlan.StandardRent,
                            unitPlan.StandardMgmtFee,
                        };
            #region 查询信息
            if (queryInfo.BuildingId.HasValue)
            {
                query = query.Where(item => item.BuildingID == queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                query = query.Where(item => item.FloorID == queryInfo.FloorId.Value);
            }
            if (queryInfo.BatchId.HasValue && queryInfo.BatchId.Value > 0)
            {
                query = query.Where(item => (from pub in Context.Project_BatchUnit
                                             where pub.Status == 1 && pub.UnitID == item.UnitID && pub.BatchID == queryInfo.BatchId.Value
                                             select pub.BatchUnitID).Any());
            }
            if (queryInfo.UnitType.HasValue)
            {
                query = query.Where(item => item.UnitType == queryInfo.UnitType.Value);
            }
            if (queryInfo.BizType.HasValue)
            {
                query = query.Where(item => item.BizTypeID == queryInfo.BizType.Value);
            }
            if (queryInfo.UnitStatus.HasValue)
            {
                query = query.Where(item => item.UnitStatus == queryInfo.UnitStatus.Value);
            }
            #endregion

            if (!query.Any())
                return list;
            var entityList = query.OrderBy(item => item.FloorID).ThenBy(item => item.UnitNumber).ToList();
            entityList.ForEach(entity =>
            {
                var info = new ProjectUnitPlanInfo()
                {
                    BuindingName = entity.BuildingName,
                    FloorName = entity.FloorName,
                    UnitId = entity.UnitID,
                    UnitNumber = entity.UnitNumber,
                    UnitArea = entity.UnitAera,
                    UnitTypeName = entity.UnitTypeName,
                    BizTypeName = entity.BizTypeName,
                    BizCategoryName = entity.BizCategoryName,
                    StandardRent = entity.StandardRent.HasValue ? entity.StandardRent.Value : 0,
                    StandardMgmtFee = entity.StandardMgmtFee.HasValue ? entity.StandardMgmtFee.Value : 0,
                };
                //后期改视图
                info.BatchCode = (from pb in Context.Project_Batch
                                  join pub in Context.Project_BatchUnit on pb.BatchID equals pub.BatchID
                                  where pb.Status == 1 && pub.Status == 1 && pub.UnitID == entity.UnitID
                                  select pb.BatchCode).FirstOrDefault();
                list.Add(info);
            });

            return list;
        }

        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public MessageBag<bool> Import(List<ProjectUnitPlanInfo> list)
        {
            var result = new MessageBag<bool>();
            result.Code = -1;

            int succeedNum = 0;
            int failedNum = 0;
            list.ForEach(info =>
            {
                try
                {
                    if (ImportPlan(info))
                    {
                        succeedNum += 1;
                    }
                    else
                    {
                        failedNum += 1;
                    }
                }
                catch (Exception)
                {
                    failedNum += 1;
                }
            });

            result.Code = 0;
            result.Desc = string.Format("导入完成，成功{0}份，失败{1}份", succeedNum, failedNum);
            return result;
        }

        /// <summary>
        /// 导入招商筹划
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool ImportPlan(ProjectUnitPlanInfo info)
        {
            info.ProjectId = (from pu in Context.Project_Unit
                              where pu.UnitID == info.UnitId
                              select pu.ProjectID).FirstOrDefault();

            //检查铺位是否有招商筹划
            InitEmptyUnitPlan(info.UnitId);

            var unitBll = CreateBizObject<Project_Unit>();
            var unit = unitBll.GetBySysNo(info.UnitId);
            if (unit == null)
                return false;

            unit.UnitAera = info.UnitArea;

            #region 类型
            var unitTypeId = (from ut in Context.View_UnitType
                              where ut.UnitTypeName == info.UnitTypeName
                              select ut.UnitTypeID).FirstOrDefault();
            if (unitTypeId > 0)
            {
                unit.UnitType = unitTypeId;
            }
            #endregion

            #region 业态
            var bizTypeId = (from bt in Context.BizType
                             where bt.Status == 1 && bt.BizTypeName == info.BizTypeName
                             select bt.BizTypeID).FirstOrDefault();
            if (bizTypeId > 0)
            {
                unit.BizTypeID = bizTypeId;
            }
            #endregion

            #region 品类
            var bizCategoryId = (from bc in Context.BizCategory
                                 where bc.Status == 1 && bc.BizCategoryName == info.BizCategoryName
                                 select bc.BizCategoryID).FirstOrDefault();
            if (bizCategoryId > 0)
            {
                unit.BizCategoryID = bizCategoryId;
            }
            #endregion

            unitBll.Update(unit);

            var planBll = CreateBizObject<Project_UnitPlan>();
            var unitPlan = planBll.TopOne(item => item.UnitID == info.UnitId);
            unitPlan.StandardRent = info.StandardRent;
            unitPlan.StandardMgmtFee = info.StandardMgmtFee;
            unitPlan.BizTypeID = unit.BizTypeID;
            unitPlan.BizCategoryID = unit.BizCategoryID;
            planBll.Update(unitPlan);

            #region 招商批次
            var batchId = (from pb in Context.Project_Batch
                           where pb.Status == 1 && pb.ProjectID == info.ProjectId && pb.BatchCode == info.BatchCode
                           select pb.BatchID).FirstOrDefault();
            if (batchId > 0)
            {
                var batchBll = CreateBizObject<Project_BatchUnit>();
                var unitBatch = batchBll.TopOne(item => item.UnitID == info.UnitId);
                if (unitBatch == null)
                {
                    unitBatch = new Project_BatchUnit
                    {
                        UnitID = info.UnitId,
                        BatchID = batchId,
                        Status = 1,
                        InDate = DateTime.Now,
                        InUser = 0,
                    };
                    batchBll.Insert(unitBatch);
                }
                else
                {
                    unitBatch.BatchID = batchId;
                    unitBatch.EditDate = DateTime.Now;
                    batchBll.Update(unitBatch);
                }
            }
            #endregion

            SaveChanges();
            return true;
        }

        /// <summary>
        /// 初始化空的铺位招商筹划信息
        /// </summary>
        /// <param name="unitId"></param>
        public void InitEmptyUnitPlan(int unitId)
        {
            var planBll = CreateBizObject<Project_UnitPlan>();
            var unitPlan = planBll.TopOne(item => item.UnitID == unitId);
            if (unitPlan == null)
            {
                unitPlan = new Project_UnitPlan
                {
                    UnitID = unitId,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = 0,
                };
                planBll.Insert(unitPlan);
            }

            var leasingBll = CreateBizObject<Project_UnitLeasing>();
            var unitLeasing = leasingBll.TopOne(item => item.UnitID == unitId);
            if (unitLeasing == null)
            {
                unitLeasing = new Project_UnitLeasing
                {
                    UnitID = unitId,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = 0,
                };
                leasingBll.Insert(unitLeasing);
            }

            var specBll = CreateBizObject<Project_UnitSpec>();
            var unitSpec1 = specBll.TopOne(item => item.UnitID == unitId && item.SpecType == 1);
            if (unitSpec1 == null)
            {
                unitSpec1 = new Project_UnitSpec
                {
                    UnitID = unitId,
                    SpecType = 1,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = 0,
                };
                specBll.Insert(unitSpec1);
            }
            var unitSpec2 = specBll.TopOne(item => item.UnitID == unitId && item.SpecType == 2);
            if (unitSpec2 == null)
            {
                unitSpec2 = new Project_UnitSpec
                {
                    UnitID = unitId,
                    SpecType = 2,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = 0,
                };
                specBll.Insert(unitSpec2);
            }

            SaveChanges();
        }
    }
}
