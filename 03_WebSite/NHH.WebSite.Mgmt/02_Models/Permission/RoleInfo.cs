using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Permission
{
    public class RoleInfo
    {
        public int RoleId { get; set; }

        [Required(ErrorMessage = "请输入角色")]
        public string RoleName { get; set; }
    }
}
