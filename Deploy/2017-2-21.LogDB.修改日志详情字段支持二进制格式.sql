USE LogDB;
GO

--2017-2-21.LogDB.修改日志详情字段支持二进制格式
--debug log
IF COL_LENGTH('dbo.t_logs_debug_log', N'detail') IS NOT NULL
	ALTER TABLE dbo.t_logs_debug_log DROP COLUMN detail;
GO

ALTER TABLE dbo.t_logs_debug_log ADD detail VARBINARY(max) NULL;

--error log
IF COL_LENGTH('dbo.t_logs_error_log', N'detail') IS NOT NULL
	ALTER TABLE dbo.t_logs_error_log DROP COLUMN detail;
GO

ALTER TABLE dbo.t_logs_error_log ADD detail VARBINARY(max) NULL;

--xml log
IF COL_LENGTH('dbo.t_logs_xml_log', N'rq') IS NOT NULL
	ALTER TABLE dbo.t_logs_xml_log DROP COLUMN rq;
GO

IF COL_LENGTH('dbo.t_logs_xml_log', N'rs') IS NOT NULL
	ALTER TABLE dbo.t_logs_xml_log DROP COLUMN rs;
GO

ALTER TABLE dbo.t_logs_xml_log ADD rq VARBINARY(max) NULL;
ALTER TABLE dbo.t_logs_xml_log ADD rs VARBINARY(max) NULL;

