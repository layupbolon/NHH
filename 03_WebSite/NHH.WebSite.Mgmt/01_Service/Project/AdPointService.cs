using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Project.Adpoint;
using NHH.Service.Project.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Project
{
    /// <summary>
    /// 广告位服务
    /// </summary>
    public class AdPointService : NHHService<NHHEntities>, IAdPointService
    {
        /// <summary>
        /// 获取广告位列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public AdPointListModel GetAdPointList(AdPointQueryInfo queryInfo)
        {
            var model = new AdPointListModel();
            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };
            var query = from a in Context.View_Project_AdPoint
                        join d in Context.Dictionary on a.AdPointType equals d.FieldValue into AdPointDic1
                        from d in AdPointDic1.DefaultIfEmpty()
                        join dc in Context.Dictionary on a.AdPointMedia equals dc.FieldValue into AdPointDic2
                        from dc in AdPointDic2.DefaultIfEmpty()
                        where a.Status == 1 && d.FieldType == "AdPointType" && dc.FieldType == "AdPointMedia"
                        select new
                        {
                            a.AdPointID,
                            a.AdPointType,
                            AdPointTypeName = d.FieldName,
                            a.ProjectID,
                            a.FloorID,
                            a.BuildingID,
                            a.Location,
                            a.AdPointNumber,
                            a.BuildingName,
                            a.FloorNumber,
                            a.FloorName,
                            a.FloorMapLocation,
                            a.FloorMapFileName,
                            AdPointMediaName = dc.FieldName
                        };
            if (queryInfo.ProjectId.HasValue)
            {
                query = query.Where(item => item.ProjectID == queryInfo.ProjectId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                query = query.Where(item => item.FloorID == queryInfo.FloorId.Value);
            }
            if (queryInfo.Type.HasValue)
            {
                query = query.Where(item => item.AdPointType == queryInfo.Type.Value);
            }
            if (!string.IsNullOrEmpty(queryInfo.AdPointNumber))
            {
                query = query.Where(item => item.AdPointNumber == queryInfo.AdPointNumber);
            }
            model.PagingInfo.TotalCount = query.Count();
            model.AdPointList = new List<AdPointListInfo>();
            var entityList = query.OrderByDescending(item => item.AdPointID).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            if (null != entityList && entityList.Count > 0)
            {
                string strFormat = string.Empty;
                entityList.ForEach(entity =>
                {
                    var info = new AdPointListInfo
                    {
                        AdPointId = entity.AdPointID,
                        AdPointTypeName = entity.AdPointTypeName,
                        AdPointMediaName = entity.AdPointMediaName,
                        AdPointNumber = entity.AdPointNumber,
                        Location = entity.Location,
                        FloorMapFileName = entity.FloorMapFileName,
                        FloorMapLocation = entity.FloorMapLocation,
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
                    model.AdPointList.Add(info);
                });
            }
            model.QueryInfo = queryInfo;

            return model;
        }

        /// <summary>
        /// 根据广告位编号获取广告位信息
        /// </summary>
        /// <param name="adPointId"></param>
        /// <returns></returns>
        public AdPointDetailModel GetAdPointDetail(int adPointId)
        {
            var model = new AdPointDetailModel();
            var entity = CreateBizObject<NHH.Entities.Project_AdPoint>().GetBySysNo(adPointId);
            if (null != entity)
            {
                model.ProjectID = entity.ProjectID;
                model.ProjectName = entity.Project.ProjectName;
                model.FloorId = entity.FloorID;
                model.FloorName =string.Format("{0} {1}",entity.Project_Building.BuildingName,entity.Project_Floor.FloorName);
                model.Location = entity.Location;
                model.AdPointType = entity.AdPointType;
                model.AdPointMedia = entity.AdPointMedia;
                model.AdPointNumber = entity.AdPointNumber;
                model.FloorMapFileName = entity.Project_Floor.FloorMapFileName;
                model.FloorMapLocation = entity.FloorMapLocation;
            }
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
            return model;
        }

        /// <summary>
        /// 保存广告位信息
        /// </summary>
        /// <param name="model"></param>
        public void SaveAdPoint(AdPointDetailModel model)
        {
            Project_AdPoint entity = null;
            var instance = CreateBizObject<NHH.Entities.Project_AdPoint>();

            if (model.AdPointID > 0)
                entity = instance.GetBySysNo(model.AdPointID); //编辑
            else
                entity = new Project_AdPoint();

            var floor = CreateBizObject<NHH.Entities.Project_Floor>().GetBySysNo(model.FloorId);
          
            entity.AdPointNumber = model.AdPointNumber;
            entity.AdPointType = model.AdPointType;
            entity.AdPointMedia = model.AdPointMedia;
            entity.Location = model.Location;
            entity.FloorMapLocation = string.Format("{0},{1}", model.FloorMapX, model.FloorMapY);
            entity.EditUser = 1;
            entity.EditDate = DateTime.Now;
            entity.Status = 1;
            if (model.AdPointID == 0) //Add
            {
                entity.ProjectID = model.ProjectID;
                entity.FloorID = model.FloorId;
                entity.BuildingID = floor.BuildingID;
                entity.Rent = 1000;
                entity.InUser = 0;
                entity.InDate = DateTime.Now;
                entity.EditDate = DateTime.Now;
                entity.EditUser = 0;
                entity.ContractStatus = 0;
                instance.Insert(entity);
            }
            else//编辑
            {
                entity.EditDate = DateTime.Now;
                entity.EditUser = 0;
                instance.Update(entity);
            }

            this.SaveChanges();
        }

    }
}
