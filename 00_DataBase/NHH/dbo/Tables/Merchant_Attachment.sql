CREATE TABLE [dbo].[Merchant_Attachment] (
    [AttachmentID]   INT            IDENTITY (1, 1) NOT NULL,
    [MerchantID]     INT            NOT NULL,
    [AttachmentType] INT            NOT NULL,
    [AttachmentName] NVARCHAR (100) NULL,
    [AttachmentPath] NVARCHAR (500) NULL,
    [Status]         INT            DEFAULT ((1)) NOT NULL,
    [InDate]         DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]         INT            DEFAULT ((0)) NOT NULL,
    [EditDate]       DATETIME       NULL,
    [EditUser]       INT            NULL,
    CONSTRAINT [PK_Merchant_Attachment] PRIMARY KEY CLUSTERED ([AttachmentID] ASC),
    CONSTRAINT [FK_Merchant_Attachment_Merchant] FOREIGN KEY ([MerchantID]) REFERENCES [dbo].[Merchant] ([MerchantID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Attachment_AttachmentType]
    ON [dbo].[Merchant_Attachment]([AttachmentType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Attachment_MerchantID]
    ON [dbo].[Merchant_Attachment]([MerchantID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Attachment_Status]
    ON [dbo].[Merchant_Attachment]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Attachment', @level2type = N'COLUMN', @level2name = N'AttachmentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户ID ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Attachment', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Attachment', @level2type = N'COLUMN', @level2name = N'AttachmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Attachment', @level2type = N'COLUMN', @level2name = N'AttachmentName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件存储路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Attachment', @level2type = N'COLUMN', @level2name = N'AttachmentPath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Attachment', @level2type = N'COLUMN', @level2name = N'Status';

