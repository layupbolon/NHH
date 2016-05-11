CREATE TABLE [dbo].[FloorMap_Unit] (
    [FloorMapUnitID] INT            IDENTITY (1, 1) NOT NULL,
    [FloorID]        INT            NULL,
    [UnitID]         INT            NULL,
    [TextPosition]   VARCHAR (50)   NULL,
    [PathLine]       VARCHAR (500)  NULL,
    [PathQuad1]      VARCHAR (100)  NULL,
    [PathQuad2]      VARCHAR (100)  NULL,
    [PathQuad3]      VARCHAR (100)  NULL,
    [PathQuad4]      VARCHAR (100)  NULL,
    [Type]           INT            NULL,
    [EditDate]       DATETIME       NULL,
    [EditUser]       INT            NULL,
    [Status]         INT            DEFAULT ((1)) NOT NULL,
    [Comments]       NVARCHAR (500) NULL,
    CONSTRAINT [PK_FloorMap_Unit] PRIMARY KEY CLUSTERED ([FloorMapUnitID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_FloorMap_Unit_FloorID]
    ON [dbo].[FloorMap_Unit]([FloorID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FloorMap_Unit_UnitID]
    ON [dbo].[FloorMap_Unit]([UnitID] ASC);

