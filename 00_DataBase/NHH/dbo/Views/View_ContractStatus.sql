
CREATE VIEW [dbo].[View_ContractStatus]
AS
    SELECT  FieldValue AS ContractStatusID ,
            FieldName AS ContractStatusName
    FROM    dbo.Dictionary WITH ( NOLOCK )
    WHERE   ( FieldType = 'ContractStatus' );


