using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Official.Common
{
    public class NHHCGPicListModel:BaseModel
    {
        public List<NHHCGPicModel> PicList { get; set; }
        
        public PagingInfo PagingInfo { get; set; }
    }
}
