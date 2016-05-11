using System.ComponentModel;

namespace NHH.Models.Official
{
    public enum NHHCGTypeEnum
    {
        [Description("铺位")]
        Unit = 1,
        [Description("广告位")]
        ADPosition = 2,
        [Description("多经点位")]
        Kiosk = 3
    }
}
