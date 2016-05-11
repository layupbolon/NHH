using NhhDataImport.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 项目楼层
    /// </summary>
    public class ProjectFloorProcess : BaseDataProcess<ProjectFloorEntity>
    {
        public ProjectFloorProcess()
        {
            SheetName = "楼层";
        }

        protected override ProjectFloorEntity ToEntity(DataRow row)
        {
            var entity = new ProjectFloorEntity();
            //entity.ProjectID = GetValue<int>(row, "项目编号");
            //entity.BuildingID = GetValue<int>(row, "楼宇编号");
            entity.BuildingName = GetValue<string>(row, "楼宇名称");
            entity.ProjectName = GetValue<string>(row, "项目名称");
            entity.FloorName = GetValue<string>(row, "楼层编号");
            entity.FloorDescription = GetValue<string>(row, "楼层说明");
            entity.FloorMapFileName = GetValue<string>(row, "楼层平面图");
            entity.TotalRentArea = GetValue<decimal>(row, "总计租面积");
            entity.TotalRentUnit = GetValue<int>(row, "总计租单元数");
            entity.ContractRentArea = GetValue<decimal>(row, "签约面积");
            entity.ContractRentUnit = GetValue<int>(row, "签约单元数");

            entity.ProjectID = ProjectUtil.GetProjectId(entity.ProjectName);
            entity.BuildingID = ProjectUtil.GetBuildingId(entity.ProjectName, entity.BuildingName);
            //return base.ToEntity(row);
            return entity;
        }

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override void ValidEntity(ProjectFloorEntity entity)
        {
            if (string.IsNullOrEmpty(entity.BuildingName) || entity.BuildingName.Length == 0)
            {
                throw new NhhException("楼宇名称不能为空");
            }
            if (string.IsNullOrEmpty(entity.ProjectName) || entity.ProjectName.Length == 0)
            {
                throw new NhhException("项目名称不能为空");
            }
            if (string.IsNullOrEmpty(entity.FloorName) || entity.FloorName.Length == 0)
            {
                throw new NhhException("楼层编号不能为空");
            }
            if (entity.ProjectID <= 0)
            {
                throw new NhhException(string.Format("项目名称 {0} 输入错误", entity.ProjectName));
            }
            if (entity.BuildingID <= 0)
            {
                throw new NhhException(string.Format("项目名称 {0} 输入错误", entity.BuildingName));
            }
            //正则表达式验证楼层编号
            var match = Regex.Match(entity.FloorName, @"^B\d{1}$|^\d{1,2}F$", RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                throw new NhhException(string.Format("楼层名称 {0} 输入错误", entity.FloorName));
            }

            match = Regex.Match(entity.FloorName, @"\d+");
            entity.FloorNumber = Convert.ToInt32(match.Value);
            match = Regex.Match(entity.FloorName, @"^B+", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                entity.FloorNumber = entity.FloorNumber * -1;
            }
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool IsExists(ProjectFloorEntity entity)
        {
            string strCmd = string.Format(@"SELECT COUNT(0) as Number
                                          FROM [dbo].[Project_Floor](Nolock)
                                          Where BuildingID='{0}' and FloorName='{1}'", entity.BuildingID, entity.FloorName);
            return SqlHelper.ExecuteScalar(strCmd) > 0;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity"></param>
        protected override void SaveData(ProjectFloorEntity entity)
        {
            string strCmd = string.Format(@"INSERT INTO [dbo].[Project_Floor]
                                           ([ProjectID]
                                          ,[FloorName]
                                          ,[FloorDescription]
                                          ,[BuildingID]
                                          ,[FloorNumber]
                                          ,[TotalRentArea]
                                          ,[TotalRentUnit]
                                          ,[ContractRentArea]
                                          ,[ContractRentUnit]
                                          ,[InDate]
                                          ,[InUser]
                                          ,[EditDate]
                                          ,[EditUser])
                                     VALUES
                                           (@ProjectID
                                           ,@FloorName
                                           ,@FloorDescription
                                           ,@BuildingID
                                           ,@FloorNumber
                                           ,@TotalRentArea
                                           ,@TotalRentUnit
                                           ,@ContractRentArea
                                           ,@ContractRentUnit
                                           ,@InDate
                                           ,@InUser
                                           ,@EditDate
                                           ,@EditUser);SELECT SCOPE_IDENTITY()");
            var paramList = new SqlParameter[] 
            { 
                new SqlParameter("@ProjectID", entity.ProjectID),
                 new SqlParameter("@FloorName", entity.FloorName),
                new SqlParameter("@FloorDescription", entity.FloorDescription),
                new SqlParameter("@BuildingID", entity.BuildingID),
                new SqlParameter("@FloorNumber", entity.FloorNumber),
                new SqlParameter("@TotalRentArea", entity.TotalRentArea),
                new SqlParameter("@TotalRentUnit", entity.TotalRentUnit),
                new SqlParameter("@ContractRentArea", entity.ContractRentArea),
                new SqlParameter("@ContractRentUnit", entity.ContractRentUnit),
                new SqlParameter("@InDate", entity.InDate),
                new SqlParameter("@InUser", entity.InUser),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser)
            };
            entity.FloorID = SqlHelper.ExecuteScalar(strCmd, paramList);
            //图片上传
            new ProjectFloorPictureProcess().ProcessPicture(entity, _worker);
        }

        /// <summary>
        /// 更新楼层
        /// </summary>
        /// <param name="entity"></param>
        protected override void UpdateData(ProjectFloorEntity entity)
        {
            //获取楼层id
            entity.FloorID = ProjectUtil.GetFloorId(entity.ProjectName, entity.BuildingName, entity.FloorName);
            string strCmd = string.Format(@"update [dbo].[Project_Floor]
                                           set [FloorName]=@FloorName
                                          ,[FloorDescription]=@FloorDescription
                                          ,[FloorNumber]=@FloorNumber
                                          ,[TotalRentArea]=@TotalRentArea
                                          ,[TotalRentUnit]=@TotalRentUnit
                                          ,[ContractRentArea]=@ContractRentArea
                                          ,[ContractRentUnit]=@ContractRentUnit
                                          ,[EditDate]=@EditDate
                                          ,[EditUser]=@EditUser
                                          where [FloorID]=@FloorID");
            var paramList = new SqlParameter[] 
            { 
                 new SqlParameter("@FloorName", entity.FloorName),
                new SqlParameter("@FloorDescription", entity.FloorDescription),
                new SqlParameter("@FloorNumber", entity.FloorNumber),
                 new SqlParameter("@TotalRentArea", entity.TotalRentArea),
                new SqlParameter("@TotalRentUnit", entity.TotalRentUnit),
                new SqlParameter("@ContractRentArea", entity.ContractRentArea),
                new SqlParameter("@ContractRentUnit", entity.ContractRentUnit),
                new SqlParameter("@EditDate", entity.EditDate),
                new SqlParameter("@EditUser", entity.EditUser),
                new SqlParameter("@FloorID",entity.FloorID)
            };
            SqlHelper.ExecuteNonQuery(strCmd, paramList);
            //图片上传
            new ProjectFloorPictureProcess().ProcessPicture(entity, _worker);
        }
    }
}
