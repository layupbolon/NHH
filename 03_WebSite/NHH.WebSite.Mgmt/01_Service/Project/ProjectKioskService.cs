using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Project.ProjectKiosk;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project
{
    public class ProjectKioskService : ServiceBase<NHHEntities>, IProjectKioskService
    {
        /// <summary>
        /// 多经点位查询页面
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public KioskListModel GetKioskList(int projectId, int? buildingId, int? floorId, int? kioskNumber)
        {
            var model = new KioskListModel();
            var project = CreateCacheBizObject<NHH.Entities.Project>().GetBySysNo(projectId);
            if (project != null)
            {
                model.ProjectInfo.Id = projectId;
                model.ProjectInfo.Name = project.ProjectName;
            }
            // Expression<Func<Project_Building, bool>> buildingWhere = building => building.ProjectID == projectId;
            Expression<Func<View_Project_Floor, bool>> floorWhere = floor => floor.ProjectID == projectId;
            var floorList = CreateBizObject<View_Project_Floor>().GetAllList(floorWhere);
            floorList.ForEach(item =>
            {
                model.FloorList.Add(
                    new FloorCommonInfo()
                    {
                        BuildingId = item.BuildingID,
                        BuildingName = item.BuildingName,
                        FloorId = item.FloorID
                        // FloorNumber = item.FloorNumber
                    }
                    );
            });
            //  Expression<Func<Project_Kiosk, bool>> KioskWhere = floor => floor.ProjectID == projectId;
            var KioskWhere = string.Format(" ProjectID={0} And FloorID={1}", projectId, floorId);
            var KioskList = CreateBizObject<Project_Kiosk>().GetAllList(KioskWhere);
            if (KioskList != null)
            {
                KioskList.ForEach(item =>
                {
                    model.KioskList.Add(new KioskDetailModel()
                    {
                        KioskId = item.KioskID,
                        //  BuildingID = item.BuildingID ?? 1,
                        //   FloorID = item.FloorID ?? 1,
                        KioskNumber = item.KioskNumber,
                        Location = item.Location,
                        Area = item.Area ?? 0,
                        FloorMapLocation = item.FloorMapLocation
                    });
                });
            }
            return model;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ShowModel"></param>
        public bool AddKiosk(KioskDetailModel detailModel)
        {
            var instance = CreateBizObject<Project_Kiosk>();
            Project_Kiosk dept = new Project_Kiosk()
            {
                //CompanyID = DetailModel.CompanyID,
                ////DepartmentID = DetailModel.DepartmentID,
                //DepartmentName = DetailModel.DepartmentName,
                //InDate = DetailModel.InDate,
                //InUser = DetailModel.InUser,
                //EditDate = DetailModel.EditDate,
                //EditUser = DetailModel.EditUser,
                //Status = 1//1：可用状态   -1：不可用
            };
            instance.Insert(dept);
            instance.SaveChanges();
            return true;

        }



        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        void UpdateKiosk(KioskDetailModel detailModel)
        {
            var conInstance = CreateBizObject<Project_Kiosk>();
            var entity = conInstance.GetBySysNo(detailModel.KioskId);
            if (entity != null)
            {
                //entity.DepartmentName = DetailModel.DepartmentName;
                //entity.CompanyID = DetailModel.CompanyID;
                //entity.EditDate = DateTime.Now;
                //entity.EditUser = 1;

                //conInstance.Update(entity);
                //conInstance.SaveChanges();
            }
        }
    }
}
