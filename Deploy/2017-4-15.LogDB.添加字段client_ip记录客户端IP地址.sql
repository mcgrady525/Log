USE LogDB;
GO

--添加字段client_ip记录客户端IP地址
--client_ip
--debug log
IF COL_LENGTH('t_logs_debug_log', 'client_ip') IS NULL
	ALTER TABLE dbo.t_logs_debug_log ADD client_ip NVARCHAR(32) NULL;
go
	
--error log
IF COL_LENGTH('t_logs_error_log', 'client_ip') IS NULL
	ALTER TABLE dbo.t_logs_error_log ADD client_ip NVARCHAR(32) NULL;
go
	
--xml log
IF COL_LENGTH('t_logs_xml_log', 'client_ip') IS NULL
	ALTER TABLE dbo.t_logs_xml_log ADD client_ip NVARCHAR(32) NULL;
go

--performance log
IF COL_LENGTH('t_logs_performance_log', 'client_ip') IS NULL
	ALTER TABLE dbo.t_logs_performance_log ADD client_ip NVARCHAR(32) NULL;
go
