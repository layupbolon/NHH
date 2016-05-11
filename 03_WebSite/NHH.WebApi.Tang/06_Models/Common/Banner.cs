using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    public class BannerMaster
    {
        public BannerMaster()
        {
            this.BannerInfoList = new List<BannerInfo>();
        }
        public int BannerID { get; set; }

        public int ProjectID { get; set; }

        public int BannerType { get; set; }

        public string BannerTypeName { get; set; }

        public int LocationType { get; set; }

        public int PageID { get; set; }

        public string Remark { get; set; }

        public List<BannerInfo> BannerInfoList { get; set; }
    }
}
