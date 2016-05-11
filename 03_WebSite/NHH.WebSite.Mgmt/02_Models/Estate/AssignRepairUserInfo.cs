using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    public class AssignRepairUserInfo
    {
        /// <summary>
        /// 指派维修人员的姓名
        /// </summary>
        public string RepairUserName { get; set; }

        /// <summary>
        /// 指派维修人员的ID
        /// </summary>
        public int RepairUserId { get; set; }

        /// <summary>
        /// 维修人的联系方式
        /// </summary>
        public string RepairMobile { get; set; }
    }
}
