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
    /// 商户品牌
    /// </summary>
    public class MerchantBrandProcess
    {
        /// <summary>
        /// 获取商户品牌ID
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void SaveMerchantBrand(ContractEntity entity)
        {
            foreach (var merchantBrand in entity.MerchantBrandList)
            {
                int merchantBrandId = SaveMerchantBrand(merchantBrand);
                var contractBrand = new ContractBrandEntity
                {
                    MerchantBrandID = merchantBrandId,
                };
                entity.ContractBrandList.Add(contractBrand);
            }
        }


        /// <summary>
        /// 保存商户品牌
        /// </summary>
        /// <param name="merchantBrand"></param>
        /// <returns></returns>
        public static int SaveMerchantBrand(MerchantBrandEntity merchantBrand)
        {
            string strCmd = string.Format(@"SELECT TOP 1 [MerchantBrandID]
                                              FROM [dbo].[Merchant_Brand](Nolock)
                                              WHere MerchantID={0} and BrandID={1}", merchantBrand.MerchantID, merchantBrand.BrandID);
            int id = SqlHelper.ExecuteScalar(strCmd);
            if (id > 0)
                return id;

            merchantBrand.Authorized = 1;
            merchantBrand.AuthorizedSince = DateTime.Now;
            merchantBrand.AuthorizedTo = DateTime.Now.AddYears(100);
            merchantBrand.OperationSince = DateTime.Now;
            merchantBrand.OperationTo = DateTime.Now.AddYears(100);

            #region Merchant_Brand
            string strCmd0 = @"INSERT INTO [dbo].[Merchant_Brand]
                                   ([MerchantID]
                                   ,[BrandID]
                                   ,[BrandName]
                                   ,[BrandType]
                                   ,[Authorized]
                                   ,[AuthorizedFile]
                                   ,[AuthorizedSince]
                                   ,[AuthorizedTo]
                                   ,[OperationSince]
                                   ,[OperationTo]
                                   ,[Revenue]
                                   ,[Status]
                                   ,[InDate]
                                   ,[InUser]
                                   ,[EditDate]
                                   ,[EditUser])
                             VALUES
                                   (@MerchantID
                                   ,@BrandID
                                   ,@BrandName
                                   ,@BrandType
                                   ,@Authorized
                                   ,@AuthorizedFile
                                   ,@AuthorizedSince
                                   ,@AuthorizedTo
                                   ,@OperationSince
                                   ,@OperationTo
                                   ,@Revenue
                                   ,@Status
                                   ,@InDate
                                   ,@InUser
                                   ,@EditDate
                                   ,@EditUser);
                            Select SCOPE_IDENTITY();";
            var paramList = new SqlParameter[]
            {
                new SqlParameter("@MerchantID", merchantBrand.MerchantID),
                new SqlParameter("@BrandID", merchantBrand.BrandID),
                new SqlParameter("@BrandName", merchantBrand.BrandName),
                new SqlParameter("@BrandType", merchantBrand.BrandType),
                new SqlParameter("@Authorized", merchantBrand.Authorized),
                new SqlParameter("@AuthorizedFile", merchantBrand.AuthorizedFile),
                new SqlParameter("@AuthorizedSince", merchantBrand.AuthorizedSince),
                new SqlParameter("@AuthorizedTo", merchantBrand.AuthorizedTo),
                new SqlParameter("@OperationSince", merchantBrand.OperationSince),
                new SqlParameter("@OperationTo", merchantBrand.OperationTo),
                new SqlParameter("@Revenue", merchantBrand.Revenue),
                new SqlParameter("@Status", merchantBrand.Status),
                new SqlParameter("@InDate", merchantBrand.InDate),
                new SqlParameter("@InUser", merchantBrand.InUser),
                new SqlParameter("@EditDate", merchantBrand.EditDate),
                new SqlParameter("@EditUser", merchantBrand.EditUser),
            };
            #endregion

            return SqlHelper.ExecuteScalar(strCmd0, paramList);
        }
    }
}
