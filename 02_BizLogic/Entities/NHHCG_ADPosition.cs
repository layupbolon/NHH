//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NHH.Entities
{
    using System;
    using System.Collections.Generic;
    using NHH.Framework;
    using NHH.Framework.Data;
    
    
    public partial class NHHCG_ADPosition : IStatusFlag
    {
        public int ADPositionID { get; set; }
        public int ProjectID { get; set; }
        public string Building { get; set; }
        public int Region { get; set; }
        public string ADPositionNumber { get; set; }
        public int ADType { get; set; }
        public string Size { get; set; }
        public string Position { get; set; }
        public string Contacts { get; set; }
        public string Feature { get; set; }
        public int ElectricityType { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public string SEO_Title { get; set; }
        public string KeyWords { get; set; }
        public string Description { get; set; }
        public string RentRemark { get; set; }
        public string FloorRemark { get; set; }
    }
}
