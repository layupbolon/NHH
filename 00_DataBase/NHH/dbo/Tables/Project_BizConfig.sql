CREATE TABLE [dbo].[Project_BizConfig] (
    [BizConfigID]   INT            IDENTITY (1, 1) NOT NULL,
    [BizConfigType] INT            NOT NULL,
    [ProjectID]     INT            NOT NULL,
	[BuildingID]     INT            NULL,
	[Param1]		VARCHAR (500) NULL,
	[Param2]		VARCHAR (500) NULL,
	[Param3]		VARCHAR (500) NULL,
	[Param4]		VARCHAR (500) NULL,
	[Param5]		VARCHAR (500) NULL,
    [Remark]        NVARCHAR (500) NULL,
    [Status]        INT            DEFAULT ((1)) NOT NULL,
    [InDate]        DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]        INT            DEFAULT ((0)) NOT NULL,
    [EditDate]      DATETIME       NULL,
    [EditUser]      INT            NULL,
    CONSTRAINT [PK_Project_BizConfig] PRIMARY KEY CLUSTERED ([BizConfigID] ASC),
    CONSTRAINT [FK_Project_BizConfig_Project] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([ProjectID]), 
    CONSTRAINT [FK_Project_BizConfig_Building] FOREIGN KEY ([BuildingID]) REFERENCES [dbo].[Project_Building]([BuildingID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Project_BizConfig_BizConfigType]
    ON [dbo].[Project_BizConfig]([BizConfigType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Project_BizConfig_ProjectID]
    ON [dbo].[Project_BizConfig]([ProjectID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'项目业务配置表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_BizConfig';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务类型：101-投诉；102-报修业务类型：101-投诉；102-报修；103-催款；110-微信公众号…；103-催款…', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_BizConfig', @level2type = N'COLUMN', @level2name = N'BizConfigType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属项目', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_BizConfig', @level2type = N'COLUMN', @level2name = N'ProjectID';


GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配置描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_BizConfig', @level2type = N'COLUMN', @level2name = N'Remark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Project_BizConfig', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'附加参数1',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Project_BizConfig',
    @level2type = N'COLUMN',
    @level2name = N'Param1'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'附加参数2',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Project_BizConfig',
    @level2type = N'COLUMN',
    @level2name = N'Param2'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'附加参数3',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Project_BizConfig',
    @level2type = N'COLUMN',
    @level2name = N'Param3'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'附加参数4',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Project_BizConfig',
    @level2type = N'COLUMN',
    @level2name = N'Param4'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'附加参数5',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Project_BizConfig',
    @level2type = N'COLUMN',
    @level2name = N'Param5'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'所属楼宇',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Project_BizConfig',
    @level2type = N'COLUMN',
    @level2name = N'BuildingID'
GO

CREATE INDEX [IX_Project_BizConfig_BuildingID] ON [dbo].[Project_BizConfig] (BuildingID)
