using System.Collections.Generic;
using NHH.Models.Common;

namespace NHH.Models.Official.Feedback
{
    public class FeedbackListModel:BaseModel
    {
        public List<FeedbackModel> FeedbackList { get; set; }

        public FeedbackQueryModel QueryInfo { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
