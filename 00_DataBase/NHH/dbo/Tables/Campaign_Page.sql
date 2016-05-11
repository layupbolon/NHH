CREATE TABLE [dbo].[Campaign_Page] (
    [PageID]        INT            IDENTITY (1, 1) NOT NULL,
    [CampaignID]    INT            NOT NULL,
    [PageType]      INT            DEFAULT ((1)) NOT NULL,
    [PageTitle]     NVARCHAR (500) NOT NULL,
    [PageCover]     NVARCHAR (500) NULL,
    [PageContent]   NVARCHAR (MAX) NOT NULL,
    [PublishStatus] INT            DEFAULT ((1)) NOT NULL,
    [PublishTime]   DATETIME       NULL,
    [PublishUser]   INT            NULL,
    [Remark]        NVARCHAR (500) NULL,
    [Status]        INT            DEFAULT ((1)) NOT NULL,
    [InDate]        DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]        INT            DEFAULT ((0)) NOT NULL,
    [EditDate]      DATETIME       NULL,
    [EditUser]      INT            NULL,
    CONSTRAINT [PK_Campaign_Page] PRIMARY KEY CLUSTERED ([PageID] ASC),
    CONSTRAINT [FK_Campaign_Page_Campaign] FOREIGN KEY ([CampaignID]) REFERENCES [dbo].[Campaign] ([CampaignID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Campaign_Page_CampaignID]
    ON [dbo].[Campaign_Page]([CampaignID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Campaign_Page_PageType]
    ON [dbo].[Campaign_Page]([PageType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Campaign_Page_PublishStatus]
    ON [dbo].[Campaign_Page]([PublishStatus] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'企划活动页面', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Page';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'页面ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Page', @level2type = N'COLUMN', @level2name = N'PageID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Page', @level2type = N'COLUMN', @level2name = N'CampaignID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'页面类型 1-H5;2-WEB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Page', @level2type = N'COLUMN', @level2name = N'PageType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'页面标题', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Page', @level2type = N'COLUMN', @level2name = N'PageTitle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'封面图片', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Page', @level2type = N'COLUMN', @level2name = N'PageCover';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'页面内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Page', @level2type = N'COLUMN', @level2name = N'PageContent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发布状态 1-未发布，2-已发布', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Page', @level2type = N'COLUMN', @level2name = N'PublishStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发布时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Page', @level2type = N'COLUMN', @level2name = N'PublishTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发布人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Page', @level2type = N'COLUMN', @level2name = N'PublishUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Page', @level2type = N'COLUMN', @level2name = N'Remark';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Page', @level2type = N'COLUMN', @level2name = N'Status';

