

CREATE VIEW [dbo].[View_Project_Unit_Contract]
AS
SELECT 
a.UnitID, a.UnitNumber, a.UnitAera, a.BizTypeID, a.ContractStatus, c.ContractStatusName, d.UnitTypeName, e.UnitStatusName,
b.BrandName, ContractStartDate = CONVERT(VARCHAR,b.ContractStartDate,111),ContractEndDate = CONVERT(VARCHAR,b.ContractEndDate,111),b.ContractArea, b.ContractUnitRent, b.ContractMgmtFee, b.DecorationLength, b.DecorationEndDate,
b.ContractLength,b.QualityBond,b.AdPointNum, b.ParkingLotNum
FROM [dbo].Project_Unit a 
LEFT OUTER JOIN View_Report_Contract b ON a.unitid=b.unitid
LEFT OUTER JOIN View_ContractStatus c ON a.ContractStatus = c.ContractStatusID
LEFT OUTER JOIN View_UnitType d ON a.UnitType = d.UnitTypeID
LEFT OUTER JOIN View_UnitStatus e ON a.UnitStatus = e.UnitStatusID





