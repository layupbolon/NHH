using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    public class MessageTemplate
    {
        public int TemplateID { get; set; }

        public string TemplateKey { get; set; }

        public int MessageType { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }
    }
}
