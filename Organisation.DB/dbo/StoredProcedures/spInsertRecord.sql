CREATE PROCEDURE [dbo].[spInsertRecord]
    @tableName VARCHAR(50),
    @columnNames VARCHAR(MAX),
    @columnValues VARCHAR(MAX)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX)

    SET @sql = N'INSERT INTO ' + QUOTENAME(@tableName) + ' (' + @columnNames + ') OUTPUT INSERTED.Id VALUES (' + @columnValues + ')'

    EXEC sp_executesql @sql
END