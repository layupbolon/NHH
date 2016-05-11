using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Documents
{
    public class ApproveInfo
    {
        /// <summary>
        /// PK
        /// </summary>
        public int ApproveID { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        public int DocumentID { get; set; }
        /// <summary>
        /// 显示的审批部门
        /// </summary>
        public string ShowDepartmentName { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        public int Approver { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime ApproveTime { get; set; }
        /// <summary>
        /// 审批理由
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 审批结果 1批准 2驳回
        /// </summary>
        public int Result { get; set; }

        public string ResultStr
        {
            get
            {
                switch (Result)
                {
                    case 1:
                        return "批准";
                    case 2:
                        return "驳回";
                    default:
                        return null;
                }
            }
        }
    }
}
