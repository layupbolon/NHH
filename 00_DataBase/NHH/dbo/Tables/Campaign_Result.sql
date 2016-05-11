CREATE TABLE [dbo].[Campaign_Result] (
    [ResultID]   INT             IDENTITY (1, 1) NOT NULL,
    [CampaignID] INT             NOT NULL,
    [Outlay]     DECIMAL (18, 2) NULL,
    [Sales]      DECIMAL (18, 2) NULL,
    [Visitors]   DECIMAL (18, 2) NULL,
    [Summary]    NVARCHAR (MAX)  NOT NULL,
    [Status]     INT             DEFAULT ((1)) NOT NULL,
    [InDate]     DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]     INT             DEFAULT ((0)) NOT NULL,
    [EditDate]   DATETIME        NULL,
    [EditUser]   INT             NULL,
    CONSTRAINT [PK_Campaign_Result] PRIMARY KEY CLUSTERED ([ResultID] ASC),
    CONSTRAINT [FK_Campaign_Result_Campaign] FOREIGN KEY ([CampaignID]) REFERENCES [dbo].[Campaign] ([CampaignID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Campaign_Result_CampaignID]
    ON [dbo].[Campaign_Result]([CampaignID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Campaign_Result_Status]
    ON [dbo].[Campaign_Result]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动效果', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Result', @level2type = N'COLUMN', @level2name = N'CampaignID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'费用开支', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Result', @level2type = N'COLUMN', @level2name = N'Outlay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'销售金额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Result', @level2type = N'COLUMN', @level2name = N'Sales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'人流统计', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Result', @level2type = N'COLUMN', @level2name = N'Visitors';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动小结', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Result', @level2type = N'COLUMN', @level2name = N'Summary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Result', @level2type = N'COLUMN', @level2name = N'Status';

