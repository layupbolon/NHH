using System;
using NHH.Models.Approve;
using NHH.Models.Common;

namespace NHH.Models.Plan.MerchantLicense
{
    public class MerchantLicenseModel : ApproveBaseModel
    {
        private int _attachmentID;

        public int AttachmentID
        {
            get { return _attachmentID; }
            set { _attachmentID = value; }
        }

        public int MerchantID { get; set; }

        public string MerchantName { get; set; }

        public int AttachmentType { get; set; }

        public string AttachmentTypeName { get; set; }

        public string AttachmentName { get; set; }

        public string AttachmentPath { get; set; }

        public string AttachmentCode { get; set; }

        public DateTime? ExpireDate { get; set; }

        public int? ExpireDay { get; set; }

        public int Status { get; set; }

        public string StatusName { get; set; }

        public int InUser { get; set; }

        public DateTime InDate { get; set; }

        public override int RefID
        {
            get { return _attachmentID; }
            set { _attachmentID = value; }
        }

        public override string ApproveUrl
        {
            get { return "/Plan/MerchantLicense/Approve"; }
        }

        public override string RedirectUrl
        {
            get { return "/Plan/MerchantLicense/List"; }
        }
    }
}
