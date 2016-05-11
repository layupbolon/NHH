using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Official.Common
{
    public class NHHCGPicModel : BaseModel
    {
        public int PicID { get; set; }

        public int Type { get; set; }

        public int RefID { get; set; }

        public string Path { get; set; }

        public int Seq { get; set; }
    }
}
