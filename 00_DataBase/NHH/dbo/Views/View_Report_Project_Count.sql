
CREATE VIEW [dbo].[View_Report_Project_Count]
AS
SELECT   ProjectID, ProjectName, ISNULL(TotalArea, 0) AS TotalArea,
                    (SELECT   COUNT(UnitID) AS Expr1
                     FROM      dbo.Project_Unit AS PU WITH (Nolock)
                     WHERE   (ProjectID = P.ProjectID) AND (Status = 1)) AS TotalUnit,
                    (SELECT   ISNULL(SUM(ContractArea), 0) AS Expr1
                     FROM      dbo.Contract AS C WITH (Nolock)
                     WHERE   (ProjectID = P.ProjectID) AND (ContractStatus IN (3, 4)) AND (Status = 1)) AS SignedTotalArea,
                    (SELECT   COUNT(CU.UnitID) AS Expr1
                     FROM      dbo.Contract AS C WITH (Nolock) INNER JOIN
                                     dbo.Contract_Unit AS CU WITH (Nolock) ON C.ContractID = CU.ContractID
                     WHERE   (C.ProjectID = P.ProjectID) AND (C.ContractStatus IN (3, 4)) AND (C.Status = 1)) AS SignedTotalUnit
FROM      dbo.Project AS P WITH (nOLOCK)



