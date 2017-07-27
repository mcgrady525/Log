USE LogDB;
GO


--xml智能提示表和性能日志智能提示表添加字段method_cname
IF COL_LENGTH('dbo.t_logs_xml_log_tip', 'method_cname') IS NULL
	ALTER TABLE dbo.t_logs_xml_log_tip ADD method_cname NVARCHAR(64) NULL;
GO

IF COL_LENGTH('dbo.t_logs_performance_log_tip', 'method_cname') IS NULL
	ALTER TABLE dbo.t_logs_performance_log_tip ADD method_cname NVARCHAR(64) NULL;
GO