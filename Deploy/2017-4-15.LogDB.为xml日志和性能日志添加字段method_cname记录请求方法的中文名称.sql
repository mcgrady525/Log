USE LogDB;
GO

--为xml日志和性能日志添加字段method_cname记录请求方法的中文名称
--method_cname
--xml log
IF COL_LENGTH('t_logs_xml_log', 'method_cname') IS NULL
	ALTER TABLE dbo.t_logs_xml_log ADD method_cname NVARCHAR(64) NULL;
GO

--performance log
IF COL_LENGTH('t_logs_performance_log', 'method_cname') IS NULL
	ALTER TABLE dbo.t_logs_performance_log ADD method_cname NVARCHAR(64) NULL;
GO