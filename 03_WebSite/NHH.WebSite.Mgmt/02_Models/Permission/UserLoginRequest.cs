using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NHH.Models.Permission
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "登录密码不能为空")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ClientIP { get; set; }
    }
}