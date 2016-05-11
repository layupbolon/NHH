using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Official.Project
{
    public class NHHCGProjectListModel : BaseModel
    {
        public List<NHHCGProjectModel> ProjectList { get; set; }

        public NHHCGProjectQueryModel QueryInfo { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
