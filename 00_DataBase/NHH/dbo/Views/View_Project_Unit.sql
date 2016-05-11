CREATE VIEW [dbo].[View_Project_Unit]
AS
SELECT   dbo.Project_Unit.UnitID, dbo.Project_Unit.ProjectID, dbo.Project_Unit.BuildingID, dbo.Project_Unit.FloorID, 
                dbo.Project_Unit.UnitNumber, dbo.Project_Unit.UnitAera, dbo.Project_Unit.UnitStatus, dbo.Project_Unit.UnitType, 
                dbo.Project_Unit.BizTypeID, dbo.Project_Unit.BizCategoryID, dbo.Project_Unit.StoreID, dbo.Project_Unit.ContractStatus, 
                dbo.Project_Unit.ContractEndDate, dbo.Project_Unit.UnitMapFileName, dbo.Project_Unit.FloorMapLocation, 
                dbo.Project_Unit.FloorMapDimension, dbo.Project_Unit.Tag, dbo.Project_Unit.Status, dbo.Project_Unit.InDate, 
                dbo.Project_Unit.InUser, dbo.Project_Unit.EditDate, dbo.Project_Unit.EditUser, dbo.Project.ProjectName, 
                dbo.Project_Building.BuildingName, dbo.Project_Floor.FloorNumber, dbo.Project_Floor.FloorName, 
                dbo.Project_Floor.FloorMapFileName
FROM      dbo.Project INNER JOIN
                dbo.Project_Unit ON dbo.Project.ProjectID = dbo.Project_Unit.ProjectID INNER JOIN
                dbo.Project_Building ON dbo.Project_Unit.BuildingID = dbo.Project_Building.BuildingID INNER JOIN
                dbo.Project_Floor ON dbo.Project_Unit.FloorID = dbo.Project_Floor.FloorID


