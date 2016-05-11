using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHH.Framework.Security.Cryptography;

namespace NHH.WebAPI.Merchant
{
    public partial class Test : System.Web.UI.Page
    {
        private SHA1HashCryptographer sha1 = new SHA1HashCryptographer();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}