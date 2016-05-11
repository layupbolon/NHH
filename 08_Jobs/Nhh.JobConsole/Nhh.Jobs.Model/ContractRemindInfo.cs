using System;

namespace Nhh.Jobs.Model
{
    public class ContractRemindInfo
    {
        public int RemindID { get; set; }

        public int ContractID { get; set; }

        public DateTime ContractEndDate { get; set; }

        public int Status { get; set; }

        public int InUser { get; set; }

        public DateTime InDate { get; set; }

        public int EditUser { get; set; }

        public DateTime EditDate { get; set; }
    }
}
