using NhhDataImport.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 品牌
    /// </summary>
    public class BrandProcess : BaseDataProcess<BrandEntity>
    {
        public BrandProcess()
        {
            SheetName = "品牌";
        }

        /// <summary>
        /// 转换为实体
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected override BrandEntity ToEntity(DataRow row)
        {
            var entity = new BrandEntity();
            entity.BrandName = GetValue<string>(row, "品牌名称");
            entity.BrandLevel = GetValue<string>(row, "品牌级次");
            entity.BrandIcon = GetValue<string>(row, "品牌图标");
            entity.BizType = GetValue<string>(row, "经营业态");
            entity.BizCategory = GetValue<string>(row, "经营品类");
            entity.ExistingProject = GetValue<string>(row, "入住项目");
            entity.Revenue = GetValue<int>(row, "年均营业额");
            entity.AreaUsage = GetValue<decimal>(row, "面积需求");
            entity.FloorBearing = GetValue<int>(row, "楼板承重");
            entity.FloorHeight = GetValue<int>(row, "楼高");
            entity.ElectricityUsage = GetValue<int>(row, "电量需求");
            entity.WaterUsage = GetValue<int>(row, "供水需求");
            entity.DrainUsage = GetValue<int>(row, "排水需求");
            entity.GasUsage = GetValue<int>(row, "燃气需求");
            entity.SmokeUsage = GetValue<int>(row, "排烟需求");
            return entity;
        }


        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override void ValidEntity(BrandEntity entity)
        {
            if (string.IsNullOrEmpty(entity.BrandName) || entity.BrandName.Length == 0)
            {
                throw new NhhException("品牌名称为空");
            }

            if (string.IsNullOrEmpty(entity.BrandLevel) || entity.BrandLevel.Length == 0)
            {
                throw new NhhException(string.Format("{0} 品牌级次为空", entity.BrandName));
            }

            //if (string.IsNullOrEmpty(entity.ExistingProject) || entity.ExistingProject.Length == 0)
            //{
            //    throw new NhhException("入住项目为空");
            //}

            entity.BrandLevelID = BrandUtil.GetBrandLevel(entity.BrandLevel);
            if (entity.BrandLevelID < 0)
            {
                throw new NhhException(string.Format("品牌级次 {0} 输入错误", entity.BrandLevel));
            }

            entity.BizTypeID = CommonUtil.GetBizTypeId(entity.BizType);
            if (entity.BizTypeID == 0) entity.BizTypeID = null;//经营业态为不能为0,可以为null
            if (entity.BizTypeID < 0)
            {
                throw new NhhException(string.Format("经营业态 {0} 输入错误", entity.BizType));
            }

            entity.BizCategoryID = CommonUtil.GetBizCategoryId(entity.BizCategory);
            if (entity.BizCategoryID == 0) entity.BizCategoryID = null;//经营品类为不能为0,可以为null
            //if (entity.BizCategoryID < 0)
            //{
            //    throw new NhhException(string.Format("经营品类 {0} 输入错误", entity.BizCategory));
            //}
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool IsExists(BrandEntity entity)
        {
            var strCmd = string.Format("SELECT COUNT(0) as Num FROM [dbo].[Brand](Nolock) Where BrandName='{0}'", entity.BrandName);
            return SqlHelper.ExecuteScalar(strCmd) > 0;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void SaveData(BrandEntity entity)
        {
            string strCmd = string.Format(@"INSERT INTO [dbo].[Brand]
                                           ([BrandName]
                                           ,[BrandLevel]
                                           ,[BrandIcon]
                                           ,[BizTypeID]
                                           ,[BizCategoryID]
                                           ,[ExistingProject]
                                           ,[Revenue]
                                           ,[AreaUsage]
                                           ,[FloorBearing]
                                           ,[FloorHeight]
                                           ,[ElectricityUsage]
                                           ,[WaterUsage]
                                           ,[DrainUsage]
                                           ,[GasUsage]
                                           ,[SmokeUsage]
                                           ,[Status]
                                           ,[InDate]
                                           ,[InUser]
                                           ,[EditDate]
                                           ,[EditUser])
                                     VALUES
                                           (@BrandName
                                           ,@BrandLevel
                                           ,@BrandIcon
                                           ,@BizTypeID
                                           ,@BizCategoryID
                                           ,@ExistingProject
                                           ,@Revenue
                                           ,@AreaUsage
                                           ,@FloorBearing
                                           ,@FloorHeight
                                           ,@ElectricityUsage
                                           ,@WaterUsage
                                           ,@DrainUsage
                                           ,@GasUsage
                                           ,@SmokeUsage
                                           ,@Status
                                           ,@InDate
                                           ,@InUser
                                           ,@EditDate
                                           ,@EditUser)");
            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@BrandName", entity.BrandName),
                new SqlParameter("@BrandLevel", entity.BrandLevelID),
                new SqlParameter("@BrandIcon", entity.BrandIcon),
                new SqlParameter("@BizTypeID", entity.BizTypeID),
                new SqlParameter("@BizCategoryID", entity.BizCategoryID),
                new SqlParameter("@ExistingProject", entity.ExistingProject),
                new SqlParameter("@Revenue", entity.Revenue),
                new SqlParameter("@AreaUsage", entity.AreaUsage),
                new SqlParameter("@FloorBearing", entity.FloorBearing),
                new SqlParameter("@FloorHeight", entity.FloorHeight),
                new SqlParameter("@ElectricityUsage", entity.ElectricityUsage),
                new SqlParameter("@WaterUsage", entity.WaterUsage),
                new SqlParameter("@DrainUsage", entity.DrainUsage),
                new SqlParameter("@GasUsage", entity.GasUsage),
                new SqlParameter("@SmokeUsage", entity.SmokeUsage),
                new SqlParameter("@Status", entity.Status),
                new SqlParameter("@InDate", entity.InDate),
                new SqlParameter("@InUser", entity.InUser),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
            };

            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }

        /// <summary>
        /// 更新品牌
        /// </summary>
        /// <param name="entity"></param>
        protected override void UpdateData(BrandEntity entity)
        {
            string strCmd = string.Format(@"UPDATE [dbo].[Brand] SET 
                                            [BrandName] = @BrandName
                                           ,[BrandLevel] = @BrandLevel
                                           ,[BrandIcon] = @BrandIcon
                                           ,[BizTypeID] = @BizTypeID
                                           ,[BizCategoryID] = @BizCategoryID
                                           ,[ExistingProject] = @ExistingProject
                                           ,[Revenue] = @Revenue
                                           ,[AreaUsage] = @AreaUsage
                                           ,[FloorBearing] = @FloorBearing
                                           ,[FloorHeight] = @FloorHeight
                                           ,[ElectricityUsage] = @ElectricityUsage
                                           ,[WaterUsage] = @WaterUsage
                                           ,[DrainUsage] = @DrainUsage
                                           ,[GasUsage] = @GasUsage
                                           ,[SmokeUsage] = @SmokeUsage
                                           ,[EditDate] = @EditDate
                                           ,[EditUser] = @EditUser
                                           WHERE BrandID=@BrandID");

            var paramList = new SqlParameter[] {
                new SqlParameter("@BrandName", entity.BrandName),
                new SqlParameter("@BrandLevel", entity.BrandLevelID),
                new SqlParameter("@BrandIcon", entity.BrandIcon),
                new SqlParameter("@BizTypeID", entity.BizTypeID),
                new SqlParameter("@BizCategoryID", entity.BizCategoryID),
                new SqlParameter("@ExistingProject", entity.ExistingProject),
                new SqlParameter("@Revenue", entity.Revenue),
                new SqlParameter("@AreaUsage", entity.AreaUsage),
                new SqlParameter("@FloorBearing", entity.FloorBearing),
                new SqlParameter("@FloorHeight", entity.FloorHeight),
                new SqlParameter("@ElectricityUsage", entity.ElectricityUsage),
                new SqlParameter("@WaterUsage", entity.WaterUsage),
                new SqlParameter("@DrainUsage", entity.DrainUsage),
                new SqlParameter("@GasUsage", entity.GasUsage),
                new SqlParameter("@SmokeUsage", entity.SmokeUsage),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
                new SqlParameter("@BrandID", BrandUtil.GetBrandId(entity.BrandName)),
            };

            SqlHelper.ExecuteNonQuery(strCmd, paramList);
        }
    }
}
