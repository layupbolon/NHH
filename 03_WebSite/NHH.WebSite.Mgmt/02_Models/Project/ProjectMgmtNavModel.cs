using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project
{
    /// <summary>
    /// 项目管理导航
    /// </summary>
    public class ProjectMgmtNavModel
    {
        public ProjectCommonInfo ProjectInfo { get; set; }

        public int Step { get; set; }
    }
}
