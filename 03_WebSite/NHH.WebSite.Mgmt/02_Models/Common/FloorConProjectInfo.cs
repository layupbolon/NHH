using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHH.Models.Common
{
    public class FloorConProjectInfo : FloorCommonInfo
    {
        int projectID;

        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }

        string falgid;

        public string Falgid
        {
            get { return falgid; }
            set { falgid = value; }
        }

    }
}
