
CREATE VIEW [dbo].[View_Project_AdPoint]
AS
SELECT   dbo.Project_AdPoint.AdPointID, dbo.Project_AdPoint.ProjectID, dbo.Project_AdPoint.BuildingID, 
                dbo.Project_AdPoint.FloorID, dbo.Project_AdPoint.AdPointNumber, dbo.Project_AdPoint.Location, 
                dbo.Project_AdPoint.FloorMapLocation, dbo.Project_AdPoint.AdPointType, dbo.Project_AdPoint.Rent, 
                dbo.Project_AdPoint.ContractStatus, dbo.Project_AdPoint.ContractEnd, dbo.Project_AdPoint.Status, 
                dbo.Project_AdPoint.InDate, dbo.Project_AdPoint.InUser, dbo.Project_AdPoint.EditDate, dbo.Project_AdPoint.EditUser, 
                dbo.Project.ProjectName, dbo.Project_Building.BuildingName, dbo.Project_Floor.FloorNumber, 
                dbo.Project_Floor.FloorMapFileName,dbo.Project_Floor.FloorName,dbo.Project_AdPoint.AdPointMedia
FROM      dbo.Project_Floor INNER JOIN
                dbo.Project_Building INNER JOIN
                dbo.Project_AdPoint INNER JOIN
                dbo.Project ON dbo.Project_AdPoint.ProjectID = dbo.Project.ProjectID ON 
                dbo.Project_Building.BuildingID = dbo.Project_AdPoint.BuildingID ON 
                dbo.Project_Floor.FloorID = dbo.Project_AdPoint.FloorID

