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
    /// 商户商铺
    /// </summary>
    public class MerchantStoreProcess
    {
        /// <summary>
        /// 保存商户商铺
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="entity"></param>
        public static void SaveMerchantStore(SqlTransaction trans, ContractEntity entity)
        {
            #region Merchant_Store
            entity.MerchantStore.StoreName = entity.BrandNames;
            entity.MerchantStore.BrandName = entity.BrandNames;
            entity.MerchantStore.RentArea = entity.ContractArea;
            entity.MerchantStore.RentContractID = entity.ContractID;
            string strCmd = @"INSERT INTO [dbo].[Merchant_Store]
                                   ([StoreName]
                                   ,[MerchantID]
                                   ,[BrandName]
                                   ,[BizTypeID]
                                   ,[BizCategoryID]
                                   ,[RentArea]
                                   ,[RentStartDate]
                                   ,[RentEndDate]
                                   ,[RentLength]
                                   ,[RentContractID]
                                   ,[Status]
                                   ,[InDate]
                                   ,[InUser]
                                   ,[EditDate]
                                   ,[EditUser])
                             VALUES
                                   (@StoreName
                                   ,@MerchantID
                                   ,@BrandName
                                   ,@BizTypeID
                                   ,@BizCategoryID
                                   ,@RentArea
                                   ,@RentStartDate
                                   ,@RentEndDate
                                   ,@RentLength
                                   ,@RentContractID
                                   ,@Status
                                   ,@InDate
                                   ,@InUser
                                   ,@EditDate
                                   ,@EditUser);
                            Select SCOPE_IDENTITY();";
            var paramList = new SqlParameter[]
                {
                    new SqlParameter("@StoreName" ,entity.MerchantStore.StoreName),
                    new SqlParameter("@MerchantID" ,entity.MerchantStore.MerchantID),
                    new SqlParameter("@BrandName", entity.MerchantStore.BrandName),
                    new SqlParameter("@BizTypeID" ,entity.MerchantStore.BizTypeID),
                    new SqlParameter("@BizCategoryID" ,entity.MerchantStore.BizCategoryID),
                    new SqlParameter("@RentArea" ,entity.MerchantStore.RentArea),
                    new SqlParameter("@RentStartDate" ,entity.MerchantStore.RentStartDate),
                    new SqlParameter("@RentEndDate" ,entity.MerchantStore.RentEndDate),
                    new SqlParameter("@RentLength" ,entity.MerchantStore.RentLength),
                    new SqlParameter("@RentContractID" ,entity.MerchantStore.RentContractID),
                    new SqlParameter("@Status" ,entity.MerchantStore.Status),
                    new SqlParameter("@InDate" ,entity.MerchantStore.InDate),
                    new SqlParameter("@InUser" ,entity.MerchantStore.InUser),
                    new SqlParameter("@EditDate" ,entity.MerchantStore.EditDate),
                    new SqlParameter("@EditUser" ,entity.MerchantStore.EditUser),
                };
            #endregion
            entity.MerchantStore.StoreID = SqlHelper.ExecuteScalar(trans, strCmd, paramList);
        }
    }
}
