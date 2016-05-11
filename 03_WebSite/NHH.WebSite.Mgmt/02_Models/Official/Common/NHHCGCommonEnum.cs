using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Official.Common
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
