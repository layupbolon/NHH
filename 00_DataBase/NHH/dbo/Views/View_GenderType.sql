
CREATE VIEW [dbo].[View_GenderType]
AS
    SELECT  FieldValue AS GenderTypeID ,
            FieldName AS GenderTypeName
    FROM    dbo.Dictionary WITH ( NOLOCK )
    WHERE   ( FieldType = 'GenderType' );



