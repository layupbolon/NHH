using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using NHH.Entities;
using NHH.Framework.Service;
using NHH.Framework.Utility;
using NHH.Framework.Web;
using NHH.Models.Common;
using NHH.Models.Official.Banner;
using NHH.Service.Official.IService;

namespace NHH.Service.Official
{
    public class BannerService : NHHService<NHHEntities>, IBannerService
    {
        /// <summary>
        /// 获取广告位信息
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public BannerListModel GetBannerList(BannerQueryInfo queryInfo)
        {
            var model = new BannerListModel()
            {
                BannerList = new List<BannerModel>(),
                QueryInfo = queryInfo
            };

            var query = from b in Context.Banner
                        join dc in Context.Dictionary.Where(a => a.FieldType == "BannerTarget") on b.BannerTarget equals
                            dc.FieldValue
                        join p in Context.Project on b.ProjectID equals p.ProjectID into queryResult2
                        from p in queryResult2.DefaultIfEmpty()
                        join nhhp in Context.NHHCG_Project on b.ProjectID equals nhhp.ProjectID into queryResult3
                        from nhhp in queryResult3.DefaultIfEmpty()
                        join dc2 in Context.Dictionary.Where(a => a.FieldType == "BannerType") on b.BannerType equals
                            dc2.FieldValue
                        join bl in Context.Banner_Location on b.LocationID equals bl.LocationID into queryResult
                        from bl in queryResult.DefaultIfEmpty()
                        join dc3 in Context.Dictionary.Where(a => a.FieldType == "BannerStatus") on b.Status equals
                            dc3.FieldValue
                        select new
                        {
                            b.BannerID,
                            b.BannerTarget,
                            BannerTargetName = dc.FieldName,
                            b.ProjectID,
                            p.ProjectName,
                            NHHCGProjectName = nhhp.ProjectName,
                            b.BannerType,
                            BannerTypeName = dc2.FieldName,
                            b.LocationID,
                            bl.LocationName,
                            b.Status,
                            BannerStatusName = dc3.FieldName,
                            b.Remark,
                            b.InUser,
                            b.InDate
                        };

            #region 查询条件

            if (queryInfo.BannerTarget.HasValue)
            {
                query = query.Where(a => a.BannerTarget == queryInfo.BannerTarget.Value);
            }
            if (queryInfo.ProjectID.HasValue)
            {
                query = query.Where(a => a.ProjectID == queryInfo.ProjectID.Value);
            }
            if (queryInfo.BannerType.HasValue)
            {
                query = query.Where(a => a.BannerType == queryInfo.BannerType.Value);
            }

            #endregion

            //分页信息
            model.PagingInfo = new PagingInfo();
            model.PagingInfo.PageIndex = queryInfo.Page ?? 1;
            model.PagingInfo.TotalCount = query.Count();

            query = query.OrderByDescending(q => q.BannerID)
                    .Skip(model.PagingInfo.SkipNum)
                    .Take(model.PagingInfo.TakeNum);
            var entityList = query.ToList();
            if (!entityList.Any())
            {
                return model;
            }

            entityList.ForEach(entity =>
            {
                model.BannerList.Add(new BannerModel()
                {
                    BannerID = entity.BannerID,
                    BannerTarget = entity.BannerTarget,
                    BannerTargetName = entity.BannerTargetName,
                    ProjectID = entity.ProjectID,
                    ProjectName = entity.BannerTarget == 1 ? entity.ProjectName : entity.BannerTarget == 2 ? entity.NHHCGProjectName : string.Empty,
                    BannerType = entity.BannerType,
                    BannerTypeName = entity.BannerTypeName,
                    LocationID = entity.LocationID,
                    LocationName = entity.LocationName,
                    Status = entity.Status,
                    BannerStatusName = entity.BannerStatusName,
                    Remark = entity.Remark,
                    InUser = entity.InUser,
                    InDate = entity.InDate
                });
            });

            return model;
        }

