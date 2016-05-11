using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Official.Kiosk
{
    public class NHHCGKioskListModel : BaseModel
    {
        public List<NHHCGKioskModel> KioskList { get; set; }

        public NHHCGKioskQueryModel QueryInfo { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
