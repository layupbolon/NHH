CREATE TABLE [dbo].[Province] (
    [ProvinceID]   INT          NOT NULL,
    [ProvinceName] VARCHAR (50) NOT NULL,
    [ProvinceType] INT          NULL,
    [RegionID]     INT          NULL,
    [Status]       INT          DEFAULT ((1)) NOT NULL,
    [InDate]       DATETIME     DEFAULT (getdate()) NOT NULL,
    [InUser]       INT          DEFAULT ((0)) NOT NULL,
    [EditDate]     DATETIME     NULL,
    [EditUser]     INT          NULL,
    CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED ([ProvinceID] ASC),
    CONSTRAINT [FK_Province_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([RegionID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Province_RegionID]
    ON [dbo].[Province]([RegionID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'省份', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Province';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'省份名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Province', @level2type = N'COLUMN', @level2name = N'ProvinceName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'省份类型：直辖市、省、自治区、特别行政区', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Province', @level2type = N'COLUMN', @level2name = N'ProvinceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属区域', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Province', @level2type = N'COLUMN', @level2name = N'RegionID';

