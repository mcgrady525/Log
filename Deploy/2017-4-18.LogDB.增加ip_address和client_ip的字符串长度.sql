USE LogDB;
GO

--增加ip_address和client_ip的字符串长度
--debug log
IF COL_LENGTH('t_logs_debug_log','ip_address') IS NOT NULL
	ALTER TABLE dbo.t_logs_debug_log ALTER COLUMN ip_address NVARCHAR(512);
go

IF COL_LENGTH('t_logs_debug_log', 'client_ip') IS NOT NULL
	ALTER TABLE dbo.t_logs_debug_log ALTER COLUMN client_ip NVARCHAR(512);
go

--error log
IF COL_LENGTH('t_logs_error_log','ip_address') IS NOT NULL
	ALTER TABLE dbo.t_logs_error_log ALTER COLUMN ip_address NVARCHAR(512);
go

IF COL_LENGTH('t_logs_error_log', 'client_ip') IS NOT NULL
	ALTER TABLE dbo.t_logs_error_log ALTER COLUMN client_ip NVARCHAR(512);
go

--xml log
IF COL_LENGTH('t_logs_xml_log','ip_address') IS NOT NULL
	ALTER TABLE dbo.t_logs_xml_log ALTER COLUMN ip_address NVARCHAR(512);
go

IF COL_LENGTH('t_logs_xml_log', 'client_ip') IS NOT NULL
	ALTER TABLE dbo.t_logs_xml_log ALTER COLUMN client_ip NVARCHAR(512);
go

--perf log
IF COL_LENGTH('t_logs_performance_log','ip_address') IS NOT NULL
	ALTER TABLE dbo.t_logs_performance_log ALTER COLUMN ip_address NVARCHAR(512);
go

IF COL_LENGTH('t_logs_performance_log', 'client_ip') IS NOT NULL
	ALTER TABLE dbo.t_logs_performance_log ALTER COLUMN client_ip NVARCHAR(512);
go
