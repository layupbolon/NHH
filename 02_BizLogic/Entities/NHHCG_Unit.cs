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
    
    
    public partial class NHHCG_Unit : IStatusFlag
    {
        public int UnitID { get; set; }
        public int ProjectID { get; set; }
        public string Building { get; set; }
        public string UnitNumber { get; set; }
        public decimal Area { get; set; }
        public decimal PropertyCosts { get; set; }
        public string Position { get; set; }
        public string Contacts { get; set; }
        public int Status { get; set; }
        public System.DateTime InDate { get; set; }
        public int InUser { get; set; }
        public string SEO_Title { get; set; }
        public string KeyWords { get; set; }
        public string Description { get; set; }
        public string RentRemark { get; set; }
        public string FloorRemark { get; set; }
        public string BizTypes { get; set; }
    }
}