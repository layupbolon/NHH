using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Official.ADPosition
{
    public class NHHCGADPositionListModel : BaseModel
    {
        public List<NHHCGADPositionModel> ADPositionList { get; set; }

        public NHHCGADPositionQueryModel QueryInfo { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
