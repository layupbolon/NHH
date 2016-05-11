
CREATE VIEW [dbo].[View_Project_Kiosk]
AS
SELECT   dbo.Project_Kiosk.KioskID, dbo.Project_Kiosk.ProjectID, dbo.Project_Kiosk.BuildingID, dbo.Project_Kiosk.FloorID, 
                dbo.Project_Kiosk.KioskNumber, dbo.Project_Kiosk.Location, dbo.Project_Kiosk.FloorMapLocation, 
                dbo.Project_Kiosk.Area, dbo.Project_Kiosk.BizTypeID, dbo.Project_Kiosk.OccupyRate, dbo.Project_Kiosk.Rent, 
                dbo.Project_Kiosk.ContractStatus, dbo.Project_Kiosk.ContractEnd, dbo.Project_Kiosk.Status, dbo.Project_Kiosk.InDate, 
                dbo.Project_Kiosk.InUser, dbo.Project_Kiosk.EditDate, dbo.Project_Kiosk.EditUser, dbo.Project.ProjectName, 
                dbo.Project_Building.BuildingName, dbo.Project_Floor.FloorNumber, dbo.Project_Floor.FloorMapFileName,dbo.Project_Floor.FloorName,
				dbo.Project_Kiosk.KioskType
FROM      dbo.Project_Building INNER JOIN
                dbo.Project INNER JOIN
                dbo.Project_Kiosk ON dbo.Project.ProjectID = dbo.Project_Kiosk.ProjectID ON 
                dbo.Project_Building.BuildingID = dbo.Project_Kiosk.BuildingID INNER JOIN
                dbo.Project_Floor ON dbo.Project_Kiosk.FloorID = dbo.Project_Floor.FloorID

