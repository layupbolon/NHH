using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Permission
{
    public class MenuInfo
    {
        public int MenuId { get; set; }

        public int SeqNo { get; set; }

        public int? ParentId { get; set; }

        public string MenuIcon { get; set; }

        public string MenuName { get; set; }

        public string MenuUrl { get; set; }
    }
}
