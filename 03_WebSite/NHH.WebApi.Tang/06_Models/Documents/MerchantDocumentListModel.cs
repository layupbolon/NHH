using System.Collections.Generic;
using NHH.Models.Common;

namespace NHH.Models.Documents
{
    public class MerchantDocumentListModel
    {
        public MerchantDocumentListModel()
        {
        }

        public MerchantDocumentListModel(int pageIndex, int pageSize)
        {
            this.PagingInfo=new PagingInfo();
            this.PagingInfo.PageIndex = pageIndex;
            this.PagingInfo.PageSize = pageSize;
        }
        public List<MerchantDocumentInfo> MerchantDocumentList { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
