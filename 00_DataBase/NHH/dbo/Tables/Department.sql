CREATE TABLE [dbo].[Department] (
    [DepartmentID]   INT           IDENTITY (100, 1) NOT NULL,
    [DepartmentName] NVARCHAR (50) NOT NULL,
    [Phone]          VARCHAR (25)  NULL,
    [ManagerID]      INT           NULL,
    [CompanyID]      INT           NOT NULL,
    [Status]         INT           DEFAULT ((1)) NOT NULL,
    [InDate]         DATETIME      DEFAULT (getdate()) NOT NULL,
    [InUser]         INT           DEFAULT ((0)) NOT NULL,
    [EditDate]       DATETIME      NULL,
    [EditUser]       INT           NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED ([DepartmentID] ASC),
    CONSTRAINT [FK_Company_Department] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID]),
    CONSTRAINT [FK_Department_Manager] FOREIGN KEY ([ManagerID]) REFERENCES [dbo].[Employee] ([EmployeeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Department_CompanyID]
    ON [dbo].[Department]([CompanyID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Department_Status]
    ON [dbo].[Department]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'部门信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Department';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'部门名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Department', @level2type = N'COLUMN', @level2name = N'DepartmentName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Department', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'部门主管', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Department', @level2type = N'COLUMN', @level2name = N'ManagerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属公司', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Department', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Department', @level2type = N'COLUMN', @level2name = N'Status';

