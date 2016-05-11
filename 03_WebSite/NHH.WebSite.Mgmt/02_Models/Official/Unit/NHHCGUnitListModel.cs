using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Official.Unit
{
    public class NHHCGUnitListModel : BaseModel
    {
        public List<NHHCGUnitModel> UnitList { get; set; }

        public NHHCGUnitQueryModel QueryInfo { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
