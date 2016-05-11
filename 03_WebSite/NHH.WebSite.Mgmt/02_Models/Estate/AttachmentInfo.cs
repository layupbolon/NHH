using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Estate
{
    public  class AttachmentInfo
    {
        public int ComplaintId { get; set; }

        public int AttchmentId { get; set; }

        public string AttachmentPath { get; set; }

        public string AttachmentName { get; set; }
    }
}
