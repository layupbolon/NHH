CREATE TABLE [dbo].[Material] (
    [MaterialID]    INT             IDENTITY (1, 1) NOT NULL,
    [MaterialType]  INT             NOT NULL,
    [MaterialName]  NVARCHAR (200)  NOT NULL,
    [MaterialSpec]  NVARCHAR (50)   NULL,
    [MaterialPrice] DECIMAL (18, 2) DEFAULT ((0.00)) NOT NULL,
    [MaterialUnit]  NVARCHAR (10)   DEFAULT ('件') NOT NULL,
    [Status]        INT             DEFAULT ((1)) NOT NULL,
    [InDate]        DATETIME        DEFAULT (getdate()) NOT NULL,
    [InUser]        INT             DEFAULT ((0)) NOT NULL,
    [EditDate]      DATETIME        NULL,
    [EditUser]      INT             NULL,
    CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED ([MaterialID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Material_MaterialType]
    ON [dbo].[Material]([MaterialType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Material_Status]
    ON [dbo].[Material]([Status] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物料清单', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Material';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物料类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Material', @level2type = N'COLUMN', @level2name = N'MaterialType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物料名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Material', @level2type = N'COLUMN', @level2name = N'MaterialName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物料规格', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Material', @level2type = N'COLUMN', @level2name = N'MaterialSpec';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物料价格', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Material', @level2type = N'COLUMN', @level2name = N'MaterialPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Material', @level2type = N'COLUMN', @level2name = N'MaterialUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Material', @level2type = N'COLUMN', @level2name = N'Status';

