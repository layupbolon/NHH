using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using NHH.Entities;
using NHH.Framework.Caching;
using NHH.Framework.Service;
using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Official.ADPosition;
using NHH.Models.Official.Common;
using NHH.Models.Official.Feedback;
using NHH.Models.Official.Kiosk;
using NHH.Models.Official.Project;
using NHH.Models.Official.Unit;
using NHH.Service.Official.IService;

namespace NHH.Service.Official
{
    public class NHHCGService : NHHService<NHHEntities>, INHHCGService
    {
        #region Common

        /// <summary>
        /// 获取官网楼层下拉列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<SelectListItemInfo> GetNHHCGFloorDropDownList(NHHCGTypeEnum type)
        {
            List<SelectListItemInfo> result = new List<SelectListItemInfo>();
            List<CommonFieldInfo> floorList = CacheManager.Default.Get<List<CommonFieldInfo>>("NHHCGFloorDropDownList");
            if (floorList == null)
            {
                floorList = (from k in Context.NHHCG_Kiosk
                            select new CommonFieldInfo()
                            {
                                FieldValue = k.FloorID,
                                FieldName = k.FloorID + "楼",
                                FieldId = (int)NHHCGTypeEnum.Kiosk
                            }).Distinct().ToList();
                CacheManager.Default.Add("NHHCGFloorDropDownList", floorList);
            }
            result = (from l in floorList
                      where l.FieldId == (int)type
                      select new SelectListItemInfo()
                      {
                          Text = l.FieldName,
                          Value = l.FieldValue.ToString()
                      }).ToList();
            return result;
        }
        /// <summary>
        /// 获取官网楼别下拉列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<SelectListItemInfo> GetNGGCGBuildingDropDownList(NHHCGTypeEnum type)
        {
            List<SelectListItemInfo> result = new List<SelectListItemInfo>();
            List<CommonFieldInfo> buildingList =
                CacheManager.Default.Get<List<CommonFieldInfo>>("NHHCGBuildingDropDownList");
            if (buildingList == null)
            {
                buildingList = ((from u in Context.NHHCG_Unit
                                 select
                                     new CommonFieldInfo()
                                     {
                                         FieldDesc = u.Building,
                                         FieldName = u.Building,
                                         FieldId = (int)NHHCGTypeEnum.Unit
                                     }).Distinct().Union(
                            from k in Context.NHHCG_Kiosk
                            select new CommonFieldInfo()
                            {
                                FieldDesc = k.Building,
                                FieldName = k.Building,
                                FieldId = (int)NHHCGTypeEnum.Kiosk
                            }).Distinct().Union(
                                from ad in Context.NHHCG_ADPosition
                                select new CommonFieldInfo()
                                {
                                    FieldDesc = ad.Building,
                                    FieldName = ad.Building,
                                    FieldId = (int)NHHCGTypeEnum.ADPosition
                                }).Distinct()).ToList();
                CacheManager.Default.Add("NHHCGBuildingDropDownList", buildingList);
            }
            result = (from l in buildingList
                      where l.FieldId == (int)type
                      select new SelectListItemInfo()
                      {
                          Text = l.FieldName,
                          Value = l.FieldDesc
                      }).ToList();
            return result;
        }
        /// <summary>
        /// 获取官网的项目下拉列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItemInfo> GetNHHCGProjectDropDownList()
        {
            List<SelectListItemInfo> result = CacheManager.Default.Get<List<SelectListItemInfo>>("NHHCGProjectDropDownList");
            if (result != null)
                return result;
            else
            {
                result = (from p in Context.NHHCG_Project
                          where p.Status == 1
                          select new SelectListItemInfo()
                          {
                              Text = p.ProjectName,
                              Value = p.ProjectID.ToString()
                          }).ToList();
                CacheManager.Default.Add("NHHCGProjectDropDownList", result);
            }
            return result;
        }

        /// <summary>
        /// 获取官网的业态类型下拉列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItemInfo> GetNHHCGBizTypeDropDownList()
        {
            List<SelectListItemInfo> result = CacheManager.Default.Get<List<SelectListItemInfo>>("NHHCGBizTypeDropDownList");
            if (result != null)
                return result;
            else
            {
                result = (from p in Context.BizType
                          where p.Status == 1
                          select new SelectListItemInfo()
                          {
                              Text = p.BizTypeName,
                              Value = p.BizTypeID.ToString()
                          }).ToList();
                CacheManager.Default.Add("NHHCGBizTypeDropDownList", result);
            }
            return result;
        }
        #endregion Common

        #region NHHCGProject
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public NHHCGProjectListModel GetNHHCGProjectList(NHHCGProjectQueryModel queryInfo)
        {
            var model = new NHHCGProjectListModel();
            model.QueryInfo = queryInfo;
            model.ProjectList = new List<NHHCGProjectModel>();

            var query = (from p in Context.NHHCG_Project
                         select new NHHCGProjectModel()
                         {
                             ProjectID = p.ProjectID,
                             ProjectName = p.ProjectName,
                             BriefName = p.BriefName,
                             Feature = p.Feature,
                             OpeningDate = p.OpeningDate,
                             OperatingArea = p.OperatingArea,
                             GrossArea = p.GrossArea,
                             Population = p.Population,
                             BusinessType = p.BusinessType,
                             MonthlyHumanTraffic = p.MonthlyHumanTraffic,
                             PublicTransport = p.PublicTransport,
                             Introduce = p.Introduce,
                             Longitude = p.Longitude,
                             Latitude = p.Latitude,
                             Status = p.Status,
                             InDate = p.InDate,
                             InUser = p.InUser,
                             Position = p.Position,
                             Tel = p.Tel
                         });


            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page ?? 1;
            model.PagingInfo.TotalCount = query.Count();

            query = query.OrderByDescending(item => item.ProjectID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);

            var entityList = query.ToList();
            if (entityList != null)
            {
                model.ProjectList = new List<NHHCGProjectModel>();
                model.ProjectList = entityList;
            }
            return model;
        }

