using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 项目基础信息
    /// </summary>
    public class ProjectBasicInfoModel : ProjectInfo
    {
        public DateTime InDate { get; set; }

        public int InUser { get; set; }

        public DateTime EditDate { get; set; }

        public int EditUser { get; set; }
    }
}
