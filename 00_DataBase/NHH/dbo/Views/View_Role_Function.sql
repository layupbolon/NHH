CREATE VIEW [dbo].[View_Role_Function]
AS
    SELECT  r.[RoleID] ,
            f.[FunctionID] ,
            f.[FunctionKey],
			f.[FunctionName],
			f.[FunctionDescription]
    FROM    [dbo].[Sys_Role_Function] rf
            JOIN [dbo].[Sys_Role] r ON rf.[RoleID] = r.[RoleID]
                                       AND r.[Status] = 1
            JOIN [dbo].[Sys_Function] f ON rf.[FunctionID] = f.[FunctionID]
                                           AND f.[Status] = 1;

