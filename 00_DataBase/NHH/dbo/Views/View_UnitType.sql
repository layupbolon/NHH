CREATE VIEW [dbo].[View_UnitType]
AS
    SELECT  FieldValue AS UnitTypeID ,
            FieldName AS UnitTypeName
    FROM    dbo.Dictionary WITH ( NOLOCK )
    WHERE   ( FieldType = 'ProjectUnitType' );

