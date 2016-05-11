CREATE TABLE [dbo].[Sys_User] (
    [UserID]        INT           IDENTITY (1, 1) NOT NULL,
    [EmployeeID]    INT           NULL,
    [UserType]      INT           DEFAULT ((1)) NOT NULL,
    [UserName]      VARCHAR (50)  NOT NULL,
    [Password]      VARCHAR (200) NOT NULL,
    [LastLoginTime] DATETIME      NULL,
    [LastLoginIP]   VARCHAR (200) NULL,
    [Status]        INT           DEFAULT ((1)) NOT NULL,
    [InDate]        DATETIME      DEFAULT (getdate()) NOT NULL,
    [InUser]        INT           DEFAULT ((0)) NOT NULL,
    [EditDate]      DATETIME      NULL,
    [EditUser]      INT           NULL,
    CONSTRAINT [PK_Sys_User] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_Sys_User_Employee] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employee] ([EmployeeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_User_EmployeeID]
    ON [dbo].[Sys_User]([EmployeeID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_User_Password]
    ON [dbo].[Sys_User]([Password] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_User_Status]
    ON [dbo].[Sys_User]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_User_UserName]
    ON [dbo].[Sys_User]([UserName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_User_UserType]
    ON [dbo].[Sys_User]([UserType] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User', @level2type = N'COLUMN', @level2name = N'UserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'员工ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User', @level2type = N'COLUMN', @level2name = N'EmployeeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User', @level2type = N'COLUMN', @level2name = N'UserType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'登录名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User', @level2type = N'COLUMN', @level2name = N'UserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'登录密码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User', @level2type = N'COLUMN', @level2name = N'Password';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后登录时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User', @level2type = N'COLUMN', @level2name = N'LastLoginTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后登录IP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User', @level2type = N'COLUMN', @level2name = N'LastLoginIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_User', @level2type = N'COLUMN', @level2name = N'Status';

