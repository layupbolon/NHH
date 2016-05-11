CREATE TABLE [dbo].[Campaign_Attachment] (
    [AttachmentID]   INT            IDENTITY (1, 1) NOT NULL,
    [CampaignID]     INT            NOT NULL,
    [AttachmentType] INT            NOT NULL,
    [AttachmentName] NVARCHAR (100) NULL,
    [AttachmentPath] NVARCHAR (500) NULL,
    [Status]         INT            DEFAULT ((1)) NOT NULL,
    [InDate]         DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]         INT            DEFAULT ((0)) NOT NULL,
    [EditDate]       DATETIME       NULL,
    [EditUser]       INT            NULL,
    CONSTRAINT [PK_Campaign_Attachment] PRIMARY KEY CLUSTERED ([AttachmentID] ASC),
    CONSTRAINT [FK_Campaign_Attachment_Campaign] FOREIGN KEY ([CampaignID]) REFERENCES [dbo].[Campaign] ([CampaignID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Campaign_Attachment_AttachmentType]
    ON [dbo].[Campaign_Attachment]([AttachmentType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Campaign_Attachment_CampaignID]
    ON [dbo].[Campaign_Attachment]([CampaignID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Campaign_Attachment_Status]
    ON [dbo].[Campaign_Attachment]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动附件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Attachment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'活动ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Attachment', @level2type = N'COLUMN', @level2name = N'CampaignID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件类型 1-策划（策划方案、调研报告、素材等）；2-执行（执行计划、费用计划等）；3-反馈（统计报表、费用报表、活动图片、活动小结等）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Attachment', @level2type = N'COLUMN', @level2name = N'AttachmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Attachment', @level2type = N'COLUMN', @level2name = N'AttachmentName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件存储路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Attachment', @level2type = N'COLUMN', @level2name = N'AttachmentPath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Campaign_Attachment', @level2type = N'COLUMN', @level2name = N'Status';

