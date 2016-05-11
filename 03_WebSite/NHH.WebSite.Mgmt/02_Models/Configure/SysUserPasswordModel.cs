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
    /// 修改个人密码model
    /// </summary>
    public class SysUserPasswordModel
    {

        private CrumbInfo _crumbInfo = new CrumbInfo { };

        /// <summary>
        /// 面包屑信息
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get
            { return _crumbInfo; }
        }

        /// <summary>
        /// 系统用户账号
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 系统用户原密码
        /// </summary>
        [Required(ErrorMessage = "请输入原密码")]
        [DataType(DataType.Password)]
        [Display(Name = "原密码")]
        public string OldPassword { get; set; }

        /// <summary>
        /// 系统用户新密码
        /// </summary>
        [Required(ErrorMessage = "请输入新密码")]
        [DataType(DataType.Password)]
        [StringLength(200, ErrorMessage = "{0} 必须至少包含 {2}个字符", MinimumLength = 6)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        /// <summary>
        /// 系统用户新密码确认
        /// </summary>
        [Required(ErrorMessage = "请输入确认新密码")]
        [Display(Name = "确认新密码")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
        public string ConfirmNewPassword { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string StatusText { get; set; }
    }
}
