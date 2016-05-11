CREATE TABLE [dbo].[Merchant_UserPaper] (
    [PaperID]     INT            IDENTITY (1, 1) NOT NULL,
    [UserID]      INT            NOT NULL,
    [PaperType]   INT            NOT NULL,
    [PaperNumber] NVARCHAR (100) NOT NULL,
    [PaperPath]   NVARCHAR (500) NOT NULL,
    [Status]      INT            DEFAULT ((1)) NOT NULL,
    [InDate]      DATETIME       DEFAULT (getdate()) NOT NULL,
    [InUser]      INT            DEFAULT ((0)) NOT NULL,
    [EditDate]    DATETIME       NULL,
    [EditUser]    INT            NULL,
    CONSTRAINT [PK_Merchant_UserPaper] PRIMARY KEY CLUSTERED ([PaperID] ASC),
    CONSTRAINT [FK_Merchant_UserPaper_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Merchant_User] ([UserID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_UserPaper_PaperType]
    ON [dbo].[Merchant_UserPaper]([PaperType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_UserPaper_Status]
    ON [dbo].[Merchant_UserPaper]([Status] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Merchant_UserPaper_UserID]
    ON [dbo].[Merchant_UserPaper]([UserID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'证件类型 身份证、健康证、毕业证、户口本、暂住证、职业资格证等', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_UserPaper', @level2type = N'COLUMN', @level2name = N'PaperType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'证件编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_UserPaper', @level2type = N'COLUMN', @level2name = N'PaperNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'证件存储路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_UserPaper', @level2type = N'COLUMN', @level2name = N'PaperPath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Merchant_UserPaper', @level2type = N'COLUMN', @level2name = N'Status';

