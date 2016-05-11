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
    /// 项目处理
    /// </summary>
    public class ProjectProcess : BaseDataProcess<ProjectEntity>
    {

        public ProjectProcess()
        {
            SheetName = "项目";
        }

        protected override ProjectEntity ToEntity(DataRow row)
        {
            var entity = new ProjectEntity();
            entity.ProjectCode = GetValue<string>(row, "项目编码");
            //entity.ProjectID = GetValue<int>(row, "项目编号");
            entity.ProjectName = GetValue<string>(row, "项目名称");
            entity.BriefName = GetValue<string>(row, "项目简称");
            entity.ProjectSummary = GetValue<string>(row, "项目简介");
            entity.Region = GetValue<string>(row, "所在区域");
            entity.Province = GetValue<string>(row, "所在省份");
            entity.City = GetValue<string>(row, "所在城市");
            entity.AddressLine = GetValue<string>(row, "详细地址");
            entity.AddressInfo = GetValue<string>(row, "详细位置");
            entity.Zipcode = GetValue<string>(row, "邮编");
            entity.Latitude = GetValue<decimal>(row, "经度");
            entity.Longitude = GetValue<decimal>(row, "纬度");
            entity.GroundArea = GetValue<decimal>(row, "地上建筑面积");
            entity.UndergroundArea = GetValue<decimal>(row, "地下建筑面积");
            entity.TotalArea = GetValue<decimal>(row, "总建筑面积");
            entity.RenderingFileName = GetValue<string>(row, "效果图");
            entity.PlanSummary = GetValue<string>(row, "规划定位");
            entity.PlanHighlight = GetValue<string>(row, "规划亮点");
            entity.ParkingLotNum = GetValue<int>(row, "停车位数量");
            entity.AdPointNum = GetValue<int>(row, "广告位数量");
            entity.MultiBizPositionNum = GetValue<int>(row, "多经点位数量");
            entity.GrandOpeningDate = GetValue<DateTime>(row, "开业日期");
            //entity.Stage = GetValue<int>(row, "所处阶段");
            entity.StageStr = GetValue<string>(row, "所处阶段");
            //entity.OwnerCompanyID = GetValue<int>(row, "业主公司");
            //entity.InvestCompanyID = GetValue<int>(row, "投资公司");
            //entity.ManageCompanyID = GetValue<int>(row, "管理公司");
            entity.OwnerCompany = GetValue<string>(row, "业主公司");
            entity.InvestCompany = GetValue<string>(row, "投资公司");
            entity.ManageCompany = GetValue<string>(row, "管理公司");
            entity.BuildingNum = GetValue<int>(row, "楼宇数量");

            //获取关联的entity信息
            entity.Stage = ProjectUtil.GetStage(entity.StageStr);
            entity.ProvinceID = CommonUtil.GetProvinceId(entity.Province);
            entity.CityID = CommonUtil.GetCityId(entity.Province, entity.City);
            entity.RegionID = CommonUtil.GetRegionId(entity.Region);
            entity.OwnerCompanyID = CommonUtil.GetCompanyId(entity.OwnerCompany, 2);
            entity.InvestCompanyID = CommonUtil.GetCompanyId(entity.InvestCompany, 1);
            entity.ManageCompanyID = CommonUtil.GetCompanyId(entity.ManageCompany, 3);
            //return base.ToEntity(row);
            return entity;
        }

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override void ValidEntity(ProjectEntity entity)
        {
            if (string.IsNullOrEmpty(entity.ProjectName) || entity.ProjectName.Length == 0)
            {
                throw new NhhException("项目名称不能为空");
            }
            if (string.IsNullOrEmpty(entity.ProjectCode) || entity.ProjectCode.Length == 0)
            {
                throw new NhhException("项目编码不能为空");
            }
            if (string.IsNullOrEmpty(entity.Province) || entity.Province.Length == 0)
            {
                throw new NhhException("项目省份不能为空");
            }
            if (string.IsNullOrEmpty(entity.Region) || entity.Region.Length == 0)
            {
                throw new NhhException("项目区域不能为空");
            }
            if (string.IsNullOrEmpty(entity.City) || entity.City.Length == 0)
            {
                throw new NhhException("项目城市不能为空");
            }
            if (string.IsNullOrEmpty(entity.StageStr) || entity.StageStr.Length == 0)
            {
                throw new NhhException("项目阶段不能为空");
            }
            if (string.IsNullOrEmpty(entity.ManageCompany) || entity.ManageCompany.Length == 0)
            {
                throw new NhhException("管理公司不能为空");
            }
            if (string.IsNullOrEmpty(entity.OwnerCompany) || entity.OwnerCompany.Length == 0)
            {
                throw new NhhException("业主公司不能为空");
            }
            if (string.IsNullOrEmpty(entity.AddressLine) || entity.AddressLine.Length == 0)
            {
                throw new NhhException("项目地址不能为空");
            }
            if (string.IsNullOrEmpty(entity.Zipcode) || entity.Zipcode.Length == 0)
            {
                throw new NhhException("项目邮编不能为空");
            }
            //验证区域
            if (entity.Stage < 0)
            {
                throw new NhhException(string.Format("项目阶段 {0} 输入错误", entity.StageStr));
            }
            if (entity.RegionID < 0)
            {
                throw new NhhException(string.Format("项目区域 {0} 输入错误", entity.Region));
            }
            if (entity.ProvinceID < 0)
            {
                throw new NhhException(string.Format("项目省份 {0} 输入错误", entity.Province));
            }
            if (entity.CityID < 0)
            {
                throw new NhhException(string.Format("项目城市 {0} 输入错误", entity.City));
            }
            if (entity.OwnerCompanyID < 0)
            {
                throw new NhhException(string.Format("业主公司 {0} 输入错误", entity.OwnerCompany));
            }
            if (entity.ManageCompanyID < 0)
            {
                throw new NhhException(string.Format("管理公司 {0} 输入错误", entity.ManageCompany));
            }

            //InvestCompanyID不能0，可以为null
            if (entity.InvestCompanyID == 0)
            {
                entity.InvestCompanyID = null;
            }
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool IsExists(ProjectEntity entity)
        {
            string strCmd = string.Format(@"SELECT COUNT(0) as Number
                                          FROM [dbo].[Project](Nolock)
                                          Where ProjectName='{0}'", entity.ProjectName);
            return SqlHelper.ExecuteScalar(strCmd) > 0;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void SaveData(ProjectEntity entity)
        {
            entity.InDate = DateTime.Now;
            entity.InUser = 1;
            string strCmd = string.Format(@"INSERT INTO [dbo].[Project]
                                           ([ProjectName]
                                          ,[BriefName]
                                          ,[ProjectCode]
                                          ,[ProjectSummary]
                                          ,[RegionID]
                                          ,[ProvinceID]
                                          ,[CityID]
                                          ,[AddressLine]
                                          ,[AddressInfo]
                                          ,[Zipcode]
                                          ,[Latitude]
                                          ,[Longitude]
                                          ,[GroundArea]
                                          ,[UndergroundArea]
                                          ,[TotalArea]
                                          ,[PlanSummary]
                                          ,[PlanHighlight]
                                          ,[ParkingLotNum]
                                          ,[AdPointNum]
                                          ,[MultiBizPositionNum]
                                          ,[GrandOpeningDate]
                                          ,[Stage]
                                          ,[ManageCompanyID]
                                          ,[Status]
                                          ,[InDate]
                                          ,[InUser]
                                          ,[EditDate]
                                          ,[EditUser])
                                     VALUES
                                           (@ProjectName
                                           ,@BriefName
                                           ,@ProjectCode
                                           ,@ProjectSummary
                                           ,@RegionID
                                           ,@ProvinceID
                                           ,@CityID
                                           ,@AddressLine
                                           ,@AddressInfo
                                           ,@Zipcode
                                           ,@Latitude
                                           ,@Longitude
                                           ,@GroundArea
                                           ,@UndergroundArea
                                           ,@TotalArea
                                           ,@PlanSummary
                                           ,@PlanHighlight
                                           ,@ParkingLotNum
                                           ,@AdPointNum
                                           ,@MultiBizPositionNum
                                           ,@GrandOpeningDate
                                           ,@Stage
                                           ,@ManageCompanyID
                                           ,@Status
                                           ,@InDate
                                           ,@InUser
                                           ,@EditDate
                                           ,@EditUser);
                                    Select @@IDENTITY;");
            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@ProjectName", entity.ProjectName),
                 new SqlParameter("@BriefName", entity.BriefName),
                new SqlParameter("@ProjectCode", entity.ProjectCode),
                new SqlParameter("@ProjectSummary", entity.ProjectSummary),
                new SqlParameter("@RegionID", entity.RegionID),
                new SqlParameter("@ProvinceID", entity.ProvinceID),
                new SqlParameter("@CityID", entity.CityID),
                new SqlParameter("@AddressLine", entity.AddressLine),
                new SqlParameter("@AddressInfo", entity.AddressInfo),
                new SqlParameter("@Zipcode", entity.Zipcode),
                new SqlParameter("@Latitude", entity.Latitude),
                new SqlParameter("@Longitude", entity.Longitude),
                new SqlParameter("@GroundArea", entity.GroundArea),
                new SqlParameter("@UndergroundArea", entity.UndergroundArea),
                new SqlParameter("@TotalArea", entity.TotalArea),
                new SqlParameter("@PlanSummary", entity.PlanSummary),
                new SqlParameter("@PlanHighlight", entity.PlanHighlight),
                new SqlParameter("@ParkingLotNum", entity.ParkingLotNum),
                new SqlParameter("@AdPointNum", entity.AdPointNum),
                new SqlParameter("@MultiBizPositionNum", entity.MultiBizPositionNum),
                new SqlParameter("@GrandOpeningDate", entity.GrandOpeningDate),
                new SqlParameter("@Stage", entity.Stage),
                new SqlParameter("@ManageCompanyID", entity.ManageCompanyID),
                new SqlParameter("@Status", entity.Status),
                new SqlParameter("@InDate", entity.InDate),
                new SqlParameter("@InUser", entity.InUser),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser)
            };
            entity.ProjectID = SqlHelper.ExecuteScalar(strCmd, paramList);
            ProjectOwnerProcess.SaveProjectOwner(entity);
            //图片上传
            new ProjectPictureProcess().ProcessPicture(entity, _worker);
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="entity"></param>
        protected override void UpdateData(ProjectEntity entity)
        {
            //获取项目id
            entity.ProjectID = ProjectUtil.GetProjectId(entity.ProjectName);
            //base.UpdateData(entity);       
            string strCmd = string.Format(@"update [dbo].[Project]
                                           set [ProjectName]=@ProjectName
                                          ,[BriefName]=@BriefName
                                          ,[ProjectCode]=@ProjectCode
                                          ,[ProjectSummary]=@ProjectSummary
                                          ,[RegionID]=@RegionID
                                          ,[ProvinceID]=@ProvinceID
                                          ,[CityID]=@CityID
                                          ,[AddressLine]=@AddressLine
                                          ,[AddressInfo]=@AddressInfo
                                          ,[Zipcode]=@Zipcode
                                          ,[Latitude]=@Latitude
                                          ,[Longitude]=@Longitude
                                          ,[GroundArea]=@GroundArea
                                          ,[UndergroundArea]=@UndergroundArea
                                          ,[TotalArea]=@TotalArea
                                          ,[PlanHighlight]=@PlanHighlight
                                          ,[ParkingLotNum]=@ParkingLotNum
                                          ,[AdPointNum]=@AdPointNum
                                          ,[MultiBizPositionNum]=@MultiBizPositionNum
                                          ,[GrandOpeningDate]=@GrandOpeningDate
                                          ,[Stage]=@Stage                       
                                          ,[ManageCompanyID]=@ManageCompanyID
                                          ,[EditDate]=@EditDate
                                          ,[EditUser]=@EditUser
                                          where ProjectID=@ProjectID");
            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@ProjectName", entity.ProjectName),
                new SqlParameter("@BriefName", entity.BriefName),
                new SqlParameter("@ProjectCode", entity.ProjectCode),
                new SqlParameter("@ProjectSummary", entity.ProjectSummary),
                new SqlParameter("@RegionID", entity.RegionID),
                new SqlParameter("@ProvinceID", entity.ProvinceID),
                new SqlParameter("@CityID", entity.CityID),
                new SqlParameter("@AddressLine", entity.AddressLine),
                new SqlParameter("@AddressInfo", entity.AddressInfo),
                new SqlParameter("@Zipcode", entity.Zipcode),
                new SqlParameter("@Latitude", entity.Latitude),
                new SqlParameter("@Longitude", entity.Longitude),
                new SqlParameter("@GroundArea", entity.GroundArea),
                new SqlParameter("@UndergroundArea", entity.UndergroundArea),
                new SqlParameter("@TotalArea", entity.TotalArea),
                new SqlParameter("@PlanHighlight", entity.PlanHighlight),
                new SqlParameter("@ParkingLotNum", entity.ParkingLotNum),
                new SqlParameter("@AdPointNum", entity.AdPointNum),
                new SqlParameter("@MultiBizPositionNum", entity.MultiBizPositionNum),
                new SqlParameter("@GrandOpeningDate", entity.GrandOpeningDate),
                new SqlParameter("@Stage", entity.Stage),
                new SqlParameter("@ManageCompanyID", entity.ManageCompanyID),
                new SqlParameter("@EditDate",DateTime.Now),
                new SqlParameter("@EditUser",entity.EditUser),
                new SqlParameter("@ProjectID",entity.ProjectID)
            };
            SqlHelper.ExecuteNonQuery(strCmd, paramList);
            //图片上传
            new ProjectPictureProcess().ProcessPicture(entity, _worker);
        }

    }
}
