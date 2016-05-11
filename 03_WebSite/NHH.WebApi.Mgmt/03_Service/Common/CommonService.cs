using NHH.Entities;
using NHH.Framework.Caching;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Common.Enum.CommonEnums;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Common
{
    /// <summary>
    /// 公共服务
    /// </summary>
    public class CommonService : NHHService<NHHEntities>, ICommonService
    {
        /// <summary>
        /// 获取维修类型
        /// </summary>
        /// <returns></returns>
        public List<RepairTypeInfo> GetRepairType()
        {
            var result = new List<RepairTypeInfo>();
            Expression<Func<Dictionary, bool>> where = field => field.FieldType == "RepairType";
            var entityList = CreateCacheBizObject<Dictionary>().GetAllList(where);
            if (entityList != null)
            {
                foreach (var entity in entityList.OrderBy(item => item.FieldValue))
                {
                    result.Add(new RepairTypeInfo
                    {
                        RepairType = entity.FieldValue,
                        RepairTypeName = entity.FieldName
                    });
                }
                //entityList.ForEach(entity => result.Add(new RepairTypeInfo
                //{
                //    RepairType = entity.FieldValue,
                //    RepairTypeName = entity.FieldName
                //}));
            }
            return result;
        }
        /// <summary>
        /// 获取公共字段列表
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public List<CommonFieldInfo> GetCommonFieldList(string fieldType)
        {
            var list = new List<CommonFieldInfo>();

            Expression<Func<Dictionary, bool>> where = field => field.FieldType == fieldType;
            var entityList = CreateCacheBizObject<Dictionary>().GetAllList(where);
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    list.Add(new CommonFieldInfo
                    {
                        FieldId = entity.FieldID,
                        FieldName = entity.FieldName,
                        FieldValue = entity.FieldValue,
                        FieldDesc = entity.FieldDesc,
                    });
                });
            }
            return list;
        }
        /// <summary>
        /// 获取公共下拉框列表
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        /// <remarks>注意这里只取Seq>0的，目的是为了把在前端不显示的列排除在外</remarks>
        public List<SelectListItemInfo> GetCommonSelectList(string fieldType)
        {
            var list = new List<SelectListItemInfo>();
            Expression<Func<Dictionary, bool>> where = field => field.FieldType == fieldType && field.Seq > 0;
            var entityList = CreateCacheBizObject<Dictionary>().GetAllList(where);
            if (entityList != null)
            {
                foreach (var entity in entityList.OrderBy(item => item.Seq))
                {
                    list.Add(new SelectListItemInfo
                    {
                        Text = entity.FieldName,
                        Value = entity.FieldValue.ToString()
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 获取经营业态列表
        /// </summary>
        /// <returns></returns>
        public List<BizTypeInfo> GetBizTypeList()
        {
            var list = new List<BizTypeInfo>();
            var entityList = CreateCacheBizObject<BizType>().GetAllList();
            entityList.ForEach(entity =>
            {
                list.Add(new BizTypeInfo
                {
                    Id = entity.BizTypeID,
                    Name = entity.BizTypeName,
                });
            });
            return list;
        }

        /// <summary>
        /// 获取经营品类列表
        /// </summary>
        /// <returns></returns>
        public List<BizCategoryInfo> GetBizCategoryList()
        {
            var list = new List<BizCategoryInfo>();
            var entityList = CreateCacheBizObject<BizCategory>().GetAllList();
            entityList.ForEach(entity =>
            {
                list.Add(new BizCategoryInfo
                {
                    Id = entity.BizCategoryID,
                    Name = entity.BizCategoryName,
                });
            });
            return list;
        }

        /// <summary>
        /// 根据业态id获取品类列表
        /// </summary>
        /// <param name="bizTypeId"></param>
        /// <returns></returns>
        public List<BizCategoryInfo> GetBizCategoryList(int bizTypeId)
        {
            var list = new List<BizCategoryInfo>();
            string where = string.Format(" BizTypeID={0}", bizTypeId);
            var entityList = CreateCacheBizObject<BizCategory>().GetAllList(where);
            entityList.ForEach(entity =>
            {
                list.Add(new BizCategoryInfo
                {
                    Id = entity.BizCategoryID,
                    Name = entity.BizCategoryName,
                });
            });
            return list;
        }

        ///// <summary>
        ///// 获取商铺状态列表
        ///// </summary>
        ///// <returns></returns>
        //public List<ProjectUnitStatusInfo> GetUnitStatusList()
        //{
        //    var list = new List<ProjectUnitStatusInfo>();
        //    var entityList = CreateCacheBizObject<View_UnitStatus>().GetAllList();
        //    entityList.ForEach(entity =>
        //    {
        //        list.Add(new ProjectUnitStatusInfo
        //        {
        //            Id = entity.UnitStatusID,
        //            Name = entity.UnitStatusName,
        //        });
        //    });
        //    return list;
        //}

        ///// <summary>
        ///// 获取商铺类型列表
        ///// </summary>
        ///// <returns></returns>
        //public List<ProjectUnitTypeInfo> GetUnitTypeList()
        //{
        //    var list = new List<ProjectUnitTypeInfo>();
        //    var entityList = CreateCacheBizObject<View_UnitType>().GetAllList();
        //    entityList.ForEach(entity =>
        //    {
        //        list.Add(new ProjectUnitTypeInfo
        //        {
        //            Id = entity.UnitTypeID,
        //            Name = entity.UnitTypeName,
        //        });
        //    });
        //    return list;
        //}

        ///// <summary>
        ///// 获取合同状态列表
        ///// </summary>
        ///// <returns></returns>
        //public List<ContractStatusInfo> GetContractStatusList()
        //{
        //    var list = new List<ContractStatusInfo>();
        //    var entityList = CreateCacheBizObject<View_ContractStatus>().GetAllList();
        //    entityList.ForEach(entity =>
        //    {
        //        list.Add(new ContractStatusInfo
        //        {
        //            Id = entity.ContractStatusID,
        //            Name = entity.ContractStatusName,
        //        });
        //    });
        //    return list;
        //}

        ///// <summary>
        ///// 获取公司类型列表
        ///// </summary>
        ///// <returns></returns>
        //public List<CompanyTypeInfo> GetCompanyTypeList()
        //{
        //    var list = new List<CompanyTypeInfo>();
        //    var entityList = CreateCacheBizObject<View_CompanyType>().GetAllList();
        //    entityList.ForEach(entity =>
        //    {
        //        list.Add(new CompanyTypeInfo
        //        {
        //            Id = entity.CompanyTypeID,
        //            Name = entity.CompanyTypeName,
        //        });
        //    });
        //    return list;
        //}

        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <returns></returns>
        public List<RegionInfo> GetRegionList()
        {
            var list = new List<RegionInfo>();
            var entityList = CreateCacheBizObject<Region>().GetAllList();
            entityList.ForEach(entity =>
            {
                list.Add(new RegionInfo
                {
                    Id = entity.RegionID,
                    Name = entity.RegionName,
                });
            });
            return list;
        }


        ///// <summary>
        ///// 获取省份列表
        ///// </summary>
        ///// <returns></returns>
        //public List<ProvinceInfo> GetProvinceList()
        //{
        //    var list = new List<ProvinceInfo>();
        //    var entityList = CreateCacheBizObject<Province>().GetAllList();
        //    entityList.ForEach(entity =>
        //    {
        //        list.Add(new ProvinceInfo
        //        {
        //            RegionId = entity.RegionID.HasValue ? entity.RegionID.Value : 0,
        //            Id = entity.ProvinceID,
        //            Name = entity.ProvinceName,
        //        });
        //    });
        //    return list;
        //}

        ///// <summary>
        ///// 获取省份列表
        ///// </summary>
        ///// <param name="regionId"></param>
        ///// <returns></returns>
        //public List<ProvinceInfo> GetProvinceList(int regionId)
        //{
        //    var list = new List<ProvinceInfo>();
        //    Expression<Func<Province, bool>> where = item => item.RegionID == regionId;
        //    var entityList = CreateCacheBizObject<Province>().GetAllList(where);
        //    entityList.ForEach(entity =>
        //    {
        //        list.Add(new ProvinceInfo
        //        {
        //            RegionId = entity.RegionID.HasValue ? entity.RegionID.Value : 0,
        //            Id = entity.ProvinceID,
        //            Name = entity.ProvinceName,
        //        });
        //    });
        //    return list;
        //}

        ///// <summary>
        ///// 获取城市列表
        ///// </summary>
        ///// <param name="provinceId"></param>
        ///// <returns></returns>
        //public List<CityInfo> GetCityList(int provinceId)
        //{
        //    var list = new List<CityInfo>();
        //    Expression<Func<City, bool>> where = item => item.ProvinceID == provinceId;
        //    var entityList = CreateCacheBizObject<City>().GetAllList(where);
        //    entityList.ForEach(entity =>
        //    {
        //        list.Add(new CityInfo
        //        {
        //            ProvinceId = entity.ProvinceID,
        //            Id = entity.CityID,
        //            Name = entity.CityName,
        //        });
        //    });
        //    return list;
        //}

        ///// <summary>
        ///// 获取区列表
        ///// </summary>
        ///// <param name="cityId"></param>
        ///// <returns></returns>
        //public List<DistrictInfo> GetDistriceList(int cityId)
        //{
        //    var list = new List<DistrictInfo>();
        //    Expression<Func<District, bool>> where = item => item.CityID == cityId;
        //    var entityList = CreateCacheBizObject<District>().GetAllList(where);
        //    entityList.ForEach(entity =>
        //    {
        //        list.Add(new DistrictInfo
        //        {
        //            CityId = entity.CityID,
        //            Id = entity.DistrictID,
        //            Name = entity.DistrictName,
        //        });
        //    });
        //    return list;
        //}

        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItemInfo> GetBrandList()
        {
            var list = new List<SelectListItemInfo>();
            Expression<Func<Brand, bool>> where = item => item.Status == 1;
            var entityList = CreateCacheBizObject<Brand>().GetAllList(where);
            if (entityList != null)
            {
                entityList.ForEach(item =>
                {
                    list.Add(new SelectListItemInfo
                    {
                        Text = item.BrandName,
                        Value = item.BrandID.ToString(),
                    });
                });
            }
            return list;
        }

        /// <summary>
        /// 获取数字下拉列表
        /// </summary>
        /// <param name="startNum"></param>
        /// <param name="endNum"></param>
        /// <returns></returns>
        public List<SelectListItemInfo> GetSelectItemList(int startNum, int endNum)
        {
            var list = new List<SelectListItemInfo>();
            for (int n = startNum; n <= endNum; n++)
            {
                list.Add(new SelectListItemInfo
                {
                    Text = n.ToString(),
                    Value = n.ToString(),
                });
            }
            return list;
        }


        ///// <summary>
        ///// 获取性别类型列表
        ///// </summary>
        ///// <returns></returns>
        //public List<GenderTypeInfo> GetGenderTypeList()
        //{
        //    var list = new List<GenderTypeInfo>();
        //    var entityList = CreateBizObject<View_GenderType>().GetAllList();
        //    entityList.ForEach(entity =>
        //    {
        //        list.Add(new GenderTypeInfo
        //        {
        //            Id = entity.GenderTypeID,
        //            Name = entity.GenderTypeName,
        //        });
        //    });
        //    return list;
        //}
        /// <summary>
        /// 发送邮件、短信、微信(列表)
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public bool SendMessageList(List<MessageInfo> modelList)
        {
            int i = 0;
            foreach (var model in modelList)
            {
                if (SendMessage(model))
                    i++;
            }
            return i > 0;
        }
        /// <summary>
        /// 发送邮件、短信、微信
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SendMessage(MessageInfo model)
        {
            var instance = CreateBizObject<Message>();
            var entity = new Message()
            {
                MessageType = model.MessageType,
                Priority = model.Priority,
                Recipient = model.Recipient,
                Subject = model.Subject,
                Content = model.Content,
                Status = 1,
                InDate = DateTime.Now,
                InUser = 1,
                EditDate = DateTime.Now,
                EditUser = 1
            };

            instance.Insert(entity);
            this.SaveChanges();
            model.MessageID = entity.MessageID;
            return model.MessageID > 0;
        }

        /// <summary>
        /// 获取消息内容
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public MessageInfo GetMessage(int messageId)
        {
            MessageInfo model = null;
            var query = from m in Context.Message
                        where m.MessageID == messageId && m.Status >= 0 && m.MessageType == (int)MessageTypeEnum.Wechat
                        select new
                        {
                            m.MessageID,
                            m.MessageType,
                            m.Subject,
                            m.Content,
                            m.Status,
                            m.InDate,
                            m.InUser
                        };
            var entity = query.FirstOrDefault();
            if (entity != null)
            {
                model = new MessageInfo();
                model.MessageID = entity.MessageID;
                model.Subject = entity.Subject;
                model.Content = entity.Content;
                //model.Status = entity.Status;
                model.InDate = entity.InDate;
                //model.InUser = entity.InUser;
            }
            return model;
        }

        /// <summary>
        /// 添加管理平台用户消息(多条)
        /// </summary>
        /// <param name="modelList"></param>
        public void SendSysUserMessageList(List<SysUserMessage> modelList)
        {
            foreach (var model in modelList)
            {
                SendSysUserMessage(model);
            }
        }
        /// <summary>
        /// 添加管理平台用户消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SendSysUserMessage(SysUserMessage model)
        {
            var instance = CreateBizObject<Sys_User_Message>();
            var entity = new Sys_User_Message()
            {
                UserID = model.UserID,
                Subject = model.Subject,
                Content = model.Content,
                SourceType = model.SourceType,
                SourceRefID = model.SourceRefID,
                Status = 1,
                InDate = DateTime.Now,
                InUser = 0,
                EditDate = DateTime.Now,
                EditUser = 0
            };

            instance.Insert(entity);
            this.SaveChanges();
            model.MessageID = entity.MessageID;
            return model.MessageID > 0;
        }

        /// <summary>
        /// 根据Key获取Message模板
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public MessageTemplate GetMessageTemplate(string key)
        {
            MessageTemplate model = null;
            var entity = (from mt in Context.Message_Template where mt.TemplateKey == key select mt).FirstOrDefault();
            if (entity != null)
            {
                model = new MessageTemplate();
                model.TemplateID = entity.TemplateID;
                model.TemplateKey = entity.TemplateKey;
                model.MessageType = entity.MessageType;
                model.Title = entity.Title;
                model.Content = entity.Content;
                model.Description = entity.Description;
            }
            return model;
        }

        /// <summary>
        /// 获取BannerLocationType
        /// </summary>
        /// <returns></returns>
        public List<BannerLocation> GetBannerLocationType()
        {
            List<BannerLocation> result = CacheManager.Default.Get<List<BannerLocation>>("BannerLocationType");
            if (result != null)
                return result;
            else
            {
                var query = (from bl in Context.Banner_Location
                    join page in Context.Dictionary on bl.PageID equals page.FieldValue
                    where page.FieldType == "PageID" && bl.Target == 1
                    select new
                    {
                        bl.LocationType,
                        bl.LocationName,
                        bl.PageID,
                        page.FieldName
                    });
                var entityList = query.ToList();
                if (entityList != null)
                {
                    result = new List<BannerLocation>();
                    entityList.ForEach(entity =>
                    {
                        result.Add(new BannerLocation
                        {
                            LocationType = entity.LocationType,
                            LocationName = entity.LocationName,
                            PageID = entity.PageID,
                            PageName = entity.FieldName
                        });
                    });
                    CacheManager.Default.Add("BannerLocationType", result);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取Banner
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="locationType"></param>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public BannerMaster GetBanner(int projectId, int locationType, int pageId)
        {
            BannerMaster result=new BannerMaster();

            var bannerList = GetAllBanner();

            if (bannerList != null)
            {
                result = (from list in bannerList
                    where list.ProjectID == projectId && list.LocationType == locationType && list.PageID == pageId
                    select new BannerMaster()
                    {
                        BannerID = list.BannerID,
                        BannerType = list.BannerType,
                        BannerTypeName = list.BannerTypeName,
                        LocationType = list.LocationType,
                        PageID = list.PageID,
                        Remark = list.Remark,
                        BannerInfoList = list.BannerInfoList
                    }).FirstOrDefault();
            }
            return result;
        }
        /// <summary>
        /// 获取某个页面的所有Banner列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public List<BannerMaster> GetBannerList(int projectId, int pageId)
        {
            List<BannerMaster> result = new List<BannerMaster>();

            var bannerList = GetAllBanner();

            if (bannerList != null)
            {
                result = (from list in bannerList
                    where list.ProjectID == projectId && list.PageID == pageId
                    select new BannerMaster()
                    {
                        BannerID = list.BannerID,
                        BannerType = list.BannerType,
                        BannerTypeName = list.BannerTypeName,
                        LocationType = list.LocationType,
                        PageID = list.PageID,
                        Remark = list.Remark,
                        BannerInfoList = list.BannerInfoList
                    }).ToList();
            }
            return result;
        }
        /// <summary>
        /// 投诉评论内容
        /// </summary>
        /// <returns></returns>
        public List<string> GetComplaintCommentList()
        {
            List<string> result = CacheManager.Default.Get<List<string>>("ComplaintComment");
            if (result == null)
            {
                result =
                    (from d in Context.Dictionary
                        where d.FieldType == "ComplaintCommentTag"
                        orderby d.Seq ascending
                        select d.FieldName).ToList();
                CacheManager.Default.Add("ComplaintComment", result);
            }
            return result;
        }

        /// <summary>
        /// 维修评论内容
        /// </summary>
        /// <returns></returns>
        public List<string> GetRepairCommentList()
        {
            List<string> result = CacheManager.Default.Get<List<string>>("RepairComment");
            if (result == null)
            {
                result =
                    (from d in Context.Dictionary
                     where d.FieldType == "RepairCommentTag"
                     orderby d.Seq ascending
                     select d.FieldName).ToList();
                CacheManager.Default.Add("RepairComment", result);
            }
            return result;
        }

        /// <summary>
        /// 获取吐槽标签
        /// </summary>
        /// <returns></returns>
        public List<string> GetComplaintTagList()
        {
            List<string> result = CacheManager.Default.Get<List<string>>("ComplaintTag");
            if (result == null)
            {
                result = (from d in Context.Dictionary
                    where d.FieldType == "ComplaintTag"
                    orderby d.Seq ascending
                    select d.FieldName).ToList();
                CacheManager.Default.Add("ComplaintTag", result);
            }
            return result;
        }

        #region Private
        /// <summary>
        /// 获取所有Banner放入缓存
        /// </summary>
        /// <returns></returns>
        private List<BannerMaster> GetAllBanner()
        {
            List<BannerMaster> bannerList = CacheManager.Default.Get<List<BannerMaster>>("BannerList");
            if (bannerList == null)
            {
                var query = (from banner in Context.Banner
                             join location in Context.Banner_Location on banner.LocationID equals location.LocationID
                             join d in Context.Dictionary on banner.BannerType equals d.FieldValue
                             where d.FieldType == "BannerType" && location.Target == 1 && banner.Status == 1
                             select new
                             {
                                 location.LocationID,
                                 location.LocationType,
                                 location.PageID,
                                 location.LocationName,
                                 banner.ProjectID,
                                 banner.BannerType,
                                 BannerTypeName = d.FieldName,
                                 banner.BannerID,
                                 banner.Remark,
                                 BannerInfoList = (from info in Context.BannerInfo
                                                   where info.BannerID == banner.BannerID
                                                   orderby info.Seq
                                                   select new
                                                   {
                                                       info.InfoID,
                                                       info.BannerID,
                                                       info.Seq,
                                                       info.Title,
                                                       info.Content,
                                                       info.ResourcePath,
                                                       info.Link
                                                   })
                             });
                var entityList = query.ToList();
                if (entityList != null)
                {
                    bannerList = new List<BannerMaster>();
                    entityList.ForEach(entity =>
                    {
                        var contract = new BannerMaster();
                        contract.BannerID = entity.BannerID;
                        contract.BannerType = entity.BannerType;
                        contract.BannerTypeName = entity.BannerTypeName;
                        contract.LocationType = entity.LocationType;
                        contract.Remark = entity.Remark;
                        contract.ProjectID = entity.ProjectID;
                        contract.PageID = entity.PageID;
                        if (entity.BannerInfoList != null)
                        {
                            entity.BannerInfoList.ToList().ForEach(item =>
                            {
                                var info = new NHH.Models.Common.BannerInfo();
                                info.Seq = item.Seq;
                                info.Title = item.Title;
                                info.Content = item.Content;
                                info.ResourcePath = NHH.Framework.Utility.UrlHelper.GetImageUrl(item.ResourcePath);
                                info.Link = item.Link;
                                contract.BannerInfoList.Add(info);
                            });
                        }
                        bannerList.Add(contract);
                    });
                }
                CacheManager.Default.Add("BannerList", bannerList);
            }
            return bannerList;
        }
        #endregion Private
    }
}
