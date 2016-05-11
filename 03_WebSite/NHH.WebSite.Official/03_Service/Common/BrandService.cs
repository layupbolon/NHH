using NHH.Models.Common;
using NHH.Service.Common.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Entities;
using NHH.Framework.Service;
namespace NHH.Service.Common
{
    /// <summary>
    /// 品牌服务
    /// </summary>
    public class BrandService : NHHService<NHHEntities>, IBrandService
    {
        /// <summary>
        /// 获取品牌详细信息
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public BrandDetailModel GetBrandDetail(int brandId)
        {
            var model = new BrandDetailModel();
            var query = from b in Context.Brand
                        join dc in Context.Dictionary on b.BrandLevel equals dc.FieldValue
                        where b.BrandID == brandId && dc.FieldType == "BrandLevel"
                        select new
                        {
                            b.BrandID,
                            b.BrandName,
                            b.BrandLevel,
                            dc.FieldName,
                            BizTypeID = b.BizTypeID.HasValue ? b.BizTypeID.Value : 0,
                            b.BizType.BizTypeName,
                            BizCategoryID = b.BizCategoryID.HasValue ? b.BizCategoryID.Value : 0,
                            b.BizCategory.BizCategoryName,
                            b.ExistingProject,
                            b.Revenue,
                            b.AreaUsage,
                            b.FloorBearing,
                            b.FloorHeight,
                            b.ElectricityUsage,
                            b.WaterUsage,
                            b.DrainUsage,
                            b.GasUsage,
                            b.SmokeUsage,
                            b.Status
                        };
            var entity = query.FirstOrDefault();
            if (entity == null)
            {
                return model;
            }
            model.BrandId = entity.BrandID;
            model.BrandName = entity.BrandName;
            model.BrandLevel = entity.BrandLevel;
            model.BrandLevelInfo = new BrandLevelInfo();
            model.BrandLevelInfo.Name = entity.FieldName;
            model.BizTypeId = entity.BizTypeID;
            model.BizTypeInfo = new BizTypeInfo();
            model.BizTypeInfo.Name = entity.BizTypeName;
            model.BizCategoryId = entity.BizCategoryID;
            model.BizCategoryInfo = new BizCategoryInfo();
            model.BizCategoryInfo.Name = entity.BizCategoryName;
            model.ExistingProject = entity.ExistingProject;
            model.Revenue = entity.Revenue.HasValue ? entity.Revenue.Value : 0;
            model.AreaUsage = entity.AreaUsage.HasValue ? entity.AreaUsage.Value : 0;
            model.FloorBearing = entity.FloorBearing.HasValue ? entity.FloorBearing.Value : 0;
            model.FloorHeight = entity.FloorHeight.HasValue ? entity.FloorHeight.Value : 0;
            model.ElectricityUsage = entity.ElectricityUsage.HasValue ? entity.ElectricityUsage.Value : 0;
            model.WaterUsage = entity.WaterUsage.HasValue ? entity.WaterUsage.Value : 0;
            model.DrainUsage = entity.DrainUsage.HasValue ? entity.DrainUsage.Value : 0;
            model.GasUsage = entity.GasUsage.HasValue ? entity.GasUsage.Value : 0;
            model.SmokeUsage = entity.SmokeUsage.HasValue ? entity.SmokeUsage.Value : 0;
            model.Status = entity.Status;
            return model;
        }

        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <param name="brandName"></param>
        /// <param name="pagingInfo"></param>
        /// <returns></returns>
        public BrandListModel GetBrandList(string brandName, PagingInfo pagingInfo)
        {
            var model = new BrandListModel();
            model.BrandList = new List<BrandInfo>();

            var query = from b in Context.Brand
                        join dc in Context.Dictionary on b.BrandLevel equals dc.FieldValue
                        where b.Status == 1 && dc.FieldType == "BrandLevel"
                        select new
                        {
                            b.BrandID,
                            b.BrandName,
                            b.BrandLevel,
                            dc.FieldName,
                            BizTypeName = b.BizType.BizTypeName,
                            BizCategoryName = b.BizCategory.BizCategoryName,
                            b.ExistingProject,
                            b.Revenue,
                            b.AreaUsage,
                            b.FloorBearing,
                            b.FloorHeight,
                            b.ElectricityUsage,
                            b.WaterUsage,
                            b.DrainUsage,
                            b.GasUsage,
                            b.SmokeUsage,
                            b.Status
                        };
            if (!string.IsNullOrEmpty(brandName))
            {
                query = query.Where(p => p.BrandName.Contains(brandName));
            }
            //分页信息
            pagingInfo.TotalCount = query.Count();
            model.PagingInfo = pagingInfo;

            query = query.OrderBy(b => b.BrandID).Skip(pagingInfo.SkipNum).Take(pagingInfo.TakeNum);
            var brandList = query.ToList();
            if (brandList == null || brandList.Count == 0)
            {
                return model;
            }

            brandList.ForEach(item =>
            {
                var brand = new BrandInfo();
                brand.BrandId = item.BrandID;
                brand.BrandName = item.BrandName;
                brand.BrandLevel = item.BrandLevel;
                brand.BrandLevelInfo = new BrandLevelInfo();
                brand.BrandLevelInfo.Name = item.FieldName;
                brand.BizTypeInfo = new BizTypeInfo();
                brand.BizTypeInfo.Name = item.BizTypeName;
                brand.BizCategoryInfo = new BizCategoryInfo();
                brand.BizCategoryInfo.Name = item.BizCategoryName;

                model.BrandList.Add(brand);
            });
            return model;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddBrand(BrandDetailModel model)
        {
            var instance = CreateBizObject<Brand>();
            Brand entity = new Brand()
            {
                BrandName = model.BrandName,
                BrandLevel = model.BrandLevel,
                BizTypeID = model.BizTypeId,
                BizCategoryID = model.BizCategoryId,
                ExistingProject = model.ExistingProject,
                Revenue = model.Revenue,
                AreaUsage = model.AreaUsage,
                FloorBearing = model.FloorBearing,
                FloorHeight = model.FloorHeight,
                ElectricityUsage = model.ElectricityUsage,
                WaterUsage = model.WaterUsage,
                DrainUsage = model.DrainUsage,
                GasUsage = model.GasUsage,
                SmokeUsage = model.SmokeUsage,
                Status = 1,
                InDate = DateTime.Now,
                InUser = 1,
                EditDate = DateTime.Now,
                EditUser = 1
            };
            instance.Insert(entity);
            SaveChanges();

            return true;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        public void UpdateBrand(BrandDetailModel model)
        {
            var instance = CreateBizObject<Brand>();
            var entity = instance.GetBySysNo(model.BrandId);
            if (entity == null)
            {
                return;
            }

            entity.BrandLevel = model.BrandLevel;
            entity.BizTypeID = model.BizTypeId;
            entity.BizCategoryID = model.BizCategoryId;
            entity.ExistingProject = model.ExistingProject;
            entity.Revenue = model.Revenue;
            entity.AreaUsage = model.AreaUsage;
            entity.FloorBearing = model.FloorBearing;
            entity.FloorHeight = model.FloorHeight;
            entity.ElectricityUsage = model.ElectricityUsage;
            entity.WaterUsage = model.WaterUsage;
            entity.DrainUsage = model.DrainUsage;
            entity.GasUsage = model.GasUsage;
            entity.SmokeUsage = model.SmokeUsage;
            entity.Status = 1;
            entity.EditDate = DateTime.Now;
            entity.EditUser = 1;

            instance.Update(entity);
            SaveChanges();
        }
    }
}
