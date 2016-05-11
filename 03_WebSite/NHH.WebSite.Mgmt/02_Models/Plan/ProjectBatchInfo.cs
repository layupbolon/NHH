using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Plan
{
    /// <summary>
    /// 项目招商批次信息
    /// </summary>
    public class ProjectBatchInfo
    {
        /// <summary>
        /// 批次ID
        /// </summary>
        public int BatchID { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// 批次编号
        /// </summary>
        [Required(ErrorMessage = "请输入批次编号")]
        public string BatchCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 新增人
        /// </summary>
        public int InUser { get; set; }
    }
}
