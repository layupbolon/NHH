using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
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
                foreach (var entity in entityList.OrderBy(item => item.Seq).ThenBy(item => item.FieldValue))
                {
                    list.Add(new CommonFieldInfo
                    {
                        FieldId = entity.FieldID,
                        FieldName = entity.FieldName,
                        FieldValue = entity.FieldValue,
                        FieldDesc = entity.FieldDesc,
                    });
                }

                //entityList.ForEach(entity =>
                //{
                //    list.Add(new CommonFieldInfo
                //    {
                //        FieldId = entity.FieldID,
                //        FieldName = entity.FieldName,
                //        FieldValue = entity.FieldValue,
                //        FieldDesc = entity.FieldDesc,
                //    });
                //});
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

        /// <summary>
        /// 获取商铺状态列表
        /// </summary>
        /// <returns></returns>
        public List<ProjectUnitStatusInfo> GetUnitStatusList()
        {
            var list = new List<ProjectUnitStatusInfo>();
            var entityList = CreateCacheBizObject<View_UnitStatus>().GetAllList();
            entityList.ForEach(entity =>
            {
                list.Add(new ProjectUnitStatusInfo
                {
                    Id = entity.UnitStatusID,
                    Name = entity.UnitStatusName,
                });
            });
            return list;
        }

        /// <summary>
        /// 获取商铺类型列表
        /// </summary>
        /// <returns></returns>
        public List<ProjectUnitTypeInfo> GetUnitTypeList()
        {
            var list = new List<ProjectUnitTypeInfo>();
            var entityList = CreateCacheBizObject<View_UnitType>().GetAllList();
            entityList.ForEach(entity =>
            {
                list.Add(new ProjectUnitTypeInfo
                {
                    Id = entity.UnitTypeID,
                    Name = entity.UnitTypeName,
                });
            });
            return list;
        }

        /// <summary>
        /// 获取合同状态列表
        /// </summary>
        /// <returns></returns>
        public List<ContractStatusInfo> GetContractStatusList()
        {
            var list = new List<ContractStatusInfo>();
            var entityList = CreateCacheBizObject<View_ContractStatus>().GetAllList();
            entityList.ForEach(entity =>
            {
                list.Add(new ContractStatusInfo
                {
                    Id = entity.ContractStatusID,
                    Name = entity.ContractStatusName,
                });
            });
            return list;
        }

        /// <summary>
        /// 获取公司类型列表
        /// </summary>
        /// <returns></returns>
        public List<CompanyTypeInfo> GetCompanyTypeList()
        {
            var list = new List<CompanyTypeInfo>();
            var entityList = CreateCacheBizObject<View_CompanyType>().GetAllList();
            entityList.ForEach(entity =>
            {
                list.Add(new CompanyTypeInfo
                {
                    Id = entity.CompanyTypeID,
                    Name = entity.CompanyTypeName,
                });
            });
            return list;
        }

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


        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <returns></returns>
        public List<ProvinceInfo> GetProvinceList()
        {
            var list = new List<ProvinceInfo>();
            var entityList = CreateCacheBizObject<Province>().GetAllList();
            entityList.ForEach(entity =>
            {
                list.Add(new ProvinceInfo
                {
                    RegionId = entity.RegionID.HasValue ? entity.RegionID.Value : 0,
                    Id = entity.ProvinceID,
                    Name = entity.ProvinceName,
                });
            });
            return list;
        }

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public List<ProvinceInfo> GetProvinceList(int regionId)
        {
            var list = new List<ProvinceInfo>();
            Expression<Func<Province, bool>> where = item => item.RegionID == regionId;
            var entityList = CreateCacheBizObject<Province>().GetAllList(where);
            entityList.ForEach(entity =>
            {
                list.Add(new ProvinceInfo
                {
                    RegionId = entity.RegionID.HasValue ? entity.RegionID.Value : 0,
                    Id = entity.ProvinceID,
                    Name = entity.ProvinceName,
                });
            });
            return list;
        }

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        public List<CityInfo> GetCityList(int provinceId)
        {
            var list = new List<CityInfo>();
            Expression<Func<City, bool>> where = item => item.ProvinceID == provinceId;
            var entityList = CreateCacheBizObject<City>().GetAllList(where);
            entityList.ForEach(entity =>
            {
                list.Add(new CityInfo
                {
                    ProvinceId = entity.ProvinceID,
                    Id = entity.CityID,
                    Name = entity.CityName,
                });
            });
            return list;
        }

        /// <summary>
        /// 获取区列表
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<DistrictInfo> GetDistriceList(int cityId)
        {
            var list = new List<DistrictInfo>();
            Expression<Func<District, bool>> where = item => item.CityID == cityId;
            var entityList = CreateCacheBizObject<District>().GetAllList(where);
            entityList.ForEach(entity =>
            {
                list.Add(new DistrictInfo
                {
                    CityId = entity.CityID,
                    Id = entity.DistrictID,
                    Name = entity.DistrictName,
                });
            });
            return list;
        }

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


        /// <summary>
        /// 获取性别类型列表
        /// </summary>
        /// <returns></returns>
        public List<GenderTypeInfo> GetGenderTypeList()
        {
            var list = new List<GenderTypeInfo>();
            var entityList = CreateBizObject<View_GenderType>().GetAllList();
            entityList.ForEach(entity =>
            {
                list.Add(new GenderTypeInfo
                {
                    Id = entity.GenderTypeID,
                    Name = entity.GenderTypeName,
                });
            });
            return list;
        }
        /// <summary>
        /// 查询民族
        /// </summary>
        /// <param name="nationid"></param>
        /// <returns></returns>
        public string GetNationStr(int nationid, string fieldType)
        {
            var list = new List<CommonFieldInfo>();
            string str = "";
            Expression<Func<Dictionary, bool>> where = (field => field.FieldType == fieldType&&field.FieldValue==nationid);
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
                str = entityList.FirstOrDefault().FieldName;
            }
            return str;
        }


    }
}
