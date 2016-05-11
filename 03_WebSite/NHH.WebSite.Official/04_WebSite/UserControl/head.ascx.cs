using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHH.Models.Official;

namespace NHH.WebSite.Official.UserControl
{
    public partial class head : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 当前光标所在位置
        /// </summary>
        public MenuEnum CurrentMenu { get; set; }
    }
}