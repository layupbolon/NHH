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
    /// 铺位租决服务
    /// </summary>
    public class ProjectUnitRequestService : NHHService<NHHEntities>, IProjectUnitRequestService
    {
        /// <summary>
        /// 获取租决列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public List<ProjectUnitRequestInfo> GetRequestList(ProjectUnitRequestListQueryInfo queryInfo)
        {
            queryInfo.ProjectName = (from p in Context.Project
                                     where p.ProjectID == queryInfo.ProjectId.Value
                                     select p.ProjectName).FirstOrDefault();

            var list = new List<ProjectUnitRequestInfo>();

            var query = from pur in Context.View_Project_UnitRequest
                        where pur.Status == 1
                        select new
                        {
                            pur.ProjectID,
                            pur.BuildingID,
                            pur.BuildingName,
                            pur.FloorID,
                            pur.FloorName,
                            pur.UnitID,
                            pur.UnitNumber,
                            pur.UnitAera,
                            pur.BizTypeID,
                            pur.BizCategoryID,
                            pur.UnitType,
                            pur.BizTypeName,
                            pur.BizCategoryName,
                            pur.UnitTypeName,
                            pur.BrandName,
                            pur.RequestArea,
                            pur.StandardRent,
                            pur.StandardMgmtFee,
                            pur.RequestStatus,
                            pur.RequestStatusName
                        };


            #region 查询条件
            if (queryInfo.ProjectId.HasValue)
            {
                query = query.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }
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
            if (queryInfo.BizType.HasValue)
            {
                query = query.Where(item => item.BizTypeID == queryInfo.BizType.Value);
            }
            if (queryInfo.RequestStatus.HasValue)
            {
                query = query.Where(item => item.RequestStatus == queryInfo.RequestStatus.Value);
            }
            #endregion

            var entityList = query.OrderBy(item => item.FloorID).ThenBy(item => item.UnitNumber).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var info = new ProjectUnitRequestInfo
                    {
                        ProjectId = entity.ProjectID,
                        BuildingName = entity.BuildingName,
                        FloorName = entity.FloorName,
                        UnitId = entity.UnitID,
                        UnitNumber = entity.UnitNumber,
                        UnitArea = entity.UnitAera,
                        UnitTypeName = entity.UnitTypeName,
                        RequestArea = entity.RequestArea.HasValue ? entity.RequestArea.Value : 0,
                        BizTypeName = entity.BizTypeName,
                        BizCategoryName = entity.BizCategoryName,
                        BrandName = entity.BrandName,
                        StandardRent = entity.StandardRent.HasValue ? entity.StandardRent.Value : 0,
                        StandardMgmtFee = entity.StandardMgmtFee.HasValue ? entity.StandardMgmtFee.Value : 0,
                        RequestStatusName = entity.RequestStatusName
                    };
                    list.Add(info);
                });
            }

            return list;
        }

        /// <summary>
        /// 获取铺位租决列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ProjectUnitRequestListModel GetList(ProjectUnitRequestListQueryInfo queryInfo)
        {
            var model = new ProjectUnitRequestListModel();
            model.QueryInfo = queryInfo;
            model.RequestList = new List<ProjectUnitRequestInfo>();
            model.PagingInfo = new Models.Common.PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };

            var query = from pur in Context.Project_UnitRequest
                        join pu in Context.View_Project_Unit on pur.UnitID equals pu.UnitID
                        join pue in Context.View_Project_UnitExt on pu.UnitID equals pue.UnitID
                        join rs in Context.Dictionary on pur.RequestStatus equals rs.FieldValue
                        where pur.Status == 1 && rs.FieldType == "UnitRequestStatus"
                        select new
                        {
                            pu.ProjectID,
                            pu.BuildingID,
                            pu.FloorID,
                            pu.BuildingName,
                            pu.FloorName,
                            pur.UnitID,
                            pu.UnitNumber,
                            pur.RequestArea,
                            pu.UnitType,
                            pu.BizTypeID,
                            pue.BizTypeName,
                            pu.BizCategoryID,
                            pur.BrandName,
                            pur.StandardRent,
                            pur.StandardMgmtFee,
                            pur.RequestStatus,
                            RequestStatusName = rs.FieldName,
                            pur.EditDate,
                        };
            #region 查询条件
            if (queryInfo.ProjectId.HasValue)
            {
                query = query.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }
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
            if (queryInfo.BizType.HasValue)
            {
                query = query.Where(item => item.BizTypeID == queryInfo.BizType.Value);
            }
            if (queryInfo.RequestStatus.HasValue)
            {
                query = query.Where(item => item.RequestStatus == queryInfo.RequestStatus.Value);
            }
            #endregion

            model.PagingInfo.TotalCount = query.Count();
            var entityList = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    var info = new ProjectUnitRequestInfo
                    {
                        BuildingName = entity.BuildingName,
                        FloorName = entity.FloorName,
                        UnitId = entity.UnitID,
                        UnitNumber = entity.UnitNumber,
                        RequestArea = entity.RequestArea,
                        BizTypeName = entity.BizTypeName,
                        BrandName = entity.BrandName,
                        StandardRent = entity.StandardRent,
                        StandardMgmtFee = entity.StandardMgmtFee,
                        RequestStatus = entity.RequestStatus,
                        RequestStatusName = entity.RequestStatusName,
                        UpdateDate = entity.EditDate.HasValue ? entity.EditDate.Value : DateTime.Now
                    };
                    model.RequestList.Add(info);
                });
            }
            return model;
        }

        /// <summary>
        /// 导入招商租决
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public MessageBag<bool> Import(List<ProjectUnitRequestInfo> list)
        {
            var result = new MessageBag<bool>();
            result.Code = 0;

            if (list == null || list.Count == 0)
                return result;

            int num = 0;
            list.ForEach(info =>
            {
                if (Import(info))
                {
                    num += 1;
                }
            });
            result.Desc = string.Format("成功导入{0}份租决", num);
            return result;
        }

        /// <summary>
        /// 导入铺位租决
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Import(ProjectUnitRequestInfo info)
        {
            #region 数据检查
            if (info.UnitId <= 0)
                return false;

            if (string.IsNullOrEmpty(info.BrandName) || info.BrandName.Length == 0)
                return false;

            if (string.IsNullOrEmpty(info.BizTypeName) || info.BizTypeName.Length == 0)
                return false;
            int bizType = (from bt in Context.BizType
                           where bt.Status == 1 && bt.BizTypeName == info.BizTypeName
                           select bt.BizTypeID).FirstOrDefault();
            if (bizType <= 0)
                return false;

            //if (string.IsNullOrEmpty(info.BizCategoryName) || info.BizCategoryName.Length == 0)
            //    return false;
            int bizCategory = (from bc in Context.BizCategory
                               where bc.Status == 1 && bc.BizCategoryName == info.BizCategoryName
                               select bc.BizCategoryID).FirstOrDefault();

            if (string.IsNullOrEmpty(info.RequestStatusName) || info.RequestStatusName.Length == 0)
                return false;
            int requestStatus = (from rs in Context.Dictionary
                                 where rs.FieldType == "UnitRequestStatus" && rs.FieldName == info.RequestStatusName
                                 select rs.FieldValue).FirstOrDefault();
            if (requestStatus <= 0)
                return false;
            #endregion

            var isInit = false;
            var requestBll = CreateBizObject<Project_UnitRequest>();

            var requestEntity = requestBll.TopOne(item => item.UnitID == info.UnitId);
            if (requestEntity == null)
            {
                requestEntity = new Project_UnitRequest
                {
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = 0
                };
                isInit = true;
            }

            requestEntity.UnitID = info.UnitId;
            requestEntity.RequestArea = info.RequestArea;
            requestEntity.BizType = bizType;
            requestEntity.BizCategory = bizCategory;
            requestEntity.BrandName = info.BrandName;
            requestEntity.StandardRent = info.StandardRent;
            requestEntity.StandardMgmtFee = info.StandardMgmtFee;
            requestEntity.RequestStatus = requestStatus;
            requestEntity.EditDate = DateTime.Now;
            requestEntity.EditUser = 0;

            if (isInit)
                requestBll.Insert(requestEntity);
            else
                requestBll.Update(requestEntity);
            
            //保存数据更新
            SaveChanges();

            return true;
        }

        /// <summary>
        /// 铺位租决进度
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ProjectUnitRequestScheduleModel Schedule(ProjectUnitRequestListQueryInfo queryInfo)
        {
            var model = new ProjectUnitRequestScheduleModel();
            model.QueryInfo = queryInfo;
            model.ScheduleData = new List<ProjectUnitRequestScheduleItem>();

            queryInfo.BuildingName = (from b in Context.Project_Building
                                      where b.Status == 1 && b.BuildingID == queryInfo.BuildingId.Value
                                      select b.BuildingName).FirstOrDefault();
            queryInfo.FloorName = (from f in Context.Project_Floor
                                   where f.Status == 1 && f.FloorID == queryInfo.FloorId.Value
                                   select f.FloorName).FirstOrDefault();

            #region 总计
            var totalQuery = from pu in Context.Project_Unit
                             join pup in Context.Project_UnitPlan on pu.UnitID equals pup.UnitID
                             where pu.Status == 1 && pup.Status == 1
                             select new
                             {
                                 pu.UnitID,
                                 pu.ProjectID,
                                 pu.BuildingID,
                                 pu.FloorID,
                                 pu.UnitType,
                                 pu.UnitAera,
                                 StandardRent = pup.StandardRent.HasValue ? pup.StandardRent.Value : 0,
                             };

            if (queryInfo.ProjectId.HasValue)
            {
                totalQuery = totalQuery.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }
            if (queryInfo.BuildingId.HasValue)
            {
                totalQuery = totalQuery.Where(item => item.BuildingID == queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                totalQuery = totalQuery.Where(item => item.FloorID == queryInfo.FloorId.Value);
            }
            if (queryInfo.UnitType.HasValue)
            {
                totalQuery = totalQuery.Where(item => item.UnitType == queryInfo.UnitType.Value);
            }
            model.TotalUnit = totalQuery.Count();
            if (model.TotalUnit > 0)
            {
                model.TotalArea = Math.Round(totalQuery.Sum(item => item.UnitAera), 2);
                model.TotalRent = Math.Round(totalQuery.Sum(item => item.StandardRent * item.UnitAera * 360) / 10000, 2);
            }
            #endregion

            var requestQuery = from pur in Context.Project_UnitRequest
                               join pu in Context.Project_Unit on pur.UnitID equals pu.UnitID
                               where pur.Status == 1 && pu.Status == 1
                               select new
                               {
                                   pu.ProjectID,
                                   pu.BuildingID,
                                   pu.FloorID,
                                   pu.UnitID,
                                   pu.UnitType,
                                   pu.UnitAera,
                                   pur.RequestStatus,
                                   pur.RequestArea,
                                   pur.StandardRent,
                               };

            if (queryInfo.ProjectId.HasValue)
            {
                requestQuery = requestQuery.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }
            if (queryInfo.BuildingId.HasValue)
            {
                requestQuery = requestQuery.Where(item => item.BuildingID == queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                requestQuery = requestQuery.Where(item => item.FloorID == queryInfo.FloorId.Value);
            }
            if (queryInfo.UnitType.HasValue)
            {
                requestQuery = requestQuery.Where(item => item.UnitType == queryInfo.UnitType.Value);
            }

            //状态列表
            var requestStatusList = (from d in Context.Dictionary
                                     where d.FieldType == "UnitRequestStatus"
                                     select new
                                     {
                                         Id = d.FieldValue,
                                         Name = d.FieldName
                                     }).ToList();

            requestStatusList.ForEach(status =>
            {
                var scheduleItem = new ProjectUnitRequestScheduleItem
                {
                    RequestStatus = status.Id,
                    RequestStatusName = status.Name,
                };

                var requestGroup = requestQuery.Where(item => item.RequestStatus == status.Id);

                scheduleItem.UnitNum = requestGroup.Count();
                if (scheduleItem.UnitNum > 0)
                {
                    scheduleItem.Area = Math.Round(requestGroup.Select(item => item.RequestArea).Sum(), 2);
                    scheduleItem.Rent = Math.Round(requestGroup.Select(item => item.RequestArea * item.StandardRent * 360).Sum() / 10000, 2);
                }

                model.ScheduleData.Add(scheduleItem);
            });

            //没有租决的铺位
            totalQuery = totalQuery.Where(item => !requestQuery.Any(item2 => item.UnitID == item2.UnitID));
            var scheduleItem1 = model.ScheduleData.Find(item => item.RequestStatus == 1);
            var unitNum = totalQuery.Count();
            scheduleItem1.UnitNum += unitNum;
            if (unitNum > 0)
            {
                scheduleItem1.Area += Math.Round(totalQuery.Select(item => item.UnitAera).Sum(), 2);
                scheduleItem1.Rent += Math.Round(totalQuery.Select(item => item.UnitAera * item.StandardRent * 360).Sum() / 10000, 2);
            }
            //占比情况
            model.ScheduleData.ForEach(item =>
            {
                if (item.UnitNum > 0)
                {
                    item.UnitNumProp = (decimal)item.UnitNum / model.TotalUnit;
                }
                if (item.Area > 0)
                {
                    item.AreaProp = item.Area / model.TotalArea;
                }
                if (item.Rent > 0)
                {
                    item.RnetProp = item.Rent / model.TotalRent;
                }
            });

            return model;
        }

        /// <summary>
        /// 铺位租决跟踪
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ProjectUnitRequestTrackModel Track(ProjectUnitRequestListQueryInfo queryInfo)
        {
            var model = new ProjectUnitRequestTrackModel();
            model.QueryInfo = queryInfo;
            model.UnitTypeList = new List<ProjectUnitRequestTrackItem>();
            model.BizTypeList = new List<ProjectUnitRequestTrackItem>();

            queryInfo.ProjectName = (from p in Context.Project
                                     where p.ProjectID == queryInfo.ProjectId.Value
                                     select p.ProjectName).FirstOrDefault();
            queryInfo.BuildingName = (from b in Context.Project_Building
                                      where b.Status == 1 && b.BuildingID == queryInfo.BuildingId.Value
                                      select b.BuildingName).FirstOrDefault();
            queryInfo.FloorName = (from f in Context.Project_Floor
                                   where f.Status == 1 && f.FloorID == queryInfo.FloorId.Value
                                   select f.FloorName).FirstOrDefault();
            #region 总计
            var totalQuery = from pu in Context.Project_Unit
                             join pup in Context.Project_UnitPlan on pu.UnitID equals pup.UnitID
                             where pu.Status == 1 && pup.Status == 1
                             select new
                             {
                                 pu.UnitID,
                                 pu.ProjectID,
                                 pu.BuildingID,
                                 pu.FloorID,
                                 pu.UnitType,
                                 pu.UnitAera,
                                 pu.BizTypeID,
                                 StandardRent = pup.StandardRent.HasValue ? pup.StandardRent.Value : 0,
                             };

            if (queryInfo.ProjectId.HasValue)
            {
                totalQuery = totalQuery.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }
            if (queryInfo.BuildingId.HasValue)
            {
                totalQuery = totalQuery.Where(item => item.BuildingID == queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                totalQuery = totalQuery.Where(item => item.FloorID == queryInfo.FloorId.Value);
            }
            if (queryInfo.UnitType.HasValue)
            {
                totalQuery = totalQuery.Where(item => item.UnitType == queryInfo.UnitType.Value);
            }
            model.TotalUnit = totalQuery.Count();
            if (model.TotalUnit > 0)
            {
                model.TotalArea = Math.Round(totalQuery.Select(item => item.UnitAera).Sum(), 2);
                model.TotalRent = Math.Round(totalQuery.Select(item => item.StandardRent * item.UnitAera * 360).Sum() / 10000, 2);
            }
            #endregion

            #region 租决
            var requestQuery = from pur in Context.Project_UnitRequest
                               join pu in Context.Project_Unit on pur.UnitID equals pu.UnitID
                               where pur.Status == 1 && pu.Status == 1
                               select new
                               {
                                   pu.ProjectID,
                                   pu.BuildingID,
                                   pu.FloorID,
                                   pu.UnitID,
                                   pu.UnitType,
                                   pu.BizTypeID,
                                   pu.UnitAera,
                                   pur.RequestStatus,
                                   pur.RequestArea,
                                   pur.StandardRent,
                               };

            if (queryInfo.ProjectId.HasValue)
            {
                requestQuery = requestQuery.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }
            if (queryInfo.BuildingId.HasValue)
            {
                requestQuery = requestQuery.Where(item => item.BuildingID == queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                requestQuery = requestQuery.Where(item => item.FloorID == queryInfo.FloorId.Value);
            }
            if (queryInfo.UnitType.HasValue)
            {
                requestQuery = requestQuery.Where(item => item.UnitType == queryInfo.UnitType.Value);
            }

            model.TotalRequestUnit = requestQuery.Count();
            if (model.TotalRequestUnit > 0)
            {
                model.TotalRequestArea = Math.Round(requestQuery.Select(item => item.RequestArea).Sum(), 2);
                model.TotalRequestRent = Math.Round(requestQuery.Select(item => item.RequestArea * item.StandardRent * 360).Sum() / 10000, 2);
            }
            #endregion

            #region 铺位类型
            var unitTypeList = (from ut in Context.Dictionary
                                where ut.FieldType == "ProjectUnitType"
                                select new
                                {
                                    UnitTypeId = ut.FieldValue,
                                    UnitTypeName = ut.FieldName
                                }).ToList();

            unitTypeList.ForEach(unitType =>
            {
                var trackItem = new ProjectUnitRequestTrackItem
                {
                    TrackType = unitType.UnitTypeId,
                    TrackTypeName = unitType.UnitTypeName,
                };

                var query1 = totalQuery.Where(item => item.UnitType == unitType.UnitTypeId);
                trackItem.UnitNum = query1.Count();

                if (trackItem.UnitNum > 0)
                {
                    trackItem.Area = Math.Round(query1.Select(item => item.UnitAera).Sum(), 2);
                    trackItem.Rent = Math.Round(query1.Select(item => item.UnitAera * item.StandardRent * 360).Sum() / 10000, 2);
                }
                var query2 = requestQuery.Where(item => item.UnitType == unitType.UnitTypeId);
                trackItem.RequestUnitNum = query2.Count();
                if (trackItem.RequestUnitNum > 0)
                {
                    trackItem.RequestArea = Math.Round(query2.Select(item => item.RequestArea).Sum(), 2);
                    trackItem.RequestRent = Math.Round(query2.Select(item => item.RequestArea * item.StandardRent * 360).Sum() / 10000, 2);
                }

                trackItem.UnitNumProp = trackItem.UnitNum <= 0 ? 1m : (decimal)trackItem.RequestUnitNum / trackItem.UnitNum;
                trackItem.AreaProp = trackItem.Area <= 0 ? 1m : trackItem.RequestArea / trackItem.Area;
                trackItem.RentProp = trackItem.Rent <= 0 ? 1m : trackItem.RequestRent / trackItem.Rent;

                model.UnitTypeList.Add(trackItem);
            });
            #endregion

            #region 业态
            var bizTypeList = (from bt in Context.BizType
                               where bt.Status == 1
                               select new
                               {
                                   bt.BizTypeID,
                                   bt.BizTypeName
                               }).ToList();
            bizTypeList.ForEach(bizType =>
            {
                var trackItem = new ProjectUnitRequestTrackItem
                {
                    TrackType = bizType.BizTypeID,
                    TrackTypeName = bizType.BizTypeName,
                };
                var query1 = totalQuery.Where(item => item.BizTypeID == bizType.BizTypeID);
                trackItem.UnitNum = query1.Count();
                if (trackItem.UnitNum > 0)
                {
                    trackItem.Area = Math.Round(query1.Select(item => item.UnitAera).Sum(), 2);
                    trackItem.Rent = Math.Round(query1.Select(item => item.UnitAera * item.StandardRent * 360).Sum() / 10000, 2);
                }
                var query2 = requestQuery.Where(item => item.BizTypeID == bizType.BizTypeID);
                trackItem.RequestUnitNum = query2.Count();
                if (trackItem.RequestUnitNum > 0)
                {
                    trackItem.RequestArea = Math.Round(query2.Select(item => item.RequestArea).Sum(), 2);
                    trackItem.RequestRent = Math.Round(query2.Select(item => item.RequestArea * item.StandardRent * 360).Sum() / 10000, 2);
                }

                trackItem.UnitNumProp = trackItem.UnitNum <= 0 ? 1m : (decimal)trackItem.RequestUnitNum / trackItem.UnitNum;
                trackItem.AreaProp = trackItem.Area <= 0 ? 1m : trackItem.RequestArea / trackItem.Area;
                trackItem.RentProp = trackItem.Rent <= 0 ? 1m : trackItem.RequestRent / trackItem.Rent;

                model.BizTypeList.Add(trackItem);
            });
            #endregion
            return model;
        }
    }
}
