--This script does not include the creation of a user account.
--One must be made and have the permission to read and execute the stored procedures.

USE [master];
DROP DATABASE IF EXISTS dayone;

CREATE DATABASE dayone;
GO

USE dayone;
CREATE TABLE c
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	byteSize INT NOT NULL,
	fileDate DATE NOT NULL,
	fileName VARCHAR(100) NOT NULL
);

CREATE TABLE s
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	byteSize INT NOT NULL,
	fileName VARCHAR(100) NOT NULL
);

USE dayone;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getDownload] (
    @tablename varchar(1), -- value will be `table1` or `table2`
	@id int
)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);

    SET @sql = 'SELECT t.* FROM @tablename t WHERE id=@id';

    SET @sql = REPLACE(@sql, '@tablename', QUOTENAME(@tablename));
	SET @sql= REPLACE(@sql,'@id',@id);
    EXEC sp_executesql @sql;
END;

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getFiles] (
    @tablename varchar(1) -- value will be `table1` or `table2`
)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);

    SET @sql = 'SELECT t.* FROM @tablename t';

    SET @sql = REPLACE(@sql, '@tablename', QUOTENAME(@tablename));

    EXEC sp_executesql @sql;
END;