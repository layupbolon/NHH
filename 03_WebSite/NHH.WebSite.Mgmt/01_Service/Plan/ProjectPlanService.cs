using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Plan;
using NHH.Service.Plan.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Plan
{
    /// <summary>
    /// 招商计划服务
    /// </summary>
    public class ProjectPlanService : NHHService<NHHEntities>, IProjectPlanService
    {
        /// <summary>
        /// 获取招商计划列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ProjectPlanListModel GetProjectPlanList(ProjectPlanListQueryInfo queryInfo)
        {
            var model = new ProjectPlanListModel();
            model.QueryInfo = queryInfo;
            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1;

            model.ProjectPlanList = new List<ProjectPlanInfo>();
            if (!queryInfo.ProjectId.HasValue)
                return model;

            var query = from unit in Context.View_Project_Unit
                        join type in Context.View_UnitType on unit.UnitType equals type.UnitTypeID
                        join status in Context.View_UnitStatus on unit.UnitStatus equals status.UnitStatusID
                        join plan in Context.Project_UnitPlan on unit.UnitID equals plan.UnitID into tempPlan
                        from unitPlan in tempPlan.DefaultIfEmpty()
                        join bizType in Context.BizType on unit.BizTypeID equals bizType.BizTypeID into tempBizType
                        from unitBizType in tempBizType.DefaultIfEmpty()
                        where unit.ProjectID == queryInfo.ProjectId.Value && unit.Status == 1
                        select new
                        {
                            unit.UnitID,
                            unit.UnitNumber,
                            unit.UnitAera,
                            unit.UnitMapFileName,
                            unit.FloorMapLocation,
                            unit.FloorID,
                            unit.FloorNumber,
                            unit.FloorName,
                            unit.BuildingID,
                            unit.BuildingName,
                            unit.UnitType,
                            type.UnitTypeName,
                            status.UnitStatusID,
                            status.UnitStatusName,
                            unitPlan.StandardRent,
                            unitPlan.StandardMgmtFee,
                            unit.BizTypeID,
                            unitBizType.BizTypeName,
                        };
            #region 查询条件
            if (queryInfo.BuildingId.HasValue)
            {
                query = query.Where(item => item.BuildingID == queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                query = query.Where(item => item.FloorID == queryInfo.FloorId.Value);
            }
            //批次
            if (queryInfo.BatchId.HasValue && queryInfo.BatchId.Value > 0)
            {
                query = query.Where(item => (from pbu in Context.Project_BatchUnit
                                             where pbu.Status == 1 && pbu.BatchID == queryInfo.BatchId.Value && pbu.UnitID == item.UnitID
                                             select pbu.BatchUnitID).Any());
            }
            //类型
            if (queryInfo.UnitType.HasValue)
            {
                query = query.Where(item => item.UnitType == queryInfo.UnitType.Value);
            }
            //业态
            if (queryInfo.BizType.HasValue)
            {
                query = query.Where(item => item.BizTypeID == queryInfo.BizType.Value);
            }
            if (queryInfo.UnitStatus.HasValue)
            {
                query = query.Where(item => item.UnitStatusID == queryInfo.UnitStatus.Value);
            }
            #endregion

            model.PagingInfo.TotalCount = query.Count();

            var entityList = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();

            if (entityList == null || entityList.Count == 0)
                return model;

            entityList.ForEach(entity =>
            {
                var info = new ProjectPlanInfo
                {
                    UnitId = entity.UnitID,
                    UnitNumber = string.IsNullOrEmpty(entity.UnitNumber) ? entity.UnitID.ToString() : entity.UnitNumber,
                    UnitArea = entity.UnitAera,
                    UnitMapFileName = entity.UnitMapFileName,
                    StandardRent = entity.StandardRent.HasValue ? entity.StandardRent.Value : 0,
                    StandardMgmtFee = entity.StandardMgmtFee.HasValue ? entity.StandardMgmtFee.Value : 0,
                };
                info.BizType = entity.BizTypeName;
                info.UnitType = entity.UnitTypeName;
                info.UnitStatus = entity.UnitStatusName;
                info.BuildingName = entity.BuildingName;
                info.FloorName = entity.FloorName;
                model.ProjectPlanList.Add(info);
            });

            return model;
        }

        /// <summary>
        /// 获取筹划铺位信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ProjectPlanUnitInfo GetUnitInfo(int unitId)
        {
            var info = new ProjectPlanUnitInfo();
            #region 查询信息
            var query = from unit in Context.Project_Unit
                        join type in Context.View_UnitType on unit.UnitType equals type.UnitTypeID
                        where unit.UnitID == unitId
                        select new
                        {
                            unit.ProjectID,
                            unit.UnitID,
                            unit.UnitAera,
                            unit.UnitNumber,
                            unit.FloorID,
                            type.UnitTypeID,
                            type.UnitTypeName,
                            unit.BizTypeID,
                            unit.BizType.BizTypeName,
                        };
            #endregion
            var entity = query.FirstOrDefault();
            if (entity == null)
                return info;

            info.UnitId = entity.UnitID;
            info.BizType = new BizTypeInfo();
            info.BizType.Id = (entity.BizTypeID.HasValue ? entity.BizTypeID.Value : 0);
            info.BizType.Name = entity.BizTypeName == null ? "" : entity.BizTypeName;
            info.UnitArea = entity.UnitAera;
            info.UnitNumber = entity.UnitNumber;
            info.UnitType = new ProjectUnitTypeInfo();
            info.UnitType.Id = entity.UnitTypeID;
            info.UnitType.Name = entity.UnitTypeName;

            #region  楼层信息

            var query1 = from f in Context.Project_Floor
                         join b in Context.Project_Building on f.BuildingID equals b.BuildingID
                         join p in Context.Project on f.ProjectID equals p.ProjectID
                         where f.FloorID == entity.FloorID
                         select new
                         {
                             f.FloorID,
                             f.FloorNumber,
                             f.FloorName,
                             b.BuildingID,
                             b.BuildingName,
                             p.ProjectID,
                             p.ProjectName,
                         };

            var floorEntity = query1.FirstOrDefault();
            if (floorEntity != null)
            {
                info.FloorName = floorEntity.FloorName;
                info.BuildingName = floorEntity.BuildingName;
                info.ProjectId = floorEntity.ProjectID;
                info.ProjectName = floorEntity.ProjectName;
            }
            #endregion

            return info;
        }

        /// <summary>
        /// 获取商铺租赁信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ProjectUnitPlanModel GetUnitPlan(int unitId)
        {
            var model = new ProjectUnitPlanModel();
            model.UnitId = unitId;

            var query = from plan in Context.Project_UnitPlan
                        join leasing in Context.Project_UnitLeasing on plan.UnitID equals leasing.UnitID
                        join type in Context.View_UnitType on plan.UnitType equals type.UnitTypeID
                        join biz in Context.BizType on plan.BizTypeID equals biz.BizTypeID
                        where plan.UnitID == unitId
                        select new
                        {
                            plan.BizTypeID,
                            plan.IsBenchmarking,
                            plan.StandardRent,
                            UnitTypeID = plan.UnitType,
                            leasing.LeasingDepartmentID,
                            leasing.LeasingPersonID,
                            type.UnitTypeName,
                            biz.BizTypeName,
                        };

            var entity = query.FirstOrDefault();

            if (entity == null)
            {
                return model;
            }
            model.StandardRent = entity.StandardRent.HasValue ? entity.StandardRent.Value : 0;
            model.IsBenchmarking = entity.IsBenchmarking.HasValue ? entity.IsBenchmarking.Value == 1 : false;
            model.UnitTypeId = entity.UnitTypeID.HasValue ? entity.UnitTypeID.Value : 0;
            model.UnitTypeName = entity.UnitTypeName;
            model.BizTypeId = entity.BizTypeID.HasValue ? entity.BizTypeID.Value : 0;
            model.BizTypeName = entity.BizTypeName;

            #region 部门公司
            if (entity.LeasingDepartmentID.HasValue)
            {
                var query1 = from d in Context.Department
                             join c in Context.Company on d.CompanyID equals c.CompanyID
                             where d.DepartmentID == entity.LeasingDepartmentID.Value
                             select new
                             {
                                 d.DepartmentID,
                                 d.DepartmentName,
                                 c.CompanyID,
                                 c.CompanyName,
                                 c.BriefName,
                             };
                var department = query1.FirstOrDefault();
                if (department != null)
                {
                    model.CompanyId = department.CompanyID;
                    model.CompanyName = department.BriefName;
                    model.DepartmentId = department.DepartmentID;
                    model.DepartmentName = department.DepartmentName;
                }
            }
            #endregion
            return model;
        }

        /// <summary>
        /// 获取商铺租赁信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ProjectUnitInfoModel GetRentInfo(int unitId)
        {
            var model = new ProjectUnitInfoModel();
            model.UnitId = unitId;

            Expression<Func<Project_UnitPlan, bool>> where = item => item.UnitID == unitId;
            var entity = CreateBizObject<Project_UnitPlan>().TopOne(where);

            if (entity == null)
            {
                model.RentFreeLength = 3;
                model.RentLengthUpper = 36;

                model.DecorationLength = 1;
                model.ParkingLotNum = 0;
                model.AdPointNum = 0;
                return model;
            }


            model.UnitPlanId = entity.UnitPlanID;
            model.UnitId = entity.UnitID;
            model.BizType = new Models.Common.BizTypeInfo();
            model.BizType.Id = entity.BizTypeID.HasValue ? entity.BizTypeID.Value : 0;
            model.BizCategory = new Models.Common.BizCategoryInfo();
            model.BizCategory.Id = entity.BizCategoryID.HasValue ? entity.BizCategoryID.Value : 0;
            model.UnitType = new Models.Common.ProjectUnitTypeInfo();
            model.UnitType.Id = entity.UnitType.HasValue ? entity.UnitType.Value : 0;

            model.StandardRent = entity.StandardRent.HasValue ? entity.StandardRent.Value : 0;
            model.RecommendedRent = entity.RecommendedRent.HasValue ? entity.RecommendedRent.Value : 0;
            model.QuotationRent = entity.QuotationRent.HasValue ? entity.QuotationRent.Value : 0;
            model.StandardMgmtFee = entity.StandardMgmtFee.HasValue ? entity.StandardMgmtFee.Value : 0;
            model.RentLengthBottom = entity.RentLengthBottom.HasValue ? entity.RentLengthBottom.Value : 0;
            model.RentLengthUpper = entity.RentLengthUpper.HasValue ? entity.RentLengthUpper.Value : 0;

            model.RentFreeLength = entity.RentFreeLength.HasValue ? entity.RentFreeLength.Value : 0;
            model.DepositMonthly = entity.DepositMonthly.HasValue ? entity.DepositMonthly.Value : 0;
            model.PaymentPeriod = entity.PaymentPeriod.HasValue ? entity.PaymentPeriod.Value : 0;
            model.IncreaseType = entity.IncreaseType.HasValue ? entity.IncreaseType.Value : 0;
            model.IncreaseAmountType = entity.IncreaseAmountType.HasValue ? entity.IncreaseAmountType.Value : 0;
            model.IncreaseAmount = entity.IncreaseAmount.HasValue ? entity.IncreaseAmount.Value : 0;
            model.IncreaseStartYears = entity.IncreaseStartYears.HasValue ? entity.IncreaseStartYears.Value : 0;
            model.IncreaseStepYears = entity.IncreaseStepYears.HasValue ? entity.IncreaseStepYears.Value : 0;
            model.BidBond = entity.BidBond.HasValue ? entity.BidBond.Value : 0;
            model.ManageBond = entity.ManageBond.HasValue ? entity.ManageBond.Value : 0;

            model.DecorationLength = entity.DecorationLength.HasValue ? entity.DecorationLength.Value : 0;
            model.DecorationMgmtFee = entity.DecorationMgmtFee.HasValue ? entity.DecorationMgmtFee.Value : 0;
            model.DecorationBond = entity.DecorationBond.HasValue ? entity.DecorationBond.Value : 0;

            model.QualityBond = entity.QualityBond.HasValue ? entity.QualityBond.Value : 0;
            model.ParkingLotNum = entity.ParkingLotNum.HasValue ? entity.ParkingLotNum.Value : 0;
            model.AdPointNum = entity.AdPointNum.HasValue ? entity.AdPointNum.Value : 0;
            model.Condition = entity.Condition.HasValue ? entity.Condition.Value.ToString() : "0";

            return model;
        }

        /// <summary>
        /// 获取商铺招商信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ProjectUnitLeasingModel GetUnitLeasing(int unitId)
        {
            var model = new ProjectUnitLeasingModel();
            model.UnitId = unitId;

            Expression<Func<Project_UnitLeasing, bool>> where = item => item.UnitID == unitId;
            var entity = CreateBizObject<Project_UnitLeasing>().TopOne(where);
            if (entity == null)
            {
                return model;
            }

            model.UnitId = entity.UnitID;
            model.UnitLeasingID = entity.UnitLeasingID;

            model.LeasingDepartmentID = entity.LeasingDepartmentID.HasValue ? entity.LeasingDepartmentID.Value : 0;
            model.LeasingPersonID = entity.LeasingPersonID.HasValue ? entity.LeasingPersonID.Value : 0;
            model.LeasingFinishDate = entity.LeasingFinishDate.HasValue ? entity.LeasingFinishDate.Value : DateTime.Now;
            model.FireProtectionAuth = entity.FireProtectionAuth.HasValue ? entity.FireProtectionAuth.Value : 0;
            model.FireProtectionAuthDate = entity.FireProtectionAuthDate.HasValue ? entity.FireProtectionAuthDate.Value : DateTime.Now;
            model.MeasureReviewDate = entity.MeasureReviewDate.HasValue ? entity.MeasureReviewDate.Value : DateTime.Now;
            //model.DesignDate = entity.DesignDate.Value;
            model.AccessDate = entity.AccessDate.HasValue ? entity.AccessDate.Value : DateTime.Now;
            //model.DecorationStartDate = entity.DecorationStartDate.Value;
            model.DecorationEndDate = entity.DecorationEndDate.HasValue ? entity.DecorationEndDate.Value : DateTime.Now;

            //招商负责部门
            if (model.LeasingDepartmentID > 0)
            {
                var department = CreateBizObject<Department>().GetBySysNo(model.LeasingDepartmentID);
                if (department != null)
                {
                    model.LeasingDepartment = string.Format("{0} {1}", department.Company.BriefName, department.DepartmentName);
                }
            }
            //招商负责人
            if (model.LeasingPersonID > 0)
            {
                var person = CreateBizObject<Employee>().GetBySysNo(model.LeasingPersonID);
                if (person != null)
                {
                    model.LeasingPerson = person.EmployeeName;
                }
            }

            return model;
        }

        /// <summary>
        /// 获取商铺交付标准
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ProjectUnitSpecModel GetUnitSpec(int unitId)
        {
            var model = new ProjectUnitSpecModel();
            model.UnitId = unitId;

            Expression<Func<Project_UnitSpec, bool>> where = item => item.UnitID == unitId && item.SpecType == 1;
            var entity = CreateBizObject<Project_UnitSpec>().TopOne(where);
            if (entity != null)
            {
                model.UnitId = entity.UnitID;
                model.UnitSpecID = entity.UnitSpecID;
                model.Floor = entity.Floor;
                model.Ceiling = entity.Ceiling;
                model.Wall = entity.Wall;
                model.Pillar = entity.Pillar;
                model.WaterSupply = entity.WaterSupply;
                model.WaterDrain = entity.WaterDrain;
                model.Door = entity.Door;
                model.Logo = entity.Logo;
                model.ElectricityUsage = entity.ElectricityUsage;
                model.FireProtection = entity.FireProtection;
                model.Broadcasting = entity.Broadcasting;
                model.AirCondition = entity.AirCondition;
                model.Smoke = entity.Smoke;
                model.Security = entity.Security;
                model.Wiring = entity.Wiring;
                model.Water = entity.Water;
                model.Gas = entity.Gas;
                model.FloorBearing = entity.FloorBearing;
            }

            //交房标准
            Expression<Func<Project_UnitPlan, bool>> wherePlan = plan => plan.UnitID == unitId;
            var planEntity = CreateBizObject<Project_UnitPlan>().TopOne(wherePlan);
            if (planEntity != null)
            {
                model.Condition = planEntity.Condition.HasValue ? planEntity.Condition.Value : 0;
                switch (model.Condition)
                {
                    case 1:
                        model.ConditionString = "精装";
                        break;
                    case 2:
                        model.ConditionString = "统装";
                        break;
                    case 3:
                        model.ConditionString = "毛坯";
                        break;
                    default:
                        model.ConditionString = "";
                        break;
                }
            }

            return model;
        }

        /// <summary>
        /// 获取商铺的商户责任
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public ProjectStoreSpecModel GetStoreSpec(int unitId)
        {
            var model = new ProjectStoreSpecModel();
            model.UnitId = unitId;

            Expression<Func<Project_UnitSpec, bool>> where = item => item.UnitID == unitId && item.SpecType == 2;
            var entity = CreateBizObject<Project_UnitSpec>().TopOne(where);
            if (entity != null)
            {
                model.UnitId = entity.UnitID;
                model.UnitSpecID = entity.UnitSpecID;
                model.Floor = entity.Floor;
                model.Ceiling = entity.Ceiling;
                model.Wall = entity.Wall;
                model.Pillar = entity.Pillar;
                model.WaterSupply = entity.WaterSupply;
                model.WaterDrain = entity.WaterDrain;
                model.Door = entity.Door;
                model.Logo = entity.Logo;
                model.ElectricityUsage = entity.ElectricityUsage;
                model.FireProtection = entity.FireProtection;
                model.Broadcasting = entity.Broadcasting;
                model.AirCondition = entity.AirCondition;
                model.Smoke = entity.Smoke;
                model.Security = entity.Security;
                model.Wiring = entity.Wiring;
                model.Water = entity.Water;
                model.Gas = entity.Gas;
                model.FloorBearing = entity.FloorBearing;
            }

            return model;
        }

        /// <summary>
        /// 保存商铺租赁信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveUnitPlan(ProjectUnitPlanModel model)
        {
            #region Project_Unit
            var unitBll = CreateBizObject<Project_Unit>();
            Expression<Func<Project_Unit, bool>> where0 = unit => unit.UnitID == model.UnitId;
            var unitEntity = unitBll.TopOne(where0);
            unitEntity.UnitStatus = 2;
            unitEntity.Tag = model.IsBenchmarking ? "标杆品牌" : "";
            unitEntity.UnitType = model.UnitTypeId;
            unitEntity.BizTypeID = model.BizTypeId;
            unitEntity.EditDate = DateTime.Now;
            unitEntity.EditUser = model.UserId;
            unitBll.Update(unitEntity);
            this.SaveChanges();
            #endregion

            #region Project_UnitPlan
            var planBll = CreateBizObject<Project_UnitPlan>();
            Expression<Func<Project_UnitPlan, bool>> where1 = plan => plan.UnitID == model.UnitId;            
            var unitPlan = planBll.TopOne(where1);
            var isExists = planBll.Exists(where1);
            if (!isExists)
            {
                unitPlan = new Project_UnitPlan
                {
                    UnitID = model.UnitId,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = model.UserId,
                };
            }
            unitPlan.BizTypeID = model.BizTypeId;
            unitPlan.UnitType = model.UnitTypeId;
            unitPlan.IsBenchmarking = model.IsBenchmarking ? 1 : 0;
            unitPlan.StandardRent = model.StandardRent;
            unitPlan.EditDate = DateTime.Now;
            unitPlan.EditUser = model.UserId;

            if (!isExists)
            {
                planBll.Insert(unitPlan);
            }
            else
            {
                planBll.Update(unitPlan);
            }
            this.SaveChanges();
            #endregion

            #region Project_UnitLeasing
            var leasingBll = CreateBizObject<Project_UnitLeasing>();
            Expression<Func<Project_UnitLeasing, bool>> where2 = leasing => leasing.UnitID == model.UnitId;
            var unitLeasing = leasingBll.TopOne(where2);
            isExists = leasingBll.Exists(where2);
            if (!isExists)
            {
                unitLeasing = new Project_UnitLeasing
                {
                    UnitID = model.UnitId,
                    InDate = DateTime.Now,
                    InUser = model.UserId,
                    Status = 1,
                };
            }
            unitLeasing.LeasingDepartmentID = model.DepartmentId;
            unitLeasing.EditDate = DateTime.Now;
            unitLeasing.EditUser = model.UserId;

            if (!isExists)
            {
                leasingBll.Insert(unitLeasing);
            }
            else
            {
                leasingBll.Update(unitLeasing);
            }
            this.SaveChanges();

            #endregion

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveRentInfo(ProjectUnitInfoModel model)
        {
            Expression<Func<Project_UnitPlan, bool>> where = item => item.UnitID == model.UnitId;

            var bll = CreateBizObject<Project_UnitPlan>();

            var entity = bll.TopOne(where);
            var isExists = bll.Exists(where);

            if (!isExists)
            {
                entity = new Project_UnitPlan();
                entity.Status = 1;
                entity.UnitID = model.UnitId;
                entity.InDate = DateTime.Now;
                entity.InUser = model.UserId;
                entity.Condition = 1;
            }

            entity.RecommendedRent = model.RecommendedRent;
            entity.QuotationRent = model.QuotationRent;
            entity.StandardMgmtFee = model.StandardMgmtFee;
            entity.RentLengthBottom = 1;
            entity.RentLengthUpper = model.RentLengthUpper;
            entity.DepositMonthly = model.DepositMonthly;
            entity.PaymentPeriod = model.PaymentPeriod;
            entity.IncreaseType = model.IncreaseType;
            entity.IncreaseStartYears = model.IncreaseStartYears;
            entity.IncreaseStepYears = model.IncreaseStepYears;
            entity.IncreaseAmount = model.IncreaseAmount;
            entity.IncreaseAmountType = model.IncreaseAmountType;
            entity.BidBond = model.BidBond;
            entity.ManageBond = model.ManageBond;
            entity.RentFreeLength = model.RentFreeLength;
            entity.DecorationLength = model.DecorationLength;
            entity.DecorationMgmtFee = model.DecorationMgmtFee;
            entity.DecorationBond = model.DecorationBond;
            entity.QualityBond = model.QualityBond;
            entity.ParkingLotNum = model.ParkingLotNum;
            entity.AdPointNum = model.AdPointNum;

            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserId;

            if (!isExists)
            {
                bll.Insert(entity);
            }
            else
            {
                bll.Update(entity);
            }
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 保存商铺招商信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveUnitLeasing(ProjectUnitLeasingModel model)
        {
            Expression<Func<Project_UnitLeasing, bool>> where = item => item.UnitID == model.UnitId;
            var bll = CreateBizObject<Project_UnitLeasing>();
            var entity = bll.TopOne(where);
            var isExists = bll.Exists(where);

            if (!isExists)
            {
                entity = new Project_UnitLeasing();
                entity.UnitID = model.UnitId;
                entity.Status = 1;
                entity.InDate = DateTime.Now;
                entity.InUser = model.UserId;
            }

            //entity.LeasingDepartment = model.LeasingDepartment;
            //entity.LeasingRoleID = model.LeasingRoleID;
            entity.LeasingPersonID = model.LeasingPersonID;
            entity.LeasingFinishDate = model.LeasingFinishDate;
            entity.FireProtectionAuth = model.FireProtectionAuth;
            entity.FireProtectionAuthDate = model.FireProtectionAuthDate;
            entity.MeasureReviewDate = model.MeasureReviewDate;
            //entity.DesignDate = model.DesignDate;
            entity.AccessDate = model.AccessDate;
            //entity.DecorationStartDate = model.DecorationStartDate;
            entity.DecorationEndDate = model.DecorationEndDate;

            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserId;

            if (!isExists)
            {
                bll.Insert(entity);
            }
            else
            {
                bll.Update(entity);
            }
            this.SaveChanges();
            return true;
        }

        /// <summary>
        /// 保存商铺交付准标
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveUnitSpec(ProjectUnitSpecModel model)
        {
            Expression<Func<Project_UnitSpec, bool>> where = item => item.UnitID == model.UnitId && item.SpecType == 1;
            var bll = CreateBizObject<Project_UnitSpec>();
            var entity = bll.TopOne(where);
            var isExists = bll.Exists(where);

            if (!isExists)
            {
                entity = new Project_UnitSpec();
                entity.UnitID = model.UnitId;
                entity.Status = 1;
                entity.InDate = DateTime.Now;
                entity.InUser = model.UserId;
                entity.SpecType = 1;
            }

            entity.Floor = model.Floor;
            entity.Ceiling = model.Ceiling;
            entity.Wall = model.Wall;
            entity.Pillar = model.Pillar;
            entity.WaterSupply = model.WaterSupply;
            entity.WaterDrain = model.WaterDrain;
            entity.Door = model.Door;
            entity.Logo = model.Logo;
            entity.ElectricityUsage = model.ElectricityUsage;
            entity.FireProtection = model.FireProtection;
            entity.Broadcasting = model.Broadcasting;
            entity.AirCondition = model.AirCondition;
            entity.Smoke = model.Smoke;
            entity.Security = model.Security;
            entity.Wiring = model.Wiring;
            entity.Water = model.Water;
            entity.Gas = model.Gas;
            entity.FloorBearing = model.FloorBearing;

            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserId;

            if (!isExists)
            {
                bll.Insert(entity);
            }
            else
            {
                bll.Update(entity);
            }
            this.SaveChanges();

            #region 保存模板

            if (model.Template)
            {
                var templateBll = CreateBizObject<Project_UnitSpec_Template>();
                Project_UnitSpec_Template template;
                template = model.TemplateId > 0 ? templateBll.GetBySysNo(model.TemplateId) : new Project_UnitSpec_Template();

                template.TemplateName = model.TemplateName;
                template.Floor = model.Floor;
                template.Ceiling = model.Ceiling;
                template.Wall = model.Wall;
                template.Pillar = model.Pillar;
                template.WaterSupply = model.WaterSupply;
                template.WaterDrain = model.WaterDrain;
                template.Door = model.Door;
                template.Logo = model.Logo;
                template.ElectricityUsage = model.ElectricityUsage;
                template.FireProtection = model.FireProtection;
                template.Broadcasting = model.Broadcasting;
                template.AirCondition = model.AirCondition;
                template.Smoke = model.Smoke;
                template.Security = model.Security;
                template.Wiring = model.Wiring;
                template.Water = model.Water;
                template.Gas = model.Gas;
                template.FloorBearing = model.FloorBearing;

                template.EditDate = DateTime.Now;
                template.EditUser = model.UserId;

                if (model.TemplateId > 0)
                {
                    templateBll.Update(template);
                }
                else
                {
                    template.InDate = DateTime.Now;
                    template.InUser = 0;
                    templateBll.Insert(template);
                }

                this.SaveChanges();
            }

            #endregion

            #region 交付标准

            if (model.Condition > 0)
            {
                Expression<Func<Project_UnitPlan, bool>> wherePlan = plan => plan.UnitID == model.UnitId;
                var planBll = CreateBizObject<Project_UnitPlan>();
                var planEntity = planBll.TopOne(wherePlan);
                if (planEntity != null)
                {
                    planEntity.Condition = model.Condition;
                    planEntity.EditDate = DateTime.Now;
                    planEntity.EditUser = model.UserId;
                    planBll.Update(planEntity);
                    this.SaveChanges();
                }
            }

            #endregion

            return true;
        }

        /// <summary>
        /// 保存商铺商户责任
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SaveStoreSpec(ProjectStoreSpecModel model)
        {
            Expression<Func<Project_UnitSpec, bool>> where = item => item.UnitID == model.UnitId && item.SpecType == 2;
            var bll = CreateBizObject<Project_UnitSpec>();
            var entity = bll.TopOne(where);
            var isExists = bll.Exists(where);

            if (!isExists)
            {
                entity = new Project_UnitSpec();
                entity.UnitID = model.UnitId;
                entity.Status = 1;
                entity.InDate = DateTime.Now;
                entity.InUser = model.UserId;
                entity.SpecType = 2;
            }

            entity.Floor = model.Floor;
            entity.Ceiling = model.Ceiling;
            entity.Wall = model.Wall;
            entity.Pillar = model.Pillar;
            entity.WaterSupply = model.WaterSupply;
            entity.WaterDrain = model.WaterDrain;
            entity.Door = model.Door;
            entity.Logo = model.Logo;
            entity.ElectricityUsage = model.ElectricityUsage;
            entity.FireProtection = model.FireProtection;
            entity.Broadcasting = model.Broadcasting;
            entity.AirCondition = model.AirCondition;
            entity.Smoke = model.Smoke;
            entity.Security = model.Security;
            entity.Wiring = model.Wiring;
            entity.Water = model.Water;
            entity.Gas = model.Gas;
            entity.FloorBearing = model.FloorBearing;

            entity.EditDate = DateTime.Now;
            entity.EditUser = model.UserId;

            if (!isExists)
            {
                bll.Insert(entity);
            }
            else
            {
                bll.Update(entity);
            }
            this.SaveChanges();

            #region 保存模板

            if (model.Template)
            {
                var templateBll = CreateBizObject<Project_UnitSpec_Template>();
                Project_UnitSpec_Template template;
                template = model.TemplateId > 0 ? templateBll.GetBySysNo(model.TemplateId) : new Project_UnitSpec_Template();

                template.TemplateName = model.TemplateName;
                template.Floor = model.Floor;
                template.Ceiling = model.Ceiling;
                template.Wall = model.Wall;
                template.Pillar = model.Pillar;
                template.WaterSupply = model.WaterSupply;
                template.WaterDrain = model.WaterDrain;
                template.Door = model.Door;
                template.Logo = model.Logo;
                template.ElectricityUsage = model.ElectricityUsage;
                template.FireProtection = model.FireProtection;
                template.Broadcasting = model.Broadcasting;
                template.AirCondition = model.AirCondition;
                template.Smoke = model.Smoke;
                template.Security = model.Security;
                template.Wiring = model.Wiring;
                template.Water = model.Water;
                template.Gas = model.Gas;
                template.FloorBearing = model.FloorBearing;

                template.EditDate = DateTime.Now;
                template.EditUser = model.UserId;

                if (model.TemplateId > 0)
                {
                    templateBll.Update(template);
                }
                else
                {
                    template.InDate = DateTime.Now;
                    template.InUser = 0;
                    templateBll.Insert(template);
                }

                this.SaveChanges();
            }

            #endregion
            return true;
        }


        /// <summary>
        /// 获取交付标准模板列表
        /// </summary>
        /// <returns></returns>
        public List<ProjectUnitSpecTemplateModel> GetProjectUnitSpecTemplateList()
        {
            var list = new List<ProjectUnitSpecTemplateModel>();
            var entityList = CreateBizObject<Project_UnitSpec_Template>().GetAllList();
            if (entityList != null)
            {
                entityList.ForEach(entity =>
                {
                    list.Add(new ProjectUnitSpecTemplateModel
                    {
                        TemplateId = entity.TemplateID,
                        TemplateName = entity.TemplateName,
                    });
                });
            }
            return list;
        }

        /// <summary>
        /// 获取交付标准模板信息
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public ProjectUnitSpecTemplateModel GetProjectUnitSpecTemplateInfo(int templateId)
        {
            var model = new ProjectUnitSpecTemplateModel();
            var entity = CreateBizObject<Project_UnitSpec_Template>().GetBySysNo(templateId);
            if (entity != null)
            {
                model.TemplateId = entity.TemplateID;
                model.TemplateName = entity.TemplateName;
                model.Floor = entity.Floor;
                model.Ceiling = entity.Ceiling;
                model.Wall = entity.Wall;
                model.Pillar = entity.Pillar;
                model.WaterSupply = entity.WaterSupply;
                model.WaterDrain = entity.WaterDrain;
                model.Door = entity.Door;
                model.Logo = entity.Logo;
                model.ElectricityUsage = entity.ElectricityUsage;
                model.FireProtection = entity.FireProtection;
                model.Broadcasting = entity.Broadcasting;
                model.AirCondition = entity.AirCondition;
                model.Smoke = entity.Smoke;
                model.Security = entity.Security;
                model.Wiring = entity.Wiring;
                model.Water = entity.Water;
                model.Gas = entity.Gas;
                model.FloorBearing = entity.FloorBearing; 
            }
            return model;
        }

    }
}
