using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    public class ApproveConfig
    {
        public int ConfigID { get; set; }
        public int ConfigType { get; set; }
        public int RoleID { get; set; }
        public int Step { get; set; }
        public string ShowDepartmentName { get; set; }
    }
}