        /// <summary>
        /// 获取位置信息
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public List<BannerLocation> GetLocationInfo(int target = 1)
        {
            var query = from l in Context.Banner_Location.Where(a => a.Target == target)
                        select new BannerLocation()
                        {
                            LocationID = l.LocationID,
                            Target = l.Target,
                            PageID = l.PageID,
                            LocationName = l.LocationName
                        };

            return query.ToList();
        }

        /// <summary>
        /// 新增广告位
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddBanner(BannerModel model)
        {
            var instance = CreateBizObject<Banner>();
            var bannerModel = new Banner()
            {
                BannerTarget = model.BannerTarget,
                ProjectID = model.ProjectID,
                BannerType = model.BannerType,
                LocationID = model.LocationID,
                Status = 1,
                Remark = model.Remark,
                InUser = model.UserID,
                InDate = DateTime.Now
            };

            instance.Insert(bannerModel);
            SaveChanges();

            return bannerModel.BannerID;
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="bannerID"></param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        public bool Cancel(int? bannerID,int currentUserID)
        {
            if (bannerID.HasValue)
            {
                var instance = CreateBizObject<Banner>();
                var banner = instance.TopOne(a => a.BannerID == bannerID.Value);
                banner.Status = -1;
                banner.EditUser = currentUserID;
                banner.EditDate = DateTime.Now;

                instance.Update(banner);
                SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取广告位详细信息
        /// </summary>
        /// <param name="bannerID"></param>
        /// <returns></returns>
        public BannerDetailModel GetBannerDetail(int bannerID)
        {
            var model = new BannerDetailModel()
            {
                BannerID = bannerID,
                BannerInfos = new List<BannerInfoModel>()
            };

            var query = from bi in Context.BannerInfo
                        where bi.BannerID == bannerID
                        select new BannerInfoModel()
                        {
                            InfoID = bi.InfoID,
                            BannerID = bi.BannerID,
                            Seq = bi.Seq,
                            Title = bi.Title,
                            Content = bi.Content,
                            ResourcePath = bi.ResourcePath,
                            Width = bi.Width,
                            Height = bi.Height,
                            Link = bi.Link,
                            InUser = bi.InUser,
                            InDate = bi.InDate
                        };

            if (query.Any())
                model.BannerInfos.AddRange(query.ToList());

            return model;
        }

        /// <summary>
        /// 删除广告位详细
        /// </summary>
        /// <param name="InfoID"></param>
        /// <returns></returns>
        public bool DeleteBannerInfo(int InfoID)
        {
            using (var trans = Context.Database.BeginTransaction())
            {
                var instance = CreateBizObject<BannerInfo>();
                var model = instance.TopOne(a => a.InfoID == InfoID);
                instance.Delete(model);

                var all = instance.GetAllList(a => a.InfoID != InfoID && a.BannerID == model.BannerID);
                if (all.Any())
                {
                    int seq = 1;
                    var list = all.ToList();
                    foreach (var item in list.OrderBy(a => a.Seq))
                    {
                        item.Seq = seq;
                        instance.Update(item);
                        seq++;
                    }
                }

                SaveChanges();
                trans.Commit();
            }
            
            return true;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditBanner(BannerModel model)
        {
            var instance = CreateBizObject<Banner>();
            var banner = instance.TopOne(a => a.BannerID == model.BannerID);
            banner.BannerType = model.BannerType;
            banner.BannerTarget = model.BannerTarget;
            banner.ProjectID = model.ProjectID;
            banner.LocationID = model.LocationID;
            banner.Remark = model.Remark;
            banner.EditUser = model.UserID;
            banner.EditDate = DateTime.Now;

            instance.Update(banner);
            SaveChanges();

            return true;
        }

        /// <summary>
        /// 获取广告位信息
        /// </summary>
        /// <param name="bannerID"></param>
        /// <returns></returns>
        public BannerModel GetBanner(int bannerID)
        {
            var query = from b in Context.Banner.Where(a => a.BannerID == bannerID)
                        select new BannerModel()
                        {
                            BannerID = b.BannerID,
                            BannerTarget = b.BannerTarget,
                            ProjectID = b.ProjectID,
                            BannerType = b.BannerType,
                            LocationID = b.LocationID,
                            Status = b.Status,
                            Remark = b.Remark
                        };

            return query.FirstOrDefault();
        }

        /// <summary>
        /// 添加广告位详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddBannerInfo(BannerInfoModel model)
        {
            var instacne = CreateBizObject<BannerInfo>();

            var query = Context.BannerInfo.Where(a => a.BannerID == model.BannerID);
            int seq;
            if (query.Any())
                seq = query.Select(a => a.Seq).Distinct().Max() + 1;
            else
                seq = 1;

            var entity = new BannerInfo()
            {
                BannerID = model.BannerID,
                Seq = seq,
                Title = model.Title,
                Content = model.Content,
                ResourcePath = model.ResourcePath,
                Width = model.Width,
                Height = model.Height,
                Link = model.Link,
                InUser = model.UserID,
                InDate = DateTime.Now
            };

            instacne.Insert(entity);
            SaveChanges();

            return true;
        }

        /// <summary>
        /// 广告位详细调整顺序
        /// </summary>
        /// <param name="infoID"></param>
        /// <param name="seqType">1为上移  2为下移</param>
        /// <param name="currentUserID"></param>
        /// <returns></returns>
        public bool BannerSeq(int infoID, int seqType,int currentUserID)
        {
            using (var trans = Context.Database.BeginTransaction())
            {
                var instance = CreateBizObject<BannerInfo>();
                var entity1 = instance.TopOne(a => a.InfoID == infoID);
                var seq1 = seqType == 1 ? entity1.Seq - 1 : seqType == 2 ? entity1.Seq + 1 : entity1.Seq;

                var entity2 = instance.TopOne(a => a.Seq == seq1 && a.BannerID == entity1.BannerID);
                var seq2 = seqType == 1 ? entity2.Seq + 1 : seqType == 2 ? entity2.Seq - 1 : entity2.Seq;

                entity1.Seq = seq1;
                entity1.EditUser = currentUserID;
                entity1.EditDate = DateTime.Now;
                entity2.Seq = seq2;
                entity2.EditUser = currentUserID;
                entity2.EditDate = DateTime.Now;

                instance.Update(entity1);
                instance.Update(entity2);

                SaveChanges();
                trans.Commit();
            }
            return true;
        }

        /// <summary>
        /// 获取广告位详细信息
        /// </summary>
        /// <param name="bannerInfoID"></param>
        /// <returns></returns>
        public BannerInfoModel GetBannerInfo(int bannerInfoID)
        {
            var model = from b in Context.BannerInfo
                        where b.InfoID == bannerInfoID
                        select new BannerInfoModel()
                        {
                            InfoID = b.InfoID,
                            BannerID = b.BannerID,
                            Seq = b.Seq,
                            Title = b.Title,
                            Content = b.Content,
                            ResourcePath = b.ResourcePath,
                            Width = b.Width,
                            Height = b.Height,
                            Link = b.Link
                        };

            return model.FirstOrDefault();
        }

        /// <summary>
        /// 编辑广告位信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditBannerInfo(BannerInfoModel model)
        {
            var instacne = CreateBizObject<BannerInfo>();
            var entity = instacne.TopOne(a => a.InfoID == model.InfoID);
            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.ResourcePath = model.ResourcePath;
            entity.Width = model.Width;
            entity.Height = model.Height;
            entity.Link = model.Link;
            entity.InUser = model.UserID;
            entity.InDate = DateTime.Now;

            instacne.Update(entity);
            SaveChanges();

            return true;
        }
    }
}
