CREATE TABLE [dbo].[Sys_Role_Function] (
    [RoleFunctionID] INT      IDENTITY (1, 1) NOT NULL,
    [RoleID]         INT      NOT NULL,
    [FunctionID]     INT      NOT NULL,
    [InDate]         DATETIME DEFAULT (getdate()) NOT NULL,
    [InUser]         INT      DEFAULT ((0)) NOT NULL,
    [EditDate]       DATETIME NULL,
    [EditUser]       INT      NULL,
    CONSTRAINT [PK_Sys_Role_Function] PRIMARY KEY CLUSTERED ([RoleFunctionID] ASC),
    CONSTRAINT [FK_Sys_Role_Function_Function] FOREIGN KEY ([FunctionID]) REFERENCES [dbo].[Sys_Function] ([FunctionID]),
    CONSTRAINT [FK_Sys_Role_Function_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Sys_Role] ([RoleID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_Role_Function_FunctionID]
    ON [dbo].[Sys_Role_Function]([FunctionID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sys_Role_Function_RoleID]
    ON [dbo].[Sys_Role_Function]([RoleID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_Role_Function', @level2type = N'COLUMN', @level2name = N'RoleID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'功能ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Sys_Role_Function', @level2type = N'COLUMN', @level2name = N'FunctionID';

