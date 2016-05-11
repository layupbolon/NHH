using NHH.Entities;
using NHH.Framework.Service;
using NHH.Models.Report;
using NHH.Service.Report.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHH.Service.Report
{
    /// <summary>
    /// KPI报表服务
    /// </summary>
    public class KPIReportService : NHHService<NHHEntities>, IKPIReportService
    {
        private List<UnitAreaRange> areaRangeList;

        /// <summary>
        /// KPI报表
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public KpiReportModel Search(KpiReportQueryInfo queryInfo)
        {
            var model = new KpiReportModel();
            model.QueryInfo = queryInfo;
            model.CountInfo = GetCountInfo(queryInfo);
            model.PagingInfo = new Models.Common.PagingInfo
            {
                PageSize = 20,
                PageIndex = queryInfo.Page.HasValue ? queryInfo.Page.Value : 1
            };

            #region 合同铺位查询
            var strCmd = @"Select @TotalCount=Count(PU.UnitID) From View_Report_Contract(Nolock) as C
                        Inner join View_Report_Project_Unit(Nolock) as PU ON C.UnitID=PU.UnitID
                        Where PU.ProjectID=@ProjectId And (C.ContractStartDate>=@StartTime Or C.ContractEndDate<=@EndTime) #Where#
                        ;With T1 as(Select PU.UnitID,
	                           PU.UnitNumber,
	                           PU.BuildingID,
	                           PU.BuildingName,
	                           PU.ProjectID,
	                           PU.FloorID,
	                           PU.FloorNumber,
	                           PU.UnitType AS UnitTypeID,
	                           PU.UnitTypeName,
	                           PU.UnitAera,
	                           PU.BizTypeID,
	                           PU.BizTypeName,
	                           PU.BizCategoryID,
	                           PU.BizCategoryName,
	                           C.ContractID,
	                           C.ContractStatus,
	                           C.ContractStatusName,
	                           C.[UnitAvgAera] as UnitArea,
	                           C.DecorationLength,
	                           C.DecorationEndDate,
	                           C.ContractLength,
	                           C.ContractEndDate,
                               C.QualityBond,
                               C.ParkingLotNum,
	                           C.AdPointNum,
	                           C.ContractUnitRent,
	                           C.ContractMgmtFee,
	                           '' As RentMode,
                               ROW_Number() Over(Order by PU.UnitID) as RowNumber
                        From View_Report_Contract(Nolock) as C
                        Inner join View_Report_Project_Unit(Nolock) as PU ON C.UnitID=PU.UnitID
                        Where PU.ProjectID=@ProjectId And (C.ContractStartDate>=@StartTime Or C.ContractEndDate<=@EndTime) #Where#)
                        Select Top (@PageSize) * From T1 Where RowNumber Between (@PageIndex-1)*@PageSize And (@PageIndex*@PageSize)";

            #region 查询条件
            var strWhere = "";
            if (queryInfo.BuildingId.HasValue)
            {
                strWhere += string.Format(" And PU.BuildingID={0}", queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                strWhere += string.Format(" And PU.FloorID={0}", queryInfo.FloorId.Value);
            }
            if (queryInfo.BizTypeId.HasValue)
            {
                strWhere += string.Format(" And PU.BizTypeID={0}", queryInfo.BizTypeId.Value);
            }
            if (queryInfo.BizCategoryId.HasValue)
            {
                strWhere += string.Format(" And BizCategoryID={0}", queryInfo.BizCategoryId.Value);
            }
            if (queryInfo.UnitType.HasValue)
            {
                strWhere += string.Format(" And PU.UnitType={0}", queryInfo.UnitType.Value);
            }
            if (queryInfo.UnitArea.HasValue)
            {
                var areaRange = GetAreaRange(queryInfo.UnitArea.Value);
                strWhere += string.Format(" And C.UnitAvgAera Between {0} And {1}", areaRange.Min, areaRange.Max);
            }
            strCmd = strCmd.Replace("#Where#", strWhere);
            #endregion

            var paramList = new SqlParameter[]
            {
                new SqlParameter("@TotalCount", System.Data.SqlDbType.Int),
                new SqlParameter("@ProjectId", queryInfo.ProjectId),
                new SqlParameter("@StartTime", queryInfo.StartTime),
                new SqlParameter("@EndTime", queryInfo.EndTime),
                new SqlParameter("@PageIndex", model.PagingInfo.PageIndex),
                new SqlParameter("@PageSize", model.PagingInfo.PageSize),
            };
            paramList[0].Direction = ParameterDirection.Output;

            #endregion
            var data = ExecSQL<KpiReportItem>(strCmd, paramList);

            model.PagingInfo.TotalCount = (int)(paramList[0].Value);

            if (data == null || data.Count == 0)
            {
                model.Data = data;

                return model;
            }

            #region 合同支付条款查询
            var contractIds = new List<int>();
            data.ForEach(item =>
            {
                contractIds.Add(item.ContractID);
            });

            strCmd = string.Format(@"Select FI.ItemName,
	                           CP.PaymentItemID,
	                           ContractID,
	                           PaymentTermsType,
	                           PaymentPeriod,
	                           DepositMonthly,
	                           PaymentMonthlyAmount,
	                           PaymentDailyAmount,
	                           CP.[IncreaseExpression],
                               CP.[CommissionExpression]
                        From Contract_PaymentTerms(Nolock) as CP
                        Inner join Finance_Item(Nolock) AS FI ON CP.PaymentItemID=FI.ItemID
                        Where CP.Status=@Status And CP.ContractID in ({0})", string.Join(",", contractIds));

            var paramList2 = new SqlParameter[] 
            {
                new SqlParameter("@Status",1)
            };
            var data1 = ExecSQL<KpiReportItemData>(strCmd, paramList2);
            #endregion
            if (data1 != null && data1.Count > 0)
            {
                data.ForEach(item =>
                {
                    //品牌
                    var paramBrand = new SqlParameter[]
                    {
                        new SqlParameter("@ContractID",item.ContractID),
                    };
                    var brandList = ExecSQL<string>(@"Select B.BrandName From dbo.Contract_Brand(Nolock) as CB
                                            Inner join dbo.Merchant_Brand(Nolock) as MB on CB.MerchantBrandID=MB.MerchantBrandID
                                            Inner join dbo.Brand(Nolock) as B on MB.BrandID=B.BrandID
                                            Where CB.ContractID=@ContractID", paramBrand);
                    if (brandList != null)
                    {
                        item.BrandNameList = string.Join("、", brandList);
                    }

                    //租金
                    var rent = data1.Find(P => P.ContractID == item.ContractID && P.PaymentItemID.Value == 1);
                    if (rent != null)
                    {
                        item.RentMode = rent.PaymentTermsTypeString;
                        item.DepositMode = rent.DepositMode;
                        item.IncreaseExpression = rent.IncreaseExpression;
                    }
                });
            }

            model.Data = data;

            return model;
        }


        /// <summary>
        /// 获取统计信息
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public KpiReportCountInfo GetCountInfo(KpiReportQueryInfo queryInfo)
        {
            var strCmd = string.Format(@"Select TotalArea,
	                           TotalUnit,
	                           SignedTotalArea,
	                           SignedTotalUnit,
                               0.0 as CountSignedTotalArea,
                               0 as CountSignedTotalUnit,
                               0.0 as CountTotalRent,
                               0.0 as CountTotalArea,
                               0 as CountTotalUnit
                        From View_Report_Project_Count(Nolock) as P
                        Where ProjectID={0}", queryInfo.ProjectId);


            var countInfo = ExecSQLToSingle<KpiReportCountInfo>(strCmd);
            if (countInfo == null)
            {
                return new KpiReportCountInfo();
            }

            #region 统计范围内的已签约面积
            strCmd = string.Format(@"Select IsNull(Sum([UnitAvgAera]),0) as CountSignedTotalArea From View_Report_Contract(Nolock) as C
                                    Where ProjectID={0} And (C.ContractStartDate<='{1}' And C.ContractEndDate>='{1}')", queryInfo.ProjectId,
                                                                                                        queryInfo.EndTime);

            if (queryInfo.BuildingId.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) AS PU Where C.UnitID=PU.UnitID And PU.BuildingID={0})", queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) as PU Where C.UnitID=PU.UnitID And PU.FloorId={0})", queryInfo.FloorId.Value);
            }
            if (queryInfo.UnitType.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) as PU Where C.UnitID=PU.UnitID And PU.UnitType={0})", queryInfo.UnitType.Value);
            }
            if (queryInfo.BizTypeId.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) as PU Where C.UnitID=PU.UnitID And PU.BizTypeId={0})", queryInfo.BizTypeId.Value);
            }
            if (queryInfo.BizCategoryId.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) as PU Where C.UnitID=PU.UnitID And PU.BizCategoryId={0})", queryInfo.BizCategoryId.Value);
            }
            countInfo.CountSignedTotalArea = ExecSQLToSingle<decimal>(strCmd);

            #endregion

            #region 统计范围内的已签约铺位数
            strCmd = string.Format(@"Select Count(Distinct UnitId) as CountSignedTotalUnit From View_Report_Contract(Nolock) as C
                                    Where ProjectID={0} And (C.ContractStartDate<='{1}' And C.ContractEndDate>='{1}')", queryInfo.ProjectId,
                                                                                                        queryInfo.EndTime);

            if (queryInfo.BuildingId.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) AS PU Where C.UnitID=PU.UnitID And PU.BuildingID={0})", queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) as PU Where C.UnitID=PU.UnitID And PU.FloorId={0})", queryInfo.FloorId.Value);
            }
            if (queryInfo.UnitType.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) as PU Where C.UnitID=PU.UnitID And PU.UnitType={0})", queryInfo.UnitType.Value);
            }
            if (queryInfo.BizTypeId.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) as PU Where C.UnitID=PU.UnitID And PU.BizTypeId={0})", queryInfo.BizTypeId.Value);
            }
            if (queryInfo.BizCategoryId.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) as PU Where C.UnitID=PU.UnitID And PU.BizCategoryId={0})", queryInfo.BizCategoryId.Value);
            }
            countInfo.CountSignedTotalUnit = ExecSQLToSingle<int>(strCmd);
            #endregion

            #region 统计范围内总面积
            strCmd = string.Format(@"Select IsNull(SUM(UnitAera),0) as UnitArea From Project_Unit(Nolock) as PU
                                    Where Status=1 And ProjectID={0}", queryInfo.ProjectId);
            if (queryInfo.BuildingId.HasValue)
            {
                strCmd += string.Format(" And BuildingID={0}", queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                strCmd += string.Format(" And FloorID={0}", queryInfo.FloorId.Value);
            }
            if (queryInfo.UnitType.HasValue)
            {
                strCmd += string.Format(" And UnitType={0}", queryInfo.UnitType.Value);
            }
            if (queryInfo.BizTypeId.HasValue)
            {
                strCmd += string.Format(" And BizTypeID={0}", queryInfo.BizTypeId.Value);
            }
            countInfo.CountTotalArea = ExecSQLToSingle<decimal>(strCmd);
            #endregion

            #region 统计范围内总铺位数
            strCmd = string.Format(@"Select Count(UnitID) as UnitNum From Project_Unit(Nolock) as PU
                                    Where Status=1 And ProjectID={0}", queryInfo.ProjectId);
            if (queryInfo.BuildingId.HasValue)
            {
                strCmd += string.Format(" And BuildingID={0}", queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                strCmd += string.Format(" And FloorID={0}", queryInfo.FloorId.Value);
            }
            if (queryInfo.UnitType.HasValue)
            {
                strCmd += string.Format(" And UnitType={0}", queryInfo.UnitType.Value);
            }
            if (queryInfo.BizTypeId.HasValue)
            {
                strCmd += string.Format(" And BizTypeID={0}", queryInfo.BizTypeId.Value);
            }
            countInfo.CountTotalUnit = ExecSQLToSingle<int>(strCmd);
            #endregion

            #region 统计范围内已签约的月总租金
            strCmd = string.Format(@"Select IsNull(Sum(C.ContractUnitRent*C.UnitAvgAera*30),0) as TotalRent From View_Report_Contract(Nolock) as C
                                    Where C.ProjectID={0} And (C.ContractStartDate<='{1}' And C.ContractEndDate>='{1}')", queryInfo.ProjectId,
                                                                                                        queryInfo.EndTime);

            if (queryInfo.BuildingId.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) as PU Where C.UnitID=PU.UnitID And PU.BuildingID={0})", queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) as PU Where C.UnitID=PU.UnitID And PU.FloorId={0})", queryInfo.FloorId.Value);
            }
            if (queryInfo.UnitType.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) as PU Where C.UnitID=PU.UnitID And PU.UnitType={0})", queryInfo.UnitType.Value);
            }
            if (queryInfo.BizTypeId.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) as PU Where C.UnitID=PU.UnitID And PU.BizTypeId={0})", queryInfo.BizTypeId.Value);
            }
            if (queryInfo.BizCategoryId.HasValue)
            {
                strCmd += string.Format(" And Exists (Select Top 1 1 From View_Report_Project_Unit(Nolock) as PU Where C.UnitID=PU.UnitID And PU.BizCategoryId={0})", queryInfo.BizCategoryId.Value);
            }

            countInfo.CountSignedTotalRent = ExecSQLToSingle<decimal>(strCmd);
            #endregion

            #region 统计月总租金
            strCmd = string.Format(@"SELECT IsNull(SUM(StandardRent*UnitAera*30),0) AS TotalRent FROM [View_Report_Project_Unit](Nolock)
                                    Where ProjectID={0}", queryInfo.ProjectId);
            if (queryInfo.BuildingId.HasValue)
            {
                strCmd += string.Format(" And BuildingID={0}", queryInfo.BuildingId.Value);
            }
            if (queryInfo.FloorId.HasValue)
            {
                strCmd += string.Format(" And FloorID={0}", queryInfo.FloorId.Value);
            }
            if (queryInfo.UnitType.HasValue)
            {
                strCmd += string.Format(" And UnitType={0}", queryInfo.UnitType.Value);
            }
            if (queryInfo.BizTypeId.HasValue)
            {
                strCmd += string.Format(" And BizTypeID={0}", queryInfo.BizTypeId.Value);
            }


            countInfo.CountTotalRent = ExecSQLToSingle<decimal>(strCmd);
            #endregion
            
            return countInfo;
        }

        /// <summary>
        /// 获取面积范围
        /// </summary>
        /// <param name="unitArea"></param>
        /// <returns></returns>
        private UnitAreaRange GetAreaRange(decimal unitArea)
        {
            if (areaRangeList == null)
            {
                areaRangeList = new List<UnitAreaRange>();
                areaRangeList.Add(new UnitAreaRange { Min = 0M, Max = 49.999M });
                areaRangeList.Add(new UnitAreaRange { Min = 50M, Max = 99.999M });
                areaRangeList.Add(new UnitAreaRange { Min = 100M, Max = 199.999M });
                areaRangeList.Add(new UnitAreaRange { Min = 200M, Max = 399.999M });
                areaRangeList.Add(new UnitAreaRange { Min = 400M, Max = 799.999M });
                areaRangeList.Add(new UnitAreaRange { Min = 800M, Max = 1499.999M });
                areaRangeList.Add(new UnitAreaRange { Min = 1500M, Max = 2999.999M });
                areaRangeList.Add(new UnitAreaRange { Min = 3000M, Max = 4999.999M });
                areaRangeList.Add(new UnitAreaRange { Min = 5000M, Max = 9999.999M });
                areaRangeList.Add(new UnitAreaRange { Min = 10000M, Max = 1000000000M });
            }

            var areaRange = areaRangeList.Find(item =>
            {
                return unitArea >= item.Min && unitArea <= item.Max;
            });

            if (areaRange == null)
                return new UnitAreaRange { Min = 0M, Max = 49.999M };

            return areaRange;
        }
    }

    /// <summary>
    /// 商铺面积范围
    /// </summary>
    public class UnitAreaRange
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
    }
}
