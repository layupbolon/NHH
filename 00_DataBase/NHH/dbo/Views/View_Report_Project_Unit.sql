
CREATE VIEW [dbo].[View_Report_Project_Unit]
AS
SELECT   dbo.Project_UnitPlan.BizTypeID, dbo.Project_UnitPlan.BizCategoryID, dbo.Project_UnitPlan.StandardRent, 
                dbo.Project_UnitPlan.IsBenchmarking, dbo.Project_UnitPlan.UnitType, dbo.BizType.BizTypeName, 
                dbo.View_UnitType.UnitTypeName, dbo.BizCategory.BizCategoryName, dbo.View_Project_Unit.BuildingName, 
                dbo.View_Project_Unit.FloorNumber, dbo.View_Project_Unit.UnitID, dbo.View_Project_Unit.BuildingID, 
                dbo.View_Project_Unit.FloorID, dbo.View_Project_Unit.UnitNumber, dbo.View_Project_Unit.UnitAera, 
                dbo.View_Project_Unit.ProjectID, dbo.View_Project_Unit.ContractStatus
FROM      dbo.View_Project_Unit INNER JOIN
                dbo.BizType INNER JOIN
                dbo.Project_UnitPlan ON dbo.BizType.BizTypeID = dbo.Project_UnitPlan.BizTypeID INNER JOIN
                dbo.View_UnitType ON dbo.Project_UnitPlan.UnitType = dbo.View_UnitType.UnitTypeID ON 
                dbo.View_Project_Unit.UnitID = dbo.Project_UnitPlan.UnitID LEFT OUTER JOIN
                dbo.BizCategory ON dbo.Project_UnitPlan.BizCategoryID = dbo.BizCategory.BizCategoryID



