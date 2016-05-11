using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    public class BannerInfo
    {
        public int InfoID { get; set; }

        public int BannerID { get; set; }

        public int Seq { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ResourcePath { get; set; }

        public string Link { get; set; }
    }
}
