CREATE TABLE [dbo].[Sys_User_Role] (
    [UserRoleID] INT      IDENTITY (1, 1) NOT NULL,
    [UserID]     INT      NOT NULL,
    [RoleID]     INT      NOT NULL,
    [InDate]     DATETIME DEFAULT (getdate()) NOT NULL,
    [InUser]     INT      DEFAULT ((0)) NOT NULL,
    [EditDate]   DATETIME NULL,
    [EditUser]   INT      NULL,
    CONSTRAINT [PK_Sys_User_Role] PRIMARY KEY CLUSTERED ([UserRoleID] ASC),
    CONSTRAINT [FK_Sys_User_Role_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Sys_Role] ([RoleID]),
    CONSTRAINT [FK_Sys_User_Role_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Sys_User] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_User_Role_RoleID]
    ON [dbo].[Sys_User_Role]([RoleID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_User_Role_UserID]
    ON [dbo].[Sys_User_Role]([UserID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User_Role', @level2type = N'COLUMN', @level2name = N'UserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User_Role', @level2type = N'COLUMN', @level2name = N'RoleID';

