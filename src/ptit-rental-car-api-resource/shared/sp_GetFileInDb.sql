CREATE PROCEDURE GetFilesInDb
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @DataQuery NVARCHAR(MAX) = N'';

    SELECT @DataQuery += 'SELECT [' + COLUMN_NAME + '] AS FileName FROM [' + TABLE_NAME + '] UNION ALL '
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE COLUMN_NAME LIKE '%FileName' OR COLUMN_NAME LIKE '%Thumbnail';

    -- Remove the trailing 'UNION ALL' from the last query
    SET @DataQuery = LEFT(@DataQuery, LEN(@DataQuery) - 10);
    EXEC sp_executesql @DataQuery;
END
go

