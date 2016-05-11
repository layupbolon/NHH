using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Project.Kiosk;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project
{
    /// <summary>
    /// 多经点位服务
    /// </summary>
    public class KioskService : NHHService<NHHEntities>, IKioskService
    {
        /// <summary>
        ///获取多经点位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public KioskListModel GetKioskList(KioskQueryInfo queryInfo)
        {
            var model = new KioskListModel();
            model.KioskList = new List<KioskListInfo>();
            model.PagingInfo = new PagingInfo();
            model.QueryInfo = queryInfo;
            var linq = from k in Context.View_Project_Kiosk
                       join dc in Context.Dictionary on k.KioskType equals dc.FieldValue into KioskDic
                       from dc in KioskDic.DefaultIfEmpty()
                       where k.Status == 1 && dc.FieldType == "KioskType"
                       select new
                       {
                           k.KioskID,
                           k.ProjectID,
                           k.FloorID,
                           k.BuildingID,
                           k.Location,
                           k.KioskNumber,
                           k.Area,
                           k.FloorNumber,
                           k.FloorName,
                           k.BuildingName,
                           k.FloorMapLocation,
                           k.FloorMapFileName,
                           KioskTypeName = dc.FieldName
                       };
            if (queryInfo.ProjectId.HasValue)
            {
                linq = linq.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                linq = linq.Where(item => item.FloorID == queryInfo.FloorId.Value);
            }
            if (!string.IsNullOrEmpty(queryInfo.KioskNumber))
            {
                linq = linq.Where(item => item.KioskNumber.Contains(queryInfo.KioskNumber));
            }
            model.PagingInfo.TotalCount = linq.Count();
            model.PagingInfo.PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1;

            var entityList = linq.OrderByDescending(item => item.KioskID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (null != entityList && entityList.Count > 0)
            {
                entityList.ForEach(entity =>
                {
                    var info = new KioskListInfo
                    {
                        Area = entity.Area,
                        KioskNumber = entity.KioskNumber,
                        Location = entity.Location,
                        KioskId = entity.KioskID,
                        FloorMapLocation = entity.FloorMapLocation,
                        FloorMapFileName = entity.FloorMapFileName,

                        KioskTypeName = entity.KioskTypeName,

                        FloorName = entity.FloorName,

                    };
                    if (!string.IsNullOrEmpty(info.FloorMapLocation))
                    {
                        var arr = info.FloorMapLocation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (arr.Length > 0)
                        {
                            int x = 0;
                            int.TryParse(arr[0], out x);
                            info.FloorMapX = x;
                        }
                        if (arr.Length > 1)
                        {
                            int y = 0;
                            int.TryParse(arr[1], out y);
                            info.FloorMapY = y;
                        }
                    }
                    model.KioskList.Add(info);
                });
            }
            return model;
        }

        /// <summary>
        /// 根据多经点位编号获取多经点位信息
        /// </summary>
        /// <param name="kioskId"></param>
        /// <returns></returns>
        public KioskDetailModel GetKioskDetail(int kioskId)
        {
            var model = new KioskDetailModel();
            var entity = CreateBizObject<NHH.Entities.Project_Kiosk>().GetBySysNo(kioskId);
            if (null != entity)
            {
                model.ProjectID = entity.ProjectID;
                model.ProjectName = entity.Project.ProjectName;
                model.FloorId = entity.FloorID;
                model.FloorName = string.Format("{0} {1}", entity.Project_Building.BuildingName, entity.Project_Floor.FloorName);
                model.Location = entity.Location;
                model.KioskNumber = entity.KioskNumber;
                model.Area = entity.Area;
                model.KioskType = entity.KioskType;
                model.FloorMapLocation = entity.FloorMapLocation;
                model.FloorMapFileName = entity.Project_Floor.FloorMapFileName;

                if (!string.IsNullOrEmpty(model.FloorMapLocation))
                {
                    var arr = model.FloorMapLocation.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    if (arr.Length > 0)
                    {
                        int x = 0;
                        int.TryParse(arr[0], out x);
                        model.FloorMapX = x;
                    }
                    if (arr.Length > 1)
                    {
                        int y = 0;
                        int.TryParse(arr[1], out y);
                        model.FloorMapY = y;
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 保存多经点位
        /// </summary>
        /// <param name="model"></param>
        public void SaveKiosk(KioskDetailModel model)
        {
            Project_Kiosk entity = null;
            var instance = CreateBizObject<NHH.Entities.Project_Kiosk>();

            if (model.KioskID > 0)
                entity = instance.GetBySysNo(model.KioskID); //编辑
            else
                entity = new Project_Kiosk();

            var floor = CreateBizObject<NHH.Entities.Project_Floor>().GetBySysNo(model.FloorId);
            entity.KioskNumber = model.KioskNumber;
            entity.Area = model.Area;
            entity.Location = model.Location;
            entity.KioskType = model.KioskType;
            entity.FloorMapLocation = string.Format("{0},{1}", model.FloorMapX, model.FloorMapY);
            if (model.KioskID == 0) //Add
            {
                entity.ProjectID = model.ProjectID;
                entity.FloorID = model.FloorId;
                entity.BuildingID = floor.BuildingID;
                entity.Rent = 1000;
                entity.InUser = 0;
                entity.InDate = DateTime.Now;
                entity.EditUser = 0;
                entity.EditDate = DateTime.Now;
                entity.ContractStatus = 0;
                entity.Status = 1;
                instance.Insert(entity);
            }
            else//编辑
            {
                entity.EditUser = 0;
                entity.EditDate = DateTime.Now;
                instance.Update(entity);
            }

            this.SaveChanges();
        }
    }
}
