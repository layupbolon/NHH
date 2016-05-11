using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Configure
{
    public class SysMenuInfo
    {
        public int MenuID { get; set; }

        [Required(ErrorMessage = "请输入菜单排序")]
        public int SeqNo { get; set; }

        public string MenuIcon { get; set; }

        [Required(ErrorMessage = "请输入菜单名称")]
        public string MenuName { get; set; }

        [Required(ErrorMessage = "请输入菜单路径")]
        public string MenuUrl { get; set; }

        public string MenuDescription { get; set; }

        public int? ParentID { get; set; }

        public int Status { get; set; }

        public DateTime InDate { get; set; }

        public int InUser { get; set; }

        public DateTime EditDate { get; set; }

        public int EditUser { get; set; }
    }
}
