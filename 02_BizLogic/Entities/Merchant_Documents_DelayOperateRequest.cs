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
    
    
    public partial class Merchant_Documents_DelayOperateRequest
    {
        public int ID { get; set; }
        public int DocumentID { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public string Reason { get; set; }
        public int InUser { get; set; }
        public System.DateTime InDate { get; set; }
        public int NeedAC { get; set; }
        public int NeedLighting { get; set; }
        public int MoreHour { get; set; }
    }
}
