using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    public class BannerLocation
    {
        public int LocationID { get; set; }

        public int LocationType { get; set; }

        public int PageID { get; set; }

        public string LocationName { get; set; }

        public string PageName { get; set; }
    }
}
