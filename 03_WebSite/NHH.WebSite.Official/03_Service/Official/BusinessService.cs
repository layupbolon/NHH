using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using NHH.Entities;
using NHH.Framework.Caching;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Service.Official.IService;
using NHH.Models.Official;
using NHH.Models.Official.Eunm;

namespace NHH.Service.Official
{
    public class BusinessService : NHHService<NHHEntities>, IBusinessService
    {
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public string GetProjectList(int? projectID)
        {
            var query = Context.NHHCG_Project.Where(a => a.Status == 1).Select(a => new { a.ProjectName, a.ProjectID });

            if (!query.Any()) return string.Empty;

            var list = query.ToList();
            var sb = new StringBuilder();
            sb.AppendLine("<option selected=\"selected\">选择唐人项目</option>");
            list.ForEach(info =>
            {
                sb.AppendFormat("<option {2} value=\"{0}\">{1}</option>",
                    string.Format("Project.aspx?projectID={0}", info.ProjectID), info.ProjectName,
                    projectID.HasValue && projectID.Value == info.ProjectID ? "selected=\"selected\"" : string.Empty);
            });

            return sb.ToString();
        }

        /// <summary>
        /// 获取顶部banner
        /// </summary>
        /// <returns></returns>
        public string GetTopBanner()
        {
            var query = from b in Context.Banner
                        join bi in Context.BannerInfo on b.BannerID equals bi.BannerID
                        where
                            b.BannerTarget == 2 &&
                            b.LocationID == (int)LocationEnum.官网商机提供页面顶部Banner && b.Status == 1
                        select new Models.Official.BannerInfo()
                        {
                            Title = bi.Title,
                            Content = bi.Content,
                            BackgroundImageUrl = bi.ResourcePath,
                            Link = bi.Link,
                            Seq = bi.Seq
                        };

            if (query.Any())
            {
                var list = query.OrderBy(a => a.Seq).ToList();
                var sb = new StringBuilder();
                list.ForEach(info =>
                {
                    sb.AppendLine("<li class=\"swiper-slide\">");
                    sb.AppendLine("<a href=\"###\">");
                    sb.AppendLine("<div class=\"txt-banner\">");
                    sb.AppendFormat("<h2>{0}</h2>", info.Title);
                    sb.AppendFormat("<p class=\"mt30\">{0}</p>", info.Content);
                    sb.AppendFormat("</div><img src=\"{0}\" alt=\"\"/></a></li>",
                        Framework.Utility.UrlHelper.GetImageUrl(info.BackgroundImageUrl));
                });

                return sb.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取banner数量
        /// </summary>
        /// <returns></returns>
        public int GetBannerCount()
        {
            var query = from b in Context.Banner
                        join bi in Context.BannerInfo on b.BannerID equals bi.BannerID
                        where
                            b.BannerTarget == 2 &&
                            b.LocationID == (int)LocationEnum.官网商机提供页面顶部Banner && b.Status == 1
                        select new Models.Official.BannerInfo()
                        {
                            Title = bi.Title,
                            Content = bi.Content,
                            BackgroundImageUrl = bi.ResourcePath,
                            Link = bi.Link,
                            Seq = bi.Seq
                        };

            return query.Count();
        }

        /// <summary>
        /// 获取官网的项目下拉列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItemInfo> GetNHHCGProjectDropDownList()
        {
            List<SelectListItemInfo> result =
                CacheManager.Default.Get<List<SelectListItemInfo>>("NHHCGProjectDropDownList");
            if (result != null)
                return result;

            result = (from p in Context.NHHCG_Project
                      where p.Status == 1
                      select new SelectListItemInfo()
                      {
                          Text = p.ProjectName,
                          Value = p.ProjectID.ToString()
                      }).ToList();
            CacheManager.Default.Add("NHHCGProjectDropDownList", result);
            return result;
        }

        /// <summary>
        /// 获取官网的业态类型下拉列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItemInfo> GetNHHCGBizTypeDropDownList()
        {
            List<SelectListItemInfo> result =
                CacheManager.Default.Get<List<SelectListItemInfo>>("NHHCGBizTypeDropDownList");
            if (result != null)
                return result;

            result = (from p in Context.BizType
                      where p.Status == 1
                      select new SelectListItemInfo()
                      {
                          Text = p.BizTypeName,
                          Value = p.BizTypeID.ToString()
                      }).ToList();
            CacheManager.Default.Add("NHHCGBizTypeDropDownList", result);

            return result;
        }

        /// <summary>
        /// 获取官网面积范围下拉列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItemInfo> GetNHHCGAreaDropDownList()
        {
            List<SelectListItemInfo> result = CacheManager.Default.Get<List<SelectListItemInfo>>("NHHCGAreaDropDownList");
            if (result != null)
                return result;

            result = (from d in Context.Dictionary
                      where d.FieldType == "UnitAreaScope"
                      select new SelectListItemInfo()
                      {
                          Text = d.FieldName,
                          Value = d.FieldValue.ToString()
                      }).ToList();
            CacheManager.Default.Add("NHHCGAreaDropDownList", result);

            return result;
        }

        /// <summary>
        /// 获取官网楼层下拉列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<SelectListItemInfo> GetNHHCGFloorDropDownList(NHHCGTypeEnum type)
        {
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
            var result = (from l in floorList
                          where l.FieldId == (int)type
                          select new SelectListItemInfo()
                          {
                              Text = l.FieldName,
                              Value = l.FieldValue.ToString()
                          }).ToList();
            return result;
        }

        /// <summary>
        /// 获取空铺信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<UnitInfo> GetUnitInfos(UnitQueryInfo info)
        {
            var query = from u in Context.NHHCG_Unit
                        where u.Status == 1
                        select new UnitInfo()
                        {
                            UnitID = u.UnitID,
                            Area = u.Area,
                            Pic =
                                Context.NHHCG_Pic.Where(a => a.RefID == u.UnitID && a.Type == (int)PicTypeEnum.商铺)
                                    .OrderBy(a => a.Seq)
                                    .Select(a => a.Path)
                                    .FirstOrDefault(),
                            UnitNumber = u.UnitNumber,
                            UnitPosition = u.Position,
                            projectID = u.ProjectID,
                            FloorRemark = u.FloorRemark,
                            bizTypes = string.IsNullOrEmpty(u.BizTypes)?"":u.BizTypes,
                            RentRemark = string.IsNullOrEmpty(u.RentRemark) ? "暂无" : u.RentRemark,
                            building = u.Building
                        };

            if (info.projectID.HasValue && info.projectID.Value > 0)
            {
                query = query.Where(a => a.projectID == info.projectID.Value);
            }
            if (!string.IsNullOrEmpty(info.building))
            {
                query = query.Where(a => a.building.Equals(info.building));
            }
            if (info.unitArea.HasValue && info.unitArea.Value > 0)
            {
                switch (info.unitArea)
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

            var list = new List<UnitInfo>();
            if (query.Any())
            {
                list = query.ToList();
                var newlist = new List<UnitInfo>();
                if (info.bizType.HasValue && info.bizType.Value > 0)
                {
                    newlist.AddRange(list.Where(unitInfo => unitInfo.bizTypes.Split(',').Contains(info.bizType.ToString())));
                    return newlist;
                }
            }

            return list;
        }

        /// <summary>
        /// 获取相关铺位信息
        /// （除去指定铺位）
        /// </summary>
        /// <param name="info"></param>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public List<UnitInfo> GetUnitInfos(UnitQueryInfo info, int unitID)
        {
            var list = GetUnitInfos(info);
            return list.Any() ? list.Where(a => a.UnitID != unitID).ToList() : new List<UnitInfo>();
        }

        /// <summary>
        /// 获取商铺信息
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public UnitInfo GetUnitInfo(int unitID)
        {
            var model = new UnitInfo();
            var query = from u in Context.NHHCG_Unit
                        join p in Context.NHHCG_Project on u.ProjectID equals p.ProjectID
                        where u.UnitID == unitID
                        select new UnitInfo()
                        {
                            UnitID = u.UnitID,
                            UnitNumber = u.UnitNumber,
                            UnitPosition = p.ProjectName + " - " + u.Building + " " + u.FloorRemark + " - " + u.UnitNumber,
                            Area = u.Area,
                            projectID = u.ProjectID,
                            projectName = p.ProjectName,
                            FloorRemark = u.FloorRemark,
                            bizTypes = string.IsNullOrEmpty(u.BizTypes)?"":u.BizTypes,
                            building = u.Building,
                            RentRemark = string.IsNullOrEmpty(u.RentRemark) ? "暂无" : u.RentRemark,
                            ProperyCosts = u.PropertyCosts,
                            Contacts = u.Contacts,
                            Position = u.Position,
                            PicList =
                                (from pic in Context.NHHCG_Pic
                                 where pic.RefID == unitID && pic.Type == (int)PicTypeEnum.商铺
                                 orderby pic.Seq
                                 select pic.Path).ToList()
                        };

            if (query.Any())
            {
                model = query.FirstOrDefault();
                if (!string.IsNullOrEmpty(model.bizTypes))
                {
                    var list = Array.ConvertAll(model.bizTypes.Split(','), Convert.ToInt32).ToList();
                    model.bizTypesName = string.IsNullOrEmpty(model.bizTypes)
                        ? string.Empty
                        : string.Join(",",
                            (Context.BizType.Where(b => list.Contains(b.BizTypeID)).Select(b => b.BizTypeName)).ToList());
                }
            }

            return model;
        }

        /// <summary>
        /// 获取官网区域范围下拉列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItemInfo> GetNHHCGRegionDropDownList()
        {
            List<SelectListItemInfo> result =
                CacheManager.Default.Get<List<SelectListItemInfo>>("NHHCGRegionDropDownList");
            if (result != null)
                return result;

            result = (from d in Context.Dictionary
                      where d.FieldType == "NHHCG_Region"
                      select new SelectListItemInfo()
                      {
                          Text = d.FieldName,
                          Value = d.FieldValue.ToString()
                      }).ToList();
            CacheManager.Default.Add("NHHCGRegionDropDownList", result);

            return result;
        }

        /// <summary>
        /// 获取官网经营范围下拉列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItemInfo> GetNHHCGBusinessScopeDropDownList()
        {
            List<SelectListItemInfo> result =
                CacheManager.Default.Get<List<SelectListItemInfo>>("NHHCGBusinessScopeDropDownList");
            if (result != null)
                return result;

            result = (from d in Context.Dictionary
                      where d.FieldType == "BusinessScope"
                      select new SelectListItemInfo()
                      {
                          Text = d.FieldName,
                          Value = d.FieldValue.ToString()
                      }).ToList();
            CacheManager.Default.Add("NHHCGBusinessScopeDropDownList", result);

            return result;
        }

        /// <summary>
        /// 获取空铺信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<KioskInfo> GetKioskInfos(KioskQueryInfo info)
        {
            var query = from k in Context.NHHCG_Kiosk
                        join dc in Context.Dictionary.Where(a => a.FieldType == "KioskAreaScope") on k.AreaScope equals dc.FieldValue
                        where k.Status == 1
                        select new KioskInfo()
                        {
                            KioskID = k.KioskID,
                            ProjectID = k.ProjectID,
                            Building = k.Building,
                            FloorID = k.FloorID,
                            Region = k.Region,
                            KioskNumber = k.KioskNumber,
                            AreaScope = k.AreaScope,
                            AreaScopeName = dc.FieldName,
                            RentRemark = string.IsNullOrEmpty(k.RentRemark) ? "暂无" : k.RentRemark,
                            Position = k.Position,
                            Contacts = k.Contacts,
                            BusinessScopes = string.IsNullOrEmpty(k.BusinessScopes) ?"":k.BusinessScopes,
                            Pic = Context.NHHCG_Pic.Where(a => a.RefID == k.KioskID && a.Type == (int)PicTypeEnum.多经点位)
                                .OrderBy(a => a.Seq)
                                .Select(a => a.Path)
                                .FirstOrDefault()
                        };

            if (info.ProjectID.HasValue && info.ProjectID.Value > 0)
            {
                query = query.Where(a => a.ProjectID == info.ProjectID.Value);
            }
            if (info.Floor.HasValue && info.Floor.Value > 0)
            {
                query = query.Where(a => a.FloorID == info.Floor.Value);
            }
            if (info.Region.HasValue && info.Region.Value > 0)
            {
                query = query.Where(a => a.Region == info.Region.Value);
            }
            if (info.AreaScope.HasValue && info.AreaScope.Value > 0)
            {
                query = query.Where(a => a.AreaScope == info.AreaScope.Value);
            }

            var list = new List<KioskInfo>();
            if (query.Any())
            {
                list = query.ToList();
                var newlist = new List<KioskInfo>();
                if (info.BusinessScope.HasValue && info.BusinessScope.Value > 0)
                {
                    newlist.AddRange(list.Where(a => a.BusinessScopes.Split(',').Contains(info.BusinessScope.ToString())));
                    return newlist;
                }
            }

            return list;
        }

        /// <summary>
        /// 获取多经点位列表
        /// （除去指定多经点位）
        /// </summary>
        /// <param name="info"></param>
        /// <param name="kioskID"></param>
        /// <returns></returns>
        public List<KioskInfo> GetKioskInfos(KioskQueryInfo info, int kioskID)
        {
            var list = GetKioskInfos(info);
            return list.Any() ? list.Where(a => a.KioskID != kioskID).ToList() : new List<KioskInfo>();
        }

        /// <summary>
        /// 获取多经点位信息
        /// </summary>
        /// <param name="kioskID"></param>
        /// <returns></returns>
        public KioskInfo GetKioskInfo(int kioskID)
        {
            var model = new KioskInfo();
            var query = from k in Context.NHHCG_Kiosk
                        join p in Context.NHHCG_Project on k.ProjectID equals p.ProjectID
                        join dc in Context.Dictionary.Where(a => a.FieldType == "NHHCG_Region") on k.Region equals dc.FieldValue
                        join dc1 in Context.Dictionary.Where(a => a.FieldType == "KioskAreaScope") on k.AreaScope equals
                            dc1.FieldValue
                        where k.KioskID == kioskID
                        select new KioskInfo()
                        {
                            KioskID = k.KioskID,
                            ProjectID = k.ProjectID,
                            ProjectName = p.ProjectName,
                            Building = k.Building,
                            FloorID = k.FloorID,
                            Region = k.Region,
                            RegionName = dc.FieldName,
                            KioskNumber = k.KioskNumber,
                            AreaScope = k.AreaScope,
                            AreaScopeName = dc1.FieldName,
                            RentRemark = k.RentRemark,
                            Position = k.Position,
                            Contacts = k.Contacts,
                            BusinessScopes = string.IsNullOrEmpty(k.BusinessScopes)?"" :k.BusinessScopes,
                            PicList =
                                Context.NHHCG_Pic.Where(a => a.Type == (int)PicTypeEnum.多经点位 && a.RefID == kioskID)
                                    .OrderBy(a => a.Seq)
                                    .Select(a => a.Path)
                                    .ToList()
                        };

            if (query.Any())
            {
                model = query.FirstOrDefault();
                if (!string.IsNullOrEmpty(model.BusinessScopes))
                {
                    var list = Array.ConvertAll(model.BusinessScopes.Split(','), Convert.ToInt32).ToList();
                    model.BusinessScopesName = string.IsNullOrEmpty(model.BusinessScopes)
                        ? string.Empty
                        : string.Join(",",
                            (Context.Dictionary.Where(b => b.FieldType == "BusinessScope" && list.Contains(b.FieldValue))
                                .Select(b => b.FieldName)).ToList());
                }
            }

            return model;
        }

        /// <summary>
        /// 获取官网广告位类型下拉列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItemInfo> GetNHHCGAdPointDropDownList()
        {
            List<SelectListItemInfo> result =
                CacheManager.Default.Get<List<SelectListItemInfo>>("NHHCGAdPointDropDownList");
            if (result != null)
                return result;

            result = (from d in Context.Dictionary
                      where d.FieldType == "AdPointMedia"
                      select new SelectListItemInfo()
                      {
                          Text = d.FieldName,
                          Value = d.FieldValue.ToString()
                      }).ToList();
            CacheManager.Default.Add("NHHCGAdPointDropDownList", result);

            return result;
        }

        /// <summary>
        /// 获取官网电费承担类型下拉列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItemInfo> GetNHHCGElectricityTypeDropDownList()
        {
            List<SelectListItemInfo> result =
                CacheManager.Default.Get<List<SelectListItemInfo>>("NHHCGElectricityTypeDropDownList");
            if (result != null)
                return result;

            result = (from d in Context.Dictionary
                      where d.FieldType == "ElectricityType"
                      select new SelectListItemInfo()
                      {
                          Text = d.FieldName,
                          Value = d.FieldValue.ToString()
                      }).ToList();
            CacheManager.Default.Add("NHHCGElectricityTypeDropDownList", result);

            return result;
        }

        /// <summary>
        /// 获取广告位列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<ADPositionInfo> GetADPositionInfos(ADPositionQueryInfo info)
        {
            var query = from a in Context.NHHCG_ADPosition
                        where a.Status == 1
                        select new ADPositionInfo()
                        {
                            ADPositionID = a.ADPositionID,
                            ProjectID = a.ProjectID,
                            Building = a.Building,
                            FloorRemark = a.FloorRemark,
                            Region = a.Region,
                            ADPositionNumber = a.ADPositionNumber,
                            ADType = a.ADType,
                            Size = a.Size,
                            RentRemark = string.IsNullOrEmpty(a.RentRemark) ? "暂定" : a.RentRemark,
                            Position = a.Position,
                            Contacts = a.Contacts,
                            Feature = a.Feature,
                            ElectricityType = a.ElectricityType,
                            Pic = Context.NHHCG_Pic.Where(p => p.RefID == a.ADPositionID && p.Type == (int)PicTypeEnum.广告位)
                                .OrderBy(p => p.Seq)
                                .Select(p => p.Path)
                                .FirstOrDefault()
                        };

            if (info.ProjectID.HasValue && info.ProjectID.Value > 0)
            {
                query = query.Where(a => a.ProjectID == info.ProjectID.Value);
            }
            if (!string.IsNullOrEmpty(info.Building))
            {
                query = query.Where(a => a.Building.Equals(info.Building));
            }
            if (info.Region.HasValue && info.Region.Value > 0)
            {
                query = query.Where(a => a.Region == info.Region.Value);
            }
            if (info.ADType.HasValue && info.ADType.Value > 0)
            {
                query = query.Where(a => a.ADType == info.ADType.Value);
            }
            if (info.ElectricityType.HasValue && info.ElectricityType.Value > 0)
            {
                query = query.Where(a => a.ElectricityType == info.ElectricityType.Value);
            }

            return query.Any() ? query.ToList() : new List<ADPositionInfo>();
        }

        public List<ADPositionInfo> GetADPositionInfos(ADPositionQueryInfo info, int ADPositionID)
        {
            var list = GetADPositionInfos(info);
            return list.Any() ? list.Where(a => a.ADPositionID != ADPositionID).ToList() : new List<ADPositionInfo>();
        }

        public ADPositionInfo GetAdPositionInfo(int ADPositionID)
        {
            var query = from a in Context.NHHCG_ADPosition
                        join p in Context.NHHCG_Project on a.ProjectID equals p.ProjectID
                        join dc in Context.Dictionary.Where(d => d.FieldType == "NHHCG_Region") on a.Region equals dc.FieldValue
                        join dc1 in Context.Dictionary.Where(d => d.FieldType == "AdPointMedia") on a.ADType equals
                            dc1.FieldValue
                        join dc2 in Context.Dictionary.Where(d => d.FieldType == "ElectricityType") on a.ElectricityType equals
                            dc2.FieldValue
                        where a.ADPositionID == ADPositionID
                        select new ADPositionInfo()
                        {
                            ADPositionID = a.ADPositionID,
                            ProjectID = a.ProjectID,
                            ProjectName = p.ProjectName,
                            Building = a.Building,
                            FloorRemark = a.FloorRemark,
                            Region = a.Region,
                            RegionName = dc.FieldName,
                            ADPositionNumber = a.ADPositionNumber,
                            ADType = a.ADType,
                            ADTypeName = dc1.FieldName,
                            Size = a.Size,
                            RentRemark = string.IsNullOrEmpty(a.RentRemark) ? "暂无" : a.RentRemark,
                            Position = a.Position,
                            Contacts = a.Contacts,
                            Feature = a.Feature,
                            ElectricityType = a.ElectricityType,
                            ElectricityTypeName = dc2.FieldName,
                            PicList =
                                Context.NHHCG_Pic.Where(pic => pic.Type == (int)PicTypeEnum.广告位 && pic.RefID == ADPositionID)
                                    .OrderBy(pic => pic.Seq)
                                    .Select(pic => pic.Path)
                                    .ToList()
                        };

            return query.Any() ? query.FirstOrDefault() : new ADPositionInfo();
        }

        /// <summary>
        /// 获取官网楼别下拉列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<SelectListItemInfo> GetNGGCGBuildingDropDownList(NHHCGTypeEnum type)
        {
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
            var result = (from l in buildingList
                          where l.FieldId == (int)type
                          select new SelectListItemInfo()
                          {
                              Text = l.FieldName,
                              Value = l.FieldDesc
                          }).ToList();
            return result;
        }
    }
}
