
CREATE VIEW [dbo].[View_CompanyType]
AS
    SELECT  FieldValue AS CompanyTypeID ,
            FieldName AS CompanyTypeName
    FROM    dbo.Dictionary WITH ( NOLOCK )
    WHERE   ( FieldType = 'CompanyType' );


