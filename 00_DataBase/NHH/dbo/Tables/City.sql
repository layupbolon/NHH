CREATE TABLE [dbo].[City] (
    [CityID]     INT          NOT NULL,
    [CityName]   VARCHAR (50) NOT NULL,
    [ZipCode]    VARCHAR (50) NULL,
    [ProvinceID] INT          NOT NULL,
    [Status]     INT          DEFAULT ((1)) NOT NULL,
    [InDate]     DATETIME     DEFAULT (getdate()) NOT NULL,
    [InUser]     INT          DEFAULT ((0)) NOT NULL,
    [EditDate]   DATETIME     NULL,
    [EditUser]   INT          NULL,
    CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED ([CityID] ASC),
    CONSTRAINT [FK_City_Province] FOREIGN KEY ([ProvinceID]) REFERENCES [dbo].[Province] ([ProvinceID])
);


GO
CREATE NONCLUSTERED INDEX [IX_City_ProvinceID]
    ON [dbo].[City]([ProvinceID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'城市', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'City';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'城市名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'City', @level2type = N'COLUMN', @level2name = N'CityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'邮编', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'City', @level2type = N'COLUMN', @level2name = N'ZipCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属省份', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'City', @level2type = N'COLUMN', @level2name = N'ProvinceID';

