CREATE TABLE [dbo].[Campaign_Comment] (
    [CommentID]         INT            IDENTITY (1, 1) NOT NULL,
    [CampaignID]        INT            NOT NULL,
    [Overall]           INT            NOT NULL,
    [CommentContent]    NVARCHAR (500) NULL,
    [CommentAdditional] NVARCHAR (500) NULL,
    [CommentUserID]     INT            NOT NULL,
    [CommentUserName]   NVARCHAR (50)  NULL,
    [Status]            INT            DEFAULT ((1)) NOT NULL,
    [InDate]            DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]            INT            DEFAULT ((0)) NOT NULL,
    [EditDate]          DATETIME       NULL,
    [EditUser]          INT            NULL,
    CONSTRAINT [PK_Campaign_Comment] PRIMARY KEY CLUSTERED ([CommentID] ASC),
    CONSTRAINT [FK_Campaign_Comment_Campaign] FOREIGN KEY ([CampaignID]) REFERENCES [dbo].[Campaign] ([CampaignID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Campaign_Comment_CampaignID]
    ON [dbo].[Campaign_Comment]([CampaignID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Campaign_Comment_Overall]
    ON [dbo].[Campaign_Comment]([Overall] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动用户评论', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评价ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Comment', @level2type = N'COLUMN', @level2name = N'CommentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Comment', @level2type = N'COLUMN', @level2name = N'CampaignID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总体评价:1-5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Comment', @level2type = N'COLUMN', @level2name = N'Overall';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评价内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Comment', @level2type = N'COLUMN', @level2name = N'CommentContent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'追加评论', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Comment', @level2type = N'COLUMN', @level2name = N'CommentAdditional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评价人ID，商户用户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Comment', @level2type = N'COLUMN', @level2name = N'CommentUserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'评价人名称，商户用户名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Comment', @level2type = N'COLUMN', @level2name = N'CommentUserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Comment', @level2type = N'COLUMN', @level2name = N'Status';

