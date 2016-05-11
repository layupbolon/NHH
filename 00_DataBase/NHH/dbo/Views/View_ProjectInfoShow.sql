
CREATE VIEW [dbo].[View_ProjectInfoShow]
AS
WITH cte AS (SELECT   ProjectID, SUM(TotalConstructionArea) AS SumTotalConstructionArea, SUM(TotalRentArea) 
                                      AS TotalRentArea
                      FROM      dbo.Project_Building
                      GROUP BY ProjectID)
    SELECT   pro.ProjectID, pro.ProjectName, pro.RegionID,  pro.ProvinceID, pro.CityID, pro.Stage, 
                    pro.GrandOpeningDate, pro.ParkingLotNum, cte_1.SumTotalConstructionArea, cte_1.TotalRentArea, cp.CompanyName, 
                    sp.ProvinceName, sc.CityName, pro.AdPointNum, slocation.RegionName
    FROM      dbo.Project AS pro INNER JOIN
                    cte AS cte_1 ON cte_1.ProjectID = pro.ProjectID INNER JOIN
                    dbo.Project_Unit AS pu ON cte_1.ProjectID = pu.ProjectID LEFT OUTER JOIN
                    dbo.Company AS cp ON pro.ManageCompanyID = cp.CompanyID LEFT OUTER JOIN
                    dbo.Region AS slocation ON pro.RegionID = slocation.RegionID LEFT OUTER JOIN
                    dbo.Province AS sp ON sp.ProvinceID = pro.ProvinceID LEFT OUTER JOIN
                    dbo.City AS sc ON sc.CityID = pro.CityID
    WHERE   (pu.UnitType = 1) OR
                    (pu.UnitType = 2)
