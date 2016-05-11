using Newtonsoft.Json;
using NHH.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHH.Models.Common.Converter;

namespace NHH.Models.Contract
{
    /// <summary>
    /// 租约信息
    /// </summary>
    public class ContractInfo
    {
        /// <summary>
        /// 合同ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ContractID { get; set; }

        /// <summary>
        /// 合约类型 1铺位 2多经点位
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ContractType { get; set; }

        /// <summary>
        /// 合约类型描述
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ContractTypeDescription {
            get
            {
                switch (ContractType)
                {
                    case 1:
                        return "铺位";
                    case 2:
                        return "多经点位";
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// 合约商户品牌ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MerchantBrandID { get; set; }

        /// <summary>
        /// 合约品牌名称
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string BrandName { get; set; }

        /// <summary>
        /// 合约铺位号列表，多个用“,”号分隔
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string UnitNumbers { get; set; }

        /// <summary>
        /// 合约号
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ContractCode { get; set; }

        /// <summary>
        /// 合同状态 1-等待签约 2-签约中 3-合同执行中 4-合同结束 5-合同中止
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ContractStatus { get; set; }

        /// <summary>
        /// 合同状态描述
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ContractStatusDescription
        {
            get
            {
                switch (ContractStatus)
                {
                    case 1:
                        return "等待签约";
                    case 2:
                        return "签约中";
                    case 3:
                        return "合同执行中";
                    case 4:
                        return "合同结束";
                    case 5:
                        return "合同中止";
                    default:
                        return null;

                }
            }
        }

        /// <summary>
        /// 项目ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ProjectID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ProjectName { get; set; }

        /// <summary>
        /// 签约人姓名
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SignerName { get; set; }

        /// <summary>
        /// 签约人身份证号
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SignerIDNumber { get; set; }

        /// <summary>
        /// 签约人联系电话
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SignerPhone { get; set; }

        ///// <summary>
        ///// 业主公司编号
        ///// </summary>
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        //public int OwnerCompanyID { get; set; }

        ///// <summary>
        ///// 业主公司名称
        ///// </summary>
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        //public string OwnerCompany { get; set; }

        ///// <summary>
        ///// 管理公司编号
        ///// </summary>
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        //public int ManageCompanyID { get; set; }

        ///// <summary>
        ///// 管理公司名称
        ///// </summary>
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        //public string ManageCompany { get; set; }


        /// <summary>
        /// 计租面积
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal ContractArea { get; set; }

        /// <summary>
        /// 租金单价
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal ContractUnitRent { get; set; }

        /// <summary>
        /// 物业费单价
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal ContractMgmtFee { get; set; }

        /// <summary>
        /// 月租金
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal MonthlyRent { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MerchantID { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string MerchantName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonConverter(typeof(ChinaLongDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? InDate { get; set; }

        /// <summary>
        /// 合同生效日期
        /// </summary>
        [JsonConverter(typeof(ChinaShortDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? ContractStartDate { get; set; }

        /// <summary>
        /// 合同结束时间
        /// </summary>
        [JsonConverter(typeof(ChinaShortDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? ContractEndDate { get; set; }

        /// <summary>
        /// 免租开始日期
        /// </summary>
        [JsonConverter(typeof(ChinaShortDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? RentFreeStartDate { get; set; }

        /// <summary>
        /// 免租结束日期
        /// </summary>
        [JsonConverter(typeof(ChinaShortDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? RentFreeEndDate { get; set; }

        /// <summary>
        /// 合同总天数
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ContractLength { get; set; }

        /// <summary>
        /// 装修期限（天）
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int DecorationLength { get; set; }

        /// <summary>
        /// 装修开始时间
        /// </summary>
        [JsonConverter(typeof(ChinaShortDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? DecorationStartDate { get; set; }

        /// <summary>
        /// 装修结束时间
        /// </summary>
        [JsonConverter(typeof(ChinaShortDateConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime? DecorationEndDate { get; set; }

        /// <summary>
        /// 停车位数量
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ParkingLotNum { get; set; }

        /// <summary>
        /// 广告位数量
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int AdPointNum{get; set;}

        /// <summary>
        /// 合约铺位列表
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<ProjectUnitInfo> UnitList { get; set; }

        /// <summary>
        /// 支付条款列表
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<ContractPaymentTermsInfo> ContractPaymentTermsInfoList { get; set; }

        /// <summary>
        /// 交付标准和商户责任列表
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<ContractUnitSpecInfo> ContractUnitSpecList { get; set; }

        /// <summary>
        /// 补充协议列表
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<ContractSupplementaryInfo> ContractSupplementaryList { get; set; }

        /// <summary>
        /// 合约附件列表
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<ContractAppendixInfo> ContractAppendixList { get; set; }

        /// <summary>
        /// 品牌列表
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<string> BrandList { get; set; }

    }
}
