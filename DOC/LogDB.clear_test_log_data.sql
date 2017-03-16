USE LogDB;
GO

--清除日志测试数据
--debug log
IF OBJECT_ID('dbo.t_logs_debug_log','U') IS NOT NULL
	TRUNCATE TABLE dbo.t_logs_debug_log;
	
--debug log tip
IF OBJECT_ID('dbo.t_logs_debug_log_tip','U') IS NOT NULL
	TRUNCATE TABLE dbo.t_logs_debug_log_tip;
	
--error log
IF OBJECT_ID('dbo.t_logs_error_log','U') IS NOT NULL
	TRUNCATE TABLE dbo.t_logs_error_log;
	
--error log tip
IF OBJECT_ID('dbo.t_logs_error_log_tip','U') IS NOT NULL
	TRUNCATE TABLE dbo.t_logs_error_log_tip;
	
--xml log
IF OBJECT_ID('dbo.t_logs_xml_log','U') IS NOT NULL
	TRUNCATE TABLE dbo.t_logs_xml_log;
	
--xml log tip
IF OBJECT_ID('dbo.t_logs_xml_log_tip','U') IS NOT NULL
	TRUNCATE TABLE dbo.t_logs_xml_log_tip;
	
--perf log
IF OBJECT_ID('dbo.t_logs_performance_log','U') IS NOT NULL
	TRUNCATE TABLE dbo.t_logs_performance_log;
	
--perf log tip
IF OBJECT_ID('dbo.t_logs_performance_log_tip','U') IS NOT NULL
	TRUNCATE TABLE dbo.t_logs_performance_log_tip;