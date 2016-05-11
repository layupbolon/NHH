CREATE VIEW [dbo].[View_Role_Menu]
	AS SELECT r.[RoleID] ,
            m.MenuID ,
			m.ParentID,
			m.SeqNo,
            m.MenuIcon,
			m.MenuName,
			m.MenuUrl,
			m.MenuDescription
		FROM Sys_Role_Menu rm  
			join  Sys_Role r on rm.RoleID=r.RoleID  AND r.[Status] = 1
			join Sys_Menu m on rm.MenuID=m.MenuID  AND m.[Status] = 1
