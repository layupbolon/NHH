CREATE TABLE [dbo].[Campaign] (
    [CampaignID]     INT             IDENTITY (1, 1) NOT NULL,
    [CampaignCode]   NVARCHAR (50)   NULL,
    [CampaignName]   NVARCHAR (500)  NOT NULL,
    [CampaignType]   INT             NOT NULL,
    [CampaignBrief]  NVARCHAR (MAX)  NULL,
    [CampaignStatus] INT             DEFAULT ((1)) NOT NULL,
    [ProjectID]      INT             NOT NULL,
    [Location]       NVARCHAR (500)  NULL,
    [StartDate]      DATETIME        NOT NULL,
    [EndDate]        DATETIME        NOT NULL,
    [Budget]         DECIMAL (18, 2) NULL,
    [PlanUserID]     INT             NULL,
    [PlanUserName]   NVARCHAR (50)   NULL,
    [PlanTime]       DATETIME        NULL,
    [RunUserID]      INT             NULL,
    [RunUserName]    NVARCHAR (50)   NULL,
    [RunTime]        DATETIME        NULL,
    [Status]         INT             DEFAULT ((1)) NOT NULL,
    [InDate]         DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]         INT             DEFAULT ((0)) NOT NULL,
    [EditDate]       DATETIME        NULL,
    [EditUser]       INT             NULL,
    CONSTRAINT [PK_Campaign] PRIMARY KEY CLUSTERED ([CampaignID] ASC),
    CONSTRAINT [FK_Campaign_PlanUser] FOREIGN KEY ([PlanUserID]) REFERENCES [dbo].[Sys_User] ([UserID]),
    CONSTRAINT [FK_Campaign_Project] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([ProjectID]),
    CONSTRAINT [FK_Campaign_RunUser] FOREIGN KEY ([RunUserID]) REFERENCES [dbo].[Sys_User] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Campaign_CampaignStatus]
    ON [dbo].[Campaign]([CampaignStatus] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Campaign_CampaignType]
    ON [dbo].[Campaign]([CampaignType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Campaign_ProjectID]
    ON [dbo].[Campaign]([ProjectID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Campaign_Status]
    ON [dbo].[Campaign]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'企划活动', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'CampaignID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'CampaignCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'CampaignName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动类型 包含商品活动(P)（例如：抽奖、买赠、发券等）；旺场活动(RP)（演绎活动、互动活动）；线上活动；其他；一共四种。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'CampaignType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动简介', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'CampaignBrief';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动状态 策划中、进行中、待小结、已完成。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'CampaignStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属项目', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'ProjectID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动地点', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'Location';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'开始时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'StartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结束时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动预算', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'Budget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'策划负责人ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'PlanUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'策划负责人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'PlanUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'策划时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'PlanTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'执行负责人ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'RunUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'执行负责人姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'RunUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'执行时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'RunTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign', @level2type = N'COLUMN', @level2name = N'Status';

