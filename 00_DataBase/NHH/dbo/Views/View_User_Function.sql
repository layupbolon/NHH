CREATE VIEW [dbo].[View_User_Function]
AS
    SELECT  DISTINCT
            u.UserID ,
            f.FunctionKey
    FROM    [dbo].[Sys_User] u
            JOIN [dbo].[Sys_User_Role] ur ON u.UserID = ur.UserID
                                             AND u.Status >= 1
            JOIN [dbo].[Sys_Role] r ON ur.RoleID = r.RoleID
                                       AND r.Status = 1
            JOIN [dbo].[Sys_Role_Function] rf ON r.RoleID = rf.RoleID
            JOIN [dbo].[Sys_Function] f ON rf.FunctionID = f.FunctionID
                                           AND f.Status = 1;

