using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Project;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project
{
    /// <summary>
    /// 项目管理服务
    /// </summary>
    public class ProjectMgmtService : NHHService<NHHEntities>, IProjectMgmtService
    {
        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Create(ProjectInfoModel model)
        {
            Entities.Project entity = new Entities.Project();
            entity.ProjectName = model.ProjectName;
            entity.ProvinceID = model.ProvinceID;
            entity.RegionID = model.RegionID;
            entity.CityID = model.CityID;
            entity.AddressLine = model.AddressLine;
            entity.Zipcode = model.ZipCode;
            entity.AddressInfo = model.AddressInfo;
            entity.Longitude = model.Longitude;
            entity.Latitude = model.Latitude;
            entity.PlanSummary = model.PlanSummary;
            entity.PlanHighlight = model.PlanHighlight;
            entity.ManageCompanyID = model.ManageCompanyId;

            entity.Stage = 1;
            entity.Status = 1;
            entity.InDate = DateTime.Now;
            entity.InUser = model.InUser;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.InUser;
            var bll = CreateBizObject<Entities.Project>();
            bll.Insert(entity);
            this.SaveChanges();


            //业主公司
            if (!string.IsNullOrEmpty(model.OwnerCompanyId) && model.OwnerCompanyId.Length > 0)
            {
                var ownerBll = CreateBizObject<Entities.Project_Owner>();
                string[] arrOwner = model.OwnerCompanyId.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string owner in arrOwner)
                {
                    //业主公司ID
                    var ownerId = Convert.ToInt32(owner);
                    //业主公司名称
                    var ownerName = (from c in Context.Company
                                     where c.CompanyID == ownerId
                                     select c.CompanyName).FirstOrDefault();
                    var ownerEntity = new Entities.Project_Owner
                    {
                        CompanyID = ownerId,
                        CompanyName = ownerName,
                        ProjectID = entity.ProjectID,
                        Status = 1,
                        InDate = DateTime.Now,
                        InUser = model.InUser,
                        EditDate = DateTime.Now,
                        EditUser = model.InUser,
                    };
                    ownerBll.Insert(ownerEntity);
                    this.SaveChanges();
                }
            }

            return entity.ProjectID;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        public void Edit(ProjectInfoModel model)
        {
            var bll = CreateBizObject<Entities.Project>();
            Entities.Project entity = bll.GetBySysNo(model.ProjectID);
            if (entity == null)
            {
                return;
            }
            entity.ProjectName = model.ProjectName;
            entity.ProvinceID = model.ProvinceID;
            entity.RegionID = model.RegionID;
            entity.CityID = model.CityID;
            entity.AddressLine = model.AddressLine;
            entity.Zipcode = model.ZipCode;
            entity.AddressInfo = model.AddressInfo;
            entity.Longitude = model.Longitude;
            entity.Latitude = model.Latitude;
            entity.PlanSummary = model.PlanSummary;
            entity.PlanHighlight = model.PlanHighlight;

            entity.ManageCompanyID = model.ManageCompanyId;


            entity.EditDate = DateTime.Now;
            entity.EditUser = model.EditUser;
            bll.Update(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 保存项目详细信息
        /// </summary>
        /// <param name="model"></param>
        public void SaveDetailInfo(ProjectInfoModel model)
        {
            var bll = CreateBizObject<Entities.Project>();
            var entity = bll.GetBySysNo(model.ProjectID);
            if (entity == null)
            {
                return;
            }

            entity.GroundArea = model.GroundArea;
            entity.UndergroundArea = model.UndergroundArea;
            entity.TotalArea = model.GroundArea + model.UndergroundArea;
            entity.Stage = model.Stage;
            entity.GrandOpeningDate = Convert.ToDateTime(model.GrandOpeningDate);
            entity.AdPointNum = model.AdPointNum;
            entity.ParkingLotNum = model.ParkingLotNum;
            entity.MultiBizPositionNum = model.MultiBizPositionNum;
            entity.RenderingFileName = model.RenderingFileName;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.EditUser;

            bll.Update(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 添加楼宇
        /// </summary>
        /// <param name="model"></param>
        public int AddBuilding(BuildingDetailInfoModel model)
        {
            Project_Building entity = new Project_Building();
            entity.ProjectID = model.ProjectID;
            entity.BuildingCode = model.BuildingCode;
            entity.BuildingName = model.BuildingName;
            entity.GroundFloorNum = model.GroundFloorNum;
            entity.UndergroundFloorNum = model.UndergroundFloorNum;
            entity.BuildingGroundArea = model.BuildingGroundArea;
            entity.BuildingUndergroundArea = model.BuildingUndergroundArea;
            entity.TotalConstructionArea = model.BuildingGroundArea + model.BuildingUndergroundArea;
            entity.PlanSummary = model.PlanSummary;
            entity.PlanHighlight = model.PlanHighlight;
            entity.ContractStore = model.ContractStore;
            entity.RenderingFileName = model.RenderingFileName;
            entity.Status = 1;
            entity.InDate = DateTime.Now;
            entity.InUser = model.InUser;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.EditUser;

            var bll = CreateBizObject<Project_Building>();
            bll.Insert(entity);
            this.SaveChanges();

            int num = model.UndergroundFloorNum;
            var floorBll = CreateBizObject<Project_Floor>();

            while (num > 0)
            {
                var floorEntity = new Project_Floor
                {
                    ProjectID = model.ProjectID,
                    BuildingID = entity.BuildingID,
                    FloorNumber = num * -1,
                    FloorName = string.Format("B{0}", num),
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = model.InUser,
                    EditDate = DateTime.Now,
                    EditUser = model.EditUser,
                };
                floorBll.Insert(floorEntity);
                this.SaveChanges();
                num -= 1;
            }
            num = model.GroundFloorNum;
            for (var n = 1; n <= num; n++)
            {
                var floorEntity = new Project_Floor
                {
                    ProjectID = model.ProjectID,
                    BuildingID = entity.BuildingID,
                    FloorNumber = n,
                    FloorName = string.Format("{0}F", n),
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = model.InUser,
                    EditDate = DateTime.Now,
                    EditUser = model.EditUser,
                };
                floorBll.Insert(floorEntity);
                this.SaveChanges();
            }
            return entity.BuildingID;
        }

        /// <summary>
        /// 更新楼宇
        /// </summary>
        /// <param name="model"></param>
        public void UpdateBuilding(BuildingDetailInfoModel model)
        {
            var bll = CreateBizObject<Project_Building>();
            Project_Building entity = bll.GetBySysNo(model.BuildingID);
            if (entity != null)
            {
                entity.BuildingGroundArea = model.BuildingGroundArea;
                entity.BuildingUndergroundArea = model.BuildingUndergroundArea;
                entity.BuildingCode = model.BuildingCode;
                entity.BuildingName = model.BuildingName;
                entity.TotalConstructionArea = model.BuildingGroundArea + model.BuildingUndergroundArea;
                entity.PlanSummary = model.PlanSummary;
                entity.PlanHighlight = model.PlanHighlight;
                entity.ContractStore = model.ContractStore;
                entity.RenderingFileName = model.RenderingFileName;
                entity.EditDate = DateTime.Now;
                entity.EditUser = model.EditUser;

                bll.Update(entity);
                this.SaveChanges();
            }
        }

        /// <summary>
        /// 添加楼层
        /// </summary>
        /// <param name="model"></param>
        public void AddFloor(FloorDetailModel model)
        {
            Project_Floor entity = new Project_Floor();
            entity.BuildingID = model.BuildingID;
            entity.ProjectID = model.ProjectID;
            entity.FloorNumber = model.FloorNumber;
            entity.FloorDescription = model.FloorDescription;
            entity.FloorMapFileName = model.FloorMapFileName;
            entity.Status = 1;
            entity.InDate = DateTime.Now;
            entity.InUser = model.InUser;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.EditUser;
            var bll = CreateBizObject<Project_Floor>();
            bll.Insert(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 更新楼层
        /// </summary>
        /// <param name="model"></param>
        public void UpdateFloor(FloorDetailModel model)
        {
            var bll = CreateBizObject<Project_Floor>();
            var entity = bll.GetBySysNo(model.FloorID);
            entity.FloorName = model.FloorName;
            entity.FloorDescription = model.FloorDescription;
            entity.FloorMapFileName = model.FloorMapFileName;
            entity.EditDate = DateTime.Now;
            entity.EditUser = model.EditUser;
            bll.Update(entity);
            this.SaveChanges();
        }

        /// <summary>
        /// 编辑项目基本信息
        /// </summary>
        /// <param name="model"></param>
        public void EditProjectInfoBasic(ProjectBasicInfoModel model)
        {
            var bll = CreateBizObject<Entities.Project>();
            Entities.Project entity = bll.GetBySysNo(model.ProjectID);
            if (entity == null)
            {
                return;
            }
            entity.ProjectName = model.ProjectName;
            entity.ProvinceID = model.ProvinceID;
            entity.RegionID = model.RegionID;
            entity.CityID = model.CityID;
            entity.AddressLine = model.AddressLine;
            entity.Zipcode = model.ZipCode;
            entity.AddressInfo = model.AddressInfo;
            entity.Longitude = model.Longitude;
            entity.Latitude = model.Latitude;
            entity.PlanSummary = model.PlanSummary;
            entity.PlanHighlight = model.PlanHighlight;
            entity.ManageCompanyID = model.ManageCompanyId;
            entity.ProjectSummary = model.ProjectSummary;

            entity.EditDate = DateTime.Now;
            entity.EditUser = model.EditUser;
            bll.Update(entity);
            this.SaveChanges();
        }
    }
}
