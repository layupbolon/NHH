using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Common;
using NHH.Models.Estate;
using NHH.Service.Estate.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Service.Estate
{
    /// <summary>
    /// 投诉评价服务
    /// </summary>
    public class ComplaintCommentService : NHHService<NHHEntities>, IComplaintCommentService
    {

        /// <summary>
        /// 投诉评价列表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public ComplaintCommentListModel GetCommentList(ComplaintCommentQueryInfo queryInfo)
        {
            var model = new ComplaintCommentListModel();
            model.QueryInfo = queryInfo;
            model.CommentListInfo = new List<ComplaintCommentInfo>();
            model.PagingInfo = new PagingInfo
            {
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };

            var query = from cm in Context.Complaint_Comment
                        join cpt in Context.Complaint on cm.ComplaintID equals cpt.ComplaintID
                        where cm.Status == 1
                        select new
                        {
                            cm.CommentID,
                            cm.ComplaintID,
                            cm.Speed,
                            cm.Attitude,
                            cm.Quality,
                            cm.Overall,
                            cm.Comment,
                            cm.Additional,
                            cpt.ServiceUserID,
                            cpt.ServiceUserName,
                            cpt.ComplaintType,
                            cpt.RequestTime
                        };
            if (query != null)
            {
                if (queryInfo.ServiceUserId.HasValue)
                {
                    query = query.Where(m => m.ServiceUserID == queryInfo.ServiceUserId.Value);
                }
                if (queryInfo.ComplaintId.HasValue)
                {
                    query = query.Where(m => m.ComplaintID == queryInfo.ComplaintId);
                }
                if (queryInfo.ComplaintType.HasValue)
                {
                    query = query.Where(m => m.ComplaintType == queryInfo.ComplaintType);
                }
                if (queryInfo.FromDate.HasValue)
                {
                    query = query.Where(m => m.RequestTime >= queryInfo.FromDate);
                }
                if (queryInfo.ToDate.HasValue)
                {
                    query = query.Where(m => m.RequestTime <= queryInfo.ToDate);
                }
            }

            model.PagingInfo.TotalCount = query.Count();

            query = query.OrderBy(queryInfo.OrderExpression).Skip(model.PagingInfo.SkipNum).Take(model.PagingInfo.TakeNum);

            var commentList = query.ToList();
            commentList.ForEach(item =>
            {
                var info = new ComplaintCommentInfo();
                info.CommentId = item.CommentID;
                info.ComplaintId = item.ComplaintID;
                info.Speed = item.Speed;
                info.Attitude = item.Attitude;
                info.Quality = item.Quality;
                info.Overall = item.Overall;
                info.Comment = item.Comment;
                info.ServiceUserName = item.ServiceUserName;
                model.CommentListInfo.Add(info);
            });
            return model;
        }

        /// <summary>
        /// 投诉评价详情
        /// </summary>
        /// <param name="complaintId"></param>
        /// <returns></returns>
        public ComplaintCommentDetailModel GetCommentDetail(int commentId)
        {
            var model = new ComplaintCommentDetailModel();
            var entity = Context.Complaint_Comment.Where(m => m.CommentID == commentId && m.Status == 1).FirstOrDefault();
            if (entity != null)
            {
                model.CommentId = entity.CommentID;
                model.ComplaintId = entity.ComplaintID;
                model.Quality = entity.Quality;
                model.Speed = entity.Speed;
                model.Attitude = entity.Attitude;
                model.Overall = entity.Overall;
                model.AllComment = string.Format("{0}", entity.Comment);
            }
            return model;
        }
    }
}
