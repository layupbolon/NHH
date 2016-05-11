CREATE TABLE [dbo].[Sys_Menu]
(
	[MenuID] INT NOT NULL IDENTITY, 
	[SeqNo] INT NOT NULL DEFAULT 99,
    [MenuIcon] VARCHAR(200) NULL, 
    [MenuName] NVARCHAR(200) NOT NULL, 
    [MenuUrl] NVARCHAR(200) NOT NULL, 
    [MenuDescription] NVARCHAR(500) NULL, 
    [ParentID] INT NULL , 
	[Status]              INT            DEFAULT ((1)) NOT NULL,
    [InDate]              DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]              INT            DEFAULT ((0)) NOT NULL,
    [EditDate]            DATETIME       NULL,
    [EditUser]            INT            NULL,
    CONSTRAINT [PK_Sys_Menu] PRIMARY KEY ([MenuID]), 
    CONSTRAINT [FK_Sys_Menu_Parent] FOREIGN KEY ([MenuID]) REFERENCES [Sys_Menu]([MenuID]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'菜单ID',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Sys_Menu',
    @level2type = N'COLUMN',
    @level2name = N'MenuID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'菜单图标（CSS 类名） 仅顶级菜单需要显示图标',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Sys_Menu',
    @level2type = N'COLUMN',
    @level2name = N'MenuIcon'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'菜单名称',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Sys_Menu',
    @level2type = N'COLUMN',
    @level2name = N'MenuName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'菜单链接',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Sys_Menu',
    @level2type = N'COLUMN',
    @level2name = N'MenuUrl'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'菜单描述',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Sys_Menu',
    @level2type = N'COLUMN',
    @level2name = N'MenuDescription'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'父菜单 默认为空，即顶级菜单',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Sys_Menu',
    @level2type = N'COLUMN',
    @level2name = N'ParentID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'状态',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Sys_Menu',
    @level2type = N'COLUMN',
    @level2name = N'Status'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'系统菜单列表',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Sys_Menu',
    @level2type = NULL,
    @level2name = NULL
GO

CREATE INDEX [IX_Sys_Menu_ParentID] ON [dbo].[Sys_Menu] (ParentID)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'菜单序号',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Sys_Menu',
    @level2type = N'COLUMN',
    @level2name = N'SeqNo'