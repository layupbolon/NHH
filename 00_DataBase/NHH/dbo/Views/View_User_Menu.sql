CREATE VIEW [dbo].[View_User_Menu]
	AS 
	SELECT  DISTINCT
            u.UserID ,
			m.MenuID,
			m.ParentID,
			m.SeqNo,
            m.MenuIcon,
			m.MenuName,
			m.MenuUrl
    FROM    [dbo].[Sys_User] u
            JOIN [dbo].[Sys_User_Role] ur ON u.UserID = ur.UserID
                                             AND u.Status >= 1
            JOIN [dbo].[Sys_Role] r ON ur.RoleID = r.RoleID
                                       AND r.Status = 1
            JOIN [dbo].[Sys_Role_Menu] rm ON r.RoleID = rm.RoleID
            JOIN [dbo].[Sys_Menu] m ON rm.MenuID = m.MenuID
                                           AND m.Status = 1;
