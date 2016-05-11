CREATE TABLE [dbo].[Project_Owner] (
    [OwnerID]     INT           IDENTITY (1, 1) NOT NULL,
    [ProjectID]   INT           NOT NULL,
    [CompanyID]   INT           NOT NULL,
    [CompanyName] NVARCHAR (50) NULL,
    [Status]      INT           DEFAULT ((1)) NOT NULL,
    [InDate]      DATETIME      DEFAULT (getdate()) NOT NULL,
    [InUser]      INT           DEFAULT ((0)) NOT NULL,
    [EditDate]    DATETIME      NULL,
    [EditUser]    INT           NULL,
    CONSTRAINT [PK_Project_Owner] PRIMARY KEY CLUSTERED ([OwnerID] ASC),
    CONSTRAINT [FK_Project_Owner_Company] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([CompanyID]),
    CONSTRAINT [FK_Project_Owner_Project] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([ProjectID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Owner_CompanyID]
    ON [dbo].[Project_Owner]([CompanyID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Owner_ProjectID]
    ON [dbo].[Project_Owner]([ProjectID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Owner', @level2type = N'COLUMN', @level2name = N'ProjectID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业主公司', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Owner', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业主名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Owner', @level2type = N'COLUMN', @level2name = N'CompanyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Owner', @level2type = N'COLUMN', @level2name = N'Status';

