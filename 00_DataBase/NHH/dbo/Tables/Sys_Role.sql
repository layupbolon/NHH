CREATE TABLE [dbo].[Sys_Role] (
    [RoleID]   INT           IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (25) NOT NULL,
    [Status]   INT           DEFAULT ((1)) NOT NULL,
    [InDate]   DATETIME      DEFAULT (getdate()) NOT NULL,
    [InUser]   INT           DEFAULT ((0)) NOT NULL,
    [EditDate] DATETIME      NULL,
    [EditUser] INT           NULL,
    CONSTRAINT [PK_Sys_Role] PRIMARY KEY CLUSTERED ([RoleID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_Role_Status]
    ON [dbo].[Sys_Role]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_Role', @level2type = N'COLUMN', @level2name = N'RoleID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_Role', @level2type = N'COLUMN', @level2name = N'RoleName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_Role', @level2type = N'COLUMN', @level2name = N'Status';

