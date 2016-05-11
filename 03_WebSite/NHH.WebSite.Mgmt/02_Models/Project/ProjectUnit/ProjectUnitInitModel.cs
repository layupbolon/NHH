using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Project.ProjectUnit
{
    /// <summary>
    /// 铺位初始化Model
    /// </summary>
    public class ProjectUnitInitModel : BaseModel
    {
        public int startNumber = 1;

        /// <summary>
        /// 铺位编号长度
        /// </summary>
        public int UnitCodeLength { get; set; }

        /// <summary>
        /// 铺位起始编号
        /// </summary>
        public int StartNumber
        {
            get { return startNumber; }
            set { startNumber = value; }
        }
    }
}
