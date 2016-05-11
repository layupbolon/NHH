using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Entity
{
    /// <summary>
    /// 合同
    /// </summary>
    public class ContractEntity : BaseEntity
    {
        public int ContractID { get; set; }
        public string ContractCode { get; set; }
        public int ContractType { get; set; }
        public int ContractStatus { get; set; }
        public string ProjectName { get; set; }
        public int ProjectID { get; set; }
        public string BuildingName { get; set; }
        public int BuildingID { get; set; }
        public string UnitNumbers { get; set; }
        public List<int> UnitIdList { get; set; }
        public decimal ContractArea { get; set; }
        public decimal ContractUnitRent { get; set; }
        public decimal ContractMgmtFee { get; set; }
        public string MerchantName { get; set; }
        public int MerchantID { get; set; }
        public string SignerName { get; set; }
        public string SignerIDNumber { get; set; }
        public string SignerPhone { get; set; }
        public int OwnerCompanyID { get; set; }
        public int? InvestCompanyID { get; set; }
        public int ManageCompanyID { get; set; }
        public string OperatorName { get; set; }
        public string OperatorPhone { get; set; }
        public int? ContractLength { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public int? RentFreeLength { get; set; }
        public DateTime? RentFreeStartDate { get; set; }
        public DateTime? RentFreeEndDate { get; set; }
        public DateTime? AccessDate { get; set; }
        public int? DecorationLength { get; set; }
        public DateTime? DecorationStartDate { get; set; }
        public DateTime? DecorationEndDate { get; set; }
        public decimal? DecorationMgmtFee { get; set; }
        public decimal? DecorationBond { get; set; }
        public int? Condition { get; set; }
        public string ConditionText { get; set; }
        public decimal? BidBond { get; set; }
        public decimal? ManageBond { get; set; }
        public decimal? QualityBond { get; set; }
        public int? ParkingLotNum { get; set; }
        public string ParkingLotMemo { get; set; }
        public int? AdPointNum { get; set; }
        public string AdPointMemo { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string AttachmentList { get; set; }

        /// <summary>
        /// 租金
        /// </summary>
        public ContractPaymentTermsEntity RentPaymentTerms { get; set; }

        /// <summary>
        /// 物业费
        /// </summary>
        public ContractPaymentTermsEntity MgmtPaymentTerms { get; set; }

        /// <summary>
        /// 租约附件
        /// </summary>
        public List<ContractAppendixEntity> AppendixList { get; set; }

        /// <summary>
        /// 铺位列表
        /// </summary>
        public List<ContractUnitEntity> UnitList { get; set; }
        
        /// <summary>
        /// 商户商铺
        /// </summary>
        public MerchantStoreEntity MerchantStore { get; set; }

        /// <summary>
        /// 交付条件
        /// </summary>
        public ContractUnitSpecEntity UnitSpec { get; set; }

        /// <summary>
        /// 商户责任
        /// </summary>
        public ContractUnitSpecEntity StoreSpec { get; set; }

        /// <summary>
        /// 补充协议
        /// </summary>
        public ContractSupplementaryEntity Supplementary { get; set; }
        
        /// <summary>
        /// 经营形式
        /// </summary>
        public string BrandTypeName { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandNames { get; set; }

        /// <summary>
        /// 品牌列表
        /// </summary>
        public List<ContractBrandEntity> ContractBrandList { get; set; }

        /// <summary>
        /// 商户品牌列表
        /// </summary>
        public List<MerchantBrandEntity> MerchantBrandList { get; set; }
    }
}
