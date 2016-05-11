CREATE TABLE [dbo].[Region] (
    [RegionID]   INT          NOT NULL,
    [RegionName] VARCHAR (50) NOT NULL,
    [Status]     INT          DEFAULT ((1)) NOT NULL,
    [InDate]     DATETIME     DEFAULT (getdate()) NOT NULL,
    [InUser]     INT          DEFAULT ((0)) NOT NULL,
    [EditDate]   DATETIME     NULL,
    [EditUser]   INT          NULL,
    CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED ([RegionID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Region';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域名称，东北、华北、华东、华南、西南、西北、台港澳等', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Region', @level2type = N'COLUMN', @level2name = N'RegionName';