        /// <summary>
        /// 获取项目
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public NHHCGProjectModel GetNHHCGProject(int projectId)
        {
            NHHCGProjectModel model = new NHHCGProjectModel();
            var query = (from p in Context.NHHCG_Project
                         where p.ProjectID == projectId
                         select new NHHCGProjectModel()
                         {
                             ProjectID = p.ProjectID,
                             ProjectName = p.ProjectName,
                             BriefName = p.BriefName,
                             Feature = p.Feature,
                             OpeningDate = p.OpeningDate,
                             OperatingArea = p.OperatingArea,
                             GrossArea = p.GrossArea,
                             Population = p.Population,
                             BusinessType = p.BusinessType,
                             MonthlyHumanTraffic = p.MonthlyHumanTraffic,
                             PublicTransport = p.PublicTransport,
                             Introduce = p.Introduce,
                             Longitude = p.Longitude,
                             Latitude = p.Latitude,
                             Status = p.Status,
                             InDate = p.InDate,
                             InUser = p.InUser,
                             Position = p.Position,
                             Tel = p.Tel,
                             MerchantLogoList = (from pic in Context.NHHCG_Pic
                                                 where pic.RefID == p.ProjectID && pic.Type == 100
                                                 orderby pic.Seq ascending
                                                 select new NHHCGPicModel()
                                                 {
                                                     Path = pic.Path,
                                                     PicID = pic.PicID,
                                                     RefID = pic.RefID,
                                                     Type = pic.Type,
                                                     Seq = pic.Seq
                                                 }).ToList(),
                             ProjectPicList = (from pic in Context.NHHCG_Pic
                                               where pic.RefID == p.ProjectID && pic.Type == 101
                                               orderby pic.Seq ascending
                                               select new NHHCGPicModel()
                                               {
                                                   Path = pic.Path,
                                                   PicID = pic.PicID,
                                                   RefID = pic.RefID,
                                                   Type = pic.Type,
                                                   Seq = pic.Seq
                                               }).ToList()
                         });
            model = query.FirstOrDefault();

            return model;
        }
        /// <summary>
        /// 添加官网项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNHHCGProject(NHHCGProjectModel model)
        {
            var instance = CreateBizObject<NHHCG_Project>();
            var entity = new NHHCG_Project()
            {
                ProjectID = model.ProjectID,
                ProjectName = model.ProjectName,
                BriefName = model.BriefName,
                Feature = model.Feature,
                OpeningDate = model.OpeningDate,
                OperatingArea = model.OperatingArea,
                GrossArea = model.GrossArea,
                Population = model.Population,
                BusinessType = model.BusinessType,
                MonthlyHumanTraffic = model.MonthlyHumanTraffic,
                PublicTransport = model.PublicTransport,
                Introduce = model.Introduce,
                Longitude = model.Longitude ?? 0,
                Latitude = model.Latitude ?? 0,
                InDate = DateTime.Now,
                InUser = NHHWebContext.Current.UserID,
                Status = 1,
                Position = model.Position,
                Tel = model.Tel
            };
            instance.Insert(entity);
            this.SaveChanges();
            model.ProjectID = entity.ProjectID;
            return entity.ProjectID > 0;
        }
        /// <summary>
        /// 编辑官网项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateNHHCGProject(NHHCGProjectModel model)
        {
            var instance = CreateBizObject<NHHCG_Project>();
            var entity = instance.GetBySysNo(model.ProjectID);
            if (entity != null)
            {
                entity.ProjectName = model.ProjectName;
                entity.BriefName = model.BriefName;
                entity.Feature = model.Feature;
                entity.OpeningDate = model.OpeningDate;
                entity.OperatingArea = model.OperatingArea;
                entity.GrossArea = model.GrossArea;
                entity.Population = model.Population;
                entity.BusinessType = model.BusinessType;
                entity.MonthlyHumanTraffic = model.MonthlyHumanTraffic;
                entity.PublicTransport = model.PublicTransport;
                entity.Introduce = model.Introduce;
                entity.Longitude = model.Longitude ?? 0;
                entity.Latitude = model.Latitude ?? 0;
                entity.Position = model.Position;
                entity.Tel = model.Tel;

                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取官网项目列表
        /// </summary>
        /// <returns></returns>
        public List<NHHCGProjectModel> GetProjectList()
        {
            var list = new List<NHHCGProjectModel>();
            var instance = CreateBizObject<NHHCG_Project>().GetAllList();
            if (instance.Any())
            {
                instance.ForEach(entity =>
                {
                    list.Add(new NHHCGProjectModel()
                    {
                        ProjectID = entity.ProjectID,
                        ProjectName = entity.ProjectName
                    });
                });
            }

            return list;
        }

        #endregion NHHCGUnit

        #region NHHCGUnit
        /// <summary>
        /// 获取铺位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public NHHCGUnitListModel GetNHHCGUnitList(NHHCGUnitQueryModel queryInfo)
        {
            var model = new NHHCGUnitListModel();
            model.QueryInfo = queryInfo;
            model.UnitList = new List<NHHCGUnitModel>();

            var query = (from u in Context.NHHCG_Unit
                         join p in Context.NHHCG_Project on u.ProjectID equals p.ProjectID
                         select new
                         {
                             u.UnitID,
                             u.ProjectID,
                             u.Building,
                             u.FloorRemark,
                             u.UnitNumber,
                             u.Area,
                             u.RentRemark,
                             u.PropertyCosts,
                             u.Position,
                             u.Contacts,
                             u.Status,
                             u.InDate,
                             u.InUser,
                             p.ProjectName
                         });

            //if (queryInfo.BizType.HasValue)
            //    query = query.Where(item => item.BizType == queryInfo.BizType.Value);
            if (queryInfo.ProjectID.HasValue)
                query = query.Where(item => item.ProjectID == queryInfo.ProjectID.Value);
            if (queryInfo.Status.HasValue)
                query = query.Where(item => item.Status == queryInfo.Status.Value);
            if (queryInfo.AreaScope.HasValue)
            {
                switch (queryInfo.AreaScope)
                {
                    case 1:
                        query = query.Where(item => item.Area < 20);
                        break;
                    case 2:
                        query = query.Where(item => item.Area >= 20 && item.Area < 50);
                        break;
                    case 3:
                        query = query.Where(item => item.Area >= 50 && item.Area < 100);
                        break;
                    case 4:
                        query = query.Where(item => item.Area >= 100 && item.Area < 200);
                        break;
                    case 5:
                        query = query.Where(item => item.Area >= 200 && item.Area < 400);
                        break;
                    case 6:
                        query = query.Where(item => item.Area >= 400 && item.Area < 800);
                        break;
                    case 7:
                        query = query.Where(item => item.Area >= 800);
                        break;
                }
            }
            if (!string.IsNullOrEmpty(queryInfo.Building))
                query = query.Where(item => item.Building == queryInfo.Building);
            if (!string.IsNullOrEmpty(queryInfo.UnitNumber))
                query = query.Where(item => item.UnitNumber.Contains(queryInfo.UnitNumber));

            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page ?? 1;
            model.PagingInfo.TotalCount = query.Count();

            query = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);

            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity => model.UnitList.Add(new NHHCGUnitModel()
                {
                    UnitID = entity.UnitID,
                    ProjectID = entity.ProjectID,
                    ProjectName = entity.ProjectName,
                    Building = entity.Building,
                    FloorRemark = entity.FloorRemark,
                    UnitNumber = entity.UnitNumber,
                    Area = entity.Area,
                    RentRemark = entity.RentRemark,
                    PropertyCosts = entity.PropertyCosts,
                    Position = entity.Position,
                    Contacts = entity.Contacts,
                    Status = entity.Status,
                    InUser = entity.InUser,
                    InDate = entity.InDate
                }));
            }
            return model;
        }

