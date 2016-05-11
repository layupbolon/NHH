using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;

namespace NHH.Models.Official
{
    public class ADPositionInfo
    {
        public int ADPositionID { get; set; }

        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public string Building { get; set; }

        public string FloorRemark { get; set; }

        public int Region { get; set; }

        public string RegionName { get; set; }

        public string ADPositionNumber { get; set; }

        public int ADType { get; set; }

        public string ADTypeName { get; set; }

        public string Size { get; set; }

        public string RentRemark { get; set; }

        public string Position { get; set; }

        public string Contacts { get; set; }

        public string Feature { get; set; }

        public int ElectricityType { get; set; }

        public string ElectricityTypeName { get; set; }

        public string Pic { get; set; }

        public List<string> PicList { get; set; } 
    }
}
