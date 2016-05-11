using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Project.ProjectUnit;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project
{
    /// <summary>
    /// 铺位调整服务
    /// </summary>
    public class ProjectUnitAdjustService : NHHService<NHHEntities>, IProjectUnitAdjustService
    {
        /// <summary>
        /// 合铺
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public MessageBag<int> Merge(ProjectUnitMergeData data)
        {
            var result = new MessageBag<int>();
            result.Data = 0;
            result.Code = -1;

            #region 数据检查
            if (data.ProjectId <= 0)
            {
                result.Desc = "请选择项目";
                return result;
            }

            if (string.IsNullOrEmpty(data.UnitNumber) || data.UnitNumber.Length == 0)
            {
                result.Desc = "请输入新铺位编号";
                return result;
            }

            if (data.UnitArea <= 0)
            {
                result.Desc = "请检查铺位面积";
                return result;
            }

            if (data.OriginalUnitID == null || data.OriginalUnitID.Length == 0)
            {
                result.Desc = "请设置需要合并的铺位";
                return result;
            }
            #endregion

            //取第一个铺位的数据
            var oldUnitId = data.OriginalUnitID[0];
            var oldUnit = (from pu in Context.Project_Unit
                           where pu.UnitID == oldUnitId
                           select new
                           {
                               pu.BuildingID,
                               pu.FloorID,
                               pu.UnitStatus,
                               pu.UnitType,
                               pu.ContractStatus,
                           }).FirstOrDefault();

            if (oldUnit == null)
            {
                result.Desc = "未找到相关铺位";
                return result;
            }

            var unitBll = CreateBizObject<Project_Unit>();
            
            //新增铺位
            var newUnit = new Project_Unit
            {
                ProjectID = data.ProjectId,
                BuildingID = oldUnit.BuildingID,
                FloorID = oldUnit.FloorID,
                UnitNumber = data.UnitNumber,
                UnitAera = data.UnitArea,
                UnitStatus = oldUnit.UnitStatus,
                UnitType = oldUnit.UnitType,
                ContractStatus = oldUnit.ContractStatus,
                Status = 1,
                InDate = DateTime.Now,
                InUser = data.InUser,
            };

            unitBll.Insert(newUnit);
            
            var adjustBll = CreateBizObject<Project_UnitAdjust>();
            foreach (int unitId in data.OriginalUnitID)
            {
                //删除老铺位
                unitBll.Delete(unitId);
                //调整日志
                var unitAdjust = new Project_UnitAdjust
                {
                    AdjustType = 1,
                    UnitID = newUnit.UnitID,
                    OriginalUnitID = unitId,
                    Remark = data.Remark,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = data.InUser,
                };
                adjustBll.Insert(unitAdjust);
            }           
            //提交数据
            SaveChanges();

            result.Code = 0;
            result.Data = newUnit.UnitID;

            return result;
        }

        /// <summary>
        /// 拆铺
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public MessageBag<bool> Split(ProjectUnitSplitData data)
        {
            var result = new MessageBag<bool>();
            result.Code = -1;

            if (data.NewUnitList == null || data.NewUnitList.Length == 0)
            {
                result.Desc = "新铺位列表无效";
                return result;
            }

            var oldUnit = (from pu in Context.Project_Unit
                           where pu.ProjectID == data.ProjectId && pu.UnitNumber == data.UnitNumber
                           select new
                           {
                               pu.UnitID,
                               pu.ProjectID,
                               pu.BuildingID,
                               pu.FloorID,
                               pu.UnitStatus,
                               pu.UnitType,
                               pu.ContractStatus,
                               pu.BizType,
                           }).FirstOrDefault();

            if (oldUnit == null)
            {
                result.Desc = "原铺位编号无效";
                return result;
            }

            var unitBll = CreateBizObject<Project_Unit>();
            //删除原铺位
            unitBll.Delete(oldUnit.UnitID);

            var adjustBll = CreateBizObject<Project_UnitAdjust>();
            foreach (var newUnit in data.NewUnitList)
            {
                var unitEntity = new Project_Unit
                {
                    ProjectID = oldUnit.ProjectID,
                    BuildingID = oldUnit.BuildingID,
                    FloorID = oldUnit.FloorID,
                    UnitNumber = newUnit.UnitCode,
                    UnitAera = newUnit.UnitArea,
                    UnitStatus = oldUnit.UnitStatus,
                    UnitType = oldUnit.UnitType,
                    ContractStatus = oldUnit.ContractStatus,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = data.InUser,
                };
                //新增铺位
                var newUnitId = CreateNewUnit(unitEntity);
                //调整日志
                var unitAdjust = new Project_UnitAdjust
                {
                    AdjustType = 2,
                    UnitID = newUnitId,
                    OriginalUnitID = oldUnit.UnitID,
                    Remark = data.Remark,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = data.InUser,
                };
                adjustBll.Insert(unitAdjust);
            }
            //保存更新
            SaveChanges();

            result.Code = 0;
            result.Desc = "铺位拆分完成";
            return result;
        }

        /// <summary>
        /// 创建新铺位
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private int CreateNewUnit(Project_Unit entity)
        {
            CreateBizObject<Project_Unit>().Insert(entity);
            SaveChanges();
            return entity.UnitID;
        }

        /// <summary>
        /// 获取铺位调整列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ProjectUnitAdjustListModel GetAdjustList(ProjectUnitAdjustListQueryInfo queryInfo)
        {
            var model = new ProjectUnitAdjustListModel();
            model.QueryInfo = queryInfo;
            model.AdjustList = new List<ProjectUnitAdjustInfo>();
            model.PagingInfo = new Models.Common.PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };

            var query = from pua in Context.Project_UnitAdjust
                        join pu1 in Context.View_Project_Unit on pua.UnitID equals pu1.UnitID
                        join pu2 in Context.Project_Unit on pua.OriginalUnitID equals pu2.UnitID
                        join d in Context.Dictionary on pua.AdjustType equals d.FieldValue
                        where pua.Status == 1 && d.FieldType == "UnitAdjustType"
                        select new
                        {
                            pu1.ProjectID,
                            pu1.ProjectName,
                            pu1.BuildingID,
                            pu1.BuildingName,
                            pu1.FloorID,
                            pu1.FloorName,
                            pu1.BizTypeID,
                            pu1.UnitType,
                            pua.AdjustID,
                            pua.AdjustType,
                            pua.UnitID,
                            pua.OriginalUnitID,
                            pua.Remark,
                            pu1.UnitNumber,
                            pu1.UnitAera,
                            OriginalUnitNumber = pu2.UnitNumber,
                            OriginalUnitArea = pu2.UnitAera,
                            AdjustDate = pua.InDate,
                            AdjustTypeName = d.FieldName,
                        };
            #region 查询条件
            if (queryInfo.ProjectId.HasValue)
            {
                query = query.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }

            if (queryInfo.AdjustType.HasValue)
            {
                query = query.Where(item => item.AdjustType == queryInfo.AdjustType.Value);
            }

            if (queryInfo.BuildingId.HasValue)
            {
                query = query.Where(item => item.BuildingID == queryInfo.BuildingId.Value);
            }

            if (queryInfo.FloorId.HasValue)
            {
                query = query.Where(item => item.FloorID == queryInfo.FloorId.Value);
            }

            if (queryInfo.BizType.HasValue)
            {
                query = query.Where(item => item.BizTypeID == queryInfo.BizType.Value);
            }

            if (queryInfo.UnitType.HasValue)
            {
                query = query.Where(item => item.UnitType == queryInfo.UnitType.Value);
            }

            if (!string.IsNullOrEmpty(queryInfo.UnitNumber) && queryInfo.UnitNumber.Length > 0)
            {
                query = query.Where(item => item.UnitNumber.Contains(queryInfo.UnitNumber));
            }

            if (!string.IsNullOrEmpty(queryInfo.Remark) && queryInfo.Remark.Length > 0)
            {
                query = query.Where(item => item.Remark.Contains(queryInfo.Remark));
            }

            #endregion

            model.PagingInfo.TotalCount = query.Count();
            var entityList = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var info = new ProjectUnitAdjustInfo
                    {
                        AdjustId = entity.AdjustID,
                        ProjectId = entity.ProjectID,
                        ProjectName = entity.ProjectName,
                        BuildingName = entity.BuildingName,
                        FloorName = entity.FloorName,
                        UnitId = entity.UnitID,
                        UnitArea = entity.UnitAera,
                        UnitNumber = entity.UnitNumber,
                        OriginalUnitID = entity.OriginalUnitID,
                        OriginalUnitArea = entity.OriginalUnitArea,
                        OriginalUnitNumber = entity.OriginalUnitNumber,
                        AdjustType = entity.AdjustTypeName,
                        AdjustDate = entity.AdjustDate,
                    };
                    model.AdjustList.Add(info);
                });
            }

            return model;
        }

        /// <summary>
        /// 获取铺位调整详情
        /// </summary>
        /// <param name="adjustId"></param>
        /// <returns></returns>
        public ProjectUnitAdjustModel GetAdjustInfo(int adjustId)
        {
            var model = new ProjectUnitAdjustModel();

            var entity = (from pua in Context.Project_UnitAdjust
                         join pu1 in Context.View_Project_Unit on pua.UnitID equals pu1.UnitID
                         join pu2 in Context.Project_Unit on pua.OriginalUnitID equals pu2.UnitID
                         join d in Context.Dictionary on pua.AdjustType equals d.FieldValue
                         where pua.Status == 1 && d.FieldType == "UnitAdjustType" && pua.AdjustID == adjustId
                         select new
                         {
                             pu1.ProjectID,
                             pu1.ProjectName,
                             pu1.BuildingID,
                             pu1.BuildingName,
                             pu1.FloorID,
                             pu1.FloorName,
                             pu1.BizTypeID,
                             pu1.UnitType,
                             pua.AdjustID,
                             pua.AdjustType,
                             pua.UnitID,
                             pua.OriginalUnitID,
                             pua.Remark,
                             pu1.UnitNumber,
                             pu1.UnitAera,
                             OriginalUnitNumber = pu2.UnitNumber,
                             OriginalUnitArea = pu2.UnitAera,
                             AdjustDate = pua.InDate,
                             AdjustTypeName = d.FieldName,
                         }).FirstOrDefault();

            if (entity != null)
            {
                model.BuildingName = entity.BuildingName;
                model.FloorName = entity.FloorName;
                model.UnitNumber = entity.UnitNumber;
                model.UnitArea = entity.UnitAera;
                model.OriginalUnitNumber = entity.OriginalUnitNumber;
                model.OriginalUnitArea = entity.OriginalUnitArea;
                model.Remark = entity.Remark;
                model.AdjustType = entity.AdjustTypeName;
                model.AdjustDate = entity.AdjustDate;
            }

            return model;
        }
    }
}
