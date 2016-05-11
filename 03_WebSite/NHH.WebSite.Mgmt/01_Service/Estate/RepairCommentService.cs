using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Estate;
using NHH.Service.Estate.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Estate
{
    /// <summary>
    /// 报修评价服务
    /// </summary>
    public class RepairCommentService : NHHService<NHHEntities>, IRepairCommentService
    {
        /// <summary>
        /// 获取点评列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public RepairCommentListModel GetCommentList(RepairCommentQueryInfo queryInfo)
        {
            var model = new RepairCommentListModel();
            model.QueryInfo = queryInfo;
            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1,
            };
            #region 查询信息
            var query = from r in Context.Repair
                        join c in Context.Repair_Comment on r.RepairID equals c.RepairID
                        where r.Status == 1 && r.RepairStatus == 3
                        select new
                        {
                            c.CommentID,
                            r.RepairID,
                            r.RepairUserName,
                            r.RepairUserID,
                            c.Speed,
                            c.Quality,
                            c.Attitude,
                            c.Overall,
                            c.Comment,
                        };
            if (queryInfo.RepairId.HasValue)
            {
                query = query.Where(item => item.RepairID == queryInfo.RepairId.Value);
            }
            if (queryInfo.RepairUserId.HasValue)
            {
                query = query.Where(item => item.RepairUserID == queryInfo.RepairUserId.Value);
            }
            #endregion
            model.PagingInfo.TotalCount = query.Count();

            var entityList = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum).ToList();
            model.CommentList = new List<RepairCommentInfo>();
            if (null != entityList)
            {

                entityList.ForEach(entity =>
                {
                    var commonInfo = new RepairCommentInfo
                    {
                        CommentId = entity.CommentID,
                        RepairId = entity.RepairID,
                        Speed = entity.Speed,
                        Quality = entity.Quality,
                        Attitude = entity.Attitude,
                        Overall = entity.Overall,
                        Comment = entity.Comment,
                        RepairUserId = entity.RepairUserID,
                        RepairUserName = entity.RepairUserName,
                    };
                    model.CommentList.Add(commonInfo);
                });
            }
            return model;
        }

        /// <summary>
        /// 获取维修点评详情弹出框
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public RepairCommentDetailModel GetCommentDetail(int commentId)
        {
            var model = new RepairCommentDetailModel();
            var entity = Context.Repair_Comment.Where(m => m.CommentID == commentId && m.Status == 1).FirstOrDefault();
            if (null != entity)
            {
                model.CommentId = entity.CommentID;
                model.RepairId = entity.RepairID;
                model.Speed = entity.Speed;
                model.Quality = entity.Quality;
                model.Attitude = entity.Attitude;
                model.Overall = entity.Overall;
                model.Comment = entity.Comment;
                model.Additional = entity.Additional;
                model.AllComment = string.Format("{0}", model.Comment);
            }
            return model;
        }

    }
}
