using NhhDataImport.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 租约支付条款
    /// </summary>
    public class ContractPaymentTermsProcess
    {
        /// <summary>
        /// 保存支付条款
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="entity"></param>
        public static void SaveContractPaymentTerms(SqlTransaction trans, ContractEntity entity)
        {
            //BuildIncreaseInfo(entity);
            //entity.RentPaymentTerms.IncreaseExpression = "";// Newtonsoft.Json.JsonConvert.SerializeObject(entity.RentPaymentTerms.PaymentIncreaseInfo);
            //BuildCommissionInfo(entity);
            //entity.RentPaymentTerms.CommissionExpression = "";//Newtonsoft.Json.JsonConvert.SerializeObject(entity.RentPaymentTerms.PaymentCommissionInfo);

            entity.RentPaymentTerms.PaymentTermsType = 1;
            if (entity.RentPaymentTerms.PaymentTermsType1 == "是" &&
                entity.RentPaymentTerms.PaymentTermsType2 == "是")
            {
                entity.RentPaymentTerms.PaymentTermsType = 3;
            }
            else if(entity.RentPaymentTerms.PaymentTermsType1=="是")
            {
                entity.RentPaymentTerms.PaymentTermsType = 1;
            }
            else if (entity.RentPaymentTerms.PaymentTermsType2 == "是")
            {
                entity.RentPaymentTerms.PaymentTermsType = 2;
            }

            #region PaymentTerms
            string strCmd = @"INSERT INTO [dbo].[Contract_PaymentTerms]
                                   ([ContractID]
                                   ,[PaymentTermsType]
                                   ,[PaymentItemID]
                                   ,[PaymentPeriod]
                                   ,[DepositMonthly]
                                   ,[PaymentDailyAmount]
                                   ,[PaymentMonthlyAmount]
                                   ,[IncreaseExpression]
                                   ,[CommissionExpression]
                                   ,[Status]
                                   ,[InDate]
                                   ,[InUser]
                                   ,[EditDate]
                                   ,[EditUser])
                             VALUES
                                   (@ContractID
                                   ,@PaymentTermsType
                                   ,@PaymentItemID
                                   ,@PaymentPeriod
                                   ,@DepositMonthly
                                   ,@PaymentDailyAmount
                                   ,@PaymentMonthlyAmount
                                   ,@IncreaseExpression
                                   ,@CommissionExpression
                                   ,@Status
                                   ,@InDate
                                   ,@InUser
                                   ,@EditDate
                                   ,@EditUser)";
            #endregion

            var paramList1 = new SqlParameter[] 
            {
                new SqlParameter("@ContractID", entity.ContractID),
                new SqlParameter("@PaymentTermsType", entity.RentPaymentTerms.PaymentTermsType),
                new SqlParameter("@PaymentItemID", entity.RentPaymentTerms.PaymentItemID),
                new SqlParameter("@PaymentPeriod", entity.RentPaymentTerms.PaymentPeriod),
                new SqlParameter("@DepositMonthly", entity.RentPaymentTerms.DepositMonthly),
                new SqlParameter("@PaymentDailyAmount", entity.RentPaymentTerms.PaymentDailyAmount),
                new SqlParameter("@PaymentMonthlyAmount", entity.RentPaymentTerms.PaymentMonthlyAmount),
                new SqlParameter("@IncreaseExpression", entity.RentPaymentTerms.IncreaseExpression),
                new SqlParameter("@CommissionExpression", entity.RentPaymentTerms.CommissionExpression),
                new SqlParameter("@Status", entity.RentPaymentTerms.Status),
                new SqlParameter("@InDate", entity.RentPaymentTerms.InDate),
                new SqlParameter("@InUser", entity.RentPaymentTerms.InUser),
                new SqlParameter("@EditDate", entity.RentPaymentTerms.EditDate),
                new SqlParameter("@EditUser", entity.RentPaymentTerms.EditUser),
            };

            SqlHelper.ExecuteNonQuery(trans, strCmd, paramList1);

            var paramList2 = new SqlParameter[] 
            {
                new SqlParameter("@ContractID", entity.ContractID),
                new SqlParameter("@PaymentTermsType", entity.MgmtPaymentTerms.PaymentTermsType),
                new SqlParameter("@PaymentItemID", entity.MgmtPaymentTerms.PaymentItemID),
                new SqlParameter("@PaymentPeriod", entity.MgmtPaymentTerms.PaymentPeriod),
                new SqlParameter("@DepositMonthly", entity.RentPaymentTerms.DepositMonthly),
                new SqlParameter("@PaymentDailyAmount", entity.MgmtPaymentTerms.PaymentDailyAmount),
                new SqlParameter("@PaymentMonthlyAmount", entity.MgmtPaymentTerms.PaymentMonthlyAmount),
                new SqlParameter("@IncreaseExpression", entity.MgmtPaymentTerms.IncreaseExpression),
                new SqlParameter("@CommissionExpression", entity.MgmtPaymentTerms.CommissionExpression),
                new SqlParameter("@Status", entity.MgmtPaymentTerms.Status),
                new SqlParameter("@InDate", entity.MgmtPaymentTerms.InDate),
                new SqlParameter("@InUser", entity.MgmtPaymentTerms.InUser),
                new SqlParameter("@EditDate", entity.MgmtPaymentTerms.EditDate),
                new SqlParameter("@EditUser", entity.MgmtPaymentTerms.EditUser),
            };
            SqlHelper.ExecuteNonQuery(trans, strCmd, paramList2);
        }


        /// <summary>
        /// 构造递增信息
        /// </summary>
        /// <param name="entity"></param>
        private static void BuildIncreaseInfo(ContractEntity entity)
        {
            //entity.RentPaymentTerms.PaymentIncreaseInfo = new PaymentIncreaseInfo();
            //entity.RentPaymentTerms.PaymentIncreaseInfo.IncreaseList = new List<PaymentIncreaseItem>();
            //entity.RentPaymentTerms.PaymentIncreaseInfo.IncreaseType = entity.RentPaymentTerms.IncreaseAmount.Value < 1 ? 1 : 2;
            //DateTime startYear = entity.ContractStartDate.Value;

            ////总年数
            //var years = (entity.ContractEndDate.Value.Year - entity.ContractStartDate.Value.Year);
            //years = years == 0 ? years = 1 : years;
            //years += 1;

            ////月度租金
            //decimal PaymentMonthlyAmount = entity.RentPaymentTerms.PaymentMonthlyAmount.Value;

            //int startYears = entity.RentPaymentTerms.IncreaseStartYears.HasValue ? entity.RentPaymentTerms.IncreaseStartYears.Value : 0;
            //int stepYears = -1;
            //decimal increaseValue = entity.RentPaymentTerms.IncreaseAmount.HasValue ? entity.RentPaymentTerms.IncreaseAmount.Value : 0;
            ////每一年的租金
            //for (int year = 1; year < years; year++)
            //{
            //    var increaseItem = new PaymentIncreaseItem
            //    {
            //        YearNum = year,
            //        StartTime = startYear,
            //        EndTime = startYear.AddYears(1),
            //    };

            //    increaseItem.IncreaseValue = increaseValue;
            //    increaseItem.PaymentMonthlyAmount = PaymentMonthlyAmount;
            //    startYear = startYear.AddYears(1);

            //    //是否设置了递增
            //    if (year < startYears || !entity.RentPaymentTerms.IncreaseStartYears.HasValue)
            //    {
            //        entity.RentPaymentTerms.PaymentIncreaseInfo.IncreaseList.Add(increaseItem);
            //        continue;
            //    }
            //    //当前年度是否递增
            //    if (year == startYears)
            //    {
            //        if (increaseValue > 1)
            //        {
            //            PaymentMonthlyAmount += increaseValue;
            //        }
            //        else
            //        {
            //            PaymentMonthlyAmount += PaymentMonthlyAmount * increaseValue;
            //        }
            //        //启用间隔年份
            //        stepYears = 0;
            //    }
            //    else
            //    {
            //        stepYears += 1;
            //        if (stepYears == entity.RentPaymentTerms.IncreaseStepYears.Value)
            //        {
            //            if (increaseValue > 1)
            //            {
            //                PaymentMonthlyAmount += increaseValue;
            //            }
            //            else
            //            {
            //                PaymentMonthlyAmount += PaymentMonthlyAmount * increaseValue;
            //            }
            //            //到了间隔年度，重置间隔年
            //            stepYears = 0;
            //        }
            //    }
            //    increaseItem.PaymentMonthlyAmount = PaymentMonthlyAmount;
            //    entity.RentPaymentTerms.PaymentIncreaseInfo.IncreaseList.Add(increaseItem);
            //}
        }

        /// <summary>
        /// 构造扣点信息
        /// </summary>
        /// <param name="entity"></param>
        private static void BuildCommissionInfo(ContractEntity entity)
        {
            //entity.RentPaymentTerms.PaymentCommissionInfo = new PaymentCommissionInfo();
            //entity.RentPaymentTerms.PaymentCommissionInfo.AmountList = new List<PaymentCommissionAmountItem>();
            //entity.RentPaymentTerms.PaymentCommissionInfo.TimeList = new List<PaymentCommissionTimeItem>();
            //entity.RentPaymentTerms.PaymentCommissionInfo.WithTax = entity.RentPaymentTerms.CommissionWithTax.Value;
            //entity.RentPaymentTerms.PaymentCommissionInfo.CommissionRemark = "";
            //if (!entity.RentPaymentTerms.CommissionType.HasValue)
            //{
            //    return;
            //}
            //decimal rant = 0;
            //if (entity.RentPaymentTerms.CommissionType.Value == 1)
            //{
            //    #region 按营业额扣点
            //    decimal amount1 = 0;
            //    decimal amount2 = 0;
            //    //第一档
            //    if (decimal.TryParse(entity.RentPaymentTerms.CommissionRange1, out amount2) &&
            //        decimal.TryParse(entity.RentPaymentTerms.CommissionRate1, out rant))
            //    {
            //        var amountItem1 = new PaymentCommissionAmountItem
            //        {
            //            Amount1 = amount1,
            //            Amount2 = amount2,
            //            Rate = rant,
            //            AmountRemark = "",
            //        };
            //        entity.RentPaymentTerms.PaymentCommissionInfo.AmountList.Add(amountItem1);
            //    }
            //    //第二档
            //    amount1 = amount2;
            //    if (decimal.TryParse(entity.RentPaymentTerms.CommissionRange2, out amount2) &&
            //        decimal.TryParse(entity.RentPaymentTerms.CommissionRate2, out rant))
            //    {
            //        var amountItem2 = new PaymentCommissionAmountItem
            //        {
            //            Amount1 = amount1,
            //            Amount2 = amount2,
            //            Rate = rant,
            //            AmountRemark = "",
            //        };
            //        entity.RentPaymentTerms.PaymentCommissionInfo.AmountList.Add(amountItem2);
            //    }
            //    //第三档
            //    amount1 = amount2;
            //    if (decimal.TryParse(entity.RentPaymentTerms.CommissionRange3, out amount2) &&
            //        decimal.TryParse(entity.RentPaymentTerms.CommissionRate3, out rant))
            //    {
            //        var amountItem3 = new PaymentCommissionAmountItem
            //        {
            //            Amount1 = amount1,
            //            Amount2 = amount2,
            //            Rate = rant,
            //            AmountRemark = "",
            //        };
            //        entity.RentPaymentTerms.PaymentCommissionInfo.AmountList.Add(amountItem3);
            //    }
            //    #endregion
            //}
            //else if (entity.RentPaymentTerms.CommissionType.Value == 2)
            //{
            //    #region 按营业年限扣点

            //    int years = entity.ContractEndDate.Value.Year - entity.ContractStartDate.Value.Year;
            //    years = years == 0 ? 1 : years;
            //    years += 1;
                
            //    DateTime startTime = entity.ContractStartDate.Value;


            //    int yearNum = 0;
            //    decimal discount = 0;
            //    DateTime year1 = entity.ContractStartDate.Value;
            //    DateTime year2 = entity.ContractEndDate.Value;
            //    //第一档
            //    if (int.TryParse(entity.RentPaymentTerms.CommissionRange1, out yearNum) ||
            //        DateTime.TryParse(entity.RentPaymentTerms.CommissionRange1, out year2))
            //    {
            //        var timeItem1 = new PaymentCommissionTimeItem();

            //        if (yearNum > 0)
            //        {
            //            year1 = entity.ContractStartDate.Value;
            //            year2 = year1.AddYears(yearNum);
            //        }

            //        timeItem1.StartTime = year1;
            //        timeItem1.EndTime = year2;
            //        //简单扣点
            //        if (decimal.TryParse(entity.RentPaymentTerms.CommissionRate1, out rant))
            //        {
            //            timeItem1.Rate = rant;
            //        }
            //        else
            //        {
            //            //折扣扣点
            //            timeItem1.Rate = 0;
            //            timeItem1.DiscountRateList = new List<DiscountRateInfo>();
            //            string[] rateList1 = entity.RentPaymentTerms.CommissionRate1.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //            foreach (string rateItem in rateList1)
            //            {
            //                var rateInfo = new DiscountRateInfo();
            //                string[] numList = rateItem.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //                //折扣1
            //                decimal.TryParse(numList[0], out discount);
            //                rateInfo.Discount1 = discount;
            //                //折扣2
            //                decimal.TryParse(numList[1], out discount);
            //                rateInfo.Discount2 = discount;
            //                //扣点比例
            //                decimal.TryParse(numList[2], out rant);
            //                rateInfo.Rate = rant;
            //                timeItem1.DiscountRateList.Add(rateInfo);
            //            }
            //        }
            //        entity.RentPaymentTerms.PaymentCommissionInfo.TimeList.Add(timeItem1);
            //    }
            //    yearNum = 0;
            //    year1 = year2;
            //    //第二档
            //    if (int.TryParse(entity.RentPaymentTerms.CommissionRange2, out yearNum) ||
            //        DateTime.TryParse(entity.RentPaymentTerms.CommissionRange2, out year2))
            //    {
            //        var timeItem2 = new PaymentCommissionTimeItem();

            //        if (yearNum > 0)
            //        {
            //            year2 = year1.AddYears(yearNum);
            //        }

            //        timeItem2.StartTime = year1;
            //        timeItem2.EndTime = year2;
            //        //简单扣点
            //        if (decimal.TryParse(entity.RentPaymentTerms.CommissionRate2, out rant))
            //        {
            //            timeItem2.Rate = rant;
            //        }
            //        else
            //        {
            //            //折扣扣点
            //            timeItem2.Rate = 0;
            //            timeItem2.DiscountRateList = new List<DiscountRateInfo>();
            //            string[] rateList1 = entity.RentPaymentTerms.CommissionRate2.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //            foreach (string rateItem in rateList1)
            //            {
            //                var rateInfo = new DiscountRateInfo();
            //                string[] numList = rateItem.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //                //折扣1
            //                decimal.TryParse(numList[0], out discount);
            //                rateInfo.Discount1 = discount;
            //                //折扣2
            //                decimal.TryParse(numList[1], out discount);
            //                rateInfo.Discount2 = discount;
            //                //扣点比例
            //                decimal.TryParse(numList[2], out rant);
            //                rateInfo.Rate = rant;
            //                timeItem2.DiscountRateList.Add(rateInfo);
            //            }
            //        }
            //        entity.RentPaymentTerms.PaymentCommissionInfo.TimeList.Add(timeItem2);
            //    }
            //    yearNum = 0;
            //    year1 = year2;
            //    //第三档
            //    if (int.TryParse(entity.RentPaymentTerms.CommissionRange3, out yearNum) ||
            //        DateTime.TryParse(entity.RentPaymentTerms.CommissionRange3, out year2))
            //    {
            //        if (yearNum > 0)
            //        {
            //            year2 = year1.AddYears(yearNum);
            //        }

            //        var timeItem3 = new PaymentCommissionTimeItem();
            //        timeItem3.StartTime = year1;
            //        timeItem3.EndTime = year2;
            //        //简单扣点
            //        if (decimal.TryParse(entity.RentPaymentTerms.CommissionRate3, out rant))
            //        {
            //            timeItem3.Rate = rant;
            //        }
            //        else
            //        {
            //            //折扣扣点
            //            timeItem3.Rate = 0;
            //            timeItem3.DiscountRateList = new List<DiscountRateInfo>();
            //            string[] rateList1 = entity.RentPaymentTerms.CommissionRate3.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //            foreach (string rateItem in rateList1)
            //            {
            //                var rateInfo = new DiscountRateInfo();
            //                string[] numList = rateItem.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //                //折扣1
            //                decimal.TryParse(numList[0], out discount);
            //                rateInfo.Discount1 = discount;
            //                //折扣2
            //                decimal.TryParse(numList[1], out discount);
            //                rateInfo.Discount2 = discount;
            //                //扣点比例
            //                decimal.TryParse(numList[2], out rant);
            //                rateInfo.Rate = rant;
            //                timeItem3.DiscountRateList.Add(rateInfo);
            //            }
            //        }
            //        entity.RentPaymentTerms.PaymentCommissionInfo.TimeList.Add(timeItem3);
            //    }
            //    #endregion
            //}
        }
    }
}
