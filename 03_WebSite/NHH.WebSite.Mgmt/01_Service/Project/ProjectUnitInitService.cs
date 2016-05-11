using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Project.ProjectUnit;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project
{
    /// <summary>
    /// 铺位初始化服务
    /// </summary>
    public class ProjectUnitInitService : NHHService<NHHEntities>, IProjectUnitInitService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="initInfo"></param>
        public MessageBag<bool> Init(ProjectUnitInitInfo initInfo)
        {
            var result = new MessageBag<bool>();
            result.Code = -1;

            if (!initInfo.FloorId.HasValue || initInfo.FloorId.Value < 1)
            {
                result.Desc = "请选择楼层";
                return result;
            }

            if (initInfo.UnitNumber <= 1)
            {
                result.Desc = "请指定初始铺位数量";
                return result;
            }

            //检查有没有铺位
            var check = from pu in Context.Project_Unit
                        where pu.FloorID == initInfo.FloorId
                        select pu.UnitID;
            if (check.Any())
            {
                result.Desc = "该楼层已有铺位，禁止初始化";
                return result;
            }

            var floorInfo = (from pf in Context.Project_Floor
                             where pf.FloorID == initInfo.FloorId
                             select new
                             {
                                 pf.FloorNumber,
                                 pf.BuildingID,
                             }).FirstOrDefault();

            if (floorInfo == null)
            {
                result.Desc = "当前楼层未设置楼层数";
                return result;
            }

            var bll = CreateBizObject<Project_Unit>();

            int unitTotal = initInfo.StartNumber + initInfo.UnitNumber;
            for (int n = initInfo.StartNumber; n <= unitTotal; n++)
            {
                var entity = new Project_Unit
                {
                    ProjectID = initInfo.ProjectId.Value,
                    BuildingID = floorInfo.BuildingID,
                    FloorID = initInfo.FloorId.Value,
                    UnitNumber = floorInfo.FloorNumber.ToString(),
                    UnitAera = 0,
                    UnitStatus = 1,
                    UnitType = 2,
                    ContractStatus = 0,
                    Status = 1,
                    InDate = DateTime.Now,
                    InUser = initInfo.UserId,
                };

                #region 楼层编号
                string unitNumber = n.ToString();
                while (unitNumber.Length < initInfo.UnitCodeLength)
                {
                    unitNumber = "0" + unitNumber;
                }
                #endregion

                entity.UnitNumber = entity.UnitNumber + unitNumber;
                if (!string.IsNullOrEmpty(initInfo.UnitPrefix) && initInfo.UnitPrefix.Length > 0)
                {
                    entity.UnitNumber = string.Format("{0}{1}", initInfo.UnitPrefix, entity.UnitNumber);
                }
                bll.Insert(entity);
                SaveChanges();
            }

            result.Code = 0;
            result.Data = true;
            result.Desc = "铺位初始化完成";
            return result;
        }
    }
}
