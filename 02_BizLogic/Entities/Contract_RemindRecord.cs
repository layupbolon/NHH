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
    
    
    public partial class Contract_RemindRecord : IStatusFlag,IEditable
    {
        public int RemindID { get; set; }
        public int ContractID { get; set; }
        public System.DateTime ContractEndDate { get; set; }
        public int Status { get; set; }
        public int InUser { get; set; }
        public System.DateTime InDate { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditUser { get; set; }
    }
}