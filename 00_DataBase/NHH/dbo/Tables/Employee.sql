CREATE TABLE [dbo].[Employee] (
    [EmployeeID]   INT            IDENTITY (1000, 1) NOT NULL,
    [EmployeeCode] VARCHAR (10)   NULL,
    [EmployeeName] NVARCHAR (50)  NOT NULL,
    [Title]        NVARCHAR (50)  NULL,
    [Email]        VARCHAR (100)  NULL,
    [Moblie]       VARCHAR (50)   NULL,
    [Phone]        VARCHAR (25)   NULL,
    [Ext]          VARCHAR (10)   NULL,
    [Gender]       INT            DEFAULT ((0)) NOT NULL,
    [Birthday]     DATETIME       NULL,
    [IDNumber]     VARCHAR (25)   NULL,
    [Tag]          NVARCHAR (100) NULL,
    [SupervisorID] INT            NULL,
    [DepartmentID] INT            NOT NULL,
    [CompanyID]    INT            NOT NULL,
    [Status]       INT            DEFAULT ((1)) NOT NULL,
    [InDate]       DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]       INT            DEFAULT ((0)) NOT NULL,
    [EditDate]     DATETIME       NULL,
    [EditUser]     INT            NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([EmployeeID] ASC),
    CONSTRAINT [FK_Employee_Company] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID]),
    CONSTRAINT [FK_Employee_Department] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[Department] ([DepartmentID]),
    CONSTRAINT [FK_Employee_Supervisor] FOREIGN KEY ([SupervisorID]) REFERENCES [dbo].[Employee] ([EmployeeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Employee_CompanyID]
    ON [dbo].[Employee]([CompanyID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Employee_DepartmentID]
    ON [dbo].[Employee]([DepartmentID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Employee_SupervisorID]
    ON [dbo].[Employee]([SupervisorID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'员工信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'员工编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'EmployeeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'员工姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'EmployeeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'职务', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'邮箱地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'手机', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Moblie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'座机', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'分机', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Ext';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'性别', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Gender';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'生日', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Birthday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'身份证号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'IDNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标签备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Tag';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'直属主管', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'SupervisorID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属部门', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'DepartmentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属公司', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Status';

