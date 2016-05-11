using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 附件信息
    /// </summary>
    public class AppendixInfo
    {
        public int AppendixID { get; set; }

        public int ContractID { get; set; }

        public int AppendixType { get; set; }

        public int AppendixTemplate { get; set; }

        public string AppendixName { get; set; }

        public string AppendixPath { get; set; }
    }
}
