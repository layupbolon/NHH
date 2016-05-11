CREATE TABLE [dbo].[Merchant_Message] (
    [MessageID]   INT            IDENTITY (1, 1) NOT NULL,
    [MerchantID]  INT            NOT NULL,
    [StoreID]     INT            NULL,
    [UserID]      INT            NOT NULL,
    [Subject]     NVARCHAR (200) NOT NULL,
    [Content]     NVARCHAR (MAX) NOT NULL,
    [SourceType]  INT            NOT NULL,
    [SourceRefID] INT            NULL,
    [Status]      INT            DEFAULT ((1)) NOT NULL,
    [InDate]      DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]      INT            DEFAULT ((0)) NOT NULL,
    [EditDate]    DATETIME       NULL,
    [EditUser]    INT            NULL,
    CONSTRAINT [PK_Merchant_Message] PRIMARY KEY CLUSTERED ([MessageID] ASC),
    CONSTRAINT [FK_Merchant_Message_Merchant] FOREIGN KEY ([MerchantID]) REFERENCES [dbo].[Merchant] ([MerchantID]),
    CONSTRAINT [FK_Merchant_Message_Store] FOREIGN KEY ([StoreID]) REFERENCES [dbo].[Merchant_Store] ([StoreID]),
    CONSTRAINT [FK_Merchant_Message_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Merchant_User] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Message_MerchantID]
    ON [dbo].[Merchant_Message]([MerchantID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Message_SourceType]
    ON [dbo].[Merchant_Message]([SourceType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Message_Status]
    ON [dbo].[Merchant_Message]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Message_StoreID]
    ON [dbo].[Merchant_Message]([StoreID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Message_UserID]
    ON [dbo].[Merchant_Message]([UserID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Message', @level2type = N'COLUMN', @level2name = N'MessageID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Message', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商铺', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Message', @level2type = N'COLUMN', @level2name = N'StoreID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户用户', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Message', @level2type = N'COLUMN', @level2name = N'UserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息标题', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Message', @level2type = N'COLUMN', @level2name = N'Subject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Message', @level2type = N'COLUMN', @level2name = N'Content';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'来源类型 1-通知；2-租约；3-账单；4-催款；5-活动；6-报修；7-投诉；', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Message', @level2type = N'COLUMN', @level2name = N'SourceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'来源单据ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Message', @level2type = N'COLUMN', @level2name = N'SourceRefID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态：未读、已读、删除', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Message', @level2type = N'COLUMN', @level2name = N'Status';

