CREATE TABLE [dbo].[Merchant_Brand] (
    [MerchantBrandID] INT           IDENTITY (1, 1) NOT NULL,
    [MerchantID]      INT           NOT NULL,
    [BrandID]         INT           NOT NULL,
    [BrandName]       NVARCHAR (50) NULL,
    [BrandType]       INT           NOT NULL,
    [Authorized]      INT           DEFAULT ((0)) NOT NULL,
    [AuthorizedFile]  INT           NULL,
    [AuthorizedSince] DATETIME      NULL,
    [AuthorizedTo]    DATETIME      NULL,
    [OperationSince]  DATETIME      NULL,
    [OperationTo]     DATETIME      NULL,
    [Revenue]         INT           NULL,
    [Status]          INT           DEFAULT ((1)) NOT NULL,
    [InDate]          DATETIME      DEFAULT (getdate()) NOT NULL,
    [InUser]          INT           DEFAULT ((0)) NOT NULL,
    [EditDate]        DATETIME      NULL,
    [EditUser]        INT           NULL,
    CONSTRAINT [PK_Merchant_Brand] PRIMARY KEY CLUSTERED ([MerchantBrandID] ASC),
    CONSTRAINT [FK_Merchant_Brand_AuthorizedFile] FOREIGN KEY ([AuthorizedFile]) REFERENCES [dbo].[Merchant_Attachment] ([AttachmentID]),
    CONSTRAINT [FK_Merchant_Brand_Brand] FOREIGN KEY ([BrandID]) REFERENCES [dbo].[Brand] ([BrandID]),
    CONSTRAINT [FK_Merchant_Brand_Merchant] FOREIGN KEY ([MerchantID]) REFERENCES [dbo].[Merchant] ([MerchantID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Brand_BrandID]
    ON [dbo].[Merchant_Brand]([BrandID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Brand_BrandType]
    ON [dbo].[Merchant_Brand]([BrandType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Brand_MerchantID]
    ON [dbo].[Merchant_Brand]([MerchantID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_Brand_Status]
    ON [dbo].[Merchant_Brand]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户品牌信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Brand';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'商户ID ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Brand', @level2type = N'COLUMN', @level2name = N'MerchantID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品牌ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Brand', @level2type = N'COLUMN', @level2name = N'BrandID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品牌名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Brand', @level2type = N'COLUMN', @level2name = N'BrandName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经营形式，直营/代理/加盟/自创', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Brand', @level2type = N'COLUMN', @level2name = N'BrandType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否已取得品牌授权', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Brand', @level2type = N'COLUMN', @level2name = N'Authorized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品牌授权文件ID，取自附件表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Brand', @level2type = N'COLUMN', @level2name = N'AuthorizedFile';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品牌授权期限', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Brand', @level2type = N'COLUMN', @level2name = N'AuthorizedSince';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品牌经营年限', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Brand', @level2type = N'COLUMN', @level2name = N'OperationSince';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'品牌年平均营业额', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Brand', @level2type = N'COLUMN', @level2name = N'Revenue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_Brand', @level2type = N'COLUMN', @level2name = N'Status';

