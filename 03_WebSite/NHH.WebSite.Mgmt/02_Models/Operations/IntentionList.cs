using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common;

namespace NHH.Models.Operations
{
    public class IntentionList : BaseModel
    {
        public List<IntentionInfo> IntentionInfoList { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public IntentionQuery QueryInfo { get; set; }
    }
}
