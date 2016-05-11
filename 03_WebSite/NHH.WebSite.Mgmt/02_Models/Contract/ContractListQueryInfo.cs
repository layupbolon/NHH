using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 合同列表查询信息
    /// </summary>
    public class ContractListQueryInfo : QueryInfo
    {
        /// <summary>
        /// 合同列表查询信息
        /// </summary>
        public ContractListQueryInfo() 
        {
            OrderBy = "ContractCode";
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 楼宇ID
        /// </summary>
        public int? BuildingId { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public int? FloorId { get; set; }

        /// <summary>
        /// 商铺编号
        /// </summary>
        public string UnitNumber { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 提交开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 提交结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string StoreName { get; set; }

        public int? UnitId { get; set; }

    }
}
