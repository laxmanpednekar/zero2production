CREATE PROCEDURE [dbo].[spDoesRecordExist]
	@tableName VARCHAR(50),
    @distinguishingUniqueKeyColumnName VARCHAR(100),
    @distinguishingUniquekeyColumnValue VARCHAR(100)
AS
BEGIN
	DECLARE @sql NVARCHAR(MAX)

     SET @sql = N'SELECT COUNT(id) FROM ' + QUOTENAME(@tableName) + ' WHERE ' + @distinguishingUniqueKeyColumnName + '= @distinguishingUniquekeyColumnValue';
     EXEC sp_executesql @sql,N'@distinguishingUniquekeyColumnValue VARCHAR(100)',@distinguishingUniquekeyColumnValue   
END
