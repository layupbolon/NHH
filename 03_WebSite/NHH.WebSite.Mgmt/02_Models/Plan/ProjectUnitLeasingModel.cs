using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 招商信息
    /// </summary>
    public class ProjectUnitLeasingModel
    {
        public int UnitId { get; set; }

        public int UnitLeasingID { get; set; }

        public int LeasingDepartmentID { get; set; }

        /// <summary>
        /// 招商负责部门
        /// </summary>
        public string LeasingDepartment { get; set; }

        /// <summary>
        /// 招商责任角色
        /// </summary>
        public int LeasingRoleID { get; set; }

        /// <summary>
        /// 招商责任人
        /// </summary>
        public int LeasingPersonID { get; set; }

        /// <summary>
        /// 招商负责人
        /// </summary>
        public string LeasingPerson { get; set; }

        /// <summary>
        /// 招商完成时间节点
        /// </summary>
        public DateTime LeasingFinishDate { get; set; }

        /// <summary>
        /// 是否需要消防报审
        /// </summary>
        public int FireProtectionAuth { get; set; }

        /// <summary>
        /// 是否需要消防报审文本
        /// </summary>
        public string FireProtectionAuthString
        {
            get
            {
                if (FireProtectionAuth == 0)
                    return "不需要";
                return "需要";
            }
        }

        /// <summary>
        /// 复尺完成时间节点
        /// </summary>
        public DateTime MeasureReviewDate { get; set; }

        /// <summary>
        /// 商户出图完成时间节点
        /// </summary>
        public DateTime DesignDate { get; set; }

        /// <summary>
        /// 消防报审完成时间节点
        /// </summary>
        public DateTime FireProtectionAuthDate { get; set; }
        
        /// <summary>
        /// 进场时间节点
        /// </summary>
        public DateTime AccessDate { get; set; }

        /// <summary>
        /// 装修开始时间节点
        /// </summary>
        public DateTime DecorationStartDate { get; set; }

        /// <summary>
        /// 装修结束时间节点
        /// </summary>
        public DateTime DecorationEndDate { get; set; }

        /// <summary>
        /// 当前用户
        /// </summary>
        public int UserId { get; set; }
    }
}
