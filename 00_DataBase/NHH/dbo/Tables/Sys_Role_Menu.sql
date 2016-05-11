CREATE TABLE [dbo].[Sys_Role_Menu]
(
	[RoleMenuID] INT NOT NULL IDENTITY, 
    [RoleID] INT NOT NULL , 
    [MenuID] INT NOT NULL , 
	[InDate]              DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]              INT            DEFAULT ((0)) NOT NULL,
    [EditDate]            DATETIME       NULL,
    [EditUser]            INT            NULL,
    CONSTRAINT [PK_Sys_Role_Menu] PRIMARY KEY ([RoleMenuID]), 
    CONSTRAINT [FK_Sys_Role_Menu_Role] FOREIGN KEY (RoleID) REFERENCES Sys_Role(RoleID), 
    CONSTRAINT [FK_Sys_Role_Menu_Menu] FOREIGN KEY (MenuID) REFERENCES Sys_Menu(MenuID) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'系统角色菜单表',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Sys_Role_Menu',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'菜单ID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Sys_Role_Menu',
    @level2type = N'COLUMN',
    @level2name = N'MenuID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'角色ID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Sys_Role_Menu',
    @level2type = N'COLUMN',
    @level2name = N'RoleID'