        /// <summary>
        /// 获取铺位
        /// </summary>
        /// <param name="kioskId"></param>
        /// <returns></returns>
        public NHHCGUnitModel GetNHHCGUnit(int kioskId)
        {
            NHHCGUnitModel model = new NHHCGUnitModel();
            var query = (from u in Context.NHHCG_Unit
                         join p in Context.NHHCG_Project on u.ProjectID equals p.ProjectID
                         where u.UnitID == kioskId
                         select new
                         {
                             u.UnitID,
                             u.ProjectID,
                             u.Building,
                             u.FloorRemark,
                             u.UnitNumber,
                             u.Area,
                             u.RentRemark,
                             u.PropertyCosts,
                             u.Position,
                             u.Contacts,
                             u.Status,
                             u.InDate,
                             u.InUser,
                             p.ProjectName,
                             u.BizTypes,
                             PicList = (from pic in Context.NHHCG_Pic
                                        where pic.RefID == u.UnitID && pic.Type == 1
                                        orderby pic.Seq ascending
                                        select new NHHCGPicModel()
                                        {
                                            PicID = pic.PicID,
                                            RefID = pic.RefID,
                                            Type = pic.Type,
                                            Path = pic.Path,
                                            Seq = pic.Seq
                                        })
                         });
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.UnitNumber = entity.UnitNumber;
                model.UnitID = entity.UnitID;
                model.Area = entity.Area;
                model.Contacts = entity.Contacts;
                model.RentRemark = entity.RentRemark;
                model.FloorRemark = entity.FloorRemark;
                model.InDate = entity.InDate;
                model.InUser = entity.InUser;
                model.Position = entity.Position;
                model.ProjectID = entity.ProjectID;
                model.ProjectName = entity.ProjectName;
                model.Building = entity.Building;
                model.PropertyCosts = entity.PropertyCosts;
                model.Status = entity.Status;
                model.BizTypes = entity.BizTypes;
                model.BizTypeList = new List<int>();
                if (!string.IsNullOrEmpty(entity.BizTypes))
                {
                    string[] types = entity.BizTypes.Split(',');
                    model.BizTypeList = Array.ConvertAll(types, Convert.ToInt32).ToList();
                    model.BizTypeNames = string.Join(",",
                        (from b in Context.BizType where model.BizTypeList.Contains(b.BizTypeID) select b.BizTypeName)
                            .ToList());
                }
                if (entity.PicList != null)
                {
                    model.PicList = entity.PicList.ToList();
                }
            }
            return model;
        }
        /// <summary>
        /// 添加官网铺位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNHHCGUnit(NHHCGUnitModel model)
        {
            var instance = CreateBizObject<NHHCG_Unit>();
            var entity = new NHHCG_Unit()
            {
                UnitNumber = model.UnitNumber,
                Area = model.Area,
                BizTypes = model.BizTypes,
                Contacts = model.Contacts,
                RentRemark = model.RentRemark,
                FloorRemark = model.FloorRemark,
                Position = model.Position,
                ProjectID = model.ProjectID,
                Building = model.Building,
                PropertyCosts = model.PropertyCosts,
                InDate = DateTime.Now,
                InUser = NHHWebContext.Current.UserID,
                Status = 1
            };
            instance.Insert(entity);
            this.SaveChanges();
            model.UnitID = entity.UnitID;
            return entity.UnitID > 0;
        }
        /// <summary>
        /// 编辑官网铺位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateNHHCGUnit(NHHCGUnitModel model)
        {
            var instance = CreateBizObject<NHHCG_Unit>();
            var entity = instance.GetBySysNo(model.UnitID);
            if (entity != null)
            {
                entity.Area = model.Area;
                entity.BizTypes = model.BizTypes;
                entity.Contacts = model.Contacts;
                entity.RentRemark = model.RentRemark;
                entity.FloorRemark = model.FloorRemark;
                entity.InDate = DateTime.Now;
                entity.InUser = NHHWebContext.Current.UserID;
                entity.Position = model.Position;
                entity.ProjectID = model.ProjectID;
                entity.Building = model.Building;
                entity.PropertyCosts = model.PropertyCosts;
                entity.Status = model.Status;
                entity.UnitNumber = model.UnitNumber;

                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion NHHCGUnit

        #region NHHCGKiosk
        /// <summary>
        /// 获取多经点位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public NHHCGKioskListModel GetNHHCGKioskList(NHHCGKioskQueryModel queryInfo)
        {
            var model = new NHHCGKioskListModel();
            model.QueryInfo = queryInfo;
            model.KioskList = new List<NHHCGKioskModel>();

            var query = (from u in Context.NHHCG_Kiosk
                         join p in Context.NHHCG_Project on u.ProjectID equals p.ProjectID
                         join d in Context.Dictionary on u.Region equals d.FieldValue
                         join area in Context.Dictionary on u.AreaScope equals area.FieldValue
                         where d.FieldType == "NHHCG_Region" && area.FieldType == "KioskAreaScope"
                         select new
                         {
                             u.KioskID,
                             u.ProjectID,
                             u.Building,
                             u.FloorID,
                             u.Region,
                             RegionName = d.FieldName,
                             u.KioskNumber,
                             u.AreaScope,
                             AreaScopeStr = area.FieldName,
                             u.RentRemark,
                             u.Position,
                             u.Contacts,
                             u.Status,
                             u.InDate,
                             u.InUser,
                             p.ProjectName,
                             u.BusinessScopes
                         });

            if (queryInfo.FloorID.HasValue)
                query = query.Where(item => item.FloorID == queryInfo.FloorID.Value);
            if (queryInfo.ProjectID.HasValue)
                query = query.Where(item => item.ProjectID == queryInfo.ProjectID.Value);
            if (queryInfo.Status.HasValue)
                query = query.Where(item => item.Status == queryInfo.Status.Value);
            if (queryInfo.AreaScope.HasValue)
                query = query.Where(item => item.AreaScope == queryInfo.AreaScope.Value);
            if (queryInfo.Region.HasValue)
                query = query.Where(item => item.Region == queryInfo.Region.Value);
            if (!string.IsNullOrEmpty(queryInfo.Building))
                query = query.Where(item => item.Building == queryInfo.Building);
            if (!string.IsNullOrEmpty(queryInfo.KioskNumber))
                query = query.Where(item => item.KioskNumber.Contains(queryInfo.KioskNumber));

            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page ?? 1;
            model.PagingInfo.TotalCount = query.Count();

            query = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);

            var entityList = query.ToList();
            if (entityList != null)
            {
                foreach (var entity in entityList)
                {
                    var contract = new NHHCGKioskModel();
                    contract.KioskID = entity.KioskID;
                    contract.ProjectID = entity.ProjectID;
                    contract.ProjectName = entity.ProjectName;
                    contract.Building = entity.Building;
                    contract.FloorID = entity.FloorID;
                    contract.Region = entity.Region;
                    contract.RegionName = entity.RegionName;
                    contract.KioskNumber = entity.KioskNumber;
                    contract.AreaScope = entity.AreaScope;
                    contract.AreaScopeStr = entity.AreaScopeStr;
                    contract.RentRemark = entity.RentRemark;
                    contract.Position = entity.Position;
                    contract.Contacts = entity.Contacts;
                    contract.Status = entity.Status;
                    contract.InUser = entity.InUser;
                    contract.InDate = entity.InDate;
                    contract.BusinessScopes = entity.BusinessScopes;
                    contract.BusinessScopeList=new List<int>();
                    if (!string.IsNullOrEmpty(entity.BusinessScopes))
                    {
                        string[] scopes = entity.BusinessScopes.Split(',');
                        contract.BusinessScopeList = Array.ConvertAll<string, int>(scopes, i => Convert.ToInt32(i)).ToList();
                        contract.BusinessScopeNames = string.Join(",",
                            (from b in Context.Dictionary where b.FieldType == "BusinessScope" && contract.BusinessScopeList.Contains(b.FieldValue) select b.FieldName)
                                .ToList<string>());
                    }
                    model.KioskList.Add(contract);
                }
            }
            return model;
        }

        /// <summary>
        /// 获取多经点位
        /// </summary>
        /// <param name="kioskId"></param>
        /// <returns></returns>
        public NHHCGKioskModel GetNHHCGKiosk(int kioskId)
        {
            NHHCGKioskModel model = new NHHCGKioskModel();
            var query = (from u in Context.NHHCG_Kiosk
                         join p in Context.NHHCG_Project on u.ProjectID equals p.ProjectID
                         join d in Context.Dictionary on u.Region equals d.FieldValue
                         join area in Context.Dictionary on u.AreaScope equals area.FieldValue
                         where u.KioskID == kioskId && d.FieldType == "NHHCG_Region"&& area.FieldType == "KioskAreaScope"
                         select new
                         {
                             u.KioskID,
                             u.ProjectID,
                             u.Building,
                             u.FloorID,
                             u.Region,
                             RegionName = d.FieldName,
                             u.KioskNumber,
                             u.AreaScope,
                             AreaScopeStr = area.FieldName,
                             u.RentRemark,
                             u.Position,
                             u.Contacts,
                             u.Status,
                             u.InDate,
                             u.InUser,
                             p.ProjectName,
                             u.BusinessScopes,
                             PicList = (from pic in Context.NHHCG_Pic
                                        where pic.RefID == u.KioskID && pic.Type == 3
                                        orderby pic.Seq ascending
                                        select new NHHCGPicModel()
                                        {
                                            PicID = pic.PicID,
                                            RefID = pic.RefID,
                                            Type = pic.Type,
                                            Path = pic.Path,
                                            Seq = pic.Seq
                                        })
                         });
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.KioskNumber = entity.KioskNumber;
                model.KioskID = entity.KioskID;
                model.AreaScope = entity.AreaScope;
                model.AreaScopeStr = entity.AreaScopeStr;
                model.Contacts = entity.Contacts;
                model.RentRemark = entity.RentRemark;
                model.FloorID = entity.FloorID;
                model.InDate = entity.InDate;
                model.InUser = entity.InUser;
                model.Position = entity.Position;
                model.ProjectID = entity.ProjectID;
                model.Building = entity.Building;
                model.Region = entity.Region;
                model.RegionName = entity.RegionName;
                model.ProjectName = entity.ProjectName;
                model.Status = entity.Status;
                model.BusinessScopes = entity.BusinessScopes;
                model.BusinessScopeList = new List<int>();
                if (!string.IsNullOrEmpty(entity.BusinessScopes))
                {
                    string[] scopes = entity.BusinessScopes.Split(',');
                    model.BusinessScopeList = Array.ConvertAll<string, int>(scopes, i => Convert.ToInt32(i)).ToList();
                    model.BusinessScopeNames = string.Join(",",
                        (from b in Context.Dictionary where b.FieldType == "BusinessScope" && model.BusinessScopeList.Contains(b.FieldValue) select b.FieldName)
                            .ToList<string>());
                }
                if (entity.PicList != null)
                {
                    model.PicList = entity.PicList.ToList();
                }
            }
            return model;
        }
        /// <summary>
        /// 添加官网多经点位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNHHCGKiosk(NHHCGKioskModel model)
        {
            var instance = CreateBizObject<NHHCG_Kiosk>();
            var entity = new NHHCG_Kiosk()
            {
                KioskNumber = model.KioskNumber,
                AreaScope = model.AreaScope,
                Contacts = model.Contacts,
                RentRemark = model.RentRemark,
                FloorID = model.FloorID,
                Position = model.Position,
                ProjectID = model.ProjectID,
                Building = model.Building,
                Region = model.Region,
                InDate = DateTime.Now,
                InUser = NHHWebContext.Current.UserID,
                Status = 1,
                BusinessScopes = model.BusinessScopes
            };
            instance.Insert(entity);
            this.SaveChanges();
            model.KioskID = entity.KioskID;
            return entity.KioskID > 0;
        }
        /// <summary>
        /// 编辑官网多经点位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateNHHCGKiosk(NHHCGKioskModel model)
        {
            var instance = CreateBizObject<NHHCG_Kiosk>();
            var entity = instance.GetBySysNo(model.KioskID);
            if (entity != null)
            {
                entity.AreaScope = model.AreaScope;
                entity.Contacts = model.Contacts;
                entity.RentRemark = model.RentRemark;
                entity.FloorID = model.FloorID;
                entity.InDate = DateTime.Now;
                entity.InUser = NHHWebContext.Current.UserID;
                entity.Position = model.Position;
                entity.ProjectID = model.ProjectID;
                entity.Building = model.Building;
                entity.Region = model.Region;
                entity.Status = model.Status;
                entity.KioskNumber = model.KioskNumber;
                entity.BusinessScopes = model.BusinessScopes;

                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion NHHCGKiosk

        #region NHHCGADPosition
        /// <summary>
        /// 获取广告位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public NHHCGADPositionListModel GetNHHCGADPositionList(NHHCGADPositionQueryModel queryInfo)
        {
            var model = new NHHCGADPositionListModel();
            model.QueryInfo = queryInfo;
            model.ADPositionList = new List<NHHCGADPositionModel>();

            var query = (from u in Context.NHHCG_ADPosition
                         join p in Context.NHHCG_Project on u.ProjectID equals p.ProjectID
                         join d in Context.Dictionary on u.ADType equals d.FieldValue
                         join r in Context.Dictionary on u.Region equals r.FieldValue
                         join e in Context.Dictionary on u.ElectricityType equals e.FieldValue
                         where d.FieldType == "AdPointMedia" && r.FieldType == "NHHCG_Region" && e.FieldType == "ElectricityType"
                         select new
                         {
                             u.ADPositionID,
                             u.ProjectID,
                             u.Building,
                             u.FloorRemark,
                             u.Region,
                             RegionName = r.FieldName,
                             u.ADPositionNumber,
                             u.ADType,
                             ADTypeName = d.FieldName,
                             u.RentRemark,
                             u.Position,
                             u.Contacts,
                             u.Status,
                             u.InDate,
                             u.InUser,
                             p.ProjectName,
                             u.Size,
                             u.Feature,
                             u.ElectricityType,
                             ElectricityTypeName = e.FieldName
                         });

            if (queryInfo.ProjectID.HasValue)
                query = query.Where(item => item.ProjectID == queryInfo.ProjectID.Value);
            if (queryInfo.Status.HasValue)
                query = query.Where(item => item.Status == queryInfo.Status.Value);
            if (queryInfo.ADType.HasValue)
                query = query.Where(item => item.ADType == queryInfo.ADType.Value);
            if (queryInfo.Region.HasValue)
                query = query.Where(item => item.Region == queryInfo.Region.Value);
            if (queryInfo.ElectricityType.HasValue)
                query = query.Where(item => item.ElectricityType == queryInfo.ElectricityType.Value);
            if (!string.IsNullOrEmpty(queryInfo.Building))
                query = query.Where(item => item.Building == queryInfo.Building);
            if (!string.IsNullOrEmpty(queryInfo.ADPositionNumber))
                query = query.Where(item => item.ADPositionNumber.Contains(queryInfo.ADPositionNumber));

            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page ?? 1;
            model.PagingInfo.TotalCount = query.Count();

            query = query.OrderByDescending(item => item.ADPositionID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);

            var entityList = query.ToList();
            if (entityList != null)
            {
                entityList.ForEach(entity => model.ADPositionList.Add(new NHHCGADPositionModel()
                {
                    ADPositionID = entity.ADPositionID,
                    ProjectID = entity.ProjectID,
                    Building = entity.Building,
                    Region = entity.Region,
                    RegionName = entity.RegionName,
                    ProjectName = entity.ProjectName,
                    FloorRemark = entity.FloorRemark,
                    ADPositionNumber = entity.ADPositionNumber,
                    ADType = entity.ADType,
                    ADTypeName = entity.ADTypeName,
                    RentRemark = entity.RentRemark,
                    Position = entity.Position,
                    Contacts = entity.Contacts,
                    Status = entity.Status,
                    InUser = entity.InUser,
                    InDate = entity.InDate,
                    Size = entity.Size,
                    Feature = entity.Feature,
                    ElectricityType = entity.ElectricityType,
                    ElectricityTypeName = entity.ElectricityTypeName
                }));
            }
            return model;
        }

        /// <summary>
        /// 获取广告位
        /// </summary>
        /// <param name="adPositionId"></param>
        /// <returns></returns>
        public NHHCGADPositionModel GetNHHCGADPosition(int adPositionId)
        {
            NHHCGADPositionModel model = new NHHCGADPositionModel();
            var query = (from u in Context.NHHCG_ADPosition
                         join p in Context.NHHCG_Project on u.ProjectID equals p.ProjectID
                         join d in Context.Dictionary on u.ADType equals d.FieldValue
                         join r in Context.Dictionary on u.Region equals r.FieldValue
                         join e in Context.Dictionary on u.ElectricityType equals e.FieldValue
                         where d.FieldType == "AdPointMedia" && r.FieldType == "NHHCG_Region" && e.FieldType == "ElectricityType" && u.ADPositionID == adPositionId
                         select new
                         {
                             u.ADPositionID,
                             u.ProjectID,
                             u.Building,
                             u.FloorRemark,
                             u.Region,
                             RegionName = r.FieldName,
                             u.ADPositionNumber,
                             u.ADType,
                             ADTypeName = d.FieldName,
                             u.RentRemark,
                             u.Position,
                             u.Contacts,
                             u.Status,
                             u.InDate,
                             u.InUser,
                             p.ProjectName,
                             u.Size,
                             u.Feature,
                             u.ElectricityType,
                             ElectricityTypeName = e.FieldName,
                             PicList = (from pic in Context.NHHCG_Pic
                                        where pic.RefID == u.ADPositionID && pic.Type == 2
                                        orderby pic.Seq ascending
                                        select new NHHCGPicModel()
                                        {
                                            PicID = pic.PicID,
                                            RefID = pic.RefID,
                                            Type = pic.Type,
                                            Path = pic.Path,
                                            Seq = pic.Seq
                                        })
                         });
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model.ADPositionNumber = entity.ADPositionNumber;
                model.ADPositionID = entity.ADPositionID;
                model.ADType = entity.ADType;
                model.ADTypeName = entity.ADTypeName;
                model.Contacts = entity.Contacts;
                model.RentRemark = entity.RentRemark;
                model.FloorRemark = entity.FloorRemark;
                model.InDate = entity.InDate;
                model.InUser = entity.InUser;
                model.Position = entity.Position;
                model.ProjectID = entity.ProjectID;
                model.Building = entity.Building;
                model.Region = entity.Region;
                model.RegionName = entity.RegionName;
                model.ProjectName = entity.ProjectName;
                model.Status = entity.Status;
                model.Size = entity.Size;
                model.Feature = entity.Feature;
                model.ElectricityType = entity.ElectricityType;
                model.ElectricityTypeName = entity.ElectricityTypeName;
                if (entity.PicList != null)
                {
                    model.PicList = entity.PicList.ToList();
                }
            }
            return model;
        }
        /// <summary>
        /// 添加官网广告位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNHHCGADPosition(NHHCGADPositionModel model)
        {
            var instance = CreateBizObject<NHHCG_ADPosition>();
            var entity = new NHHCG_ADPosition()
            {
                ADPositionNumber = model.ADPositionNumber,
                ADType = model.ADType,
                Contacts = model.Contacts,
                RentRemark = model.RentRemark,
                FloorRemark = model.FloorRemark,
                Position = model.Position,
                ProjectID = model.ProjectID,
                Building = model.Building,
                Region = model.Region,
                Feature = model.Feature,
                ElectricityType = model.ElectricityType,
                InDate = DateTime.Now,
                InUser = NHHWebContext.Current.UserID,
                Status = 1,
                Size = model.Size
            };
            instance.Insert(entity);
            this.SaveChanges();
            model.ADPositionID = entity.ADPositionID;
            return entity.ADPositionID > 0;
        }
        /// <summary>
        /// 编辑官网广告位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateNHHCGADPosition(NHHCGADPositionModel model)
        {
            var instance = CreateBizObject<NHHCG_ADPosition>();
            var entity = instance.GetBySysNo(model.ADPositionID);
            if (entity != null)
            {
                entity.ADType = model.ADType;
                entity.Contacts = model.Contacts;
                entity.RentRemark = model.RentRemark;
                entity.FloorRemark = model.FloorRemark;
                entity.InDate = DateTime.Now;
                entity.InUser = NHHWebContext.Current.UserID;
                entity.Position = model.Position;
                entity.ProjectID = model.ProjectID;
                entity.Building = model.Building;
                entity.Region = model.Region;
                entity.Feature = model.Feature;
                entity.ElectricityType = model.ElectricityType;
                entity.Status = model.Status;
                entity.ADPositionNumber = model.ADPositionNumber;
                entity.Size = model.Size;

                instance.Update(entity);
                this.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion NHHCGADPosition

        #region NHHCGPic
        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNHHCGPic(NHHCGPicModel model)
        {
            var instance = CreateBizObject<NHHCG_Pic>();
            int seq = (from p in Context.NHHCG_Pic
                       where p.RefID == model.RefID && p.Type == model.Type
                       orderby p.Seq descending
                       select p.Seq).FirstOrDefault() + 1;

            var entity = new NHHCG_Pic()
            {
                Type = model.Type,
                RefID = model.RefID,
                Path = model.Path,
                Seq = seq
            };
            instance.Insert(entity);
            this.SaveChanges();
            PicResetSeq(model.RefID, model.Type);
            return entity.PicID > 0;
        }
        /// <summary>
        /// 获取图片列表
        /// </summary>
        /// <param name="refID"></param>
        /// <param name="type">1店铺 2广告位 3多经点位</param>
        /// <returns></returns>
        public NHHCGPicListModel GetPicList(int refID, int type)
        {
            var model = new NHHCGPicListModel();
            model.PicList = new List<NHHCGPicModel>();
            model.PicList = (from p in Context.NHHCG_Pic
                             where p.RefID == refID && p.Type == type
                             orderby p.Seq ascending
                             select new NHHCGPicModel()
                             {
                                 Path = p.Path,
                                 PicID = p.PicID,
                                 RefID = p.RefID,
                                 Seq = p.Seq,
                                 Type = p.Type
                             }).ToList();
            return model;
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="picId"></param>
        /// <returns></returns>
        public bool DeletePic(int picId)
        {
            var instance = CreateBizObject<NHHCG_Pic>();
            var model = instance.TopOne(a => a.PicID == picId);
            int type = model.Type;
            int refId = model.RefID;
            instance.Delete(model);
            SaveChanges();
            //重新序列化图片顺序
            PicResetSeq(refId, type);
            return true;
        }
        #endregion NHHCGPic

        #region NHHCGFeedback

        /// <summary>
        /// 获取官网反馈列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public FeedbackListModel GetFeedbackListModel(FeedbackQueryModel queryInfo)
        {
            var model = new FeedbackListModel
            {
                QueryInfo = queryInfo,
                FeedbackList = new List<FeedbackModel>()
            };

            var query = from fb in Context.NHHCG_Feedback
                        join dc in Context.Dictionary.Where(a => a.FieldType == "FeedbackType") on fb.FeedbackType equals
                            dc.FieldValue
                        join dc2 in Context.Dictionary.Where(a => a.FieldType == "UserType") on fb.UserType equals
                            dc2.FieldValue
                        join dc3 in Context.Dictionary.Where(a => a.FieldType == "FeedbackStatus") on fb.Status equals
                            dc3.FieldValue
                        join user in Context.Sys_User on fb.Accepter equals user.UserID into queryResult
                        from user in queryResult.DefaultIfEmpty()
                        select new FeedbackModel()
                        {
                            FeedbackID = fb.FeedbackID,
                            FeedbackType = fb.FeedbackType,
                            FeedbackTypeName = dc.FieldName,
                            UserType = fb.UserType,
                            UserTypeName = dc2.FieldName,
                            CustomerName = fb.CustomerName,
                            Phone = fb.Phone,
                            Remark = fb.Remark,
                            Status = fb.Status,
                            FeedbackStatusName = dc3.FieldName,
                            Accepter = fb.Accepter.Value,
                            AccepterName = user.UserName,
                            AcceptResult = fb.AcceptResult,
                            AcceptTime = fb.AcceptTime.Value
                        };

            #region 查询条件

            if (queryInfo.FeedbackType.HasValue)
            {
                query = query.Where(a => a.FeedbackType == queryInfo.FeedbackType.Value);
            }
            if (queryInfo.UserType.HasValue)
            {
                query = query.Where(a => a.UserType == queryInfo.UserType.Value);
            }
            if (queryInfo.Status.HasValue)
            {
                query = query.Where(a => a.Status == queryInfo.Status.Value);
            }

            #endregion

            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page ?? 1,
                TotalCount = query.Count()
            };

            query =
                query.OrderByDescending(item => item.FeedbackID)
                    .Skip(model.PagingInfo.SkipNum)
                    .Take(model.PagingInfo.TakeNum);
            var entityList = query.ToList();
            if (entityList.Any())
            {
                model.FeedbackList = entityList;
            }
            return model;
        }

        /// <summary>
        /// 获取官网反馈明细
        /// </summary>
        /// <param name="feedbackID"></param>
        /// <returns></returns>
        public FeedbackModel GetFeedbackModel(int feedbackID)
        {
            var model = from fb in Context.NHHCG_Feedback
                        join dc in Context.Dictionary.Where(a => a.FieldType == "FeedbackType") on fb.FeedbackType equals
                            dc.FieldValue
                        join dc2 in Context.Dictionary.Where(a => a.FieldType == "UserType") on fb.UserType equals
                            dc2.FieldValue
                        join dc3 in Context.Dictionary.Where(a => a.FieldType == "FeedbackStatus") on fb.Status equals
                            dc3.FieldValue
                        join user in Context.Sys_User on fb.Accepter equals user.UserID into queryResult
                        from user in queryResult.DefaultIfEmpty()
                        where fb.FeedbackID == feedbackID
                        select new FeedbackModel()
                        {
                            FeedbackID = fb.FeedbackID,
                            FeedbackType = fb.FeedbackType,
                            FeedbackTypeName = dc.FieldName,
                            UserType = fb.UserType,
                            UserTypeName = dc2.FieldName,
                            CustomerName = fb.CustomerName,
                            Phone = fb.Phone,
                            Remark = fb.Remark,
                            Status = fb.Status,
                            FeedbackStatusName = dc3.FieldName,
                            Accepter = fb.Accepter.Value,
                            AccepterName = user.UserName,
                            AcceptResult = fb.AcceptResult,
                            AcceptTime = fb.AcceptTime.Value
                        };

            return model.FirstOrDefault();
        }

        /// <summary>
        /// 处理反馈意见
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ProcessFeedback(FeedbackModel model)
        {
            var instance = CreateBizObject<NHHCG_Feedback>();
            var entity = instance.TopOne(a => a.FeedbackID == model.FeedbackID);
            entity.Accepter = model.Accepter;
            entity.AcceptTime = DateTime.Now;
            entity.AcceptResult = model.AcceptResult;
            entity.Status = 2;
            instance.Update(entity);
            SaveChanges();
            return true;
        }

        #endregion

        #region Private
        /// <summary>
        /// 重新序列化图片顺序
        /// </summary>
        /// <param name="refId"></param>
        /// <param name="type"></param>
        public void PicResetSeq(int refId, int type)
        {
            var instance = CreateBizObject<NHHCG_Pic>();
            var all = instance.GetAllList(a => a.RefID == refId && a.Type == type);
            if (all.Any())
            {
                int seq = 1;
                var list = all.ToList();
                foreach (var item in list.OrderBy(a => a.Seq))
                {
                    item.Seq = seq;
                    instance.Update(item);
                    seq++;
                }
            }
            this.SaveChanges();
        }
        #endregion Private
    }
}
