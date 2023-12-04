CREATE PROCEDURE [dbo].[spUpdateRecord]
    @tableName VARCHAR(50),
    @columnsToUpdate VARCHAR(MAX),
    @id VARCHAR(22)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX)

    SET @sql = N'UPDATE ' + QUOTENAME(@tableName) + ' SET ' +  @columnsToUpdate + ' WHERE Id=@id'

    EXEC sp_executesql @sql,N'@id VARCHAR(22)',@id
END
