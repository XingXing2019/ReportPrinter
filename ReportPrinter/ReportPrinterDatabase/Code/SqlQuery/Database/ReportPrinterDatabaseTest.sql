IF DB_ID('ReportPrinterTest') IS NULL
BEGIN
	CREATE DATABASE ReportPrinterTest
END

ALTER DATABASE ReportPrinterTest
SET READ_COMMITTED_SNAPSHOT ON
GO

ALTER DATABASE ReportPrinterTest
SET ALLOW_SNAPSHOT_ISOLATION ON
GO