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
    
    
    public partial class Merchant_Documents_DecorateRequest
    {
        public int ID { get; set; }
        public int DocumentID { get; set; }
        public string DecorationCompanyName { get; set; }
        public string DecorationCompanyAddress { get; set; }
        public string PersonInCharge { get; set; }
        public string Phone { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Remark { get; set; }
        public int InUser { get; set; }
        public System.DateTime InDate { get; set; }
        public int ElectricityConsumption { get; set; }
    }
}