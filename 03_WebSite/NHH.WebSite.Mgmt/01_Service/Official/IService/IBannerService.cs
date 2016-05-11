using System.Collections.Generic;
using NHH.Models.Official.Banner;

namespace NHH.Service.Official.IService
{
    public interface IBannerService
    {
        BannerListModel GetBannerList(BannerQueryInfo queryInfo);

        List<BannerLocation> GetLocationInfo(int target = 1);

        int AddBanner(BannerModel model);

        bool Cancel(int? bannerID, int currentUserID);

        BannerDetailModel GetBannerDetail(int bannerID);

        bool DeleteBannerInfo(int InfoID);

        bool EditBanner(BannerModel model);

        BannerModel GetBanner(int bannerID);

        bool AddBannerInfo(BannerInfoModel model);

        bool BannerSeq(int infoID, int seqType, int currentUserID);

        BannerInfoModel GetBannerInfo(int bannerInfoID);

        bool EditBannerInfo(BannerInfoModel model);
    }
}
