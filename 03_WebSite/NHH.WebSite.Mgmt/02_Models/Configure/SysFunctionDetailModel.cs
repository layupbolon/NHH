using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    /// <summary>
    /// 功能模块详情
    /// </summary>
    public class SysFunctionDetailModel : BaseModel
    {

        /// <summary>
        /// 功能模块Id
        /// </summary>
        public int FunctionId { get; set; }

        /// <summary>
        /// 功能模块key
        /// </summary>
        [Required(ErrorMessage = "请输入功能键值")]
        public string FunctionKey { get; set; }

        /// <summary>
        /// 功能模块名称
        /// </summary>
        [Required(ErrorMessage = "请输入功能名称")]
        public string FunctionName { get; set; }

        /// <summary>
        /// 功能模块描述
        /// </summary>
        [Required(ErrorMessage = "请输入功能描述")]
        public string FunctionDescription { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public UserInfo UserInfo { get; set; }
    }
}
