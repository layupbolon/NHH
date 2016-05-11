using System.Collections.Generic;

namespace NHH.Models.Official.Banner
{
    public class BannerDetailModel
    {
        public int BannerID { get; set; }

        public List<BannerInfoModel> BannerInfos { get; set; }
    }
}
