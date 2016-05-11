using System.Collections.Generic;
using NHH.Models.Common;

namespace NHH.Models.Official.Banner
{
    public class BannerListModel : BaseModel
    {
        public List<BannerModel> BannerList { get; set; }

        public BannerQueryInfo QueryInfo { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
