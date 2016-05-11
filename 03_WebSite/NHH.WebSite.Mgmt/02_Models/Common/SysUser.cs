using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    public class SysUser
    {
        private CrumbInfo _crumbInfo = new CrumbInfo();

        /// <summary>
        /// 面包屑
        /// </summary>
        public CrumbInfo CrumbInfo
        {
            get { return _crumbInfo; }
        }
        
        public int UserID { get; set; }
        
        public int EmployeeID { get; set; }
        
        public int UserType { get; set; }

        [StringLength(50, ErrorMessage = "登录名长度过长")]
        [Required(ErrorMessage = "请输入登录名")]
        public string UserName { get; set; }
        
        [StringLength(50, ErrorMessage = "密码长度过长")]
        [Required(ErrorMessage = "请输入密码")]
        
        public string Password { get; set; }
        
        public System.DateTime LastLoginTime { get; set; }
        
        public string LastLoginIP { get; set; }
        
        public int Status { get; set; }
        
        public int InUser { get; set; }
        
        public Nullable<int> EditUser { get; set; }
    }
}
