CREATE TABLE [dbo].[Project_Floor] (
    [FloorID]          INT             IDENTITY (1, 1) NOT NULL,
    [BuildingID]       INT             NOT NULL,
    [ProjectID]        INT             NOT NULL,
    [FloorMapFileName] VARCHAR (500)   NULL,
    [FloorNumber]      INT             NOT NULL,
    [FloorName]        VARCHAR (10)    NULL,
    [FloorDescription] NVARCHAR (500)  NULL,
    [TotalRentArea]    DECIMAL (18, 2) NULL,
    [TotalRentUnit]    INT             NULL,
    [ContractRentArea] DECIMAL (18, 2) NULL,
    [ContractRentUnit] INT             NULL,
    [Status]           INT             DEFAULT ((1)) NOT NULL,
    [InDate]           DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]           INT             DEFAULT ((0)) NOT NULL,
    [EditDate]         DATETIME        NULL,
    [EditUser]         INT             NULL,
    CONSTRAINT [PK_Project_Floor] PRIMARY KEY CLUSTERED ([FloorID] ASC),
    CONSTRAINT [FK_Project_Floor_Building] FOREIGN KEY ([BuildingID]) REFERENCES [dbo].[Project_Building] ([BuildingID]),
    CONSTRAINT [FK_Project_Floor_Project] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([ProjectID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Floor_BuildingID]
    ON [dbo].[Project_Floor]([BuildingID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Floor_ProjectID]
    ON [dbo].[Project_Floor]([ProjectID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_Floor_Status]
    ON [dbo].[Project_Floor]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目楼层信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Floor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼宇ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Floor', @level2type = N'COLUMN', @level2name = N'BuildingID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Floor', @level2type = N'COLUMN', @level2name = N'ProjectID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'平面图路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Floor', @level2type = N'COLUMN', @level2name = N'FloorMapFileName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼层，B1、1F、1.5F、2F', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Floor', @level2type = N'COLUMN', @level2name = N'FloorNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'楼层说明，大堂、夹层、屋顶', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Floor', @level2type = N'COLUMN', @level2name = N'FloorDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总计租面积', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Floor', @level2type = N'COLUMN', @level2name = N'TotalRentArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总计租单元数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Floor', @level2type = N'COLUMN', @level2name = N'TotalRentUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'签约面积', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Floor', @level2type = N'COLUMN', @level2name = N'ContractRentArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'签约单元数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Floor', @level2type = N'COLUMN', @level2name = N'ContractRentUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_Floor', @level2type = N'COLUMN', @level2name = N'Status';

