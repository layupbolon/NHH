CREATE TABLE [dbo].[District] (
    [DistrictID]   INT           NOT NULL,
    [DistrictName] NVARCHAR (50) NOT NULL,
    [CityID]       INT           NOT NULL,
    [Status]       INT           DEFAULT ((1)) NOT NULL,
    [InDate]       DATETIME      DEFAULT (getdate()) NOT NULL,
    [InUser]       INT           DEFAULT ((0)) NOT NULL,
    [EditDate]     DATETIME      NULL,
    [EditUser]     INT           NULL,
    CONSTRAINT [PK_District] PRIMARY KEY CLUSTERED ([DistrictID] ASC),
    CONSTRAINT [FK_District_City] FOREIGN KEY ([CityID]) REFERENCES [dbo].[City] ([CityID])
);


GO
CREATE NONCLUSTERED INDEX [IX_District_CityID]
    ON [dbo].[District]([CityID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区县名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'District', @level2type = N'COLUMN', @level2name = N'DistrictName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属城市', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'District', @level2type = N'COLUMN', @level2name = N'CityID';

