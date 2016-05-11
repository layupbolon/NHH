CREATE VIEW [dbo].[View_Repair_Complaint_Supervisor]
AS
SELECT   dbo.Project_BizConfig.*,dbo.Department.ManagerID, dbo.Sys_User.UserID AS GovernorUserId, dbo.Sys_User.UserName AS GovernorUserName, 
                Sys_User_1.UserID AS ManagerUserId, Sys_User_1.UserName AS ManagerUserName
FROM      dbo.Project_BizConfig LEFT OUTER JOIN
                dbo.Sys_User AS Sys_User_1 RIGHT OUTER JOIN
                dbo.Department ON Sys_User_1.EmployeeID = dbo.Department.ManagerID ON 
                dbo.Project_BizConfig.Param2 = convert(varchar(500),dbo.Department.DepartmentID) LEFT OUTER JOIN
                dbo.Sys_User ON dbo.Project_BizConfig.Param3 = convert(varchar(500),dbo.Sys_User.EmployeeID)