use ApiDB
DECLARE @sql NVARCHAR(MAX) = N'';

SELECT @sql += 'DELETE FROM ' + QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) + ' WHERE IsDeleted = 1; ' + CHAR(13)
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME = 'IsDeleted';

PRINT @sql; -- Opcional: Para ver el script antes de ejecutarlo
EXEC sp_executesql @sql;
