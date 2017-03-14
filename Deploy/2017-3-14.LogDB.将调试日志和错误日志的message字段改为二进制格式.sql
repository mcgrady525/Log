USE LogDB;
GO

--将调试日志和错误日志的message字段改为二进制类型
IF COL_LENGTH('t_logs_debug_log', 'message') IS NOT NULL
 BEGIN
	ALTER TABLE dbo.t_logs_debug_log DROP COLUMN message;
	ALTER TABLE dbo.t_logs_debug_log ADD [message] VARBINARY(max) NULL;
 END
 
 IF COL_LENGTH('t_logs_error_log', 'message') IS NOT NULL
	BEGIN
	ALTER TABLE dbo.t_logs_error_log DROP COLUMN message;
	ALTER TABLE dbo.t_logs_error_log ADD [message] VARBINARY(max) NULL;	
	END