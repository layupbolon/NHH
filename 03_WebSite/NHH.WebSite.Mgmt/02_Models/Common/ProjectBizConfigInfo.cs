using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    /// <summary>
    /// 项目业务配置信息
    /// </summary>
    public class ProjectBizConfigInfo
    {
        public int BizConfigID { get; set; }

        /// <summary>
        /// 业务类型
        /// 101：投诉
        /// 102：维修
        /// 103：催款
        /// 110: 微信公众帐号
        /// </summary>
        public int BizConfigType { get; set; }

        /// <summary>
        /// 业务类型名称
        /// </summary>
        public string BizConfigTypeName { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 楼宇ID
        /// </summary>
        public int? BuildingID { get; set; }

        /// <summary>
        /// 楼宇名称
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// 参数1
        /// </summary>
        public string Param1 { get; set; }

        /// <summary>
        /// 参数2
        /// </summary>
        public string Param2 { get; set; }

        /// <summary>
        /// 参数3
        /// </summary>
        public string Param3 { get; set; }

        /// <summary>
        /// 参数4
        /// </summary>
        public string Param4 { get; set; }

        /// <summary>
        /// 参数5
        /// </summary>
        public string Param5 { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public int Status { get; set; }

        public DateTime InDate { get; set; }

        public int InUser { get; set; }

        public DateTime? EditDate { get; set; }

        public int? EditUser { get; set; }
    }
}
