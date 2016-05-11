CREATE TABLE [dbo].[Merchant_StoreImage] (
    [ImageID]    INT            IDENTITY (1, 1) NOT NULL,
    [StoreID]    INT            NOT NULL,
    [MerchantID] INT            NOT NULL,
    [SeqNo]      INT            NULL,
    [ImageName]  NVARCHAR (50)  NULL,
    [ImagePath]  NVARCHAR (200) NOT NULL,
    [Status]     INT            DEFAULT ((1)) NOT NULL,
    [InDate]     DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]     INT            DEFAULT ((0)) NOT NULL,
    [EditDate]   DATETIME       NULL,
    [EditUser]   INT            NULL,
    CONSTRAINT [PK_Merchant_StoreImage] PRIMARY KEY CLUSTERED ([ImageID] ASC),
    CONSTRAINT [FK_Merchant_StoreImage_Merchant] FOREIGN KEY ([MerchantID]) REFERENCES [dbo].[Merchant] ([MerchantID]),
    CONSTRAINT [FK_Merchant_StoreImage_Store] FOREIGN KEY ([StoreID]) REFERENCES [dbo].[Merchant_Store] ([StoreID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_StoreImage_MerchantID]
    ON [dbo].[Merchant_StoreImage]([MerchantID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_StoreImage_SeqNo]
    ON [dbo].[Merchant_StoreImage]([SeqNo] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_StoreImage_Status]
    ON [dbo].[Merchant_StoreImage]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_StoreImage_StoreID]
    ON [dbo].[Merchant_StoreImage]([StoreID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图片ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_StoreImage', @level2type = N'COLUMN', @level2name = N'ImageID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'店铺ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_StoreImage', @level2type = N'COLUMN', @level2name = N'StoreID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户ID ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_StoreImage', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图片序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_StoreImage', @level2type = N'COLUMN', @level2name = N'SeqNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图片名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_StoreImage', @level2type = N'COLUMN', @level2name = N'ImageName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图片路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_StoreImage', @level2type = N'COLUMN', @level2name = N'ImagePath';

