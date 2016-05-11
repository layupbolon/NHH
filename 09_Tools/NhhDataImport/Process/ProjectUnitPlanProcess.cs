using NhhDataImport.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhhDataImport.Process
{
    /// <summary>
    /// 商铺筹划
    /// </summary>
    public class ProjectUnitPlanProcess
    {
        /// <summary>
        /// 保存商铺筹划
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="entity"></param>
        public static void SaveProjectUnitPlan(SqlTransaction trans, ProjectUnitEntity entity)
        {
            #region Project_UnitPlan
            string strCmd = @"INSERT INTO [dbo].[Project_UnitPlan]
                                   ([UnitID]
                                   ,[BizTypeID]
                                   ,[BizCategoryID]
                                   ,[UnitType]
                                   ,[IsBenchmarking]
                                   ,[StandardRent]
                                   ,[RecommendedRent]
                                   ,[QuotationRent]
                                   ,[StandardMgmtFee]
                                   ,[RentLengthUpper]
                                   ,[RentLengthBottom]
                                   ,[IncreaseType]
                                   ,[IncreaseAmountType]
                                   ,[IncreaseAmount]
                                   ,[IncreaseStartYears]
                                   ,[IncreaseStepYears]
                                   ,[DepositMonthly]
                                   ,[PaymentPeriod]
                                   ,[BidBond]
                                   ,[ManageBond]
                                   ,[RentFreeLength]
                                   ,[DecorationLength]
                                   ,[DecorationMgmtFee]
                                   ,[DecorationBond]
                                   ,[QualityBond]
                                   ,[ParkingLotNum]
                                   ,[AdPointNum]
                                   ,[Condition]
                                   ,[Status]
                                   ,[InDate]
                                   ,[InUser]
                                   ,[EditDate]
                                   ,[EditUser])
                             VALUES
                                   (@UnitID
                                   ,@BizTypeID
                                   ,@BizCategoryID
                                   ,@UnitType
                                   ,@IsBenchmarking
                                   ,@StandardRent
                                   ,@RecommendedRent
                                   ,@QuotationRent
                                   ,@StandardMgmtFee
                                   ,@RentLengthUpper
                                   ,@RentLengthBottom
                                   ,@IncreaseType
                                   ,@IncreaseAmountType
                                   ,@IncreaseAmount
                                   ,@IncreaseStartYears
                                   ,@IncreaseStepYears
                                   ,@DepositMonthly
                                   ,@PaymentPeriod
                                   ,@BidBond
                                   ,@ManageBond
                                   ,@RentFreeLength
                                   ,@DecorationLength
                                   ,@DecorationMgmtFee
                                   ,@DecorationBond
                                   ,@QualityBond
                                   ,@ParkingLotNum
                                   ,@AdPointNum
                                   ,@Condition
                                   ,@Status
                                   ,@InDate
                                   ,@InUser
                                   ,@EditDate
                                   ,@EditUser)";
            var paramList = new SqlParameter[]
            {
                new SqlParameter("@UnitID", entity.UnitID),
                new SqlParameter("@BizTypeID", entity.BizTypeID),
                new SqlParameter("@BizCategoryID", entity.BizCategoryID),
                new SqlParameter("@UnitType", entity.UnitType),
                new SqlParameter("@IsBenchmarking", entity.UnitPlan.IsBenchmarking),
                new SqlParameter("@StandardRent", entity.UnitPlan.StandardRent),
                new SqlParameter("@RecommendedRent", entity.UnitPlan.RecommendedRent),
                new SqlParameter("@QuotationRent", entity.UnitPlan.QuotationRent),
                new SqlParameter("@StandardMgmtFee", entity.UnitPlan.StandardMgmtFee),
                new SqlParameter("@RentLengthUpper", entity.UnitPlan.RentLengthUpper),
                new SqlParameter("@RentLengthBottom", entity.UnitPlan.RentLengthBottom),
                new SqlParameter("@IncreaseType", entity.UnitPlan.IncreaseType),
                new SqlParameter("@IncreaseAmountType", entity.UnitPlan.IncreaseAmountType),
                new SqlParameter("@IncreaseAmount", entity.UnitPlan.IncreaseAmount),
                new SqlParameter("@IncreaseStartYears", entity.UnitPlan.IncreaseStartYears),
                new SqlParameter("@IncreaseStepYears", entity.UnitPlan.IncreaseStepYears),
                new SqlParameter("@DepositMonthly", entity.UnitPlan.DepositMonthly),
                new SqlParameter("@PaymentPeriod", entity.UnitPlan.PaymentPeriod),
                new SqlParameter("@BidBond", entity.UnitPlan.BidBond),
                new SqlParameter("@ManageBond", entity.UnitPlan.ManageBond),
                new SqlParameter("@RentFreeLength", entity.UnitPlan.RentFreeLength),
                new SqlParameter("@DecorationLength", entity.UnitPlan.DecorationLength),
                new SqlParameter("@DecorationMgmtFee", entity.UnitPlan.DecorationMgmtFee),
                new SqlParameter("@DecorationBond", entity.UnitPlan.DecorationBond),
                new SqlParameter("@QualityBond", entity.UnitPlan.QualityBond),
                new SqlParameter("@ParkingLotNum", entity.UnitPlan.ParkingLotNum),
                new SqlParameter("@AdPointNum", entity.UnitPlan.AdPointNum),
                new SqlParameter("@Condition", entity.UnitPlan.Condition),
                new SqlParameter("@Status", entity.UnitPlan.Status),
                new SqlParameter("@InDate", entity.UnitPlan.InDate),
                new SqlParameter("@InUser", entity.UnitPlan.InUser),
                new SqlParameter("@EditDate", entity.UnitPlan.EditDate),
                new SqlParameter("@EditUser", entity.UnitPlan.EditUser),
            };
            #endregion

            SqlHelper.ExecuteNonQuery(trans, strCmd, paramList);
        }

        /// <summary>
        /// 更新商铺筹划
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="entity"></param>
        public static void UpdateProjectUnitPlan(SqlTransaction trans, ProjectUnitEntity entity)
        {
            string strCmd = @"UPDATE [dbo].[Project_UnitPlan]
                               SET [BizTypeID] = @BizTypeID
                                  ,[UnitType] = @UnitType
                                  ,[IsBenchmarking] = @IsBenchmarking
                                  ,[StandardRent] = @StandardRent
                                  ,[RecommendedRent] = @RecommendedRent
                                  ,[QuotationRent] = @QuotationRent
                                  ,[StandardMgmtFee] = @StandardMgmtFee
                                  ,[RentLengthUpper] = @RentLengthUpper
                                  ,[RentLengthBottom] = @RentLengthBottom
                                  ,[IncreaseType] = @IncreaseType
                                  ,[IncreaseAmountType] = @IncreaseAmountType
                                  ,[IncreaseAmount] = @IncreaseAmount
                                  ,[IncreaseStartYears] = @IncreaseStartYears
                                  ,[IncreaseStepYears] = @IncreaseStepYears
                                  ,[DepositMonthly] = @DepositMonthly
                                  ,[PaymentPeriod] = @PaymentPeriod
                                  ,[BidBond] = @BidBond
                                  ,[ManageBond] = @ManageBond
                                  ,[RentFreeLength] = @RentFreeLength
                                  ,[DecorationLength] = @DecorationLength
                                  ,[DecorationMgmtFee] = @DecorationMgmtFee
                                  ,[DecorationBond] = @DecorationBond
                                  ,[QualityBond] = @QualityBond
                                  ,[ParkingLotNum] = @ParkingLotNum
                                  ,[AdPointNum] = @AdPointNum
                                  ,[Condition] = @Condition  
                                  ,[EditDate] = @EditDate
                                  ,[EditUser] = @EditUser
                             WHERE [UnitID] = @UnitID";
            var paramList = new SqlParameter[]
            {
                new SqlParameter("@UnitID", entity.UnitID),
                new SqlParameter("@BizTypeID", entity.UnitPlan.BizTypeID),
                new SqlParameter("@UnitType", entity.UnitPlan.UnitType),
                new SqlParameter("@IsBenchmarking", entity.UnitPlan.IsBenchmarking),
                new SqlParameter("@StandardRent", entity.UnitPlan.StandardRent),
                new SqlParameter("@RecommendedRent", entity.UnitPlan.RecommendedRent),
                new SqlParameter("@QuotationRent", entity.UnitPlan.QuotationRent),
                new SqlParameter("@StandardMgmtFee", entity.UnitPlan.StandardMgmtFee),
                new SqlParameter("@RentLengthUpper", entity.UnitPlan.RentLengthUpper),
                new SqlParameter("@RentLengthBottom", entity.UnitPlan.RentLengthBottom),
                new SqlParameter("@IncreaseType", entity.UnitPlan.IncreaseType),
                new SqlParameter("@IncreaseAmountType", entity.UnitPlan.IncreaseAmountType),
                new SqlParameter("@IncreaseAmount", entity.UnitPlan.IncreaseAmount),
                new SqlParameter("@IncreaseStartYears", entity.UnitPlan.IncreaseStartYears),
                new SqlParameter("@IncreaseStepYears", entity.UnitPlan.IncreaseStepYears),
                new SqlParameter("@DepositMonthly", entity.UnitPlan.DepositMonthly),
                new SqlParameter("@PaymentPeriod", entity.UnitPlan.PaymentPeriod),
                new SqlParameter("@BidBond", entity.UnitPlan.BidBond),
                new SqlParameter("@ManageBond", entity.UnitPlan.ManageBond),
                new SqlParameter("@RentFreeLength", entity.UnitPlan.RentFreeLength),
                new SqlParameter("@DecorationLength", entity.UnitPlan.DecorationLength),
                new SqlParameter("@DecorationMgmtFee", entity.UnitPlan.DecorationMgmtFee),
                new SqlParameter("@DecorationBond", entity.UnitPlan.DecorationBond),
                new SqlParameter("@QualityBond", entity.UnitPlan.QualityBond),
                new SqlParameter("@ParkingLotNum", entity.UnitPlan.ParkingLotNum),
                new SqlParameter("@AdPointNum", entity.UnitPlan.AdPointNum),
                new SqlParameter("@Condition", entity.UnitPlan.Condition),
                new SqlParameter("@EditDate", entity.UnitPlan.EditDate),
                new SqlParameter("@EditUser", entity.UnitPlan.EditUser),
            };

            SqlHelper.ExecuteNonQuery(trans, strCmd, paramList);
        }
    }
}
