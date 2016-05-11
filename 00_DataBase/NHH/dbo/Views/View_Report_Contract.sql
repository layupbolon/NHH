
CREATE VIEW [dbo].[View_Report_Contract]
AS
SELECT   dbo.View_ContractStatus.ContractStatusName, dbo.Contract.ContractID, dbo.Contract.ContractType, 
                dbo.Contract.ContractStatus, dbo.Contract.ProjectID, dbo.Contract.MerchantID, dbo.Brand.BrandName, 
                dbo.Contract.DecorationLength, dbo.Contract.DecorationEndDate, dbo.Contract.ContractEndDate, 
                dbo.Contract.ContractLength, dbo.Contract.QualityBond, dbo.Contract.AdPointNum, dbo.Contract.ParkingLotNum, 
                dbo.Contract_Unit.UnitID, dbo.Contract_Unit.UnitAvgAera, dbo.Contract.ContractStartDate, dbo.Brand.BrandLevel, 
                dbo.Contract.ContractArea, dbo.Contract.ContractUnitRent, dbo.Contract.ContractMgmtFee, 
                dbo.Merchant_Brand.MerchantBrandID, dbo.Contract.ContractCode
FROM      dbo.Contract INNER JOIN
                dbo.View_ContractStatus ON dbo.Contract.ContractStatus = dbo.View_ContractStatus.ContractStatusID INNER JOIN
                dbo.Contract_Unit ON dbo.Contract.ContractID = dbo.Contract_Unit.ContractID INNER JOIN
                dbo.Brand INNER JOIN
                dbo.Merchant_Brand ON dbo.Brand.BrandID = dbo.Merchant_Brand.BrandID INNER JOIN
                dbo.Contract_Brand ON dbo.Merchant_Brand.MerchantBrandID = dbo.Contract_Brand.MerchantBrandID ON 
                dbo.Contract.ContractID = dbo.Contract_Brand.ContractID
